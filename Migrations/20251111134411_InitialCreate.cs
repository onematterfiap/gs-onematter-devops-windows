using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OneMatter.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // --- TABELAS DO IDENTITY (JÁ EXISTENTES) COMENTADAS ---

            // migrationBuilder.CreateTable(
            //     name: "AspNetRoles",
            //     columns: table => new
            //     {
            //         Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
            //         Name = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: true),
            //         NormalizedName = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: true),
            //         ConcurrencyStamp = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "AspNetUsers",
            //     columns: table => new
            //     {
            //         Id = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
            //         UserName = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: true),
            //         NormalizedUserName = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: true),
            //         Email = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: true),
            //         NormalizedEmail = table.Column<string>(type: "NVARCHAR2(256)", maxLength: 256, nullable: true),
            //         EmailConfirmed = table.Column<bool>(type: "NUMBER(1)", nullable: false),
            //         PasswordHash = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
            //         SecurityStamp = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
            //         ConcurrencyStamp = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
            //         PhoneNumber = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
            //         PhoneNumberConfirmed = table.Column<bool>(type: "NUMBER(1)", nullable: false),
            //         TwoFactorEnabled = table.Column<bool>(type: "NUMBER(1)", nullable: false),
            //         LockoutEnd = table.Column<DateTimeOffset>(type: "TIMESTAMP(7) WITH TIME ZONE", nullable: true),
            //         LockoutEnabled = table.Column<bool>(type: "NUMBER(1)", nullable: false),
            //         AccessFailedCount = table.Column<int>(type: "NUMBER(10)", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            //     });

            // --- NOSSAS TABELAS NOVAS (MANTIDAS) ---

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Genero = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    FotoUrl = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: true),
                    Skills = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Experiencia = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Title = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Location = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            // --- TABELAS DO IDENTITY (JÁ EXISTENTES) COMENTADAS ---

            // migrationBuilder.CreateTable(
            //     name: "AspNetRoleClaims",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
            //             .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
            //         RoleId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
            //         ClaimType = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
            //         ClaimValue = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
            //             column: x => x.RoleId,
            //             principalTable: "AspNetRoles",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "AspNetUserClaims",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
            //             .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
            //         UserId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
            //         ClaimType = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
            //         ClaimValue = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_AspNetUserClaims_AspNetUsers_UserId",
            //             column: x => x.UserId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "AspNetUserLogins",
            //     columns: table => new
            //     {
            //         LoginProvider = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
            //         ProviderKey = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
            //         ProviderDisplayName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
            //         UserId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
            //         table.ForeignKey(
            //             name: "FK_AspNetUserLogins_AspNetUsers_UserId",
            //             column: x => x.UserId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "AspNetUserRoles",
            //     columns: table => new
            //     {
            //         UserId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
            //         RoleId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
            //         table.ForeignKey(
            //             name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
            //             column: x => x.RoleId,
            //             principalTable: "AspNetRoles",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK_AspNetUserRoles_AspNetUsers_UserId",
            //             column: x => x.UserId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "AspNetUserTokens",
            //     columns: table => new
            //     {
            //         UserId = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
            //         LoginProvider = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
            //         Name = table.Column<string>(type: "NVARCHAR2(128)", maxLength: 128, nullable: false),
            //         Value = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
            //         table.ForeignKey(
            //             name: "FK_AspNetUserTokens_AspNetUsers_UserId",
            //             column: x => x.UserId,
            //             principalTable: "AspNetUsers",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // --- NOSSA TABELA NOVA (MANTIDA) ---

            migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Status = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AppliedAt = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    SkillScore = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    JobId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CandidateId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplications_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobApplications_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            // --- ÍNDICES DO IDENTITY (COMENTADOS) ---

            // migrationBuilder.CreateIndex(
            //     name: "IX_AspNetRoleClaims_RoleId",
            //     table: "AspNetRoleClaims",
            //     column: "RoleId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "RoleNameIndex",
            //     table: "AspNetRoles",
            //     column: "NormalizedName",
            //     unique: true,
            //     filter: "\"NormalizedName\" IS NOT NULL");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_AspNetUserClaims_UserId",
            //     table: "AspNetUserClaims",
            //     column: "UserId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_AspNetUserLogins_UserId",
            //     table: "AspNetUserLogins",
            //     column: "UserId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_AspNetUserRoles_RoleId",
            //     table: "AspNetUserRoles",
            //     column: "RoleId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "EmailIndex",
            //     table: "AspNetUsers",
            //     column: "NormalizedEmail");
            //
            // migrationBuilder.CreateIndex(
            //     name: "UserNameIndex",
            //     table: "AspNetUsers",
            //     column: "NormalizedUserName",
            //     unique: true,
            //     filter: "\"NormalizedUserName\" IS NOT NULL");

            // --- NOSSOS ÍNDICES NOVOS (MANTIDOS) ---

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_CandidateId",
                table: "JobApplications",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobId",
                table: "JobApplications",
                column: "JobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Comentamos tudo aqui também
            // migrationBuilder.DropTable(
            //     name: "AspNetRoleClaims");
            //
            // migrationBuilder.DropTable(
            //     name: "AspNetUserClaims");
            //
            // migrationBuilder.DropTable(
            //     name: "AspNetUserLogins");
            //
            // migrationBuilder.DropTable(
            //     name: "AspNetUserRoles");
            //
            // migrationBuilder.DropTable(
            //     name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "JobApplications");

            // migrationBuilder.DropTable(
            //     name: "AspNetRoles");
            //
            // migrationBuilder.DropTable(
            //     name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}