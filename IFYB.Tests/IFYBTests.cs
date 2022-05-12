using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace IFYB.Tests;

[TestClass]
public class IFYBTests
{
    
    HttpClient httpClient = GetHttpClient();

    [TestMethod]
    public async Task RegisterAndSendOrder()
    {
        await Get($"reset", HttpStatusCode.OK);
        
        var response = await Post("authenticate", HttpStatusCode.OK, new {
            email = "aa@bb.cc"
        });
        JToken? idToken = response.GetValue("id");
        Assert.IsNotNull(idToken);
        int clientId = (int)idToken;
        Assert.AreNotEqual(0, clientId);


        response = await Post($"authenticate/{clientId}", HttpStatusCode.OK, new {
            password = "123456"
        });
        string? jwt = response.GetValue("jwt")?.ToString();
        Assert.IsNotNull(jwt);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", jwt);

        await Get($"clients/name", HttpStatusCode.NotFound);
        await Post($"clients/name", HttpStatusCode.OK, new {
            name = "First User"
        });
        var nameResponse = await Get($"clients/name", HttpStatusCode.OK);
        JToken? nameToken = nameResponse.GetValue("name");
        Assert.IsNotNull(nameToken);
        Assert.AreEqual("First User", nameToken.ToString());
        response = await Post($"git-accesses", HttpStatusCode.OK, new {
            url = "https://github.com/BootGen/VueStart",
            accessMode = 0
        });
        idToken = response.GetValue("id");
        Assert.IsNotNull(idToken);
        int gitAccessId = (int)idToken;
        Assert.AreNotEqual(0, gitAccessId);
        var order = new
        {
            framework = 0,
            version = "6.0",
            thirdPartyTool = "",
            projectDescription = "hello",
            bugDescription = "bello",
            gitAccessId = gitAccessId
        };
        response = await Post($"orders", HttpStatusCode.OK, order);
        idToken = response.GetValue("id");
        Assert.IsNotNull(idToken);
        int orderId = (int)idToken;
        response = await Get($"orders/{orderId}", HttpStatusCode.OK);
        Assert.AreEqual(JObject.FromObject(order).ToString(), response.ToString());
    }

    [TestMethod]
    public async Task SendContactMessage()
    {
        await Get($"reset", HttpStatusCode.OK);

        var message = new
        {
            name = "First User",
            email = "aa@bb.cc",
            text = "hello"
        };
        var response = await Post("contact", HttpStatusCode.OK, message);

        response = await Post("authenticate/admin", HttpStatusCode.OK, new {
            email = "admin@admin.com"
        });
        JToken? idToken = response.GetValue("id");
        Assert.IsNotNull(idToken);
        int clientId = (int)idToken;
        Assert.AreNotEqual(0, clientId);


        response = await Post($"authenticate/admin/{clientId}", HttpStatusCode.OK, new {
            password = "123456"
        });
        string? jwt = response.GetValue("jwt")?.ToString();
        Assert.IsNotNull(jwt);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", jwt);

        var messages = await GetArray($"admin/contact-messages", HttpStatusCode.OK);
        Assert.AreEqual(JObject.FromObject(message).ToString(), messages[0].ToString());
    }


    private async Task<JObject> Get(string route, HttpStatusCode expectedStatus)
    {
        var response = await httpClient.GetAsync(route);
        Assert.AreEqual(expectedStatus, response.StatusCode);
        return ReadJObject(response);
    }
    private async Task<JArray> GetArray(string route, HttpStatusCode expectedStatus)
    {
        var response = await httpClient.GetAsync(route);
        Assert.AreEqual(expectedStatus, response.StatusCode);
        return ReadJArray(response);
    }

    private async Task<JObject> Post(string route, HttpStatusCode expectedStatus, object data)
    {
        var json = JObject.FromObject(data);
        var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(route, content);
        Assert.AreEqual(expectedStatus, response.StatusCode);
        return ReadJObject(response);
    }

    private static JObject ReadJObject(HttpResponseMessage response)
    {
        using var reader = new StreamReader(response.Content.ReadAsStream());
        string respContent = reader.ReadToEnd();
        if (string.IsNullOrEmpty(respContent))
            return null!;

        return JObject.Parse(respContent);
    }
    private static JArray ReadJArray(HttpResponseMessage response)
    {
        using var reader = new StreamReader(response.Content.ReadAsStream());
        string respContent = reader.ReadToEnd();
        if (string.IsNullOrEmpty(respContent))
            return null!;

        return JArray.Parse(respContent);
    }

    private static HttpClient GetHttpClient()
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:5000");
        return httpClient;
    }
}