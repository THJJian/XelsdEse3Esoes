namespace CPSS.Common.Core.Exception
{
    public class WebViewErrorCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        public static ErrorCodeItem Success = new ErrorCodeItem(0, "成功");

        /// <summary>
        /// 系统发生异常
        /// </summary>
        public static ErrorCodeItem Exception = new ErrorCodeItem(1, "系统发生异常");

        /// <summary>
        /// 用户信息不存在
        /// </summary>
        public static ErrorCodeItem NotExistUserInfo = new ErrorCodeItem(2, "用户信息不存在");

        /// <summary>
        /// 您未登录或已登录超时,请重新登录
        /// </summary>
        public static ErrorCodeItem LoginRequired = new ErrorCodeItem(3, "您未登录或已登录超时,请重新登录");

        /// <summary>
        /// 登录失败
        /// </summary>
        public static ErrorCodeItem SigninFailed = new ErrorCodeItem(4, "登录失败");

        /// <summary>
        /// 用户名格式错误，必须为公司编号:用户名。如->ABC公司:张三
        /// </summary>
        public static ErrorCodeItem SigninInfoError = new ErrorCodeItem(5, "用户名格式错误，必须为公司编号:用户名。如->ABC公司:张三");

        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        public static ErrorCodeItem UserNameOrPwdError = new ErrorCodeItem(6, "用户名或密码错误");

        /// <summary>
        /// 实体验证出错
        /// </summary>
        public static ErrorCodeItem ModelValidateError = new ErrorCodeItem(7, "实体验证出错");

        /// <summary>
        /// 验证码错误
        /// </summary>
        public static ErrorCodeItem VerifyCodeError = new ErrorCodeItem(8, "验证码错误");

        /// <summary>
        /// 数据不存在
        /// </summary>
        public static ErrorCodeItem NotExistsDataInfo = new ErrorCodeItem(9, "数据不存在");

        /// <summary>
        /// 数据已经存在
        /// </summary>
        public static ErrorCodeItem ExistsDataInfo = new ErrorCodeItem(10, "数据已经存在");
    }
}