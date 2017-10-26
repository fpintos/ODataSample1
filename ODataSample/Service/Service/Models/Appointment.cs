// ---------------------------------------------------------------------------
// <copyright file="Appointment.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

namespace ODataSample.Service.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.OData.Query;

    [Filter]
    public class Appointment
    {
        [Key]
        public string Id
        {
            get;
            set;
        }

        public string Subject
        {
            get;
            set;
        }

        [NotFilterable]
        public string DontFilterOnThis
        {
            get;
            set;
        }
    }
}