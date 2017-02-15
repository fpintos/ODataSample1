<Query Kind="Statements">
  <Connection>
    <ID>976ff5c9-fdb0-47be-9cc2-f49aca177762</ID>
    <Persist>true</Persist>
    <Driver Assembly="OData4DynamicDriver" PublicKeyToken="ac4f2d9e4b31c376">OData4.OData4DynamicDriver</Driver>
    <Server>https://localhost:44399/api/v1.0/</Server>
  </Connection>
  <Namespace>LINQPad.User.ODataSample.Service.Models</Namespace>
</Query>

this.BuildingRequest += (s, e) =>
{
	e.Headers["Accept"] = "application/json;odata.metadata=full";
};

this.businesses.ByKey("0").getAppointmentSubset(1,2).Dump();