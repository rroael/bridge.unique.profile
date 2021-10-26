using Bridge.Commons.System.Contracts.Settings;

namespace Bridge.Unique.Profile.System.Settings
{
    public class AppSettings : IAppSettings
    {
        public string EmailValidationPath { get; set; }
        public string EmailExternalValidationPath { get; set; }
        public Jwt Jwt { get; set; }
        public AmazonGlobal AmazonGlobal { get; set; }
        public AzureGlobal AzureGlobal { get; set; }
        public AzureStorage AzureStorage { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public Logging Logging { get; set; }
        public SmtpServer SmtpServer { get; set; }
        public Redis Redis { get; set; }
        public string Environment { get; set; }
    }
}