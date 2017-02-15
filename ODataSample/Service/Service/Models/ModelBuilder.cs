// ---------------------------------------------------------------------------
// <copyright file="ModelBuilder.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

namespace ODataSample.Service.Models
{
    using System.Web.OData.Builder;

    public class ModelBuilder : ODataConventionModelBuilder
    {
        public ModelBuilder()
        {
            this.EnableLowerCamelCase();

            var appointmentType = this.EntityType<Appointment>();
            var businessType = this.EntityType<Business>();

            var businessAppointmentsPath = new[] { "bindingParameter", "appointments" };
            var function = businessType.Function("getAppointmentSubset");
            function.ReturnsCollectionViaEntitySetPath<Appointment>(businessAppointmentsPath);
            function.OptionalReturn = false;
            function.Parameter<int>("start").OptionalParameter = false;
            function.Parameter<int>("count").OptionalParameter = false;

            this.EntitySet<Business>("businesses");
        }
    }
}