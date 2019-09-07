using System;
using System.Security.Authentication;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;

namespace ReviewApp.Api.Infrastructure.Extensions
{
    public static class KestrelOptionsExtension
    {
        private const string AspnetCorePort = "ASPNETCORE_PORT";

        public static void ConfigureHttps(KestrelServerOptions options, IConfiguration configuration)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), "Kestrel server options cannot be null");
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), "Configuration cannot be null");
            }

            options.ListenAnyIP(
                GetPort(configuration),
                listenOptions =>
                {
                    var httpsConnectionAdapterOptions = new HttpsConnectionAdapterOptions
                    {
                        ServerCertificate = X509CertificateProvider.Get(configuration),
                        SslProtocols = SslProtocols.Tls12
                    };

                    listenOptions.UseHttps(httpsConnectionAdapterOptions);
                });
        }

        private static int GetPort(IConfiguration configuration)
        {
            var port = configuration.GetValue<int>(AspnetCorePort);
            if (port < 0 || port > ushort.MaxValue)
            {
                throw new ArgumentException($"Port number value must be between 0 and {ushort.MaxValue}", nameof(port));
            }

            return port;
        }
    }
}