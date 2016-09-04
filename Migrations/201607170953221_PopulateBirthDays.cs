namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateBirthDays : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Customers SET Birthday = CAST('1980-01-01' AS DATETIME) WHERE Id = 1");
            Sql("UPDATE Customers SET Birthday = null WHERE Id = 2");
        }
        
        public override void Down()
        {
        }
    }
}
