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

    CSPPLib.RegNameSpace(ns).LocalStorage = (function () {
        var __isLocalStorage = window.localStorage ? true : false;

        function _setCookie(key, value, seconds) {
            var argv = arguments;
            var argc = argv.length;
            var expires;
            if (seconds == null) {
                expires = new Date(2100, 11, 31);
            } else {
                expires = new Date();
                expires.setTime(expires.getTime() + seconds * 1000);
            }
            var path = (3 < argc) ? argv[3] : "/";
            var domain = (4 < argc) ? argv[4] : null;
            if (!domain) {
                domain = document.domain;
                domain = domain.substring(domain.lastIndexOf(".", domain.lastIndexOf(".") - 1) + 1);
            }
            var secure = (5 < argc) ? argv[5] : false;
            document.cookie = key + "=" + escape(value) + ";expires=" + expires.toGMTString() + (path == null ? "" : ";path=" + path) + (domain == "" ? "" : ";domain=" + domain) + (secure == null ? ";secure" : "");
        }

        function _removeCookie(key) {
            _setCookie(key, "", -1);
        }

        function _getCookie(key, nounescape) {
            var p = key + "=";
            var start = document.cookie.indexOf(p);
            if (start == -1) return null;
            var end = document.cookie.indexOf(";", start);
            var v = document.cookie.substring(start + p.length, (end > start ? end : document.cookie.length));
            return !nounescape ? unescape(v) : v;
        }

        var _set = function (key, value) {
            if (__isLocalStorage) {
                window.localStorage.setItem(key, value);
            } else {
                _setCookie(key, value);
            }
        };

        var _get = function (key) {
            if (__isLocalStorage) {
                return window.localStorage.getItem(key);
            } else {
                return _getCookie(key, false);
            }
        };

        var _remove = function (key) {
            if (__isLocalStorage) {
                localStorage.removeItem(key);
            } else {
                _removeCookie(key);
            }
        };

        return {
            get: function (key) {
                return _get("__localStorage__" + key);
            },
            set: function (key, value) {
                _set("__localStorage__" + key, value);
            },
            remove: function (key) {
                _remove("__localStorage__" + key);
            },
            getCookie: _getCookie,
            setCookie: _setCookie,
            removeCookie: _removeCookie
        };

    })();

    CSPPLib.RegNameSpace(ns).AjaxRequest = (function() {

        var __success = function(jsonResult) {
            _msgbox.success("", jsonResult.Data.ErrorMessage);
        }

        var __error = function (xmlHttpRequest) {
            if (xmlHttpRequest.readyState != 0 && xmlHttpRequest.readyState != 0) {
                _msgbox.error("", "执行过程中错误。");
            }
        }

        ///<summary>后台的请求参数必须包含在RequestWebViewData里面</summary>
        var _postData = function(_url_, _data_, _onSuccess_, _onComplete_, _onError_) {
            $.post(_url_, _data_,
                function (jsonResult) {
                    if (_onSuccess_ && typeof _onSuccess_ === "function")
                        _onSuccess_(jsonResult);
                    else __success(jsonResult);
                }, "json")
                .error(function (XMLHttpRequest) {
                    //状态码 
                    //0 － （未初始化）还没有调用send()方法 
                    //1 － （载入）已调用send()方法，正在发送请求 
                    //2 － （载入完成）send()方法执行完成，已经接收到全部响应内容 
                    //3 － （交互）正在解析响应内容 
                    //4 － （完成）响应内容解析完成，可以在客户端调用了
                    if (_onError_ && typeof _onError_ === "function")
                        _onError_(XMLHttpRequest);
                    else __error(XMLHttpRequest);
                })
                .complete(function () {
                    if (_onComplete_ && typeof _onComplete_ === "function")
                        _onComplete_();
                });
        };

        return {
            postData: _postData
        }
    })();

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