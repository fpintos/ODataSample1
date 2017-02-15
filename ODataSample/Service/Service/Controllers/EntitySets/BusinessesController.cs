// ---------------------------------------------------------------------------
// <copyright file="BusinessesController.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

namespace ODataSample.Service.Controllers.EntitySets
{
    using System.Linq;
    using System.Web.OData.Routing;
    using ODataSample.Service.Models;
    using System.Web.OData;

    [ODataRoutePrefix("businesses")]
    public class BusinessesController : ODataController
    {
        [ODataRoute]
        public IQueryable<Business> Get() =>
            Enumerable.Range(0, 5).Select(i =>
                new Business
                {
                    Id = i.ToString(),
                }).AsQueryable();
    }
}