using System.IO;
using System.Web;

namespace CPSS.Common.Core.Helper.BuildFilePath
{
    public class ExistsFileHelper
    {
        /// <summary>
        /// 确定指定文件是否存在。
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool ExistsFile(string filePath)
        {
            var _filePath = HttpContext.Current.Server.MapPath(filePath);
            return File.Exists(_filePath);
        }
    }
}