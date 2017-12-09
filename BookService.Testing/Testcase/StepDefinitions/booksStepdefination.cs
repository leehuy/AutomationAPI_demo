using System;
using System.Net.Http;
using TechTalk.SpecFlow;
using BookService.Testing.Core;
using static BookService.Testing.Resources.CommonActions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookService.Testing.Testcase.StepDefinitions
{
    [Binding]
    public sealed class booksStepdefination : ApiIntegrationTestBase
    {
        private static HttpResponseMessage _response;
        
        [Given(@"I get the request to server:(.*)")]
        public void GivenIGetTheRequestToServerApiBooks(string url)
        {
            _response = GetAuthorizedRequest(url).GetAsync().Result;
        }

        [Then(@"I get the returned message with code:")]
        public void ThenIGetTheReturnedMessageWithCode(Table responseTable)
        {
            VerifyMessage(_response, responseTable);
        }

        [Then(@"Respone message Book details with the same data:")]
        public void ThenResponeMessageBookDetailsWithTheSameData(Table responseTable)
        {
            //var data = (JObject)JsonConvert.DeserializeObject(_response.Content.ReadAsStringAsync().Result);
            //string jsoncontent = data.ToString();
            var data = _response.Content.ReadAsStringAsync().Result;
            //Console.WriteLine(data);
            foreach (var row in responseTable.Rows)
            {
                Assert.IsTrue(data.Contains("\"Id\":" + row[0]));
                Assert.IsTrue(data.Contains("\"Title\":\"" + row[1]));
                Assert.IsTrue(data.Contains("\"Year\":" + row[2]));
                Assert.IsTrue(data.Contains("\"Price\":" + row[3]));
                Assert.IsTrue(data.Contains("\"Genre\":\"" + row[4]));
                Assert.IsTrue(data.Contains("\"AuthorId\":" + row[5]));
            }
        }

    }
}
