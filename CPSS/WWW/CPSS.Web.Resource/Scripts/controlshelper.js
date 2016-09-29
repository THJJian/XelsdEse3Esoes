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

        var _init = function (__onClickButton, __onChange) {
            self = this;
            $(".easyui-textbox")
                .textbox({
                    onClickButton: __onClickButton,
                    onChange: __onChange
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