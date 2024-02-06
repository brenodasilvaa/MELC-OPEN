﻿using System.Net.Http.Headers;
using MELC.WebApp.MVC.Extensions;

namespace MELC.WebApp.MVC.Services.Handlers
{
    public class HttpClientAuthorizationHandler : DelegatingHandler
    {
        private readonly IUser _user;

        public HttpClientAuthorizationHandler(IUser user)
        {
            _user = user;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorizationHeader = _user.ObterHttpContext().Request.Headers["Authorization"];

            if (!string.IsNullOrWhiteSpace(authorizationHeader))
            {
                request.Headers.Add("Authorization", new List<string> { authorizationHeader });
            }

            var token = _user.ObterUserToken();

            if (token != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
