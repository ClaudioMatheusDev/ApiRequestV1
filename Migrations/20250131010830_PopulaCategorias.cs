﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIRequest.Migrations
{
    /// <inheritdoc />
    public partial class PopulaCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Categorias (Nome, ImagemUrl) VALUES ('Bebidas', 'bebidas.jpg')");
            mb.Sql("INSERT INTO Categorias (Nome, ImagemUrl) VALUES ('Lanches', 'Lanches.jpg')");
            mb.Sql("INSERT INTO Categorias (Nome, ImagemUrl) VALUES ('Sobremesas', 'Sobremesas.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Categorias");
        }
    }
}
