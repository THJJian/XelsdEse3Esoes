namespace CPSS.Common.Core.Type
{
    /// <summary>
    /// 单据类型
    /// </summary>
    public enum InvoiceType
    {
        /// <summary>
        /// 销售出库单
        /// </summary>
        Sale = 10,

        /// <summary>
        /// 销售退货单
        /// </summary>
        SaleReturned = 11,

        /// <summary>
        /// 采购入库单
        /// </summary>
        Buy = 20,

        /// <summary>
        /// 采购入库退货单
        /// </summary>
        BuyReturned = 21,

        /// <summary>
        /// 入库单(库存)
        /// </summary>
        StockIn = 30,

        /// <summary>
        /// 出库单(库存)
        /// </summary>
        StockOut = 31,
    }
}