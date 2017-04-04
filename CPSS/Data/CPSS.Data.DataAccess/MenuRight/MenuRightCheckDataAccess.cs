using System.Data;
using System.Data.SqlClient;
using CPSS.Common.Core.DataAccess.DataAccess;
using CPSS.Data.DataAccess.Interfaces.MenuRight;
using CPSS.Data.DataAccess.Interfaces.MenuRight.Parameters;
using CPSS.Data.DataAcess.DataModels.MenuRight;

namespace CPSS.Data.DataAccess.MenuRight
{
    public class MenuRightCheckDataAccess : GenericDataAccessBase<MenuRightCheckDataModel>, IMenuRightCheckDataAccess
    {
        public MenuRightCheckDataAccess(IDbConnection _connection) : base(_connection)
        {
        }

        public MenuRightCheckDataModel CheckMenuRightByMenuID(MenuRightCheckParameter parameter)
        {
            this.ExecuteSQL = "SELECT CASE WHEN COUNT(1)>0 THEN TRUE ELSE FALSE END HaveRight FROM userright WHERE menuid=@MenuID AND userid=@UserID";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@UserID", parameter.UserID), 
                new SqlParameter("@MenuID", parameter.MenuID) 
            };
            return this.ExecuteReadSqlToMenuRightCheckDataModel();
        }
    }
}