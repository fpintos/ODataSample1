// ---------------------------------------------------------------------------
// <copyright file="BusinessAppointmentsController.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

namespace ODataSample.Service.Controllers.EntitySets
{
    using System.Linq;
    using System.Web.OData.Routing;
    using ODataSample.Service.Models;
    using System.Web.OData;
    using System.Web.OData.Query;

    [ODataRoutePrefix("businesses({BusinessId})/appointments")]
    public class BusinessAppointmentsController : ODataController
    {
        public string BusinessId => this.GetUrlParameter();

        [ODataRoute]
        public IQueryable<Appointment> Get(ODataQueryOptions<Appointment> options)
        {
            new EnableQueryAttribute().ValidateQuery(this.ActionContext.Request, options);
            return Enumerable.Range(0, 5)
                .Select(
                    i =>
                        new Appointment
                        {
                            Id = this.BusinessId + "." + i.ToString(),
                            Subject = "Subject for " + this.BusinessId + "." + i.ToString(),
                            DontFilterOnThis = options.Filter?.RawValue
                        })
                .AsQueryable();
        }
    }
}