;
(function(ns) {
    CPSSLib.RegNameSpace(ns).addStorage = (function () {
        var self;

        var fn_clear = function() {
            textBoxHelper.setValue("txtSerialNumber", "");
            textBoxHelper.setValue("txtName", "");
            textBoxHelper.setValue("txtSpelling", "");
            textBoxHelper.setValue("txtSort", "");
            textBoxHelper.setValue("txtComment", "");
        }

        //数据封装
        var fn_data_encapsulation = function () {
            var data = {
                SerialNumber: textBoxHelper.getValue("txtSerialNumber"),
                Name: textBoxHelper.getValue("txtName"),
                Spelling: textBoxHelper.getValue("txtSpelling"),
                Alias: textBoxHelper.getValue("txtAlias"),
                Sort: textBoxHelper.getValue("txtSort"),
                Comment: textBoxHelper.getValue("txtComment"),
                ParentId: $("#txtParentId").val()
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
                case "rtBasicStorage_TB_Add_Save":
                    fn_postData(undefined, "/basic/addstorage", function (result) {
                        if (result.ErrorCode === 0)
                            _msgbox.success(result.ErrorMessage,
                                function () {
                                    parent.storage.level(undefined, 1);
                                    parent._window.close();
                                });
                        else
                            _msgbox.error(result.ErrorMessage);
                    });
                    break;
                case "rtBasicStorage_TB_Add_Save_Go":
                    fn_postData(undefined, "/basic/addstorage", function (result) {
                        if (result.ErrorCode === 0)
                            _msgbox.success(result.ErrorMessage,
                                function () {
                                    fn_clear();
                                });
                        else
                            _msgbox.error(result.ErrorMessage);
                    });
                    break;
                case "rtBasicStorage_TB_Add_Cancel":
                    parent._window.close();
                    break;
            }
        }

        var fn_init = function() {
            self = this;
            $("#txtName").textbox({
                onChange: function(nVal, oVal) {
                    if (nVal === oVal) return;
                    if (nVal === "" || nVal === null) {
                        $("#txtSpelling").textbox("setValue", "");
                        return;
                    }
                    fn_postData({ data: { ChineseChar: nVal } }, "/commonajax/getspellingbychinesechar", function (result) {
                        $("#txtSpelling").textbox("setValue", result.rows.Spelling);
                    });
                }
            });
            $("#billbutton a").on("click", function () {
                fn_button_click(this);
            });
        }

        return {
            init: fn_init
        }
    })();
})("Page");