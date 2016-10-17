using System;
using System.Collections.Generic;
using System.Data;
using CPSS.Common.Core.Paging.Interface;

namespace CPSS.Common.Core.Paging
{
    public class PageData<T> : IPageData
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PageData()
        {
            Datas = new List<T>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="datas"></param>
        public PageData(IList<T> datas)
        {
            Datas = datas;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="dataCount"></param>
        public PageData(IList<T> datas, int dataCount)
        {
            Datas = datas;
            DataCount = dataCount;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="count"></param>
        public PageData(DataTable dt, int count = 0)
        {
            try
            {
                var result = new List<T>();
                DataCount = count;
                if (dt != null)
                {
                    for (var j = 0; j < dt.Rows.Count; j++)
                    {
                        var t = (T)Activator.CreateInstance(typeof(T));
                        var properties = t.GetType().GetProperties();
                        foreach (var property in properties)
                        {
                            for (var i = 0; i < dt.Columns.Count; i++)
                            {
                                // 属性与字段名称一致的进行赋值   
                                if (!property.Name.ToLower().Equals(dt.Columns[i].ColumnName.ToLower())) continue;
                                if (dt.Rows[j][i] == DBNull.Value)
                                    switch (property.PropertyType.ToString())
                                    {
                                        case "System.Int32":
                                            property.SetValue(t, 0, null);
                                            break;
                                        case "System.DateTime":
                                            property.SetValue(t, DateTime.Now, null);
                                            break;
                                        case "System.String":
                                            property.SetValue(t, string.Empty, null);
                                            break;
                                        case "System.Boolean":
                                            property.SetValue(t, false, null);
                                            break;
                                        case "System.Decimal":
                                            property.SetValue(t, 0, null);
                                            break;
                                        case "System.Int64":
                                            property.SetValue(t, 0, null);
                                            break;
                                    }
                                else
                                    switch (property.PropertyType.ToString())
                                    {
                                        case "System.Int32":
                                            property.SetValue(t, int.Parse(dt.Rows[j][i].ToString()), null);
                                            break;
                                        case "System.DateTime":
                                            property.SetValue(t, Convert.ToDateTime(dt.Rows[j][i].ToString()), null);
                                            break;
                                        case "System.String":
                                            property.SetValue(t, dt.Rows[j][i].ToString(), null);
                                            break;
                                        case "System.Boolean":
                                            property.SetValue(t, Convert.ToBoolean(dt.Rows[j][i].ToString()), null);
                                            break;
                                        case "System.Decimal":
                                            property.SetValue(t, Convert.ToDecimal(dt.Rows[j][i].ToString()), null);
                                            break;
                                        case "System.Int64":
                                            property.SetValue(t, int.Parse(dt.Rows[j][i].ToString()), null);
                                            break;
                                    }
                                break;
                            }
                        }
                        result.Add(t);
                    }
                }
                Datas = result;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        /// <summary>
        /// 数据列表
        /// </summary>
        public IList<T> Datas { get; set; }

        /// <summary>
        /// 总数据量
        /// </summary>
        public int DataCount { get; set; }

        /// <summary>
        /// 命令名称
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}