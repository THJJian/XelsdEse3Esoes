;
(function (ns) {
    //主页的Tab标签设置
    CSPPLib.RegNameSpace(ns).TabsHelper = (function() {
        var self,
            tab_container_id = "#main_tabs";
        var tabContent = "<iframe scrolling=\"auto\" frameborder=\"0\"  src=\"{0}\" style=\"width:100%; height:99%;\"></iframe>";

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
                builder.appendFrt(tabContent, [_url]);
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
        var self,
            left_nav_container_id = "#left_nav";

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

        __init();

        return {
            setPanelHeight: _setPanelHeight
        };
    })();
})("Main");