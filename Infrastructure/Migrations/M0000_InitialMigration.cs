using Domain.Entities;
using FluentMigrator;
using FluentMigrator.Postgres;

namespace Infrastructure.Migrations;

[Migration(2026011701)]
public class M0001_InitialMigration : Migration
{
    public override void Up()
    {
        Create.Table("devices")
            .WithColumn("id").AsCustom("uuid").PrimaryKey().NotNullable()
            .WithColumn("name").AsString(255).Unique().NotNullable();

        Create.Table("sessions")
            .WithColumn("id").AsCustom("uuid").PrimaryKey().NotNullable()
            .WithColumn("device_id").AsCustom("uuid").NotNullable()
            .WithColumn("name").AsString(255).Nullable()
            .WithColumn("start_time").AsDateTimeOffset().Nullable()
            .WithColumn("end_time").AsDateTimeOffset().Nullable()
            .WithColumn("version").AsString(50).Nullable();

        Execute.Sql(@"
            ALTER TABLE devices 
            ALTER COLUMN id SET DEFAULT uuidv7();
            
            ALTER TABLE sessions 
            ALTER COLUMN id SET DEFAULT uuidv7();
        ");

        Create.ForeignKey("FK_sessions_devices")
            .FromTable("sessions").ForeignColumn("device_id")
            .ToTable("devices").PrimaryColumn("id");
    }

    public override void Down()
    {
        Delete.ForeignKey("FK_sessions_devices").OnTable("sessions");
        Delete.Table("sessions");
        Delete.Table("devices");
    }
}
