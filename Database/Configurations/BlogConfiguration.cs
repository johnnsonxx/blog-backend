using blog_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blog_backend.Database.Configurations;

public sealed class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.ToTable("blogs", table =>
        {
            table.HasCheckConstraint(
                "ck_blogs_title_not_blank",
                "length(trim(title)) > 0");
            table.HasCheckConstraint(
                "ck_blogs_slug_not_blank",
                "length(trim(slug)) > 0");
            table.HasCheckConstraint(
                "ck_blogs_status",
                "status IN ('draft', 'published', 'archived')");
            table.HasCheckConstraint(
                "ck_blogs_published_at",
                "status <> 'published' OR published_at IS NOT NULL");
        });

        builder.HasKey(blog => blog.Id);

        builder.Property(blog => blog.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(blog => blog.AuthorId)
            .HasColumnName("author_id");

        builder.Property(blog => blog.Title)
            .HasColumnName("title")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(blog => blog.Slug)
            .HasColumnName("slug")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(blog => blog.Content)
            .HasColumnName("content")
            .IsRequired();

        builder.Property(blog => blog.Status)
            .HasColumnName("status")
            .HasMaxLength(20)
            .HasDefaultValue("draft")
            .IsRequired();

        builder.Property(blog => blog.PublishedAt)
            .HasColumnName("published_at")
            .HasColumnType("timestamp with time zone");

        builder.Property(blog => blog.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("now()");

        builder.Property(blog => blog.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("now()");

        builder.HasIndex(blog => blog.Slug)
            .IsUnique()
            .HasDatabaseName("ux_blogs_slug");

        builder.HasIndex(blog => blog.AuthorId)
            .HasDatabaseName("ix_blogs_author_id");

        builder.HasIndex(blog => new { blog.Status, blog.PublishedAt })
            .IsDescending(false, true)
            .HasDatabaseName("ix_blogs_status_published_at");

        builder.HasOne(blog => blog.Author)
            .WithMany(user => user.Blogs)
            .HasForeignKey(blog => blog.AuthorId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_blogs_author");
    }
}
