using System.Linq;
using System.Web.Mvc;

namespace CPSS.Common.Helper.Extension
{
    public static class ControllerExtension
    {
        /// <summary>
        /// 数据模型如果验证不能通过，构造并返回所有错误信息
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static string GenerateModelValidateErrMessage(this Controller controller, ModelStateDictionary modelState, out bool isValid)
        {
            isValid = modelState.IsValid;
            var errorMessages = string.Empty;
            if (isValid) return errorMessages;
            foreach (var error in modelState.Values.SelectMany(value => value.Errors))
            {
                if (string.IsNullOrEmpty(errorMessages)) errorMessages = error.ErrorMessage;
                else errorMessages += string.Concat(error.ErrorMessage, "<br/>");
            }
            return errorMessages;
        }
    }
}