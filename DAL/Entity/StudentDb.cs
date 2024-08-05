using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.Entity
{
    public partial class StudentDb : DbContext
    {
        public StudentDb()
            : base("name=StudentDb")
        {
        }

        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Faculty>()
                .HasMany(e => e.Majors)
                .WithRequired(e => e.Faculty)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Faculty>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.Faculty)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Major>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.Major)
                .WillCascadeOnDelete();
        }
    }
}
