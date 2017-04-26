;
(function (ns) {

    CPSSLib.RegNameSpace(ns).linkButtonHelper = (function() {
        var self;

        var fn_init = function(onClick) {
            self = this;
            if(typeof onClick !== "function") onClick = function() { return false; }
            self.linkbutton = $(".easyui-linkbutton").linkbutton({
                onClick: onClick
            });
        }

        return {
            init: fn_init
        };
    })();

    CPSSLib.RegNameSpace(ns).textBoxHelper = (function() {
        var self;

        var fn_init = function (onClickButton, onChange, invoiceType, queryType) {
            self = this;
            if (typeof onClickButton !== "function") onClickButton = function () { return false; };
            if (typeof onChange !== "function") onChange = function () { return false;}
            $(".easyui-textbox")
                .textbox({
                    onClickButton: onClickButton,
                    onChange: onChange,
                    inputEvents: {
                        keydown: function (e) {
                            var _textbox = $(e.data.target),
                                _tabIndex = parseInt(_textbox.attr("tabindex")),
                                _value = _textbox.textbox("options").value;
                            var _keyCode = e.keyCode;
                            switch (_keyCode) {
                                case 13:
                                    if (_value === null || _value === "") {
                                        //TODO 弹出选择窗口
                                        _msgbox.alert("弹出选择窗口");
                                        break;
                                    }
                                    var _next_tabindex = _tabIndex + 1;
                                    if ($("input[type='text'][tabindex=" + _next_tabindex + "]").length === 0)
                                        _next_tabindex = 0;
                                    $("input[type='text'][tabindex=" + _next_tabindex + "]").focus();
                                    break;
                            }
                        }
                    }
                });
        };

        var fn_setValue = function (ctrlId, value) {
            $("#" + ctrlId).textbox("setValue", value);
        }

        var fn_getValue = function (ctrlId) {
            return $("#" + ctrlId).textbox("getValue");
        };

        return {
            init: fn_init,
            setValue: fn_setValue,
            getValue: fn_getValue
        }
    })();

    CPSSLib.RegNameSpace(ns).comboboxHelper = (function() {
        
        var fn_setValue = function(ctrlId, value) {
            $("#" + ctrlId).combobox("setValue", value);
        }

        var fn_getValue = function(ctrlId) {
            return $("#" + ctrlId).combobox("getValue");
        }

        var fn_getText = function(ctrlId) {
            return $("#" + ctrlId).combobox("getText");
        }

        return {
            setValue: fn_setValue,
            getValue: fn_getValue,
            getText: fn_getText
        }
    })();

    CPSSLib.RegNameSpace(ns).combogridHelper = (function() {
        
        var fn_setValue = function (ctrlId) {
            $("#" + ctrlId).combogrid("setValue", value);
        }

        var fn_getValue = function(ctrlId) {
            return $("#" + ctrlId).combogrid("getValue");
        }

        var fn_getText = function (ctrlId) {
            return $("#" + ctrlId).combobox("getText");
        }

        return {
            getValue: fn_getValue,
            getText: fn_getText
        }
    })();

    CPSSLib.RegNameSpace(ns).numberboxHelper = (function () {
        var self;

        var fn_init = function (onClickButton, onChange, invoiceType, queryType) {
            self = this;
            if (typeof onClickButton !== "function") onClickButton = function () { return false; };
            if (typeof onChange !== "function") onChange = function () { return false; }
            $(".easyui-textbox")
                .textbox({
                    onClickButton: onClickButton,
                    onChange: onChange,
                    inputEvents: {
                        keydown: function (e) {
                            var _textbox = $(e.data.target),
                                _tabIndex = parseInt(_textbox.attr("tabindex")),
                                _value = _textbox.textbox("options").value;
                            var _keyCode = e.keyCode;
                            switch (_keyCode) {
                                case 13:
                                    if (_value === null || _value === "") {
                                        //TODO 弹出选择窗口
                                        _msgbox.alert("弹出选择窗口");
                                        break;
                                    }
                                    var _next_tabindex = _tabIndex + 1;
                                    if ($("input[type='text'][tabindex=" + _next_tabindex + "]").length === 0)
                                        _next_tabindex = 0;
                                    $("input[type='text'][tabindex=" + _next_tabindex + "]").focus();
                                    break;
                            }
                        }
                    }
                });
        };

        var fn_setValue = function (ctrlId, value) {
            $("#" + ctrlId).textbox("setValue", value);
        }

        var fn_getValue = function (ctrlId) {
            return $("#" + ctrlId).textbox("getValue");
        };

        return {
            init: fn_init,
            setValue: fn_setValue,
            getValue: fn_getValue
        }
    })();
})("UI");

var linkButtonHelper = CPSSLib.UI.linkButtonHelper;
var textBoxHelper = CPSSLib.UI.textBoxHelper;
var comboboxHelper = CPSSLib.UI.comboboxHelper;
var combogridHelper = CPSSLib.UI.combogridHelper;
var numberboxHelper = CPSSLib.UI.numberboxHelper;