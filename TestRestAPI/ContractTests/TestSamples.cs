using System;
using FluentAssertions;
//using RestSharp;
//using Newtonsoft.Json.Linq;
//using RestSharp.Serializers.Utf8Json;
using NUnit.Framework;


namespace TestRestAPI
{
    public class TestSamples : TestSamplesBase
    {

       //[Test]
        public void TestInsertWorkToolWithSuccess() 
        {
            var workTool = new WorkTool
            {
                Name = "Ferramenta de teste 999",
                SerialNumber = "989855541111",
                IsActive = true,
                ExternalPartner = new ExternalPartner { Id = 10, SiteId = 2 },
                WorkToolType = new WorkToolType { Id= 1, SiteId= 2 }          

            };
         
            Request("site-safety-coordination/external-partner/work-tools");
            RequestType("POST", "joao@external.com", "12345678");
            RequestBody(workTool);
            Response().StatusCode.Should().Be(200);
        }

        [Test]     
        public void getWorkToolList()
        {       
            Request("site-safety-coordination/external-partner/work-tools");
            RequestType("GET", "joao@external.com", "12345678"); 
            Response().StatusCode.Should().Be(200);
        }        

        /*
         * TO DO IMPLEMENT
        
        [Test]
        public void InsertCompanyDocumentation()
        {
            var document = new Document
            {
                Name = "Rosetta",
                Description = "Description",
                Identity = new Identity 
                { 
                    Id = 10, 
                    SiteId = 2 
                },
                DocumentType = new DocumentType 
                {
                    Name = "Declaração de não divida à Autoridade Tributaria",
                    Id = 18,
                    SiteId = 2
                }
            };         
           
            var client = new RestClient("http://urano.eqs.local:8096/api/");
            client.UseUtf8Json();
            var request = new RestRequest("site-safety-coordination/external-partner/documents", Method.POST);       
            request.AddHeader("Authorization", GetToken("joao@external.com", "12345678"));
          //  request.AddHeader("Content-Type", "multipart/form-data");
            request.AddFile("rosetta", "/workspace/TestRestAPI/TestRestAPI/Resources/rosetta.txt", "multipart/form-data");           
            request.AddParameter("document", document, "multipart/form-data",  ParameterType.RequestBody);            
            var resp = client.Execute(request);
            Console.WriteLine(resp.Content);
            resp.StatusCode.Should().Be(200);
            

        }
         
         */
    }
}
