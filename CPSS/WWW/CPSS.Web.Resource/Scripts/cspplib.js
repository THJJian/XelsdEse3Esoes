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

    CSPPLib.RegNameSpace(ns).Window = (function () {
        var _win,
            _strBuilder = CSPPLib.Utils.StringBuilder,
            _win_html = "<div id=\"{0}\" class=\"easyui-window\" title=\"{1}\" style=\"width:{2}px; height: {3}px; display: none;\" ><iframe frameborder=\"0\" style=\"width:100%; height: {4}px;\" src=\"{5}\" scrolling=\"no\"></iframe></div>";

        var _open = function (_win_id_, _win_title_, _win_width_, _win_height_, _win_src_) {
            var __win_id_ = "#" + _win_id_;
            _strBuilder.appendFrt(_win_html, [_win_id_, _win_title_, _win_width_, _win_height_, _win_height_ - 39, _win_src_]);
            var _html = _strBuilder.toString();
            $(document.body).append($(_html));
            _win = $(__win_id_).window({
                //iconCls: "icon-save",
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collapsible: false,
                onClose: function() {
                    var _13 = $.data(this, "window");
                    if (_13.shadow) {
                        _13.shadow.remove();
                    }
                    if (_13.mask) {
                        _13.mask.remove();
                    }
                    _13.window.remove();
                }
            });
            _win.window("open");
        }

        var _close = function () {
            if (_win)
                _win.window("close");
        }

        return {
            open: _open,
            close: _close
        };
    })();

})("Utils");

var _msgbox = CSPPLib.Utils.MsgBox;
var _window = CSPPLib.Utils.Window;