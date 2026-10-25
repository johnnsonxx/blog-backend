using blog_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blog_backend.Database.Configurations;

public sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("comments", table =>
        {
            table.HasCheckConstraint(
                "ck_comments_content_not_blank",
                "length(trim(content)) > 0");
            table.HasCheckConstraint(
                "ck_comments_status",
                "status IN ('visible', 'hidden', 'deleted')");
        });

        builder.HasKey(comment => comment.Id);

        builder.Property(comment => comment.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(comment => comment.BlogId)
            .HasColumnName("blog_id");

        builder.Property(comment => comment.AuthorId)
            .HasColumnName("author_id");

        builder.Property(comment => comment.Content)
            .HasColumnName("content")
            .IsRequired();

        builder.Property(comment => comment.Status)
            .HasColumnName("status")
            .HasMaxLength(20)
            .HasDefaultValue("visible")
            .IsRequired();

        builder.Property(comment => comment.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("now()");

        builder.Property(comment => comment.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("timestamp with time zone")
            .HasDefaultValueSql("now()");

        builder.HasIndex(comment => new { comment.BlogId, comment.CreatedAt })
            .HasDatabaseName("ix_comments_blog_id_created_at");

        builder.HasIndex(comment => comment.AuthorId)
            .HasDatabaseName("ix_comments_author_id");

        builder.HasOne(comment => comment.Blog)
            .WithMany(blog => blog.Comments)
            .HasForeignKey(comment => comment.BlogId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_comments_blog");

        builder.HasOne(comment => comment.Author)
            .WithMany(user => user.Comments)
            .HasForeignKey(comment => comment.AuthorId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_comments_author");
    }
}
