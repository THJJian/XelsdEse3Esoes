using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CPSS.Common.Core.DataAccess.DataAccess;
using CPSS.Data.DataAccess.Interfaces.HeadButtons;
using CPSS.Data.DataAccess.Interfaces.HeadButtons.Parameters;
using CPSS.Data.DataAcess.DataModels.HeadButtons;

namespace CPSS.Data.DataAccess.HeadButtons
{
    public class HeadButtonsDataAccess : GenericDataAccessBase<HeadButtonsDataModel>, IHeadButtonsDataAccess
    {
        public HeadButtonsDataAccess(IDbConnection _connection) : base(_connection)
        {
        }
        
        public IList<HeadButtonsDataModel> QueryHeadButtonsViewModelsByMenuID(HeadButtonsParameter parameter)
        {
            this.ExecuteSQL = "SELECT title ButtonText,buttonid ButtonName, isenabled ButtonDisabled, iconcls ButtonIconCls FROM dbo.menu WHERE parentid IN(SELECT classid FROM dbo.menu WHERE menuid = @menuid) ORDER BY classid ASC";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@menuid", parameter.MenuID)
            };
            return this.ExecuteReadSqlToHeadButtonsDataModelList();
        }
    }
}