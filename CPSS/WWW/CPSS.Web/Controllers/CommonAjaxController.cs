using System.Web.Mvc;
using CPSS.Common.Core;
using CPSS.Common.Core.Helper.Extension;
using CPSS.Common.Core.Mvc;
using CPSS.Common.Core.Mvc.Filters;
using CPSS.Service.ViewService.ViewModels.Common.Request;
using CPSS.Service.ViewService.ViewModels.Common.Respond;

namespace CPSS.Web.Controllers
{
    [HaveToLogin]
    public class CommonAjaxController : WebBaseController
    {
        #region 获取汉字首字母

        private string getSpelling(string cnChar)
        {
            byte[] arrCN = cnChar.ToBytes();
            if (arrCN.Length <= 1) return cnChar;
            int area = arrCN[0];
            int pos = arrCN[1];
            int code = (area << 8) + pos;
            int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
            for (int i = 0; i < 26; i++)
            {
                int max = 55290;
                if (i != 25) max = areacode[i + 1];
                if (areacode[i] <= code && code < max)
                {
                    return new byte[] { (byte)(65 + i) }.ToUTF8String();
                }
            }
            return "*";
        }

        private string GetChineseSpelling(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += getSpelling(strText.Substring(i, 1));
            }
            return myStr;
        }

        #endregion

        /// <summary>
        /// 获取汉字拼音首字母
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetSpellingByChineseChar(RequestWebViewData<RequestGetNameSpellingViewModel> request)
        {
            var respond = new RespondWebViewData<RespondGetNameSpellingViewModel>
            {
                rows = new RespondGetNameSpellingViewModel
                {
                    Spelling = this.GetChineseSpelling(request.data.ChineseChar)
                }
            };
            return Json(respond);
        }
    }
}