namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6152ad40-97e3-4c3a-9f0b-6e32d1028208', N'Admin1@Vidly.com', 0, N'AEU2NYONJqiGVfkzF1SBtIs43O4bUu6qv32/omsOJGXm594OUWcepCjbIAIaU/1POg==', N'324b2ae9-8411-4440-bcee-fdbb3f23743e', NULL, 0, 0, NULL, 1, 0, N'Admin1@Vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c7baeb99-d4bb-4899-9ffd-d57ae0a10fe8', N'admin@Vidly.com', 0, N'ADV/g++p6VAFsHG1nkhfnj7IJ6XjzXcoTjV+5LKoNMtcZRcDAmjLukiJBXvyngd8Nw==', N'd1cc51db-bcd7-4f7c-8c16-176b46acc884', NULL, 0, 0, NULL, 1, 0, N'admin@Vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'e0c417b1-6840-4293-8cff-ce56f1b15fd8', N'guest@vidly.com', 0, N'AIiCJ8qqF4QfSlYsIiASPtYKt1Wxlpnsr7E9fgbrlmeN5YkD4OL2rGx+cHnbPJDQpw==', N'2eae91bf-a0cc-4566-b500-f59a555f884e', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'5cd56203-1f70-4c3e-b63c-5af8feec12fc', N'CanManageMovie')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'56b70b4e-1b39-40b5-ab2f-7140c469e225', N'CanManageMovies')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6152ad40-97e3-4c3a-9f0b-6e32d1028208', N'56b70b4e-1b39-40b5-ab2f-7140c469e225')");

        }
        
        public override void Down()
        {
        }
    }
}
