﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace Yadebs.Db.Migrations
{
    /// <inheritdoc />
    public partial class NodaForJournal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<LocalDate>(
                name: "Date",
                table: "Journals",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Journals",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(LocalDate),
                oldType: "date");
        }
    }
}
