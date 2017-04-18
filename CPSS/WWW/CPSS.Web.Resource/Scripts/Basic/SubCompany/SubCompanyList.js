;
(function(ns) {
    CPSSLib.RegNameSpace(ns).subCompanyList = (function () {
        var self;

        //此方法的subCompany.level("****")中的subCompany是页面上定义的此JS自己的引用
        function fn_cellFormatter(val, row, index) {
            var html = "";
            if (row.ParentId.length >= 12) {
                var classId = row.ParentId.substr(0, row.ParentId.length - 6);
                html = "<a href=\"javascript:void(0);\" onclick=\"subCompany.level('" + classId + "')\" style=\"text-decoration:none;\">←</a>";
            }
            if (row.ChildNumber > 0) html += "<a href=\"javascript:void(0);\" onclick=\"subCompany.level('" + row.ClassId + "')\" style=\"text-decoration:none;\">→</a>";
            return html;
        }

        function fn_level(classId, reload) {
            if (typeof reload !== "undefined") self.reload = reload;
            if (typeof classId === "undefined")
                self.grid.datagrid("load",
                {
                    reload: self.reload,
                    data: {
                        ParentId: $("#txtParentId").val()
                    }
                });
            else
                self.grid.datagrid("load",
                {
                    reload: self.reload,
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
                pageSize: 10,
                pageList: [10, 20, 30, 50],
                queryParams: {
                    reload: self.reload,
                    data: {
                        ParentId: ($("#txtParentId").val() === null || $("#txtParentId").val() === "") ? "000001" : $("#txtParentId").val()
                    }
                },
                rowStyler: function(index, row) {
                    if (row.Deleted === 1) return "color: #FF0000;";
                    return "";
                },
                onLoadSuccess: function (data) {
                    $("#txtParentId").val(data.parentId);
                    self.reload = 0;
                    self.grid.datagrid("options").queryParams.reload = self.reload;
                },
                onDblClickRow: function (index, row) {

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

        var fn_postData = function (flag) {
            if (self.isDeletedButtonClick) return;
            self.isDeletedButtonClick = true;
            var msg = "请选择需要删除的分公司",
                url = "/basic/deletecompany",
                selectRow = self.grid.datagrid("getSelected");
            if (flag === 0) {
                if (selectRow.ChildNumber > 0) {
                    _msgbox.error("不能删除含有未删除的子节点的节点");
                    return;
                }
            }
            else {
                if (selectRow.Deleted === 0) {
                    _msgbox.error("改节点未被删除，不需要恢复删除。");
                    return;
                }
                url = ["/basic/redeletecompany"].join("");
                msg = "请选择需要恢复删除的分公司";
            }

            if (!fn_check_select_row(selectRow)) {
                _msgbox.alert(msg);
                return;
            }
            var data = {
                data: {
                    ComId: selectRow.ComId
                }
            }
            CPSSLib.Utils.AjaxRequest.postData(url,
                data,
                function (result) {
                    self.isDeletedButtonClick = false;
                    if (result.ErrorCode === 0) {
                        self.reload = 1;
                        _msgbox.success(result.ErrorMessage, function() {
                            fn_level();
                        });
                    }
                });
        }

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
                    if (selectRow.Deleted === 1) {
                        _msgbox.alert("删除状态的分公司不允许修改");
                        return;
                    }
                    url = ["/basic/editcompany?userid=", userId_g, "&comid=", selectRow.ComId].join("");
                    _window.open("add_company", "修改分公司信息", 600, 550, url);
                    break;
                case "rtBasicCom_TB_Delete":
                    fn_postData(0);
                    break;
                case "rtBasicCom_TB_Resume":
                    fn_postData(1);
                    break;
                case "rtBasicCom_TB_Class":
                    selectRow = self.grid.datagrid("getSelected");
                    if (!fn_check_select_row(selectRow)) {
                        _msgbox.success("请选择分公司再进行新增下级操作");
                        return;
                    }
                    url = ["/basic/addcompany?userid=", userId_g, "&parentid=", selectRow.ClassId].join("");
                    _window.open("add_company", "新增子级分公司信息", 600, 550, url);
                    break;
                case "rtBasicCom_TB_Preview":
                    self.lodop.PREVIEW();
                    break;
                case "rtBasicCom_TB_Design":
                    var page = {
                        billHead: {
                            SerialNumber: "",
                            Name: "",
                            Spelling: "",
                            PriceMode: "",
                            Email: "",
                            LinkMan: "",
                            LinkTel: "",
                            Status: ""
                        }
                    };
                    self.lodop.PRINT_INIT("分公司资料");                    self.lodop.SET_PRINT_STYLE("FontColor", "#0000FF");
                    self.lodop.SET_PRINT_STYLE("Alignment", 3);
                    self.lodop.ADD_PRINT_TEXT(10, 5, 70, 20, "编号：");
                    self.lodop.ADD_PRINT_TEXT(10, 75, 110, 20, page.billHead.SerialNumber);
                    self.lodop.ADD_PRINT_TEXT(10, 205, 70, 20, "名称：");
                    self.lodop.ADD_PRINT_TEXT(10, 275, 110, 20, page.billHead.Name);
                    self.lodop.ADD_PRINT_TEXT(10, 405, 70, 20, "拼音：");
                    self.lodop.ADD_PRINT_TEXT(10, 475, 110, 20, page.billHead.Spelling);
                    self.lodop.ADD_PRINT_TEXT(10, 605, 70, 20, "售价方式：");
                    self.lodop.ADD_PRINT_TEXT(10, 675, 110, 20, page.billHead.PriceMode);
                    self.lodop.ADD_PRINT_TEXT(40, 5, 70, 20, "Email：");
                    self.lodop.ADD_PRINT_TEXT(40, 75, 110, 20, page.billHead.Email);
                    self.lodop.ADD_PRINT_TEXT(40, 205, 70, 20, "联系人：");
                    self.lodop.ADD_PRINT_TEXT(40, 275, 110, 20, page.billHead.LinkMan);
                    self.lodop.ADD_PRINT_TEXT(40, 405, 70, 20, "联系电话：");
                    self.lodop.ADD_PRINT_TEXT(40, 475, 110, 20, page.billHead.LinkTel);
                    self.lodop.ADD_PRINT_TEXT(40, 605, 70, 20, "状态：");
                    self.lodop.ADD_PRINT_TEXT(40, 675, 110, 20, page.billHead.Status);

                    self.lodop.On_Return = function (TaskID, Value) {
                        _msgbox.alert(Value);
                    };
                    self.lodop.PRINT_DESIGN();
                    break;
            }
        }

        var fn_init = function (userId_g) {
            self = this;
            self.lodop = getLodop();
            self.reload = 0;
            fn_init_grid();
            $("#billbutton a").on("click", function () {
                fn_button_click(this, userId_g);
            });

            //查询数据会重新绑定datagrid的数据
            $("#btnSearch").on("click", function () {
                self.grid.datagrid("load",
                {
                    data: {
                        SerialNumber: textBoxHelper.getValue("txtSerialNumber"),
                        Name: textBoxHelper.getValue("txtName"),
                        Spelling: textBoxHelper.getValue("txtSpelling"),
                        PriceMode: comboboxHelper.getValue("ddlPriceMode"),
                        Status: comboboxHelper.getValue("ddlStatus"),
                        Email: textBoxHelper.getValue("txtEmail"),
                        LinkMan: textBoxHelper.getValue("txtLinkMan"),
                        LinkTel: textBoxHelper.getValue("txtLinkTel"),
                        ParentId: ($("#txtParentId").val() === null || $("#txtParentId").val() === "") ? "000001" : $("#txtParentId").val()
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