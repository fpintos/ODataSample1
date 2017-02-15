// ---------------------------------------------------------------------------
// <copyright file="AppointmentController.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

namespace ODataSample.Service.Controllers.EntityReferences
{
    using System.Web.Http;
    using System.Web.OData;
    using System.Web.OData.Routing;
    using ODataSample.Service.Models;

    [ODataRoutePrefix("businesses({BusinessId})/appointments({AppointmentId})")]
    public class AppointmentController : ODataController
    {
        public string AppointmentId => this.GetUrlParameter();

        [EnableQuery]
        [ODataRoute]
        public SingleResult<Appointment> Get()
        {
            var appointment = new Appointment { Id = this.AppointmentId };
            return appointment.AsSingleResult();
        }
    }
}