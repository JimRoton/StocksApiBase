namespace SearchApi.Configs
{
    public class RedisConfig
    {
        public string connectionString { get; set; }

        public int ttl { get; set; }
    }
}