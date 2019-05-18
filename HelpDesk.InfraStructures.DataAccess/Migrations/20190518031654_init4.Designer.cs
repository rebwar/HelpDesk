﻿// <auto-generated />
using HelpDesk.InfraStructures.DataAccess.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HelpDesk.InfraStructures.DataAccess.Migrations
{
    [DbContext(typeof(HelpDeskContext))]
    [Migration("20190518031654_init4")]
    partial class init4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HelpDesk.Domain.Core.Articles.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abstract")
                        .HasMaxLength(1000);

                    b.Property<int>("AspNetUsersId");

                    b.Property<string>("Body")
                        .IsRequired();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Image")
                        .IsRequired()
                        .IsUnicode(false);

                    b.Property<int>("Likes");

                    b.Property<string>("PDF");

                    b.Property<string>("PublishDate")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<int>("Status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Video");

                    b.Property<int>("ViewCount");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("HelpDesk.Domain.Core.Articles.ArticleComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArticleId");

                    b.Property<int>("CommentId");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("CommentId");

                    b.ToTable("ArticleComment");
                });

            modelBuilder.Entity("HelpDesk.Domain.Core.Articles.ArticleTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArticleId");

                    b.Property<int>("TagId");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("TagId");

                    b.ToTable("ArticleTag");
                });

            modelBuilder.Entity("HelpDesk.Domain.Core.Categories.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<string>("ImagePath");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("HelpDesk.Domain.Core.Comments.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddedDate")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Email");

                    b.Property<string>("IpSender")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("HelpDesk.Domain.Core.Images.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ArticleId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("HelpDesk.Domain.Core.Tags.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("HelpDesk.Domain.Core.Articles.Article", b =>
                {
                    b.HasOne("HelpDesk.Domain.Core.Categories.Category")
                        .WithMany("Articles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HelpDesk.Domain.Core.Articles.ArticleComment", b =>
                {
                    b.HasOne("HelpDesk.Domain.Core.Articles.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HelpDesk.Domain.Core.Comments.Comment", "Comment")
                        .WithMany()
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HelpDesk.Domain.Core.Articles.ArticleTag", b =>
                {
                    b.HasOne("HelpDesk.Domain.Core.Articles.Article", "Article")
                        .WithMany("Tags")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HelpDesk.Domain.Core.Tags.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
