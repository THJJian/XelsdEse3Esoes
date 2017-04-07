;
(function(ns) {
    CPSSLib.RegNameSpace(ns).subCompanyList = (function () {
        var self;
        var fn_init_grid = function () {
            self.grid = $("#dvgDataGrid");
            self.grid.datagrid({
                rownumbers: true,
                singleSelect: true,
                pagination: true,
                url: "/basic/getcompanylist",
                method: "post",
                pageSize: 10,
                pageList: [10, 20, 30, 50],
                queryParams: {
                    data: {
                        Email: $("#txtEmail").textbox("getValue"),
                        LinkMan:$("#txtLinkMan").textbox("getValue"),
                        LinkTel:$("#txtLinkTel").textbox("getValue"),
                        Name: $("#txtName").textbox("getValue"),
                        ParentId: ($("#txtParentId").val() === null || $("#txtParentId").val() === "") ? "000001" : $("#txtParentId").val(),
                        PriceMode:$("#ddlPriceMode").combobox("getValue"),
                        SerialNumber:$("#txtSerialNumber").textbox("getValue"),
                        Spelling:$("#txtSpelling").textbox("getValue"),
                        Status: $("#ddlPriceMode").combobox("getValue")
                    }
                },
                onLoadSuccess: function (data) {
                    $("#txtParentId").val(data.parentId);
                },
                onClickRow: function (index, row) {

                },
                onDblClickRow: function (index, row) {

                },
                onSelect: function (index, row) {

                }
            });
        }

        var fn_check_select_row = function(selectRow) {
            if (selectRow === null) return false;
            return true;
        }

        //alert($("#txtSerialNumber").textbox("getValue"));
        var fn_button_click = function (sender, userId_g) {
            var id = $(sender).attr("id");
            var selectRow, url;
            switch (id) {
                case "rtBasicCom_TB_Add":
                    var parentId = $("#txtParentId").val();
                    url = ["/basic/addcompany?userid=", userId_g, "&parentid=", parentId].join("");
                    _window.open("add_company", "新增公司信息", 600, 550, url);
                    break;
                case "rtBasicCom_TB_Edit":
                    selectRow = self.grid.datagrid("getSelected");
                    url = ["/basic/editcompany?userid=", userId_g, "&comid=", selectRow.ComId].join("");
                    _window.open("add_company", "新增公司信息", 600, 550, url);
                    break;
                case "rtBasicCom_TB_Delete":

                    break;
                case "rtBasicCom_TB_Resume":

                    break;
                case "rtBasicCom_TB_Class":
                    selectRow = self.grid.datagrid("getSelected");
                    if (!fn_check_select_row(selectRow)) {
                        _msgbox.success("请选择行再进行新增操作。");
                        return;
                    }

                    break;
                case "rtBasicCom_TB_Preview":

                    break;
                case "rtBasicCom_TB_Design":

                    break;
            }
        }

        var fn_init = function (userId_g) {
            self = this;
            fn_init_grid();
            $("#billbutton a").on("click", function () {
                fn_button_click(this, userId_g);
            });
        }

        return {
            init: fn_init
        }
    })();
})("Page");