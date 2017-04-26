;
(function(ns) {
    CPSSLib.RegNameSpace(ns).editEmployee = (function () {
        var self;

        //数据封装
        var fn_data_encapsulation = function () {
            var data = {
                SerialNumber: textBoxHelper.getValue("txtSerialNumber"),
                Name: textBoxHelper.getValue("txtName"),
                Spelling: textBoxHelper.getValue("txtSpelling"),
                Sort: textBoxHelper.getValue("txtSort"),
                Comment: textBoxHelper.getValue("txtComment"),
                EmpId: self.empId
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
                case "rtBasicEmp_TB_Edit_Save":
                    fn_postData(undefined, "/basic/editcompany", function (result) {
                        if (result.ErrorCode === 0)
                            _msgbox.success(result.ErrorMessage,
                                function() {
                                    parent.employee.level(undefined, 1);
                                    parent._window.close();
                                });
                        else
                            _msgbox.error(result.ErrorMessage);
                    });
                    break;
                case "rtBasicEmp_TB_Edit_Cancel":
                    parent._window.close();
                    break;
            }
        }

        var fn_init = function (empId) {
            self = this;
            self.empId = empId;
            $("#txtName").textbox({
                onChange: function (nVal, oVal) {
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

        return{
            init: fn_init
        };
    })();
})("Page");