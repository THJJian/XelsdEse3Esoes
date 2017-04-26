using System;

namespace CPSS.Service.ViewService.ViewModels.Common.Respond
{
    [Serializable]
    public class RespondGetAllDepartmentViewModel
    {
        public int DepId { set; get; }

        public string SerialNumber { set; get; }

        public string Name { set; get; }

        public string Spelling { set; get; }

        public string Comment { set; get; }
    }
}