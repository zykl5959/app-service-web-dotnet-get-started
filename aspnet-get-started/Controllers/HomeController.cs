using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;

namespace aspnet_get_started.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SecretClientOptions options = new SecretClientOptions()
                {
                    Retry =
                    {
                        Delay= TimeSpan.FromSeconds(2),
                        MaxDelay = TimeSpan.FromSeconds(16),
                        MaxRetries = 5,
                        Mode = RetryMode.Exponential
                    }
                };
            var client = new SecretClient(new Uri("https://yitest.vault.azure.net/"), new DefaultAzureCredential(),options);

            KeyVaultSecret secret = client.GetSecret("test");

            string secretValue = secret.Value;

            System.Console.WriteLine(secretValue);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page. Test Yi";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}