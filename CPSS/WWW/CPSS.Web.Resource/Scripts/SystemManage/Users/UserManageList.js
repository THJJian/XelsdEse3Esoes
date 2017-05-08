;
(function(ns) {
    CPSSLib.RegNameSpace(ns).userManageList = (function() {
        var self;

        function fn_level() {
            self.grid.datagrid("load");
        }

        var fn_check_select_row = function (selectRow) {
            if (selectRow === null || !selectRow) return false;
            return true;
        };

        var fn_postData = function (flag) {
            var msg = "请选择需要删除的用户资料",
                url = "/basic/deleteunit",
                selectRow = self.grid.datagrid("getSelected");

            if (!fn_check_select_row(selectRow)) {
                if (flag !== 0) msg = "请选择需要恢复删除的用户资料";
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
                    UserId: selectRow.UserId
                }
            };
            CPSSLib.Utils.AjaxRequest.postData(url,
                data,
                function (result) {
                    self.isDeletedButtonClick = false;
                    if (result.ErrorCode === 0) {
                        self.reload = 1;
                        _msgbox.success(result.ErrorMessage,
                            function () {
                                fn_level();
                            });
                    }
                });
        };

        var fn_button_click = function (sender, userId_g) {
            var id = $(sender).attr("id");
            var selectRow, url;
            switch (id) {
                case "rtBasicManage_TB_Add":
                    var parentId = $("#txtParentId").val();
                    url = ["/systemmanage/adduser?userid=", userId_g].join("");
                    _window.open("add_user", "新增用户资料", 400, 300, url);
                    break;
                case "rtBasicManage_TB_Edit":
                    selectRow = self.grid.datagrid("getSelected");
                    if (!fn_check_select_row(selectRow)) {
                        _msgbox.alert("请选择需要修改的用户资料");
                        return;
                    }
                    if (selectRow.Deleted === 2) {
                        _msgbox.alert("删除状态的用户资料不允许修改");
                        return;
                    }
                    url = ["/systemmanage/edituser?userid=", userId_g, "&uid=", selectRow.UserId].join("");
                    _window.open("edit_user", "修改用户资料", 400, 300, url);
                    break;
                case "rtBasicManage_TB_Delete":
                    fn_postData(0);
                    break;
                case "rtBasicManage_TB_Resume":
                    fn_postData(1);
                    break;
                case "rtBasicManage_TB_Preview":
                    self.printer.preview();
                    break;
                case "rtBasicManage_TB_Design":
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
                url: "/systemmanage/getuserlist",
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
                    fn_button_click($("#rtBasicManage_TB_Edit"));
                }
            });
            var paging = self.grid.datagrid("getPager");
            paging.pagination({
                showRefresh: false
            });
        };

        var fn_init = function (userId_g, menuId) {
            self = this;

            fn_init_grid();

            self.printer = CPSSLib.Print.printer.init({
                pageName: "用户资料",
                menuId: menuId,//暂时不使用，主要用户自定义打印样式
                billHead: [
                    { title: "职员信息", type: "combogrid", id: "ddlEmployee", labelWidth: 70 },
                    { title: "用户名", type: "text", id: "txtUserName", labelWidth: 70 }
                ],
                billGrid: self.grid
            });
            $("#billbutton a")
                .on("click",
                    function () {
                        fn_button_click(this, userId_g);
                    });

            //查询数据会重新绑定datagrid的数据
            $("#btnSearch")
                .on("click",
                    function () {
                        self.grid.datagrid("load",
                        {
                            data: {
                                UserName: textBoxHelper.getValue("txtUserName"),
                                EmpId: combogridHelper.getValue("ddlEmployee")
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
})("Sys");