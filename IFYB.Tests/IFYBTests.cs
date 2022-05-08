using System;
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
    public async Task CreateUser()
    {
        var response = await Post("clients", new {
            name = "First User",
            email = "aa@bb.cc"
        });
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    private async Task<HttpResponseMessage> Post(string route, object data) {
        
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost:5000");
        var json = JObject.FromObject(data);
        var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
        return await httpClient.PostAsync(route, content);
    }
}