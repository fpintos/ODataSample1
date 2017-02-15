// ---------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------

namespace ODataSample.Service
{
    using System.Web;
    using System.Web.Http;

    using Microsoft.OData;
    using Microsoft.OData.UriParser;
    using System.Web.OData.Extensions;
    using System.Net.Http.Headers;
    using System.Web.OData.Routing.Conventions;
    using System.Linq;
    using Framework;
    using Models;

    public class SampleApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(
                config =>
                {
                    config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
                    config.MapHttpAttributeRoutes();
                    config.MapODataServiceRoute("odata", "api/v1.0", builder =>
                    {
                        builder.AddService(ServiceLifetime.Singleton, _ => new ModelBuilder().GetEdmModel());
                        builder.AddService<ODataUriResolver>(ServiceLifetime.Singleton, _ => new UnqualifiedCallAndEnumPrefixFreeResolver { EnableCaseInsensitive = true });
                        builder.AddService(ServiceLifetime.Singleton, _ => ODataRoutingConventions.CreateDefaultWithAttributeRouting("odata", config).AsEnumerable());
                    });
                });
        }
    }
}