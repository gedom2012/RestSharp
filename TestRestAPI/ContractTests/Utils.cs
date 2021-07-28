using System;
using System.Collections.Generic;
using System.Text;

namespace TestRestAPI.ContractTests
{
    class Utils
    {
        /*
 * 
 *         [Fact]
public void TestAuthenticateWithSuccess()
{
    // basic status code verification
    var response = GetAuthenticate(new Authenticate { Email = "joao@external.com", Password = "12345678" });
    response.StatusCode.Should().Be(200);


}

[Fact]
public void ExtractBody()
{
    // extract all response body
    var response = GetAuthenticate(new Authenticate { Email = "joao@external.com", Password = "12345678" });
    string body = response.Content; 
    Console.WriteLine(body);       
    response.StatusCode.Should().Be(200);


}
 * 
 * 
        [Fact]
public void ExtractSpecificVAlueFromBody()

// Extract the response body value with Jobject   using Newtonsoft.Json.Linq;
{
    var response = GetAuthenticate(new Authenticate { Email = "joao@external.com", Password = "12345678" });
    var jobject = JObject.Parse(response.Content);
    string result = jobject.GetValue("result").ToString(); // extract all body
    string token = (string)jobject["result"]["token"]; // xtract the specific attibute
    Console.WriteLine(token);         


}
 * 
 * 
 */
    }
}
