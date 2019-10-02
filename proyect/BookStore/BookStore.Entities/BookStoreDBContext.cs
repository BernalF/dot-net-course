using BookStore.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Entities
{
    public class BookStoreDBContext : DbContext
    {
        public BookStoreDBContext()
        {
        }

        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Authors>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(70);
            });

            modelBuilder.Entity<Books>(entity =>
            {
                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(90);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_books_authors");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_books_categories");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(30);
            });

            modelBuilder.Entity<Reviews>(entity =>
            {
                entity.Property(e => e.InsertDate).HasColumnType("datetime");

                entity.Property(e => e.Review).HasMaxLength(1000);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_reviews_books");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_reviews_users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.UserName).HasMaxLength(10);
            });
        }
    }
}
