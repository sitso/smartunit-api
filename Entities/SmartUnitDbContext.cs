using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SmartUnitApi.Entities
{
    public partial class SmartUnitDbContext : DbContext
    {
        public SmartUnitDbContext(DbContextOptions<SmartUnitDbContext> options)
            : base(options) { }
        public virtual DbSet<Actuator> Actuator { get; set; }
        public virtual DbSet<ActuatorLog> ActuatorLog { get; set; }
        public virtual DbSet<Alarm> Alarm { get; set; }
        public virtual DbSet<AlarmLog> AlarmLog { get; set; }
        public virtual DbSet<AlarmType> AlarmType { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<County> County { get; set; }
        public virtual DbSet<EdgeRouter> EdgeRouter { get; set; }
        public virtual DbSet<Municipality> Municipality { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Sensor> Sensor { get; set; }
        public virtual DbSet<SensorAlarm> SensorAlarm { get; set; }
        public virtual DbSet<SensorLog> SensorLog { get; set; }
        public virtual DbSet<SensorType> SensorType { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserMunicipality> UserMunicipality { get; set; }
        public virtual DbSet<UserPermissions> UserPermissions { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actuator>(entity =>
            {
                entity.Property(e => e.Description).HasColumnType("varchar(100)");

                entity.Property(e => e.MeasureUnit).HasColumnType("varchar(50)");

                entity.Property(e => e.Name).HasColumnType("varchar(50)");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Actuator)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("R_14");
            });

            modelBuilder.Entity<ActuatorLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("XPKActuatorLog");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Actuator)
                    .WithMany(p => p.ActuatorLog)
                    .HasForeignKey(d => d.ActuatorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("R_10");
            });

            modelBuilder.Entity<Alarm>(entity =>
            {
                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Alarm)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("R_11");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Alarm)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("R_15");
            });

            modelBuilder.Entity<AlarmLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("XPKAlarmLog");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Alarm)
                    .WithMany(p => p.AlarmLog)
                    .HasForeignKey(d => d.AlarmId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("R_12");
            });

            modelBuilder.Entity<AlarmType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("XPKAlarmType");

                entity.Property(e => e.Description).HasColumnType("varchar(100)");

                entity.Property(e => e.Name).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("varchar(50)");

                entity.HasOne(d => d.Municipality)
                    .WithMany(p => p.Area)
                    .HasForeignKey(d => d.MunicipalityId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("R_34");
            });

            modelBuilder.Entity<County>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<EdgeRouter>(entity =>
            {
                entity.HasKey(e => e.RouterId)
                    .HasName("XPKEdgeRouter");

                entity.Property(e => e.Name).HasColumnType("varchar(100)");

                entity.Property(e => e.SocketId).HasColumnType("varchar(200)");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.EdgeRouter)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("R_16");
            });

            modelBuilder.Entity<Municipality>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("varchar(50)");

                entity.HasOne(d => d.County)
                    .WithMany(p => p.Municipality)
                    .HasForeignKey(d => d.CountyId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("R_51");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Sensor>(entity =>
            {
                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Sensor)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("R_7");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.Sensor)
                    .HasForeignKey(d => d.UnitId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("R_13");
            });

            modelBuilder.Entity<SensorAlarm>(entity =>
            {
                entity.HasKey(e => new { e.SensorId, e.AlarmId })
                    .HasName("XPKSensor_Alarm");

                entity.HasOne(d => d.Alarm)
                    .WithMany(p => p.SensorAlarm)
                    .HasForeignKey(d => d.AlarmId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("R_78");

                entity.HasOne(d => d.Sensor)
                    .WithMany(p => p.SensorAlarm)
                    .HasForeignKey(d => d.SensorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("R_77");
            });

            modelBuilder.Entity<SensorLog>(entity =>
            {
                entity.HasKey(e => e.LogId)
                    .HasName("XPKSensorLog");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Sensor)
                    .WithMany(p => p.SensorLog)
                    .HasForeignKey(d => d.SensorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("R_8");
            });

            modelBuilder.Entity<SensorType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("XPKSensorType");

                entity.Property(e => e.Description).HasColumnType("varchar(100)");

                entity.Property(e => e.MeasureUnit).HasColumnType("varchar(50)");

                entity.Property(e => e.Name).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("varchar(50)");

                entity.Property(e => e.SerialNumber).HasColumnType("varchar(20)");

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Unit)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("R_6");

                entity.HasOne(d => d.Router)
                    .WithMany(p => p.Unit)
                    .HasForeignKey(d => d.RouterId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("R_22");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FcmToken).HasColumnType("varchar(400)");
            });

            modelBuilder.Entity<UserMunicipality>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.MunicipalityId })
                    .HasName("XPKUser_Municipality");

                entity.HasOne(d => d.Municipality)
                    .WithMany(p => p.UserMunicipality)
                    .HasForeignKey(d => d.MunicipalityId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("R_48");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserMunicipality)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("R_47");
            });

            modelBuilder.Entity<UserPermissions>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PermissionId })
                    .HasName("XPKUser_Permission");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.UserPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("R_68");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPermissions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("R_67");
            });
        }
    }
}