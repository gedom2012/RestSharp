using NUnit.Framework;
using RestSharp;
using TestRestAPI.ResourcesFactory;

namespace TestRestAPI
{
    public class TestSamples : TestSamplesBase
    {
        private string _response;

        [SetUp]
        public void Setup()
        {
            var obj = WorkToolFactory.CreateWorkTool();
            _response = NewRequest(Method.POST, "insert/the/route", obj).Content;
        }

        [Test]
        public void Create_Work_Tool_Should_Be_Return_Success()
        {
            Assert.True(ValidateSchemaJson(_response, "Schema.json"));
        }

        [Test]
        public void Get_Work_Tool_List_Should_Be_Return_Success()
        {
            var resp = NewRequest(Method.GET, "insert/the/route").Content;

            Assert.True(ValidateSchemaJson(resp, "Schema.json"));
        }

        [Test]
        public void Get_Work_Tool_Should_Be_Return_Success()
        {
            var id = ExtractValueFromBody(_response, "id");

            var resp = NewRequest(Method.GET, $"insert/the/route/{id}").Content;

            Assert.True(ValidateSchemaJson(resp, "Schema.json"));
        }

        [Test]
        public void Search_Work_Tool_Should_Be_Return_Success()
        {
            string searchParameter = "work tool";
            var id = ExtractValueFromBody(_response, "id");

            var resp = NewRequest(Method.GET, $"insert/the/route/{id}", searchParameter).Content;

            Assert.True(ValidateSchemaJson(resp, "Schema.json"));
        }

        [Test]
        public void Update_Work_Tool_Should_Be_Return_Success()
        {
            var id = ExtractValueFromBody(_response, "id");
            var obj = WorkToolFactory.EditWorkTool();

            var resp = NewRequest(Method.PUT, $"insert/the/route/{id}", obj).Content;

            Assert.True(ValidateSchemaJson(resp, "Schema.json"));
        }

        public void Update_Work_Tool_Document_Should_Be_Return_Success()
        {
            string documentName = "rosetta.txt";
            var id = ExtractValueFromBody(_response, "id");
            var obj = WorkToolFactory.EditWorkTool();

            var resp = NewRequest(Method.PUT, $"insert/the/route/{id}", obj, documentName).Content;

            Assert.True(ValidateSchemaJson(resp, "Schema.json"));
        }

        public void Delete_Work_Tool_Should_Be_Return_Success()
        {
            var id = ExtractValueFromBody(_response, "id");

            var resp = NewRequest(Method.DELETE, $"insert/the/route/{id}").Content;

            Assert.True(ValidateSchemaJson(resp, "Schema.json"));
        }

    }
}
