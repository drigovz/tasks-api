using Microsoft.EntityFrameworkCore.Migrations;

namespace TasksApi.Migrations
{
    public partial class PopulatingDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Tasks(Name, IsCompleted) " +
                                "VALUES('Terminar desenvolvimento do programa', 0), " +
                                      "('Ajustar código de teste', 1), " +
                                      "('Concluir documentação', 0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Tasks");
        }
    }
}
