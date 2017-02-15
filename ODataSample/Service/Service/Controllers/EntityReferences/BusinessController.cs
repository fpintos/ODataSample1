// ---------------------------------------------------------------------------
// <copyright file="BusinessController.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

namespace ODataSample.Service.Controllers.EntityReferences
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.OData;
    using System.Web.OData.Routing;
    using ODataSample.Service.Models;

    [ODataRoutePrefix("businesses({BusinessId})")]
    public class BusinessController : ODataController
    {
        public string BusinessId => this.GetUrlParameter();

        [EnableQuery]
        [ODataRoute]
        public SingleResult<Business> Get()
        {
            var business = new Business { Id = this.BusinessId };
            return business.AsSingleResult();
        }

        [HttpGet]
        [ODataRoute("getAppointmentSubset(start={start},count={count})")]
        public IQueryable<Appointment> GetAppointmentSubset(int start, int count) =>
            Enumerable.Range(start, count).Select(i =>
                new Appointment
                {
                    Id = BusinessId + "." + i.ToString(),
                }).AsQueryable();
    }
}