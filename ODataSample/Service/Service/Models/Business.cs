// ---------------------------------------------------------------------------
// <copyright file="Business.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

namespace ODataSample.Service.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.OData.Builder;

    using Microsoft.OData.Edm;

    public class Business
    {
        [Key]
        public string Id
        {
            get;
            set;
        }

        [Contained]
        [ActionOnDelete(EdmOnDeleteAction.Cascade)]
        public Appointment[] Appointments
        {
            get;
            set;
        }
    }
}