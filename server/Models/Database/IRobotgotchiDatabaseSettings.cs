namespace Models.Database
{
    public interface IRobotgotchiDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

        // collections
        string UserNonceCollectionName { get; set; }
        string UserCollectionName { get; set; }
    }

    public class RobotgotchiDatabaseSettings : IRobotgotchiDatabaseSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string UserNonceCollectionName { get; set; } = string.Empty;
        public string UserCollectionName { get; set; } = string.Empty;
    }

}
