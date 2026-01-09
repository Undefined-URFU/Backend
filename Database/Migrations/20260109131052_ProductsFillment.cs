using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CosmeticsRecommendationSystem.Database.Migrations
{
    /// <inheritdoc />
    public partial class ProductsFillment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Database/Sql/ProductsFillment.sql");
            var file = File.ReadAllText(path);

            migrationBuilder.Sql(file);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM public.\"Products\"");
        }
    }
}
