using Microsoft.Owin.Testing;
using AuthenticatedOwinIntegrationTests;
using System;
using Owin;
using Microsoft.Owin;
using System.Web.Http;

namespace BookService.Testing.Core
{

        public abstract class ApiIntegrationTestBase : IntegrationTestBase
        {
            public static TestServer InMemoryWebServer { get; set; }

        public RequestBuilder GetAuthorizedRequest(string uri)
        {
            //var token = IdSrvTestTokenCreator.GetToken();
            //return InMemoryWebServer.CreateRequest(uri).AddHeader("Authorization", "Bearer " + token.AccessToken);

            return InMemoryWebServer.CreateRequest(uri);
        }

        protected static void SetupInMemoryServer()
            {
                InMemoryWebServer = TestServer.Create(app =>
                {
                    //SetInMemoryUnityContainer();
                    //var config = new HttpConfiguration { DependencyResolver = new UnityApiResolver(InMemoryUnityContainer) };
                    //WebApiConfig.Register(config);
                    //app.UseWebApi(config);
                    //AutoMapperConfiguration.Configure();

                    SetInMemoryUnityContainer();
                    var config = new HttpConfiguration();
                    WebApiConfig.Register(config);
                    app.UseWebApi(config);

                    //var apiStartup = new Startup();
                    //apiStartup.Configuration(app);

                });
            }

            protected static void StopInMemoryServer()
            {
                try
                {
                    InMemoryWebServer.Dispose();
                }
                catch (Exception e)
                {
                    Console.WriteLine(@"Could not stop server: {0}", e);
                }
            }
        }
    }

