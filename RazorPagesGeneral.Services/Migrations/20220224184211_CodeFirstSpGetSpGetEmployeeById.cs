using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesGeneral.Services.Migrations
{
    public partial class CodeFirstSpGetSpGetEmployeeById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create procedure CodeFirstSpGetSpGetEmployeeById
                                @Id int
                                 as
                                 Begin
	                                Select * from Employees 
	                                where Id = @Id
                                 End";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Drop procedure CodeFirstSpGetSpGetEmployeeById";
            migrationBuilder.Sql(procedure);
        }
    }
}
