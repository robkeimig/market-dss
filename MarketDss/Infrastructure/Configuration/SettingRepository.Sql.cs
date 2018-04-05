namespace MarketDss.Infrastructure.Configuration
{
    public partial class SettingRepository
    {
        private static string SelectAllQuery =>
           @"SELECT * 
                FROM Settings";

        private static string ExistsByNameQuery =>
            @"SELECT 1 
                FROM Settings   
                WHERE Name = @Name";
        private static string SelectByNameQuery =>
            @"SELECT Id, Name, Value 
                FROM Settings 
                WHERE Name = @Name";
        private static string InsertQuery =>
            @"INSERT INTO Settings 
                   ( Name,  Value) 
            VALUES (@Name, @Value)";

        private static string UpdateByNameQuery =>
            @"UPDATE Settings 
                SET Value = @Value 
                WHERE Name = @Name";
        private static string DeleteByNameQuery =>
            @"DELETE FROM Settings
                WHERE Name = @Name";
    }
}
