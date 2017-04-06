using System;
using System.Collections.Generic;
using System.Linq;
using CPSS.Common.Core;
using CPSS.Common.Core.Helper.Cached;
using CPSS.Data.DataAccess.Interfaces;
using CPSS.Data.DataAccess.Interfaces.Basic.Parameters;
using CPSS.Service.ViewService.Interfaces.Basic;
using CPSS.Service.ViewService.ViewModels.SubCompany.Request;
using CPSS.Service.ViewService.ViewModels.SubCompany.Respond;

namespace CPSS.Service.ViewService.Basic
{
    public class SubCompanyViewService : ISubCompanyViewService
    {
        private const string PRE_CACHE_KEY = "CPSS.Service.ViewService.Basic.SubCompanyViewService.{0}";
        private readonly IsubcompanyDataAccess mSubCompanyDataAccess;

        public SubCompanyViewService(IsubcompanyDataAccess subCompanyDataAccess)
        {
            this.mSubCompanyDataAccess = subCompanyDataAccess;
        }

        public RespondWebViewData<List<RespondQuerySubCompanyViewModel>> GetQueryCompanyList(RequestWebViewData<RequestQuerySubCompanyViewModel> request)
        {
            if(request.data == null) request.data = new RequestQuerySubCompanyViewModel();
            return MemcacheHelper.Get(() =>
            {
                var parameter = new QuerySubCompanyListParameter
                {
                    Email = request.data.Email,
                    LinkMan = request.data.LinkMan,
                    LinkTel = request.data.LinkTel,
                    Name = request.data.Name,
                    PageIndex = request.page,
                    PageSize = request.rows,
                    ParentId = request.data.ParentId,
                    PriceMode = request.data.PriceMode,
                    SerialNumber = request.data.SerialNumber,
                    Spelling = request.data.Spelling,
                    Status = request.data.Status
                };
                var pageDataList = this.mSubCompanyDataAccess.GetQuerySubCompanyList(parameter);
                var respond = new RespondWebViewData<List<RespondQuerySubCompanyViewModel>>
                {
                    total = pageDataList.DataCount,
                    rows = pageDataList.Datas.Select(item=>new RespondQuerySubCompanyViewModel
                    {
                        ClassId = item.classid,
                        ComId = item.subcomid,
                        Email = item.email,
                        Comment = item.comment,
                        LinkMan = item.linkman,
                        LinkTel = item.linktel,
                        Name = item.name,
                        ParentId = item.parentid,
                        PriceMode = item.pricemode.ToString(),
                        SerialNumber = item.serialnumber,
                        sort = item.sort.ToString(),
                        Status = item.status.ToString(),
                        Spelling = item.pinyin
                    }).ToList()
                };

                return respond;
            }
            , string.Format(PRE_CACHE_KEY, "GetQueryCompanyList")
            , DateTime.Now.AddMinutes(30)
            , request.page
            , request.rows
            , request.data.ParentId
            , request.data.Email
            , request.data.LinkMan
            , request.data.LinkTel
            , request.data.Name
            , request.data.PriceMode
            , request.data.SerialNumber
            , request.data.Spelling
            , request.data.Status);
        }
    }
}