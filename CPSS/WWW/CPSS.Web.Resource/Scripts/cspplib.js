;
var CSPPLib = (function () {
    if (!window.console) {
        window.console = {};
        window.console.log = function () {
        };
    }
    return {
        RegNameSpace: function (ns) {
            var domains = ns.split(".");
            var cur_domain = CSPPLib;
            for (var i = 0; i < domains.length; i++) {
                var domain = domains[i];
                if (typeof (cur_domain[domain]) === "undefined") {
                    cur_domain[domain] = {};
                }
                cur_domain = cur_domain[domain];
            }
            return cur_domain;
        }
    };
})();

(function (ns) {

    CSPPLib.RegNameSpace(ns).StringBuilder = (function () {
        var self;

        var init = function() {
            self = this;
            self.stringBuilder = [];
        }

        var _append = function (_str) {
            if (_str === "") return;
            self.stringBuilder.push(_str);
        }

        var _appendFrt = function (_str, _arr) {
            if (_arr === null || typeof _arr !== "object") throw "appendFrt函数的_arr参数错误，应为数组。";
            if (_str === "") return;
            var isNotEnd = true;
            var reg = new RegExp(/\{(\d+)\}/g);
            var _tmp_str = _str;
            do {
                var group = reg.exec(_str);
                if (group === null) {
                    isNotEnd = false;
                    continue;
                }
                var index = parseInt(group[1]);
                try {
                    _tmp_str = _tmp_str.replace(group[0], _arr[index]);
                } catch (e) {
                    console.log(e);
                }
            } while (isNotEnd)
            _append(_tmp_str);
        }

        var _toString = function () {
            var _result = self.stringBuilder.join("");
            self.stringBuilder = [];
            return _result;
        }

        init();

        return {
            append: _append,
            appendFrt: _appendFrt,
            toString: _toString
        };
    })();

    CSPPLib.RegNameSpace(ns).MsgBox = (function() {

        var __getTitle = function(_title) {
            var __title = (typeof _title === "undefined" || _title === "") ? "系统提示" : _title;
            return __title;
        }

        var _alert = function (__title, __msgContent) {
            $.messager.alert(__getTitle(__title), __msgContent);
        };

        var __msg = function (__title, __msgContent, __icon, __fun) {
            var ___fun = (typeof __fun === "function") ? __fun : null;
            if (___fun === null) $.messager.alert(__getTitle(__title), __msgContent, __icon);
            else $.messager.alert(__getTitle(__title), __msgContent, __icon, ___fun);
        };

        var _info = function (__title, __msgContent, __fun) {
            __msg(__title, __msgContent, "info", __fun);
        }

        var _success = function() {
            
        }

        var _warning = function (__title, __msgContent, __fun) {
            __msg(__title, __msgContent, "warning", __fun);
        }

        var _error = function (__title, __msgContent, __fun) {
            __msg(__title, __msgContent, "error", __fun);
        }

        var _question = function (__title, __msgContent, __fun) {
            __msg(__title, __msgContent, "question", __fun);
        }

        var _confirm = function (__title, __msgContent, __fun) {
            var ___fun = (typeof __fun === "function") ? __fun : function(isOk) {
                _alert("系统提示", isOk);
            };
            $.messager.confirm(__getTitle(__title), __msgContent, ___fun);
        };

        var _prompt = function (__title, __msgContent, __fun) {
            var ___fun = (typeof __fun === "function") ? __fun : function(__result) {
                if (__result) {
                    _alert("系统提示", ["您输入的内容是:", result, "；请自行处理输入内容。"].join(""));
                };
            }
            var ___msgContent = (typeof __msgContent === "undefined" || __msgContent === "") ? "请在下面文本框内输入内容：" : __msgContent;
            $.messager.prompt(__getTitle(__title), ___msgContent, ___fun);
        }

        return {
            alert: _alert,
            info: _info,
            success: _success,
            warning: _warning,
            error: _error,
            question: _question,
            confirm: _confirm,
            prompt: _prompt
        };

    })();

})("Utils");

/*
(function(ns) {
    CSPPLib.RegNameSpace(ns).Accordion = (function () {
        var self;

        var __init__accordion = function(_data) {
            $(self.ctrlId).accordion({ animate: false });
            $.each(_data, function (_i, _item) {
                var builder = CSPPLib.RegNameSpace("Utils").StringBuilder;
                builder.append("<ul>");
                var menu_item = "<li><div><a ref=\"{0}\" href=\"javascript:void(0)\" onclick=\"{1}\" ><span class=\"icon {2}\" >&nbsp;</span><span class=\"nav\">{3}</span></a></div></li>";
                $.each(_item.menus, function (__i, __item) {
                    var __data = [
                        __item.menuid,
                        __item.onClick,
                        _item.icon,
                        __item.menuName
                    ];
                    builder.appendFrt(menu_item, __data);
                });
                builder.append("</ul>");

                $(self.ctrlId).accordion("add", {
                    title: _item.title,
                    content: builder.ToString(),
                    iconCls: _item.icon
                });
            });

            $(".easyui-accordion li a").click(function () {
                var title = $(this).children(".nav").text();

                var url = $(this).attr("rel");
                var menuid = $(this).attr("ref");
                var icon = getIcon2(self.data, menuid);

                var exist = $("#tabs").tabs("exists", title);//返回false或true
                if (url) {
                    if (!exist) {
                        addTab(title, url, icon);
                    }
                    else {
                        $("#tabs").tabs("select", title);
                    }
                }
                $(".easyui-accordion li div").removeClass("selected");
                $(this).parent().addClass("selected");
            }).hover(function () {
                $(this).parent().addClass("hover");
            }, function () {
                $(this).parent().removeClass("hover");
            });

            //选中第一个
            var panels = $("#nav").accordion("panels");
            var t = panels[0].panel("options").title;
            $("#nav").accordion("select", t);
        }

        var init = function(_settings) {
            self = this;
            self.ctrlId = "#" + _settings.id;
            //self.title = _settings.title;

            __init__accordion(_settings.data);
        };

        var remove = function() {
            
        }

        var update = function (_data) {
            remove();
            __init__accordion(_data);
        };

        return {
            InitAccordion: init,
            Update: update
        };
    })();


})("Main");
*/

var MsgBox = CSPPLib.Utils.MsgBox;