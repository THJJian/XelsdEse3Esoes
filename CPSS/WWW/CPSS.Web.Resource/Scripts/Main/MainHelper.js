;
(function (ns) {
    var tab_container_id = "#main_tabs",
        zoomRate = 0.996,
        left_nav_container_id = "#left_nav";

    //主页的Tab标签设置
    CSPPLib.RegNameSpace(ns).TabsHelper = (function() {
        var self;
        var tabContent = "<iframe scrolling=\"no\" frameborder=\"0\"  src=\"{0}\" style=\"width:100%; height: {1}px;\"></iframe>";

        var __init = function() {
            self = this;
        };

        var __tabClose = function() {
            /*双击关闭TAB选项卡*/
            $(".tabs-inner").dblclick(function() {
                var subtitle = $(this).children(".tabs-closable").text();
                $(tab_container_id).tabs("close", subtitle);
            });
        };

        var _addTab = function (_title, _url, _icon) {
            if (!$(tab_container_id).tabs("exists", _title)) {
                var builder = CSPPLib.Utils.StringBuilder;
                var panel_0_height = $(tab_container_id).tabs("tabs")[0].panel("options").height;
                builder.appendFrt(tabContent, [_url, zoomRate * panel_0_height]);
                var _content = builder.toString();
                $(tab_container_id).tabs("add", {
                    title: _title,
                    content: _content,
                    closable: true,
                    icon: _icon
                });
            } else {
                $(tab_container_id).tabs("select", _title);
            }
            __tabClose();
        };

        __init();

        return {
            addTab: _addTab
        };
    })();

    //初始化Accordion的Panel默认高度
    CSPPLib.RegNameSpace(ns).PanelHelper = (function() {
        var self;

        var __init = function() {
            self = this;
        };

        var _setPanelHeight = function() {
            var _accordion_options = $(left_nav_container_id).accordion("options"),
                _panels = $(left_nav_container_id).accordion("panels"),
                _panel_count = _panels.length;
            if (_panel_count === 0) return;
            var _panel_0 = _panels[0],
                _panel_0_header = _panel_0.panel("header"),
                _panel_0_header_height = _panel_0_header.height(),
                _panel_0_header_padding_top = _panel_0_header.css("padding-top"),
                _panel_0_header_padding_bottom = _panel_0_header.css("padding-bottom");
            var _panel_height = _accordion_options.height - (_panel_0_header_height + parseInt(_panel_0_header_padding_top) + parseInt(_panel_0_header_padding_bottom)) * _panel_count;
            $.each(_panels, function(__i, __panel) {
                __panel.panel("resize", { height: _panel_height + 28 });
            });
        }

        var _onExpand = function() {
            alert("_onExpand");
        }

        __init();

        return {
            setPanelHeight: _setPanelHeight,
            onExpand: _onExpand
        };
    })();

    $(document.body).ready(function () {
        var height = $(window).height();
        $("#main_layout").attr("style", "width:100%; height:" + height + "px");
        $("#main_layout")
            .layout("resize",
            {
                width: "100%",
                height: height + "px"
            });

        //初始化Accordion的Panel默认高度
        CSPPLib.Main.PanelHelper.setPanelHeight();

        //初始化第一个tab里面的panel的内容的高度
        var panel_0_height = $(tab_container_id).tabs("tabs")[0].panel("options").height;
        $("#frm_about").css("height", [(zoomRate * panel_0_height), "px"].join(""));

        var _left_nav_accordion_panels = $(left_nav_container_id).accordion("panels"),
            //菜单accordion的高度
            _left_nav_accordion_height = $(left_nav_container_id).accordion("options").height,
            _left_nav_panel_count = _left_nav_accordion_panels.length;
        $.each(_left_nav_accordion_panels, function(__index, __panel) {
            __panel.panel({
                onExpand: function () {
                    var __self = $(this);
                    __self.find("ul").slideDown(500, function() {
                        var __easyui_panel = $(__self).find(".easyui-panel"),
                            //菜单panel的高度
                            __left_nav_panel_height = __easyui_panel.height(),
                            __left_nav_panel_header = __self.panel("header"),
                            //菜单panel的header高度
                            __left_nav_panel_header_height = __left_nav_panel_header.height(),
                            //菜单panel的header的border-top
                            __left_nav_panel_header_border_top_width = parseInt($(__left_nav_panel_header).css("border-top-width")),
                            //菜单panel的haeder的border-bottom
                            __left_nav_panel_header_border_bottom_width = parseInt($(__left_nav_panel_header).css("border-bottom-width")),
                            //菜单panel的header的padding-top
                            __left_nav_panel_header_padding_top = parseInt($(__left_nav_panel_header).css("padding-top")),
                            //菜单panel的header的padding-bottom
                            __left_nav_panel_header_padding_bottom = parseInt($(__left_nav_panel_header).css("padding-bottom")),
                            //菜单panel的header总高度
                            __left_nav_panel_heder_sum_height = __left_nav_panel_header_height + __left_nav_panel_header_border_top_width + __left_nav_panel_header_border_bottom_width + __left_nav_panel_header_padding_top + __left_nav_panel_header_padding_bottom,
                            //菜单accordion除所有panel的header高度后的净余高度
                            __left_nav_accordion_residue_height = _left_nav_accordion_height - (_left_nav_panel_count * __left_nav_panel_heder_sum_height + 1);

                        if ((__left_nav_panel_height - __left_nav_panel_heder_sum_height) > __left_nav_accordion_residue_height) __easyui_panel.css({ width: "156px" });
                        else __easyui_panel.css({ width: "173px" });
                    });
                }
            });
        });

        //初始化左边菜单的事件
        $(".easyui-accordion li a")
            .on("click", function() {
                var _self = $(this),
                    _title = _self.attr("sub-title"),
                    _url = _self.attr("url"),
                    _icons = _self.attr("icons");
                if (_icons === "") _icons = null;
                CSPPLib.Main.TabsHelper.addTab(_title, _url, _icons);

                $(".easyui-accordion li div").removeClass("selected");
                $(this).parent().addClass("selected");
            })
            .hover(function () {
                $(this).parent().addClass("hover");
            }, function () {
                $(this).parent().removeClass("hover");
            });

    });

})("Main");