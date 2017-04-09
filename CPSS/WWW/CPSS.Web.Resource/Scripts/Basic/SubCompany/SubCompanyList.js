;
(function(ns) {
    CPSSLib.RegNameSpace(ns).subCompanyList = (function () {
        var self;

        function fn_cellFormatter(val, row, index) {
            if (row.ChildNumber === 0) return "";
            //TODO 既有上级又有下级
            return '<a href="javascript:void(0);" onclick="subCompany.level(\'' + row.ClassId + '\')">+</a>';
        }


        function fn_level(classId) {
            self.grid.datagrid("load",
            {
                data: {
                    ParentId: classId
                }
            });
        }

        var fn_init_grid = function () {
            self.grid = $("#dvgDataGrid");
            self.grid.datagrid({
                rownumbers: true,
                singleSelect: true,
                pagination: true,
                url: "/basic/getcompanylist",
                method: "post",
                pageSize: 1,//10,
                pageList: [1, 10, 20, 30, 50],
                queryParams: {
                    data: {
                        Email: $("#txtEmail").textbox("getValue"),
                        LinkMan: $("#txtLinkMan").textbox("getValue"),
                        LinkTel: $("#txtLinkTel").textbox("getValue"),
                        Name: $("#txtName").textbox("getValue"),
                        ParentId: ($("#txtParentId").val() === null || $("#txtParentId").val() === "") ? "000001" : $("#txtParentId").val(),
                        PriceMode: $("#ddlPriceMode").combobox("getValue"),
                        SerialNumber: $("#txtSerialNumber").textbox("getValue"),
                        Spelling: $("#txtSpelling").textbox("getValue"),
                        Status: $("#ddlStatus").combobox("getValue")
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
            var paging = self.grid.datagrid("getPager");
            paging.pagination({
                showRefresh: false
            });
        }

        var fn_check_select_row = function(selectRow) {
            if (selectRow === null || !selectRow) return false;
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
                    if (!fn_check_select_row(selectRow)) {
                        _msgbox.alert("请选择需要修改的分公司");
                        return;
                    }
                    url = ["/basic/editcompany?userid=", userId_g, "&comid=", selectRow.ComId].join("");
                    _window.open("add_company", "新增公司信息", 600, 550, url);
                    break;
                case "rtBasicCom_TB_Delete":
                    selectRow = self.grid.datagrid("getSelected");
                    if (!fn_check_select_row(selectRow)) {
                        _msgbox.alert("请选择需要删除的分公司");
                        return;
                    }
                    url = ["/basic/deletedcompany?comid=", selectRow.ComId].join("");

                    break;
                case "rtBasicCom_TB_Resume":
                    selectRow = self.grid.datagrid("getSelected");
                    if (!fn_check_select_row(selectRow)) {
                        _msgbox.alert("请选择需要恢复删除的分公司");
                        return;
                    }
                    url = ["/basic/redeletedcompany?comid=", selectRow.ComId].join("");

                    break;
                case "rtBasicCom_TB_Class":
                    selectRow = self.grid.datagrid("getSelected");
                    if (!fn_check_select_row(selectRow)) {
                        _msgbox.success("请选择分公司再进行新增下级操作");
                        return;
                    }
                    url = ["/basic/addcompany?userid=", userId_g, "&parentid=", selectRow.ClassId].join("");
                    _window.open("add_company", "新增公司信息", 600, 550, url);
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

            //查询数据会重新绑定datagrid的数据
            $("#btnSearch").on("click", function () {
                self.grid.datagrid("load",
                {
                    data: {
                        Email: $("#txtEmail").textbox("getValue"),
                        LinkMan: $("#txtLinkMan").textbox("getValue"),
                        LinkTel: $("#txtLinkTel").textbox("getValue"),
                        Name: $("#txtName").textbox("getValue"),
                        ParentId: ($("#txtParentId").val() === null || $("#txtParentId").val() === "") ? "000001" : $("#txtParentId").val(),
                        PriceMode: $("#ddlPriceMode").combobox("getValue"),
                        SerialNumber: $("#txtSerialNumber").textbox("getValue"),
                        Spelling: $("#txtSpelling").textbox("getValue"),
                        Status: $("#ddlStatus").combobox("getValue")
                    }
                });
            });
            return this;
        }

        return {
            init: fn_init,
            cellFormatter: fn_cellFormatter,
            level: fn_level
        }
    })();
})("Page");