using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace IFYB.Tests;

[TestClass]
public class IFYBTests
{
    [TestMethod]
    public async Task RegisterAndSendOrder()
    {
        var response = await Post("clients", HttpStatusCode.OK, new {
            name = "First User",
            email = "aa@bb.cc"
        });
        JToken? idToken = response.GetValue("id");
        Assert.IsNotNull(idToken);
        int clientId = (int)idToken;
        Assert.AreNotEqual(0, clientId);
        response = await Post($"clients/{clientId}/git-accesses", HttpStatusCode.OK, new {
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
        response = await Post($"clients/{clientId}/orders", HttpStatusCode.OK, order);
        idToken = response.GetValue("id");
        Assert.IsNotNull(idToken);
        int orderId = (int)idToken;
        response = await Get($"clients/{clientId}/orders/{orderId}", HttpStatusCode.OK);
        Assert.AreEqual(JObject.FromObject(order).ToString(), response.ToString());
    }

    private async Task<JObject> Get(string route, HttpStatusCode expectedStatus)
    {
        HttpClient httpClient = GetHttpClient();
        var response = await httpClient.GetAsync(route);
        Assert.AreEqual(expectedStatus, response.StatusCode);
        return ReadJObjest(response);
    }

    private async Task<JObject> Post(string route, HttpStatusCode expectedStatus, object data)
    {
        HttpClient httpClient = GetHttpClient();
        var json = JObject.FromObject(data);
        var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(route, content);
        Assert.AreEqual(expectedStatus, response.StatusCode);
        return ReadJObjest(response);
    }

    private static JObject ReadJObjest(HttpResponseMessage response)
    {
        using var reader = new StreamReader(response.Content.ReadAsStream());
        string respContent = reader.ReadToEnd();
        if (string.IsNullOrEmpty(respContent))
            return null!;

        return JObject.Parse(respContent);
    }

    private static HttpClient GetHttpClient()
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:5000");
        return httpClient;
    }
}