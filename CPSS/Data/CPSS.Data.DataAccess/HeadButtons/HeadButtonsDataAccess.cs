using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CPSS.Common.Core.DataAccess;
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
            this.ExecuteSQL = "SELECT Title ButtonText,ButtonID ButtonName, IsEnabled ButtonDisabled, IconCls ButtonIconCls FROM dbo.sys_menus WHERE ParentClassID IN(SELECT ClassID FROM dbo.sys_menus WHERE menuid = @menuid)";
            this.DataParameter = new IDbDataParameter[]
            {
                new SqlParameter("@menuid", parameter.MenuID)
            };
            return this.ExecuteReadSqlToHeadButtonsDataModelList();
        }
    }
}