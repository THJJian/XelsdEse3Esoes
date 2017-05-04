namespace CPSS.Common.Core.SysConfig
{
    public class SystemParameterConfigConst
    {
        #region 基本设置

        /// <summary>
        /// 数量保留小数位数(2-5) int
        /// </summary>
        public const string QtyScale = "QtyScale";

        /// <summary>
        /// 单价保留小数位数(2-5) int
        /// </summary>
        public const string UnitPriceScale = "UnitPriceScale";

        /// <summary>
        /// 金额保留小数位数(2-5) int
        /// </summary>
        public const string TotalScale = "TotalScale";

        /// <summary>
        /// 扣率保留小数位数(0-5) int
        /// </summary>
        public const string DiscountRateScale = "DiscountRateScale";

        /// <summary>
        /// 默认税率(%) int
        /// </summary>
        public const string TaxRate = "TaxRate";

        /// <summary>
        /// 统一使用默认税率(%) bool
        /// </summary>
        public const string UseSameTaxRate = "UseSameTaxRate";

        #endregion

        #region 价格设置

        /// <summary>
        /// 启用售价跟踪 bool
        /// </summary>
        public const string UseSaleTrace = "UseSaleTrace";

        /// <summary>
        /// 启用进价跟踪 bool
        /// </summary>
        public const string UseBuyTrace = "UseBuyTrace";

        /// <summary>
        /// 价格跟踪方式 int
        /// </summary>
        public const string SaleBuyPriceTrace = "SaleBuyPriceTrace";

        #endregion

        #region 单据设置

        /// <summary>
        /// 使用最近进价作为成本价 bool
        /// </summary>
        public const string UseRecentBuyPrice2CostPrice = "UseRecentBuyPrice2CostPrice";

        /// <summary>
        /// 自动生成单据编号 bool
        /// </summary>
        public const string AutoGenerateBillNumber = "AutoGenerateBillNumber";

        /// <summary>
        /// 单据打印占单据编号 bool
        /// </summary>
        public const string PrintAddCount = "PrintAddCount";

        /// <summary>
        /// 不允许修改单据日期 bool
        /// </summary>
        public const string NotModifyBillDate = "NotModifyBillDate";

        /// <summary>
        /// 序列号打印，每行打印 int
        /// </summary>
        public const string SNPrintStyle = "SNPrintStyle";

        /// <summary>
        /// 保留原始制单人 bool
        /// </summary>
        public const string KeepOrignalInputMan = "KeepOrignalInputMan";

        /// <summary>
        /// 零售单启用POS bool
        /// </summary>
        public const string RetailUsePos = "RetailUsePos";

        /// <summary>
        /// 允许零售单欠款 bool
        /// </summary>
        public const string RetailCanMakeBalance = "RetailCanMakeBalance";

        /// <summary>
        /// 开单商品有重复行时提示 bool
        /// </summary>
        public const string ProductRepeatAlert = "ProductRepeatAlert";

        /// <summary>
        /// 保存检查序列号是否可开 bool
        /// </summary>
        public const string CheckSNIsSell = "CheckSNIsSell";

        /// <summary>
        /// 开单过滤库存数量小于等于零的商品 bool
        /// </summary>
        public const string FilterZeroProduct = "FilterZeroProduct";

        /// <summary>
        /// 开单保存检查是否小于可开单数量 bool
        /// </summary>
        public const string LessThanStorageQtyAlert = "LessThanStorageQtyAlert";

        /// <summary>
        /// 在单据保存时不检查强制序列号数量 bool
        /// </summary>
        public const string NotCheckSNForSave = "NotCheckSNForSave";

        /// <summary>
        /// 出入库类单据商品重复时自动合并商品行，该商品数量+1 bool
        /// </summary>
        public const string QuickInputDisplayWithRows = "QuickInputDisplayWithRows";

        #endregion

        #region 其它设置

        /// <summary>
        /// 基础数据选择和报表每页显示行数(10-50) int
        /// </summary>
        public const string DataSelectPageSize = "DataSelectPageSize";

        #endregion

    }
}