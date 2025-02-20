using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillForge.Data.Entities;

namespace SkillForge.Data.Configs
{
    public class CoursesConfig : IEntityTypeConfiguration<Courses>
    {
        public void Configure(EntityTypeBuilder<Courses> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .IsRequired()              
                .HasMaxLength(100);         

            builder.Property(c => c.Description)
                .HasMaxLength(500);         

            builder.Property(c => c.Teacher)
                .IsRequired()               
                .HasMaxLength(50);          

            builder.ToTable("Courses");
        }
    }
}
