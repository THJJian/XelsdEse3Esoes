;
(function(ns) {
    CPSSLib.RegNameSpace(ns).unitList = (function () {
        var self;

        function fn_level() {
            self.grid.datagrid("load");
        }

        var fn_check_select_row = function(selectRow) {
            if (selectRow === null || !selectRow) return false;
            return true;
        };

        var fn_postData = function(flag) {
            var msg = "请选择需要删除的计量单位",
                url = "/basic/deleteunit",
                selectRow = self.grid.datagrid("getSelected");

            if (!fn_check_select_row(selectRow)) {
                if (flag !== 0) msg = "请选择需要恢复删除的计量单位";
                _msgbox.alert(msg);
                return;
            }

            if (flag === 1) {
                if (selectRow.Deleted === 0) {
                    _msgbox.error("该节点未被删除，不需要恢复删除。");
                    return;
                }
                url = ["/basic/redeleteunit"].join("");
            }

            if (self.isDeletedButtonClick) return;
            self.isDeletedButtonClick = true;

            var data = {
                data: {
                    UnitId: selectRow.UnitId
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
            case "rtBasicUnit_TB_Add":
                var parentId = $("#txtParentId").val();
                url = ["/basic/addunit?userid=", userId_g].join("");
                _window.open("add_unit", "新增计量单位资料", 400, 300, url);
                break;
            case "rtBasicUnit_TB_Edit":
                selectRow = self.grid.datagrid("getSelected");
                if (!fn_check_select_row(selectRow)) {
                    _msgbox.alert("请选择需要修改的计量单位");
                    return;
                }
                if (selectRow.Deleted === 2) {
                    _msgbox.alert("删除状态的计量单位不允许修改");
                    return;
                }
                url = ["/basic/editunit?userid=", userId_g, "&unitid=", selectRow.UnitId].join("");
                _window.open("edit_unit", "修改计量单位资料", 400, 300, url);
                break;
            case "rtBasicUnit_TB_Delete":
                fn_postData(0);
                break;
            case "rtBasicUnit_TB_Resume":
                fn_postData(1);
                break;
            case "rtBasicUnit_TB_Preview":
                self.printer.preview();
                break;
            case "rtBasicUnit_TB_Design":
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
                url: "/basic/getunitlist",
                method: "post",
                pageSize: 10,
                pageList: [10, 20, 30, 50],
                queryParams: {
                    data: {
                    }
                },
                rowStyler: function (index, row) {
                    if (row.Deleted === 2) return "color: #FF0000;";
                    return "";
                },
                onDblClickRow: function (index, row) {
                    fn_button_click($("#rtBasicUnit_TB_Edit"));
                }
            });
            var paging = self.grid.datagrid("getPager");
            paging.pagination({
                showRefresh: false
            });
        };

        var fn_init = function(userId_g, menuId) {
            self = this;

            fn_init_grid();

            self.printer = CPSSLib.Print.printer.init({
                pageName: "计量单位资料",
                menuId: menuId,//暂时不使用，主要用户自定义打印样式
                billHead: [
                    { title: "名称", type: "text", id: "txtName", labelWidth: 70 },
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
                                Name: textBoxHelper.getValue("txtName"),
                                Status: comboboxHelper.getValue("ddlStatus")
                            }
                        });
                    });
            return self;
        };

        return {
            init: fn_init,
            level: fn_level
        };
    })();
})("Page");