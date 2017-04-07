;
(function(ns) {
    CPSSLib.RegNameSpace(ns).addSubCompany = (function () {
        var self;

        var fn_clear = function() {
            $("#txtSerialNumber").textbox("setValue", "");
            $("#txtName").textbox("setValue", "");
            $("#txtSpelling").textbox("setValue", "");
            $("#txtEmail").textbox("setValue", "");
            $("#ddlPriceMode").combobox("setValue", "0");
            $("#txtLinkMan").textbox("setValue", "");
            $("#txtLinkTel").textbox("setValue", "");
            $("#txtSort").textbox("setValue", "");
            $("#txtComment").textbox("setValue", "");
        }

        //数据封装
        var fn_data_encapsulation = function () {
            var data = {
                SerialNumber: $("#txtSerialNumber").textbox("getValue"),
                Name: $("#txtName").textbox("getValue"),
                Spelling: $("#txtSpelling").textbox("getValue"),
                PriceMode: $("#ddlPriceMode").combobox("getValue"),
                Email: $("#txtEmail").textbox("getValue"),
                LinkMan: $("#txtLinkMan").textbox("getValue"),
                LinkTel: $("#txtLinkTel").textbox("getValue"),
                Sort: $("#txtSort").textbox("getValue"),
                ParentId: $("#txtParentId").val(),
                Comment: $("#txtComment").textbox("getValue")
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
                case "rtBasicCom_TB_Add_Save":
                    fn_postData(undefined, "/basic/addcompany", function(result) {
                        _msgbox.success(result.ErrorMessage, function () {
                            parent._window.close();
                        });
                    });
                    break;
                case "rtBasicCom_TB_Add_Save_Go":
                    fn_postData(undefined, "/basic/addcompany", function(result) {
                        fn_call_back(result);
                        fn_clear();
                    });
                    break;
                case "rtBasicCom_TB_Add_Cancel":
                    parent._window.close();
                    break;
            }
        }

        var fn_init = function() {
            self = this;
            $("#txtName").textbox({
                onChange: function(nVal, oVal) {
                    if (nVal == oVal) return;
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