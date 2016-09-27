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

    CSPPLib.RegNameSpace(ns).MessageBox = (function() {

        var __default_options = { allowClose: false };

        var __getTitle = function(_title) {
            var __title = (typeof _title === "undefined" || _title === "") ? "系统提示" : _title;
            return __title;
        }

        var __msg = function (__title, __msgContent, __callback) {
            var ___fun = (typeof __callback === "function") ? __callback : null;
            if (___fun === null) return $.ligerDialog.alert(__msgContent, __title, "none", null, __default_options);
            else return $.ligerDialog.alert(__msgContent, __title, "none", ___fun, __default_options);
        };

        var _alert = function (__title, __msgContent, __callback) {
            return __msg(__getTitle(__title), __msgContent, __callback);
        };

        var _success = function (__title, __msgContent, __callback) {
            return $.ligerDialog.success(__msgContent, __getTitle(__title), __callback, __default_options);
        }

        var _warn = function (__title, __msgContent, __callback) {
            return $.ligerDialog.warn(__msgContent, __getTitle(__title), __callback, __default_options);
        }

        var _error = function (__title, __msgContent, __callback) {
            return $.ligerDialog.error(__msgContent, __getTitle(__title), __callback, __default_options);
        }

        var _question = function (__title, __msgContent) {
            return $.ligerDialog.question(__msgContent, __getTitle(__title));
        }

        var _confirm = function (__title, __msgContent, __callback) {
            return $.ligerDialog.confirm(__msgContent, __getTitle(__title), __callback, __default_options);
        };

        var _prompt_m = function (__title, __msgContent, __ismulti, __callback) {
            return $.ligerDialog.prompt(__getTitle(__title), __msgContent, __ismulti, __callback);
        }

        var _prompt = function (__title, __msgContent, __callback) {
            return _prompt_m(__title, __msgContent, false, __callback);
        }

        return {
            success: _success,
            alert: _alert,
            warn: _warn,
            error: _error,
            question: _question,
            confirm: _confirm,
            promptm:_prompt_m,
            prompt: _prompt
        };

    })();

})("Utils");

var msgbox = CSPPLib.Utils.MessageBox;