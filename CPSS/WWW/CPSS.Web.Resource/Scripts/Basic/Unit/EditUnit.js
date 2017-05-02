;
(function(ns) {
    CPSSLib.RegNameSpace(ns).editUnit = (function () {
        var self;

        //数据封装
        var fn_data_encapsulation = function () {
            var data = {
                Name: textBoxHelper.getValue("txtName"),
                Sort: textBoxHelper.getValue("txtSort"),
                UnitId: self.unitId
            };
            return data;
        }

        //默认回调函数
        var fn_call_back = function (result) {
            if (result.ErrorCode !== 0)
                _msgbox.error(result.ErrorMessage);
            else
                _msgbox.alert(result.ErrorMessage);
        }

        var fn_postData = function (data, url, callBack) {
            if (typeof data !== "object") data = { data: fn_data_encapsulation() };
            if (typeof callBack !== "function") callBack = fn_call_back;
            CPSSLib.Utils.AjaxRequest.postData(url,
                data,
                function (result) {
                    callBack(result);
                });
        }

        var fn_button_click = function (sender) {
            var id = $(sender).attr("id");
            switch (id) {
                case "rtBasicUnit_TB_Edit_Save":
                    fn_postData(undefined, "/basic/editunit", function (result) {
                        if (result.ErrorCode === 0)
                            _msgbox.success(result.ErrorMessage,
                                function() {
                                    parent.unit.level();
                                    parent._window.close();
                                });
                        else
                            _msgbox.error(result.ErrorMessage);
                    });
                    break;
                case "rtBasicUnit_TB_Edit_Cancel":
                    parent._window.close();
                    break;
            }
        }

        var fn_init = function (unitId) {
            self = this;
            self.unitId = unitId;
            $("#billbutton a").on("click", function () {
                fn_button_click(this);
            });
        }

        return{
            init: fn_init
        };
    })();
})("Page");