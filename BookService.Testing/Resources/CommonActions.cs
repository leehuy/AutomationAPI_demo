using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace BookService.Testing.Resources
{
    public static class CommonActions
    {
        public static void VerifyMessage(HttpResponseMessage response, Table responseTable)
        {
            foreach (var row in responseTable.Rows)
            {
                Assert.AreEqual(Convert.ToInt32(row[0]), (int)response.StatusCode);
                Assert.AreEqual(row[1], response.ReasonPhrase);
            }
        }

        public static bool CheckValueinListDatabase(List<Dictionary<string, string>> listDict, string key, string value)
        {
            foreach (Dictionary<string, string> column in listDict)
            {
                foreach (var cell in column)
                {
                    if (cell.Key.ToLower().Contains(key.ToLower()) && cell.Value.ToLower().Contains(value.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }


    }
}
