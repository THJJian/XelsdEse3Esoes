using System;
using System.ComponentModel.DataAnnotations;

namespace CPSS.Web.Models
{
    [Serializable]
    public class RequestUserInfo
    {
        /// <summary>
        /// 账户名
        /// </summary>
        [Required(ErrorMessage = "账户名不允许为空", AllowEmptyStrings = false)]
        [RegularExpression(@"\w{5,}@\w{2,}", ErrorMessage = "账户名格式不正确，必须为[公司编号@账户名]。如：xxxxx@zhangsan[xxxxx>=5字，zhangsan>=2字]")]
        public string LoginName { set; get; }

        /// <summary>
        /// 账户密码
        /// </summary>
        [Required(ErrorMessage = "账户密码不允许为空", AllowEmptyStrings = false)]
        public string LoginPwd { set; get; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "验证码不允许为空", AllowEmptyStrings = false)]
        public string ValidCode { get; set; }
    }
}