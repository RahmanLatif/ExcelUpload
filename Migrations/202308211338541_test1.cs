namespace ExcelUpload.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Gender = c.Int(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        DOH = c.DateTime(nullable: false),
                        Department = c.String(),
                        BasicSalary = c.Double(nullable: false),
                        TotalSalary = c.Double(nullable: false),
                        MFIG = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(),
                        Operation = c.String(),
                        Message = c.String(),
                        dateTime = c.DateTime(nullable: false),
                        EmployeeData = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Logs", new[] { "EmployeeId" });
            DropTable("dbo.Logs");
            DropTable("dbo.Employees");
        }
    }
}
