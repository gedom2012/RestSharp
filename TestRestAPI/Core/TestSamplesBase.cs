using RestSharp;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using System.Net;

namespace TestRestAPI
{

    public class TestSamplesBase
    {
        public string AdminUser { get; } = "admin@company.com";
        public string Password { get; } = "12345678";

        private RestClient _client;
        private RestRequest _request;
        private readonly string _baseAPI = "http://localhost:5000/api/";
        private readonly string _resourcesPath = "TestRestAPI/Resources";

        public string GetToken(string email, string password)
        {
            var client = new RestClient(_baseAPI);
            var request = new RestRequest("authenticate", Method.POST);
            request.AddJsonBody(new Authenticate { Email = email, Password = password });
            var response = client.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode == HttpStatusCode.MethodNotAllowed)
            {
                throw new Exception($"MethodNotAllowed {response.StatusCode} and body {response.Content}");
            }
            else if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Generic Error!!!! {response.StatusCode} and body {response.Content}");
            }
            else if (response == null)
            {
                throw new Exception("Error to generate the authentication, response body empty.");
            }

            var jobject = JObject.Parse(response.Content);
            string token = (string)jobject["result"]["token"];
            return $"Bearer {token}";
        }

        public IRestResponse NewRequest(Method method, string path, Object obj = null, string document = null)
        {
            Client();
            Request(path);
            RequestType(method, AdminUser, Password);
            if (obj != null)
            {
                if (document != null)
                {
                    AddFile(obj, document);
                }
                else
                {
                    RequestBody(obj);
                }
            }
            var resp = Response();
            return resp;
        }

        public IRestResponse NewRequest(Method method, string path, string searchParameter)
        {
            Client();
            Request(path);
            RequestType(method, AdminUser, Password);
            RequestParameters("searchTerm", searchParameter);
            return Response();
        }

        public string ExtractValueFromBody(string response, string value1, string value2)
        {
            if (response == null || response == "")
            {
                throw new Exception("Response body empty.");
            }
            try
            {
                string attributeValue;
                var jobject = JObject.Parse(response);
                attributeValue = (string)jobject["result"][value1][value2];
                return attributeValue;
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine($"Parse error {response} {e.StackTrace}");
                return null;
            }

        }

        public string ExtractValueFromBody(string response, string value1, int index, string value2)
        {
            if (response == null || response == "")
            {
                throw new Exception("Response body empty.");
            }
            try
            {
                string attributeValue;
                var jobject = JObject.Parse(response);
                attributeValue = (string)jobject["result"][value1][index][value2];
                return attributeValue;
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine($"Parse error {response} {e.StackTrace}");
                return null;
            }

        }

        public string ExtractValueFromBody(string response, string value, bool isBodyList)
        {
            if (response == null || response == "")
            {
                throw new Exception("Response body empty.");
            }

            string attributeValue;
            try
            {
                JObject jobject = JObject.Parse(response);
                if (isBodyList)
                {
                    attributeValue = (string)jobject["result"][0][value];
                }
                else
                {
                    attributeValue = (string)jobject["result"][value];
                }
                return attributeValue;
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine($"Parse error {response} {e.StackTrace}");
                return null;
            }

        }

        public string ExtractValueFromBody(string response, string objectName, string value, bool isBodyList)
        {
            if (response == null || response == "")
            {
                throw new Exception("Response body empty.");
            }
            try
            {
                string attributeValue;

                var jobject = JObject.Parse(response);
                if (isBodyList)
                {
                    attributeValue = (string)jobject["result"][0][objectName][value];
                }
                else
                {
                    attributeValue = (string)jobject["result"][objectName][value];
                }
                return attributeValue;
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine($"Parse error {response} {e.StackTrace}");
                return null;
            }

        }
        public string ExtractValueFromBody(string response, string objectName)
        {
            if (response == null || response == "")
            {
                throw new Exception("Response body empty.");
            }
            try
            {
                string attributeValue;
                var jobject = JObject.Parse(response);
                attributeValue = (string)jobject[objectName];
                return attributeValue;
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine($"Parse error {response} {e.StackTrace}");
                return null;
            }

        }

        public RestClient Client()
        {
            _client = new RestClient(_baseAPI);
            return _client;
        }

        public RestRequest Request(string path)
        {
            _request = new RestRequest(path);
            return _request;
        }

        public void RequestType(Method methodType, string email, string password)
        {
            _request.AddHeader("Authorization", GetToken(email, password));
            _request.RequestFormat = DataFormat.Json;

            switch (methodType
    )
            {
                case Method.GET:
                    _request.Method = Method.GET;
                    break;
                case Method.POST:
                    _request.Method = Method.POST;
                    break;
                case Method.PUT:
                    _request.Method = Method.PUT;
                    break;
                case Method.DELETE:
                    _request.Method = Method.DELETE;
                    break;
            }
        }

        public void RequestParameters(string parameter, string value)
        {
            _request.AddParameter(parameter, value);
        }

        public void RequestBody(object obj)
        {
            _request.AddJsonBody(obj);
        }

        public void AddFile(object obj, string fileName)
        {
            _request.AddFile("rosetta", $"./{_resourcesPath}/Files/" + fileName, "multipart/form-data");
            _request.AddParameter("document", JsonConvert.SerializeObject(obj), "multipart/form-data", ParameterType.RequestBody);
        }

        public bool ValidateSchemaJson(string content, string jsonFileName)
        {
            if (content == null)
            {
                throw new Exception("The content is null");
            }
            try
            {
                string basePath = Environment.CurrentDirectory;
                string relativePath = $"./{_resourcesPath}/Schemas/{jsonFileName}";
                string fullPath = Path.GetFullPath(relativePath, basePath);

                using StreamReader file = File.OpenText(fullPath);
                using JsonTextReader reader = new JsonTextReader(file);
                JObject schemaToValidate = JObject.Parse(content);
                JSchema schema = JSchema.Load(reader);

                if (!schemaToValidate.IsValid(schema))
                {
                    Console.WriteLine(content);
                }
                return schemaToValidate.IsValid(schema);

            }
            catch (Exception)
            {
                Console.WriteLine(content);
                return false;
            }

        }

        public IRestResponse Response()
        {
            var response = _client.Execute(_request);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode == HttpStatusCode.MethodNotAllowed)
            {
                throw new Exception($"MethodNotAllowed {response.StatusCode} and body {response.Content}");
            }
            else if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Generic Error {response.StatusCode} and body {response.Content}");
            }
            return response;

        }

    }
}