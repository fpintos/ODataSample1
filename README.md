# Sample ODATA Service

This sample demonstrates two bugs where the response of a function is not serialized properly,
when using Microsoft.AspNet.OData 6.0.0 and Microsoft.OData.Edm 7.0.0.

## Setup
The ODATA container provides an entity set of 'Business'.
'Business' has a contained set of 'Appointments' and a function to get a subset of these appointments.
The sample is using in-memory data, to keep things simple.

## What works
Asking for the appointments using the contained entity set works.
A client proxy is able to deserialize and present the data as expected.

Requests and responses look like this:

    https://localhost:44399/api/v1.0/businesses('0')/appointments

    {
      "@odata.context": "https://localhost:44399/api/v1.0/$metadata#businesses('0')/appointments",
      "value": [
        { "id": "0.0" },
        ...
      ]
    }

With full metadata:

    https://localhost:44399/api/v1.0/businesses('0')/appointments?$format=application/json;odata.metadata=full

    {
      "@odata.context": "https://localhost:44399/api/v1.0/$metadata#businesses('0')/appointments",
      "value": [
        {
          "@odata.type": "#ODataSample.Service.Models.Appointment",
          "@odata.id": "businesses('0')/appointments('0.0')",
          "@odata.editLink": "businesses('0')/appointments('0.0')",
          "id": "0.0"
        },
        ...
      ]
    }

## What does not work
When getting a collection of appointments using the function provided by the service, the payload either
has the wrong context (when using minimal metadata) or fails to serialize due to a lack of context
(when using full metadata).

### Bug 1: Incorrect context in the payload when minimal metadata is used
Invoking the function with minimal metadata (the default) causes the payload context to be invalid:

    https://localhost:44399/api/v1.0/businesses('0')/getAppointmentSubset(start=1,count=2)

    {
      "@odata.context": "https://localhost:44399/api/v1.0/$metadata#appointments",
      "value": [
        { "id": "0.1" },
        { "id": "0.2" }
      ]
    }

Compare the returned metadata with the one from the entity set:  
Entity Set:`"@odata.context": "https://localhost:44399/api/v1.0/$metadata#businesses('0')/appointments",`  
Function: `"@odata.context": "https://localhost:44399/api/v1.0/$metadata#appointments",`

The one from the function lacks the `businesses('0')/` prefix.
When this payload is received by a client proxy (for example, the one generated by LINQPad ODATAv4 driver)
it fails to deserialize, claiming that the payload is invalid.

### BUG 2: Serialization failure when full metadata is used
Invoking the function with full metadata causes the payload to fail to serialize:

    https://localhost:44399/api/v1.0/businesses('0')/getAppointmentSubset(start=1,count=2)?$format=application/json;odata.metadata=full

    {
      "error": {
        "code": "",
        "message": "An error has occurred.",
        "innererror": {
          "message": "The 'ObjectContent`1' type failed to serialize the response body for content type 'application/json; odata.metadata=full'.",
          "type": "System.InvalidOperationException",
          "stacktrace": "",
          "internalexception": {
            "message": "Parent id or contained context url is missing which is required to compute id for contained instance. Specify ODataUri in the ODataMessageWriterSettings or return parent id or context url in the payload.",
            "type": "Microsoft.OData.ODataException",
            "stacktrace": "   at Microsoft.OData.Evaluation.ODataConventionalEntityMetadataBuilder.ComputeIdForContainment()\r\n   at Microsoft.OData.Evaluation.ODataConventionalEntityMetadataBuilder.ComputeAndCacheId()\r\n   at Microsoft.OData.Evaluation.ODataConventionalEntityMetadataBuilder.GetId()\r\n   at Microsoft.OData.Evaluation.ODataConventionalEntityMetadataBuilder.TryGetIdForSerialization(Uri& id)\r\n   at Microsoft.OData.JsonLight.ODataJsonLightResourceSerializer.WriteResourceStartMetadataProperties(IODataJsonLightWriterResourceState resourceState)\r\n   at Microsoft.OData.JsonLight.ODataJsonLightWriter.StartResource(ODataResource resource)\r\n   at Microsoft.OData.ODataWriterCore.<>c__DisplayClass14.<WriteStartResourceImplementation>b__12()\r\n   at Microsoft.OData.ODataWriterCore.InterceptException(Action action)\r\n   at Microsoft.OData.ODataWriterCore.WriteStartResourceImplementation(ODataResource resource)\r\n   at Microsoft.OData.ODataWriterCore.WriteStart(ODataResource resource)\r\n   at System.Web.OData.Formatter.Serialization.ODataResourceSerializer.WriteResource(Object graph, ODataWriter writer, ODataSerializerContext writeContext, IEdmTypeReference expectedType)\r\n   at System.Web.OData.Formatter.Serialization.ODataResourceSerializer.WriteObjectInline(Object graph, IEdmTypeReference expectedType, ODataWriter writer, ODataSerializerContext writeContext)\r\n   at System.Web.OData.Formatter.Serialization.ODataResourceSetSerializer.WriteResourceSet(IEnumerable enumerable, IEdmTypeReference resourceSetType, ODataWriter writer, ODataSerializerContext writeContext)\r\n   at System.Web.OData.Formatter.Serialization.ODataResourceSetSerializer.WriteObjectInline(Object graph, IEdmTypeReference expectedType, ODataWriter writer, ODataSerializerContext writeContext)\r\n   at System.Web.OData.Formatter.Serialization.ODataResourceSetSerializer.WriteObject(Object graph, Type type, ODataMessageWriter messageWriter, ODataSerializerContext writeContext)\r\n   at System.Web.OData.Formatter.ODataMediaTypeFormatter.WriteToStream(Type type, Object value, Stream writeStream, HttpContent content, HttpContentHeaders contentHeaders)\r\n   at System.Web.OData.Formatter.ODataMediaTypeFormatter.WriteToStreamAsync(Type type, Object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)\r\n   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)\r\n   at System.Web.Http.WebHost.HttpControllerHandler.<WriteBufferedResponseContentAsync>d__1b.MoveNext()"
          }
        }
      }
    }

I suppose this happens because the serializer is trying to build a valid context for the objects,
so it can build the id and edit links. Lacking the proper context data, it rightfully fails.

## Expectation
We should be able to get the full list of appointments by using the contained entity set
and a subset by invoking the function. In both cases, the appointments 'belong' to the contained
entity set and their context should reflect that.

A client proxy should be able to deserialize the payload of both the entity set and of the function,
with minimal or full metadata.

