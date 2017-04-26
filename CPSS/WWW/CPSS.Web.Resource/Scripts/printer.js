;
(function(ns) {
    CPSSLib.RegNameSpace(ns).printer = (function () {

        var self;
        var fn_init = function(settings) {
            self = this;
            self.lodop = getLodop();
            self.lodop.SET_PRINT_STYLE("Alignment", 3);
            self.lodop.SET_PRINT_STYLE("FontColor", "#0000FF");

            var defaultSettings = {
                pageName: "",//初始化打印控件的名称
                menuId: 0,//当前菜单的ID。如：分公司基础资料的导航菜单ID = 630
                headTop: 10, //表头离纸上边距离
                headLeft: 5, //表头离纸左边距离
                headRowCount: 4, //表头每行显示4个字段(含字段的标签名)
                headRowHeight: 20,//表头每行高度
                gridTop: 80, //表格离纸上边距离
                gridLeft: 5, //表格离纸左边距离
                gridRowHeight: 20,//行高
                printPageWidth: 800, //打印纸的宽度
                billHead: [],//每个字段的格式如：{ title: "编号", type: "text", id: "txtSerialNumber", labelWidth: 80 },{ title: "售价方式", type: "combobox", id: "ddlPriceMode", labelWidth: 80 }
                billGrid: null
            };
            self.finalSettings = $.extend({}, defaultSettings, settings);//初始传入和默认配置合并后的最终配置
            if (self.finalSettings.pageName === "" || self.finalSettings.pageName === null) throw "打印页名称不能为空";
            self.lodop.PRINT_INIT(self.finalSettings.pageName);

            return this;
        }

        //从服务器down已经设计好的打印样式
        var fn_getPrintDesignStyle = function () {
            //TODO 未实现自定义打印样式
            return;
            //var data = {
            //    data: {
            //        menuId : self.finalSettings.menuId
            //    }
            //};
            //CPSSLib.Utils.AjaxRequest.postData("/commonajax/getprintdesignstyle",
            //    data,
            //    function (result) {
            //        if (result.ErrorCode === 0) {
            //        }
            //    });
        }

        var fn_getHeadItemValueById = function (headItem) {
            var value = "";
            switch (headItem.type) {
                case "text":
                    value = textBoxHelper.getValue(headItem.id);
                    break;
                case "combobox":
                    value = comboboxHelper.getText(headItem.id);
                    break;
                case "combogrid":
                    value = combogridHelper.getText(headItem.id);
                    break;
            }
            return value;
        }

        var fn_createHeadPrintStyle = function() {
            var hLeft = self.finalSettings.headLeft,
                hTop = self.finalSettings.headTop,
                avgWidth = parseInt((self.finalSettings.printPageWidth - self.finalSettings.headLeft * 2) / self.finalSettings.headRowCount),
                headItemIndex = 0;
            $.each(self.finalSettings.billHead, function (index, item) {
                var value = fn_getHeadItemValueById(item);

                if (index != 0 && index % self.finalSettings.headRowCount === 0) {//换行的时候需要重新计算top
                    hTop += self.finalSettings.headRowHeight + self.finalSettings.headTop;
                    hLeft = self.finalSettings.headLeft;
                    headItemIndex = 0;
                }
                //self.lodop.ADD_PRINT_TEXT(top, left, width, height, value);
                self.lodop.ADD_PRINT_TEXT(hTop, hLeft, item.labelWidth, self.finalSettings.headRowHeight, item.title);
                hLeft = hLeft + item.labelWidth;
                var valWidth = (avgWidth * (headItemIndex + 1)) - hLeft;
                self.lodop.ADD_PRINT_TEXT(hTop, hLeft, valWidth, self.finalSettings.headRowHeight, value);
                hLeft = avgWidth * (++headItemIndex);
            });
        }

        var fn_createGridPrintStyle = function() {
            if (self.finalSettings.billGrid === null) return;
            var gridCells = [];
            $.each(self.finalSettings.billGrid.datagrid("options").columns[0], function (index, item) {
                var className = $("#dvgDataGrid").find("th[field='" + item.field + "']").attr("class");
                if (typeof className !== "undefined" && className.toLowerCase().indexOf("notprint") >= 0) return;
                if (!item.hidden && item.title) gridCells.push({ title: item.title, field: ["$", item.title, "$", item.field, "$"].join("") });
            });
            var pageWidth = self.finalSettings.printPageWidth,
                top = self.finalSettings.gridTop,
                left = self.finalSettings.headLeft,
                avgWidth = parseInt((pageWidth - left) / gridCells.length);

            $.each(gridCells, function (index, item) {
                self.lodop.ADD_PRINT_TEXT(top, left, avgWidth, self.finalSettings.gridRowHeight, item.title);
                top += self.finalSettings.gridRowHeight;
                self.lodop.ADD_PRINT_TEXT(top, left, avgWidth, self.finalSettings.gridRowHeight, item.field);
                top -= self.finalSettings.gridRowHeight;
                left += avgWidth;
            });
        }

        var fn_createDesignPrintStyle = function () {
            fn_createHeadPrintStyle();
            fn_createGridPrintStyle();
        }
        
        //使用默认样式
        var fn_createGridPrintData = function () {
            var gridData = self.finalSettings.billGrid.datagrid("getData").rows;
            if (self.finalSettings.billGrid === null) return;
            var gridCells = [];
            $.each(self.finalSettings.billGrid.datagrid("options").columns[0], function (index, item) {
                var className = $("#dvgDataGrid").find("th[field='" + item.field + "']").attr("class");
                if (typeof className !== "undefined" && className.toLowerCase().indexOf("notprint") >= 0) return;
                if (!item.hidden && item.title) gridCells.push({ title: item.title, field: item.field });
            });
            var pageWidth = self.finalSettings.printPageWidth,
                top = self.finalSettings.gridTop,
                left = self.finalSettings.headLeft,
                avgWidth = parseInt((pageWidth - left) / gridCells.length);

            $.each(gridCells, function (index, item) {
                self.lodop.ADD_PRINT_TEXT(top, left, avgWidth, self.finalSettings.gridRowHeight, item.title);
                $.each(gridData, function (rowIndex, row) {
                    top += self.finalSettings.gridRowHeight;
                    self.lodop.ADD_PRINT_TEXT(top, left, avgWidth, self.finalSettings.gridRowHeight, row[item.field]);
                });
                top = self.finalSettings.gridTop;
                left += avgWidth;
            });
        }

        //self.lodop.ADD_PRINT_TEXT(top, left, width, height, value);
        var fn_createPrintData = function () {
            fn_createHeadPrintStyle();
            fn_createGridPrintData();
        }

        //打印预览
        var fn_preview = function () {
            fn_createPrintData();
            self.lodop.PREVIEW();
        }

        //样式设计
        var fn_design = function() {
            fn_createDesignPrintStyle();
            self.lodop.On_Return = function (taskId, value) {
                //TODO 自定义的样式需要保存到数据库。系统默认的样式不允许修改
                var val = value.replace(/LODOP/g, "self.lodop");
                //_msgbox.alert(val);
            }
            self.lodop.PRINT_DESIGN();
        }

        //直接打印
        var fn_print = function() {
            fn_createPrintData();
            self.lodop.PRINT();
        }

        /*
        不需要打印的列添加样式名"notprint",不区分大小写。
        本身隐藏的列不会打印，固不需要加样式名"notprint"。
        */
        return {
            init: fn_init,
            design: fn_design,//设计
            preview: fn_preview,//预览
            print: fn_print//直接打印
        }
    })();
})("Print");
