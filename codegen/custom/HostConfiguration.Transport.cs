using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MailChannels.EmailApi.Client
{
    public partial class HostConfiguration
    {
        partial void OnAddApiHttpClientBuilder(IHttpClientBuilder builder, Action<IHttpClientBuilder>? userBuilder, ref bool suppressDefault)
        {
            builder.ConfigureHttpClient(client => client.Timeout = TimeSpan.FromSeconds(30));
            builder.ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
            {
                AllowAutoRedirect = false,
                UseCookies = false,
                ConnectTimeout = TimeSpan.FromSeconds(10)
            });
            builder.AddHttpMessageHandler(() => new HttpsOnlyHandler());
            // HostConfiguration invokes userBuilder after these defaults, permitting
            // trusted applications to configure their own handler and timeouts.
        }
    }

    internal sealed class HttpsOnlyHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri?.Scheme != Uri.UriSchemeHttps)
                throw new HttpRequestException("MailChannels requires an HTTPS endpoint.");
            return base.SendAsync(request, cancellationToken);
        }
    }
}
