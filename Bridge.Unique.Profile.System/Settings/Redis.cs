namespace Bridge.Unique.Profile.System.Settings
{
    public class Redis
    {
        public string Server { get; set; }
        public string KeyPrefix { get; set; }
        public int DefaultTtl { get; set; }

        public string[] GetServer()
        {
            return Server.Contains(";") ? Server.Split(";") : new[] { Server };
        }
    }
}