﻿;
(function(ns) {
    CPSSLib.RegNameSpace(ns).addEmployee = (function () {
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
                ParentId: $("#txtParentId").val(),
                SerialNumber: textBoxHelper.getValue("txtSerialNumber"),
                Name: textBoxHelper.getValue("txtName"),
                Spelling: textBoxHelper.getValue("txtSpelling"),
                DepId: combogridHelper.getValue("ddlDepartment"),
                DepName: combogridHelper.getText("ddlDepartment"),
                LowestDiscount: numberboxHelper.getValue("txtLowestDiscount"),
                PreInAdvanceTotal: numberboxHelper.getValue("txtPreInAdvanceTotal"),
                PrepayFeeTotal: numberboxHelper.getValue("txtPrePayFeeTotal"),
                Mobile: textBoxHelper.getValue("txtMobile"),
                Address: textBoxHelper.getValue("txtAddress"),
                Sort: textBoxHelper.getValue("txtSort"),
                Comment: textBoxHelper.getValue("txtComment")
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
                case "rtBasicEmp_TB_Add_Save":
                    fn_postData(undefined, "/basic/addemployee", function (result) {
                        if (result.ErrorCode === 0)
                            _msgbox.success(result.ErrorMessage,
                                function () {
                                    parent.employee.level(undefined, 1);
                                    parent._window.close();
                                });
                        else
                            _msgbox.error(result.ErrorMessage);
                    });
                    break;
                case "rtBasicEmp_TB_Add_Save_Go":
                    fn_postData(undefined, "/basic/addemployee", function (result) {
                        if (result.ErrorCode === 0)
                            _msgbox.success(result.ErrorMessage,
                                function () {
                                    fn_clear();
                                });
                        else
                            _msgbox.error(result.ErrorMessage);
                    });
                    break;
                case "rtBasicEmp_TB_Add_Cancel":
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