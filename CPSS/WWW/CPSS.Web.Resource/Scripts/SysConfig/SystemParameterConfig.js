;
(function(ns) {
    CPSSLib.RegNameSpace(ns).systemParameterConfig = (function () {
        var self;

        var fn_postData = function (data, url, callBack) {
            if (typeof callBack !== "function") callBack = fn_call_back;
            CPSSLib.Utils.AjaxRequest.postData(url,
                { data: data },
                function (result) {
                    callBack(result);
                });
        }

        var fn_switch_change = function (ctrlId) {
            try {
                $("#" + ctrlId).switchbutton({
                    onChange: function (checked) {
                        self.option[ctrlId] = checked ? 1 : 0;
                    }
                });
            } catch (e) {
            } 
        }

        var fn_init = function (valueOption) {
            self = this;
            self.option = $.extend({}, valueOption);

            var notSwitchbuttons = ["QtyScale", "UnitPriceScale", "TotalScale", "DiscountRateScale", "TaxRate", "SaleBuyPriceTrace", "SNPrintStyle", "DataSelectPageSize"].join(",");
            for (var field in self.option) {
                if(notSwitchbuttons.indexOf(field) >= 0) continue;
                fn_switch_change(field);
            }

            $("#basic_set_panel,#price_set_panel,#bill_set_panel,#other_set_panel")
                .panel({
                    tools: [
                        {
                            iconCls: "icon-save",
                            handler: function () {
                                //debugger;
                                var panel_id = $(this).parents("div.panel-header").next().attr("id").toLowerCase();
                                var data;
                                switch (panel_id) {
                                    case "basic_set_panel":
                                        data = {
                                            ParameterConfigList: [
                                                { ParameterConfigName: "QtyScale", ParameterConfigValue: numberboxHelper.getValue("QtyScale") },
                                                { ParameterConfigName: "UnitPriceScale", ParameterConfigValue: numberboxHelper.getValue("UnitPriceScale") },
                                                { ParameterConfigName: "TotalScale", ParameterConfigValue: numberboxHelper.getValue("TotalScale") },
                                                { ParameterConfigName: "DiscountRateScale", ParameterConfigValue: numberboxHelper.getValue("DiscountRateScale") },
                                                { ParameterConfigName: "TaxRate", ParameterConfigValue: numberboxHelper.getValue("TaxRate") },
                                                { ParameterConfigName: "UseSameTaxRate", ParameterConfigValue: self.option.UseSameTaxRate }
                                            ]
                                        };
                                        fn_postData(data, "/systemparameterconfig/savesystemparameterconfig", function (result) {
                                            if (result.ErrorCode === 0)
                                                _msgbox.alert(result.ErrorMessage);
                                            else
                                                _msgbox.error(result.ErrorMessage);
                                        });
                                        break;
                                    case "price_set_panel":
                                        data = {
                                            ParameterConfigList: [
                                                { ParameterConfigName: "UseSaleTrace", ParameterConfigValue: self.option.UseSaleTrace },
                                                { ParameterConfigName: "UseBuyTrace", ParameterConfigValue: self.option.UseBuyTrace },
                                                { ParameterConfigName: "SaleBuyPriceTrace", ParameterConfigValue: comboboxHelper.getValue("TotalScale") }
                                            ]
                                        };
                                        fn_postData(data, "/systemparameterconfig/savesystemparameterconfig", function (result) {
                                            if (result.ErrorCode === 0)
                                                _msgbox.alert(result.ErrorMessage);
                                            else
                                                _msgbox.error(result.ErrorMessage);
                                        });
                                        break;
                                    case "bill_set_panel":
                                        data = {
                                            ParameterConfigList: [
                                                { ParameterConfigName: "UseRecentBuyPrice2CostPrice", ParameterConfigValue: self.option.UseRecentBuyPrice2CostPrice },
                                                { ParameterConfigName: "AutoGenerateBillNumber", ParameterConfigValue: self.option.AutoGenerateBillNumber },
                                                { ParameterConfigName: "PrintAddCount", ParameterConfigValue: self.option.PrintAddCount },
                                                { ParameterConfigName: "NotModifyBillDate", ParameterConfigValue: self.option.NotModifyBillDate },
                                                { ParameterConfigName: "SNPrintStyle", ParameterConfigValue: numberboxHelper.getValue("SNPrintStyle") },
                                                { ParameterConfigName: "KeepOrignalInputMan", ParameterConfigValue: self.option.KeepOrignalInputMan },
                                                { ParameterConfigName: "RetailUsePos", ParameterConfigValue: self.option.RetailUsePos },
                                                { ParameterConfigName: "RetailCanMakeBalance", ParameterConfigValue: self.option.RetailCanMakeBalance },
                                                { ParameterConfigName: "ProductRepeatAlert", ParameterConfigValue: self.option.ProductRepeatAlert },
                                                { ParameterConfigName: "CheckSNIsSell", ParameterConfigValue: self.option.CheckSNIsSell },
                                                { ParameterConfigName: "FilterZeroProduct", ParameterConfigValue: self.option.FilterZeroProduct },
                                                { ParameterConfigName: "LessThanStorageQtyAlert", ParameterConfigValue: self.option.LessThanStorageQtyAlert },
                                                { ParameterConfigName: "NotCheckSNForSave", ParameterConfigValue: self.option.NotCheckSNForSave },
                                                { ParameterConfigName: "QuickInputDisplayWithRows", ParameterConfigValue: self.option.QuickInputDisplayWithRows }
                                            ]
                                        };
                                        fn_postData(data, "/systemparameterconfig/savesystemparameterconfig", function (result) {
                                            if (result.ErrorCode === 0)
                                                _msgbox.alert(result.ErrorMessage);
                                            else
                                                _msgbox.error(result.ErrorMessage);
                                        });
                                        break;
                                    case "other_set_panel":
                                        data = {
                                            ParameterConfigList: [
                                                { ParameterConfigName: "DataSelectPageSize", ParameterConfigValue: numberboxHelper.getValue("DataSelectPageSize") }
                                            ]
                                        };
                                        fn_postData(data, "/systemparameterconfig/savesystemparameterconfig", function (result) {
                                            if (result.ErrorCode === 0)
                                                _msgbox.alert(result.ErrorMessage);
                                            else
                                                _msgbox.error(result.ErrorMessage);
                                        });
                                        break;
                                }
                            }
                        }
                    ]
                });
            return this;
        }

        return {
            init: fn_init
        };
    })();
})("Sys");