using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web;
using MobileAppAPI.Models;
using MobileAppAPI.Models.Admin;

namespace MobileAppAPI.Authorization
{
    public class ApplicationAuthenticationHandler : DelegatingHandler
    {
        private const string InvalidToken = "Invalid URL";
        private const string MissingToken = "Invalid Token Value";
        commonEncryption commonEncrpt = new commonEncryption();
        HeaderKey headerKey = new HeaderKey();
        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            // Write your Authentication code here
            try
            { 
            headerKey.GetHeaderDetail();
            }
            catch (Exception er)
            {
                return requestCancel(request, cancellationToken, "Network Problem");
            }

            IEnumerable<string> mobileApiKeyHeaderValues = null;

            // Checking the Header values

            if (request.Headers.TryGetValues(headerKey.HeaderKeyID, out mobileApiKeyHeaderValues))
            {
                string[] apiKeyHeaderValue = mobileApiKeyHeaderValues.First().Split(':');
               
                // Validating header value must have both APP ID & APP key

                if (apiKeyHeaderValue.Length == 2)
                {
                   
                    var appID = apiKeyHeaderValue[0];
                    var AppKey = apiKeyHeaderValue[1];

                    if (appID.Equals(headerKey.AppID) && AppKey.Equals(headerKey.AppKey))
                    {
                        var userNameClaim = new Claim(ClaimTypes.Name, appID);

                        var identity = new ClaimsIdentity(new[] { userNameClaim }, "AppApiKey");

                        var principal = new ClaimsPrincipal(identity);

                        Thread.CurrentPrincipal = principal;

                        if (System.Web.HttpContext.Current != null)
                        {
                            System.Web.HttpContext.Current.User = principal;
                        }
                    }
                    else
                    {
                        // Web request cancel reason APP key is NULL
                        return requestCancel(request, cancellationToken, InvalidToken);
                    }
                }
                else
                {
                    // Web request cancel reason missing APP key or APP ID
                    return requestCancel(request, cancellationToken, MissingToken);
                }
            }
            else
            {
                // Web request cancel reason APP key missing all parameters
                return requestCancel(request, cancellationToken, MissingToken);
            }

            return base.SendAsync(request, cancellationToken);
        }
        private System.Threading.Tasks.Task<HttpResponseMessage> requestCancel(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken, string message)
        {
            CancellationTokenSource _tokenSource = new CancellationTokenSource();

            cancellationToken = _tokenSource.Token;

            _tokenSource.Cancel();

            HttpResponseMessage response = new HttpResponseMessage();

            response = request.CreateResponse(System.Net.HttpStatusCode.BadRequest);

            response.Content = new StringContent(message);

            return base.SendAsync(request, cancellationToken).ContinueWith(task =>
            {
                return response;

            });
        }
    }
   
}