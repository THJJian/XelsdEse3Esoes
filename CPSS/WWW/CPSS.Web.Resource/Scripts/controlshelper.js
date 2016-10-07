;
(function (ns) {

    CSPPLib.RegNameSpace(ns).linkButtonHelper = (function() {
        var self;

        var _init = function(__onClick) {
            self = this;
            self.linkbutton = $(".easyui-linkbutton").linkbutton({
                onClick: __onClick
            });
        }

        return {
            init: _init
        };
    })();

    CSPPLib.RegNameSpace(ns).textBoxHelper = (function() {
        var self;

        var _init = function (__onClickButton, __onChange, __invoiceType, __queryType) {
            self = this;
            $(".easyui-textbox")
                .textbox({
                    onClickButton: __onClickButton,
                    onChange: __onChange,
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
                                        MsgBox.alert("", "弹出选择窗口");
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

        var _setValue = function(__textboxid, __value) {
            $("#" + __textboxid).textbox("setValue", __value);
        }

        var _getValue = function (__textboxid) {
            return $("#" + __textboxid).textbox("getValue");
        };

        return {
            init: _init,
            setValue: _setValue,
            getValue: _getValue
        }
    })();

})("UI");

var linkButtonHelper = CSPPLib.UI.linkButtonHelper;
var textBoxHelper = CSPPLib.UI.textBoxHelper;