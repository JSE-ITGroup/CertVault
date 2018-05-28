namespace CertVault.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Certificates",
                c => new
                    {
                        CertificateNumber = c.String(nullable: false, maxLength: 128),
                        MemberID = c.Int(nullable: false),
                        SymbolIsin = c.String(nullable: false, maxLength: 128),
                        Volume = c.Long(nullable: false),
                        Status = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                        ApprovedAt = c.DateTime(),
                        ApprovedBy = c.String(maxLength: 128),
                        WithdrawRequestAt = c.DateTime(),
                        WithdrawRequestBy = c.String(maxLength: 128),
                        WithdrawApprovedAt = c.DateTime(),
                        WithdrawApprovedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(nullable: false),
                        Client = c.String(),
                        ClientId = c.Int(),
                        VaultID = c.Int(),
                    })
                .PrimaryKey(t => new { t.CertificateNumber, t.MemberID, t.SymbolIsin })
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApprovedBy)
                .ForeignKey("dbo.AspNetUsers", t => t.WithdrawRequestBy)
                .ForeignKey("dbo.AspNetUsers", t => t.WithdrawApprovedBy)
                .ForeignKey("dbo.Vaults", t => t.VaultID)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .Index(t => t.CreatedBy)
                .Index(t => t.ApprovedBy)
                .Index(t => t.WithdrawRequestBy)
                .Index(t => t.WithdrawApprovedBy)
                .Index(t => t.ClientId)
                .Index(t => t.VaultID);
            
            CreateTable(
                "dbo.Vaults",
                c => new
                    {
                        VaultId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 128),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VaultId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy, cascadeDelete: false)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 128),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy)
                .Index(t => t.CreatedBy)
                .Index(t => t.UpdatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Certificates", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Clients", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Clients", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Certificates", "VaultID", "dbo.Vaults");
            DropForeignKey("dbo.Vaults", "UpdatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Vaults", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Certificates", "WithdrawApprovedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Certificates", "WithdrawRequestBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Certificates", "ApprovedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Certificates", "CreatedBy", "dbo.AspNetUsers");
            DropIndex("dbo.Clients", new[] { "UpdatedBy" });
            DropIndex("dbo.Clients", new[] { "CreatedBy" });
            DropIndex("dbo.Vaults", new[] { "UpdatedBy" });
            DropIndex("dbo.Vaults", new[] { "CreatedBy" });
            DropIndex("dbo.Certificates", new[] { "VaultID" });
            DropIndex("dbo.Certificates", new[] { "ClientId" });
            DropIndex("dbo.Certificates", new[] { "WithdrawApprovedBy" });
            DropIndex("dbo.Certificates", new[] { "WithdrawRequestBy" });
            DropIndex("dbo.Certificates", new[] { "ApprovedBy" });
            DropIndex("dbo.Certificates", new[] { "CreatedBy" });
            DropTable("dbo.Clients");
            DropTable("dbo.Vaults");
            DropTable("dbo.Certificates");
        }
    }
}
