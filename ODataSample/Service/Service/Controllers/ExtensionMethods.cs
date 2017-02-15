// ---------------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

namespace ODataSample.Service.Controllers
{
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Web.Http;

    public static class ExtensionMethods
    {
        public static string GetUrlParameter(this ApiController controller, [CallerMemberName]string parameterName = null) =>
            (string)controller.RequestContext.RouteData.Values[parameterName];

        public static SingleResult<T> AsSingleResult<T>(this T value) =>
            new[] { value }.AsSingleResult();

        public static SingleResult<T> AsSingleResult<T>(this T[] values) =>
            new SingleResult<T>(values.AsQueryable());
    }
}