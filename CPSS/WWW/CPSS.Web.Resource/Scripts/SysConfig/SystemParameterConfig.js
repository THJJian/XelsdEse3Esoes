;
(function(ns) {
    CPSSLib.RegNameSpace(ns).systemParameterConfig = (function() {
        var fn_init = function() {
            $("#UseSameTaxRate,#UseSaleTrace,#UseBuyTrace,#UseRecentBuyPrice2CostPrice,#AutoGenerateBillNumber,#PrintAddCount,#NotModifyBillDate,#KeepOrignalInputMan,#RetailUsePos,#RetailCanMakeBalance,#ProductRepeatAlert,#CheckeSNIsSell,#FilterZeroProduct,#LessThanStorageQtyAlert,#NotCheckSNForSave,#QuickInputDisplayWithRows")
                .switchbutton({
                    checked: true,
                    onChange: function(checked) {
                        console.log(checked);
                    }
                });

            $("#basic_set_panel,#price_set_panel,#bill_set_panel,#other_set_panel")
                .panel({
                    tools: [
                        {
                            iconCls: "icon-save",
                            handler: function () {
                                //debugger;
                                var panel_id = $(this).parents("div.panel-header").next().attr("id").toLowerCase();
                                switch (panel_id) {
                                    case "basic_set_panel":
                                        //TODO 保存相应更改
                                        var data = {
                                            ParameterConfigList: [
                                                { ParameterConfigName: "QtyScale", ParameterConfigValue: "" },
                                                { ParameterConfigName: "UnitPriceScale", ParameterConfigValue: "" },
                                                { ParameterConfigName: "TotalScale", ParameterConfigValue: "" },
                                                { ParameterConfigName: "DiscountRateScale", ParameterConfigValue: "" },
                                                { ParameterConfigName: "TaxRate", ParameterConfigValue: "" },
                                                { ParameterConfigName: "UseSameTaxRate", ParameterConfigValue: "" }
                                            ]
                                        };
                                        _msgbox.alert(panel_id);
                                        break;
                                    case "price_set_panel":
                                        //TODO 保存相应更改
                                        _msgbox.alert(panel_id);
                                        break;
                                    case "bill_set_panel":
                                        //TODO 保存相应更改
                                        _msgbox.alert(panel_id);
                                        break;
                                    case "other_set_panel":
                                        //TODO 保存相应更改
                                        _msgbox.alert(panel_id);
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