﻿;
(function(ns) {
    CPSSLib.RegNameSpace(ns).subCompanyList = (function() {
        var self;

        //此方法的subCompany.level("****")中的subCompany是页面上定义的此JS自己的引用
        function fn_cellFormatter(val, row, index) {
            var html = "";
            if (row.ParentId.length >= 12) {
                var classId = row.ParentId.substr(0, row.ParentId.length - 6);
                html = "<a href=\"javascript:void(0);\" onclick=\"subCompany.level('" + classId + "')\" style=\"text-decoration:none;\">←</a>";
            }
            if (row.ChildNumber > 0)
                html += "<a href=\"javascript:void(0);\" onclick=\"subCompany.level('" + row.ClassId + "')\" style=\"text-decoration:none;\">→</a>";
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

        var fn_check_select_row = function(selectRow) {
            if (selectRow === null || !selectRow) return false;
            return true;
        };

        var fn_postData = function(flag) {
            var msg = "请选择需要删除的分公司",
                url = "/basic/deletecompany",
                selectRow = self.grid.datagrid("getSelected");

            if (!fn_check_select_row(selectRow)) {
                if(flag !== 0) msg = "请选择需要恢复删除的分公司";
                _msgbox.alert(msg);
                return;
            }

            if (flag === 0) {
                if (selectRow.ChildNumber > 0) {
                    _msgbox.error("不能删除含有未删除的子节点的节点");
                    return;
                }
            } else {
                if (selectRow.Deleted === 1) {
                    _msgbox.error("该节点未被删除，不需要恢复删除。");
                    return;
                }
                url = ["/basic/redeletecompany"].join("");
            }

            if (self.isDeletedButtonClick) return;
            self.isDeletedButtonClick = true;

            var data = {
                data: {
                    ComId: selectRow.ComId
                }
            };
            CPSSLib.Utils.AjaxRequest.postData(url,
                data,
                function(result) {
                    self.isDeletedButtonClick = false;
                    if (result.ErrorCode === 0) {
                        self.reload = 1;
                        _msgbox.success(result.ErrorMessage,
                            function() {
                                fn_level();
                            });
                    }
                });
        };

        var fn_button_click = function(sender, userId_g) {
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
                if (selectRow.Deleted === 2) {
                    _msgbox.alert("删除状态的分公司不允许修改");
                    return;
                }
                url = ["/basic/editcompany?userid=", userId_g, "&comid=", selectRow.ComId].join("");
                _window.open("edit_company", "修改分公司信息", 600, 550, url);
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
                self.printer.preview();
                break;
            case "rtBasicCom_TB_Design":
                self.printer.design();
                break;
            }
        };


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
                rowStyler: function (index, row) {
                    if (row.Deleted === 2) return "color: #FF0000;";
                    return "";
                },
                onLoadSuccess: function (data) {
                    $("#txtParentId").val(data.parentId);
                    self.reload = 0;
                    self.grid.datagrid("options").queryParams.reload = self.reload;
                },
                onDblClickRow: function (index, row) {
                    if (row.ChildNumber > 0)
                        fn_level(row.ClassId);
                    else
                        fn_button_click($("#rtBasicCom_TB_Edit"));
                }
            });
            var paging = self.grid.datagrid("getPager");
            paging.pagination({
                showRefresh: false
            });
        };

        var fn_init = function(userId_g, menuId) {
            self = this;

            self.reload = 0;
            fn_init_grid();

            self.printer = CPSSLib.Print.printer.init({
                pageName: "分公司资料",
                menuId: menuId,//暂时不使用，主要用户自定义打印样式
                billHead: [
                    { title: "编号", type: "text", id: "txtSerialNumber", labelWidth: 70 },
                    { title: "名称", type: "text", id: "txtName", labelWidth: 70 },
                    { title: "拼音", type: "text", id: "txtSpelling", labelWidth: 70 },
                    { title: "售价方式", type: "combobox", id: "ddlPriceMode", labelWidth: 70 },
                    { title: "Email", type: "text", id: "txtEmail", labelWidth: 70 },
                    { title: "联系人", type: "text", id: "txtLinkMan", labelWidth: 70 },
                    { title: "联系电话", type: "text", id: "txtLinkTel", labelWidth: 70 },
                    { title: "状态", type: "combobox", id: "ddlStatus", labelWidth: 70 }
                ],
                billGrid: self.grid
            });
            $("#billbutton a")
                .on("click",
                    function() {
                        fn_button_click(this, userId_g);
                    });

            //查询数据会重新绑定datagrid的数据
            $("#btnSearch")
                .on("click",
                    function() {
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
        };

        return {
            init: fn_init,
            cellFormatter: fn_cellFormatter,
            level: fn_level
        };
    })();
})("Page");