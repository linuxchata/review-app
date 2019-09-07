using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

using Microsoft.Extensions.Configuration;

namespace ReviewApp.Api.Infrastructure
{
    public static class X509CertificateProvider
    {
        private const string ServerCertificateSerialNumber = "Security:ServerCertificate:SerialNumber";

        private const string ServerCertificatePath = "Security:ServerCertificate:Path";

        private const string ServerCertificatePassword = "Security:ServerCertificate:Password";

        private static readonly StoreName StoreName = StoreName.My;

        private static readonly StoreLocation StoreLocation = StoreLocation.LocalMachine;

        public static X509Certificate2 Get(IConfiguration configuration)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return GetWindowsCertificate(configuration);
            }
            else
            {
                return GetLinuxCertificate(configuration);
            }
        }

        private static X509Certificate2 GetWindowsCertificate(IConfiguration configuration)
        {
            var serialNumber = GetCertificateSerialNumber(configuration);

            using (var store = new X509Store(StoreName, StoreLocation))
            {
                store.Open(OpenFlags.ReadOnly);
                var certificates = store.Certificates.Find(
                    X509FindType.FindBySerialNumber,
                    serialNumber,
                    true);
                if (certificates.Count > 0)
                {
                    return certificates[0];
                }
            }

            throw new InvalidOperationException(
                $"Valid certificate with serial number {serialNumber} cannot be found in store {StoreName} under {StoreLocation} location");
        }

        private static X509Certificate2 GetLinuxCertificate(IConfiguration configuration)
        {
            var path = GetCertificatePath(configuration);
            var password = GetCertificatePassword(configuration);

            return new X509Certificate2(path, password);
        }

        private static string GetCertificateSerialNumber(IConfiguration configuration)
        {
            var certificateSerialNumber = configuration.GetValue<string>(ServerCertificateSerialNumber);
            if (string.IsNullOrWhiteSpace(certificateSerialNumber))
            {
                throw new InvalidOperationException("Server certificate serial number cannot be null or empty");
            }

            return certificateSerialNumber;
        }

        private static string GetCertificatePath(IConfiguration configuration)
        {
            var certificatePath = configuration.GetValue<string>(ServerCertificatePath);
            if (string.IsNullOrWhiteSpace(certificatePath))
            {
                throw new InvalidOperationException("Server certificate path cannot be null or empty");
            }

            return certificatePath;
        }

        private static string GetCertificatePassword(IConfiguration configuration)
        {
            var certificatePassword = configuration.GetValue<string>(ServerCertificatePassword);
            if (string.IsNullOrWhiteSpace(certificatePassword))
            {
                throw new InvalidOperationException("Server certificate password cannot be null or empty");
            }

            return certificatePassword;
        }
    }
}