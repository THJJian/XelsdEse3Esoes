using System;
using System.ComponentModel.DataAnnotations;

namespace CPSS.Service.ViewService.ViewModels.User.Request
{
    [Serializable]
    public class RequestSigninUserViewModel
    {
        [Required(ErrorMessage = "请输入用户名")]
        [RegularExpression("(\\w+):(\\w+)", ErrorMessage = "用户名格式错误，必须为公司编号:用户名。如->ABC公司:张三")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "请输入用户密码")]
        public string UserPwd { set; get; }

        [Required(ErrorMessage = "请输入验证码")]
        public string ValidCode { set; get; }

        public Guid UserID_g { get; set; }

        public int UserID { get; set; }
    }
}