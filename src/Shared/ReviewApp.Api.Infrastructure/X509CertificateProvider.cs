using System;
using System.Security.Cryptography.X509Certificates;

namespace ReviewApp.Api.Infrastructure
{
    public static class X509CertificateProvider
    {
        private static readonly StoreName StoreName = StoreName.My;

        private static readonly StoreLocation StoreLocation = StoreLocation.LocalMachine;

        public static X509Certificate2 Get(string serialNumber)
        {
            if (string.IsNullOrWhiteSpace(serialNumber))
            {
                throw new ArgumentNullException(nameof(serialNumber), "Serial number cannot be null or empty");
            }

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
    }
}