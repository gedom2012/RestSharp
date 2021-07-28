using FluentAssertions;
using System;
using NUnit.Framework;

namespace TestRestAPI.ContractTests
{
    public class TestContract : TestSamplesBase
    {
        [Test]
        public void TestSchemaValidator()
        {
            var workTool = new WorkTool
            {
                Name = "Martelo the thor",
                SerialNumber = "12345679",
                IsActive = true,
                ExternalPartner = new ExternalPartner { Id = 10, SiteId = 2 },
                WorkToolType = new WorkToolType { Id = 1, SiteId = 2 }

            };

            Request("site-safety-coordination/external-partner/work-tools");
            RequestType("POST", "joao@external.com", "12345678");
            RequestBody(workTool);
            var resp = Response();

            resp.StatusCode.Should().Be(200);

            try
            {
                Assert.True(ValidateSchemaJson(resp.Content, @"C:\workspace\TestRestAPI\TestRestAPI\Resources\Schemas\Schema.json"));
            }
            catch
            {
                Console.WriteLine(resp.Content);
                Assert.True(false);
            }

        }

    }
}
