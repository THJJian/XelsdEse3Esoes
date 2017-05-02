;
(function(ns) {
    CPSSLib.RegNameSpace(ns).addUnit = (function () {
        var self;

        var fn_clear = function() {
            textBoxHelper.setValue("txtName", "");
            textBoxHelper.setValue("txtSort", "");
        }

        //数据封装
        var fn_data_encapsulation = function () {
            var data = {
                Name: textBoxHelper.getValue("txtName"),
                Sort: textBoxHelper.getValue("txtSort")
            };
            return data;
        }

        //默认回调函数
        var fn_call_back = function(result) {
            if (result.ErrorCode !== 0) 
                _msgbox.error(result.ErrorMessage);
            else 
                _msgbox.alert(result.ErrorMessage);
        }

        var fn_postData = function (data, url,callBack) {
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
                case "rtBasicUnit_TB_Add_Save":
                    fn_postData(undefined, "/basic/addunit", function (result) {
                        if (result.ErrorCode === 0)
                            _msgbox.success(result.ErrorMessage,
                                function () {
                                    parent.unit.level();
                                    parent._window.close();
                                });
                        else
                            _msgbox.error(result.ErrorMessage);
                    });
                    break;
                case "rtBasicUnit_TB_Add_Save_Go":
                    fn_postData(undefined, "/basic/addunit", function (result) {
                        if (result.ErrorCode === 0)
                            _msgbox.success(result.ErrorMessage,
                                function () {
                                    fn_clear();
                                });
                        else
                            _msgbox.error(result.ErrorMessage);
                    });
                    break;
                case "rtBasicUnit_TB_Add_Cancel":
                    parent._window.close();
                    break;
            }
        }

        var fn_init = function() {
            self = this;
            $("#billbutton a").on("click", function () {
                fn_button_click(this);
            });
        }

        return {
            init: fn_init
        }
    })();
})("Page");