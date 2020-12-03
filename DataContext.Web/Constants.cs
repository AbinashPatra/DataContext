namespace DataContext.Web
{
	public class Constants
	{
		public const string RedisConfigurationSection = "RedisConfiguration";
		public const string DataProtectionFilePath = "Properties:DataProtectionFile";
		public const string AppNamePath = "Properties:AppName";
		public class RedisConfigPath
		{
			public const string Dns = "RedisConfiguration:Dns";
			public const string Port = "RedisConfiguration:Port";
			public const string UseSsl = "RedisConfiguration:UseSsl";
			public const string ConnectRetry = "RedisConfiguration:ConnectRetry";
			public const string ConnectTimeout = "RedisConfiguration:ConnectTimeout";
			public const string SyncTimeout = "RedisConfiguration:SyncTimeout";
			public const string DefaultDatabase = "RedisConfiguration:DefaultDatabase";
			public const string AbortOnConnectFail = "RedisConfiguration:AbortOnConnectFail";
			public const string Password = "RedisConfiguration:Password";
		}
	}
}
