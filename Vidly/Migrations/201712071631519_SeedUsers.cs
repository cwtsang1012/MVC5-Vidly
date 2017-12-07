namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5db9b868-6e5f-47b6-a9c0-aa30de92e102', N'guest@vidly.com', 0, N'AHioGDhMwKdENa0QXfT4VIFzut6Ak4u6ipKIm7Jk8tkX6VsteqBd5Pr3wcggG5DcFw==', N'49b4ca98-0f86-4d08-b167-0187147b9d46', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'83d1cc16-d543-4363-ad57-92e941dd63ed', N'admin@vidly.com', 0, N'AAYjAKvSz05lzZQLbUf76zo51RYMuLPXIdJoaIseDl/QDhRs2Wn6GNog28Q9kZXRDA==', N'87d6a1a6-6ff9-4216-be4b-611cff851693', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4647af9b-782c-490c-add0-7080d92c2f15', N'CanManageMovie')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'83d1cc16-d543-4363-ad57-92e941dd63ed', N'4647af9b-782c-490c-add0-7080d92c2f15')

            ");
        }
        
        public override void Down()
        {
        }
    }
}
