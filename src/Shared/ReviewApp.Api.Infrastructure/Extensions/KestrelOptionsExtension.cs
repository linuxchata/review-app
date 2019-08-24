using System.Security.Authentication;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;

namespace ReviewApp.Api.Infrastructure.Extensions
{
    public static class KestrelOptionsExtension
    {
        public static void ConfigureHttps(KestrelServerOptions options)
        {
            var certificate = X509CertificateProvider.Get("00b2464ccb4bf7a30f");

            options.ListenAnyIP(
                7001,
                listenOptions =>
                {
                    var httpsConnectionAdapterOptions = new HttpsConnectionAdapterOptions
                    {
                        ServerCertificate = certificate,
                        SslProtocols = SslProtocols.Tls12
                    };

                    listenOptions.UseHttps(httpsConnectionAdapterOptions);
                });
        }
    }
}