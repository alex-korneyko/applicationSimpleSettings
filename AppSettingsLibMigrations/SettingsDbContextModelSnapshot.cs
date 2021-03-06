// <auto-generated />
using ApplicationSimpleSettings;
using ApplicationSimpleSettings.Dao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApplicationSimpleSettings.AppSettingsLibMigrations
{
    [DbContext(typeof(SettingsDbContext))]
    partial class SettingsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ApplicationSimpleSettings.Model.ApplicationSettingsEntry", b =>
                {
                    b.Property<string>("TypeName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("JsonValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TypeName");

                    b.ToTable("ApplicationSettingsEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
