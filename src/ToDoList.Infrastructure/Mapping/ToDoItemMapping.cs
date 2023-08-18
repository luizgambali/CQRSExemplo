using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure.Mapping
{
    public class ToDoItemMapping : IEntityTypeConfiguration<ToDoItem>
    {
        public void Configure(EntityTypeBuilder<ToDoItem> builder)
        {
            builder.ToTable("ToDoItem");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.Title).HasColumnName("Title").HasMaxLength(100).IsRequired();
            builder.Property(e => e.Detail).HasColumnName("Detail").HasMaxLength(250).IsRequired();
            builder.Property(e => e.DeadLine).HasColumnName("DeadLine").IsRequired();
            builder.Property(e => e.Status).HasColumnName("Status").IsRequired();
            builder.Property(e => e.Type).HasColumnName("Type").IsRequired();
            builder.Property(e => e.CreatedAt).HasColumnName("CreatedAt").IsRequired();
            builder.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");
            builder.Ignore(e => e.Notifications);

        }
    }
}