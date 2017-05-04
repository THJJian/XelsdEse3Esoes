using System;

namespace CPSS.Service.ViewService.ViewModels.Common.Respond
{
    [Serializable]
    public class RespondGetAllEmployeeViewModel
    {
        public int EmpId { get; set; }

        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public string Spelling { get; set; }
    }
}