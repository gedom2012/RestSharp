using RestSharp;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace TestRestAPI
{

    public class TestSamplesBase
    {
        private RestClient client;
        private RestRequest request;
        private readonly string BaseAPI = "http://urano.eqs.local:8096/api/";

        [SetUp]
        public void Setup()
        {
            Client();
        }

        private IRestResponse GetAuthenticate(Authenticate authenticate)
        {
            var client = new RestClient(BaseAPI);
            var request = new RestRequest("authenticate", Method.POST);
            request.AddJsonBody(authenticate);
            return client.Execute(request);
        }

        public string GetToken(string email, string password)
        {
            var response = GetAuthenticate(new Authenticate { Email = email, Password = password });
            var jobject = JObject.Parse(response.Content);
            string token = (string)jobject["result"]["token"];
            return $"Bearer {token}";
        }

        public RestClient Client()
        {
            client = new RestClient(BaseAPI);
            return client;
        }

        public RestRequest Request(string path)
        {
            request = new RestRequest(path);
            return request;
        }
 
        public void RequestType(string methodType, string email, string password)
        {
            try
            {
                request.AddHeader("Authorization", GetToken(email, password));
                request.RequestFormat = DataFormat.Json;

                if (methodType.Equals("GET"))
                {
                    request.Method = Method.GET;
                }
                else if (methodType.Equals("POST"))
                {
                    request.Method = Method.POST;
                }
                else if (methodType.Equals("PUT"))
                {
                    request.Method = Method.PUT;
                }
                else
                {
                    request.Method = Method.DELETE;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void RequestBody(object obj)
        {
            request.AddJsonBody(obj);
        }

        public bool ValidateSchemaJson(string content, string pathJson)
        {
            //using StreamReader file = File.OpenText(@"c:/workspace/TestRestAPI/TestRestAPI/Resources/Schema.json");   
            using StreamReader file = File.OpenText(pathJson);
            using JsonTextReader reader = new JsonTextReader(file);
            JObject schemaToValidate = JObject.Parse(content);                   
            JSchema schema = JSchema.Load(reader);
            return schemaToValidate.IsValid(schema);  

        }

        public IRestResponse Response()
        {
            var response = client.Execute(request);
            return response;
        }
    }

}