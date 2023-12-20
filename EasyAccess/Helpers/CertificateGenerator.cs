using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;

namespace EasyAccess.Helpers
{
    public class CertificateGenerator
    {
        public static X509Certificate2 CreateSelfSignedCertificate(string subject, string password)
        {
            using (RSA rsa = RSA.Create(2048))
            {
                // Create a certificate request with the RSA key and specified subject
                var request = new CertificateRequest(
                    subject,
                    rsa,
                    HashAlgorithmName.SHA256,
                    RSASignaturePadding.Pkcs1);

                // Set the certificate validity period
                var notBefore = DateTimeOffset.Now;
                var notAfter = notBefore.AddYears(1);

                // Create a self-signed certificate
                var certificate = request.CreateSelfSigned(notBefore, notAfter);

                // Export the certificate with the private key to a PFX (PKCS #12) format byte array
                // The certificate is protected with the specified password
                var certificateBytes = certificate.Export(X509ContentType.Pfx, password);

                // Create a new X509Certificate2 object from the exported bytes
                // The certificate includes the private key and is flagged as exportable
                return new X509Certificate2(
                    certificateBytes,
                    password,
                    X509KeyStorageFlags.Exportable);
            }
        }

        public static void ExportCertificateToFile(X509Certificate2 certificate, string filePath)
        {
            File.WriteAllBytes(filePath, certificate.Export(X509ContentType.Pfx, "YourPassword"));
        }

        public static string ExportCertificateToPEM(X509Certificate2 certificate)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("-----BEGIN CERTIFICATE-----");
            builder.AppendLine(Convert.ToBase64String(certificate.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks));
            builder.AppendLine("-----END CERTIFICATE-----");
            return builder.ToString();
        }
    }
}
