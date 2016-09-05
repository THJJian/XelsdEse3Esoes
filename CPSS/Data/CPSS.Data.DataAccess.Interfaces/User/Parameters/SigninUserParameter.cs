namespace CPSS.Data.DataAccess.Interfaces.User.Parameters
{
    public class SigninUserParameter
    {
        /// <summary>
        /// 登录账户
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 账户密码
        /// </summary>
        public string UserPwd { set; get; }

        /// <summary>
        /// 用户公司编号
        /// </summary>
        public string CompanySerialNum { set; get; }
    }
}