// ---------------------------------------------------------------------------
// <copyright file="Appointment.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

namespace ODataSample.Service.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Appointment
    {
        [Key]
        public string Id
        {
            get;
            set;
        }
    }
}