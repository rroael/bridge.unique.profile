using Amazon.Runtime;
using Bridge.Commons.Notification.Aws.Settings;

namespace Bridge.Unique.Profile.System.Settings
{
    public class AmazonGlobal : AwsCredentials
    {
        public string ProfileName { get; set; }

        public AWSCredentials GetAwsCredentials()
        {
            return new BasicAWSCredentials(AccessKey, SecretKey);
        }
    }
}