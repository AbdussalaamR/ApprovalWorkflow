﻿// <auto-generated />
using System;
using MEMOJET.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MEMOJET.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("MEMOJET.Entities.Approval", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ApprovalRole")
                        .HasColumnType("text");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ResponsibilityCentreId")
                        .HasColumnType("int");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Approvals");
                });

            modelBuilder.Entity("MEMOJET.Entities.ApprovalRespoCentre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ApprovalId")
                        .HasColumnType("int");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ResponsibilityCentreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApprovalId");

                    b.HasIndex("ResponsibilityCentreId");

                    b.ToTable("ApprovalRespoCentres");
                });

            modelBuilder.Entity("MEMOJET.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ApprovalComment")
                        .HasColumnType("text");

                    b.Property<int>("ApprovalId")
                        .HasColumnType("int");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("UserFormId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApprovalId");

                    b.HasIndex("UserFormId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("MEMOJET.Entities.Form", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Data")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("RespoCentreId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Forms");
                });

            modelBuilder.Entity("MEMOJET.Entities.ResponsibilityCentre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ResponsibilityCentres");
                });

            modelBuilder.Entity("MEMOJET.Entities.UploadedDoc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CommentId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Data")
                        .HasColumnType("varbinary(4000)");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Extension")
                        .HasColumnType("text");

                    b.Property<string>("FileType")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("UploadedBy")
                        .HasColumnType("int");

                    b.Property<int>("UserFormId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("UserFormId");

                    b.HasIndex("UserId");

                    b.ToTable("UploadedDocs");
                });

            modelBuilder.Entity("MEMOJET.Entities.UserForm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ApprovalAction")
                        .HasColumnType("int");

                    b.Property<int>("ApprovalId")
                        .HasColumnType("int");

                    b.Property<int>("ApprovalStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("ArrivedApproval")
                        .HasColumnType("datetime");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Data")
                        .HasColumnType("text");

                    b.Property<int>("FormId")
                        .HasColumnType("int");

                    b.Property<string>("FormType")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ResponsibilityCentreId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.HasIndex("UserId");

                    b.ToTable("UserForms");
                });

            modelBuilder.Entity("MEMOJET.Identity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("MEMOJET.Identity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MEMOJET.Identity.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("MEMOJET.Entities.ApprovalRespoCentre", b =>
                {
                    b.HasOne("MEMOJET.Entities.Approval", "Approval")
                        .WithMany("ApprovalResponsibilityCentres")
                        .HasForeignKey("ApprovalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MEMOJET.Entities.ResponsibilityCentre", "ResponsibilityCentre")
                        .WithMany("ApprovalResponsibilityCentres")
                        .HasForeignKey("ResponsibilityCentreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Approval");

                    b.Navigation("ResponsibilityCentre");
                });

            modelBuilder.Entity("MEMOJET.Entities.Comment", b =>
                {
                    b.HasOne("MEMOJET.Entities.Approval", null)
                        .WithMany("Comments")
                        .HasForeignKey("ApprovalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MEMOJET.Entities.UserForm", "UserForm")
                        .WithMany("Comments")
                        .HasForeignKey("UserFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserForm");
                });

            modelBuilder.Entity("MEMOJET.Entities.UploadedDoc", b =>
                {
                    b.HasOne("MEMOJET.Entities.Comment", "Comment")
                        .WithMany("UplodedDocs")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MEMOJET.Entities.UserForm", "UserForm")
                        .WithMany("UplodedDocs")
                        .HasForeignKey("UserFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MEMOJET.Identity.User", null)
                        .WithMany("UploadedDocs")
                        .HasForeignKey("UserId");

                    b.Navigation("Comment");

                    b.Navigation("UserForm");
                });

            modelBuilder.Entity("MEMOJET.Entities.UserForm", b =>
                {
                    b.HasOne("MEMOJET.Entities.Form", "Form")
                        .WithMany("UserForms")
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MEMOJET.Identity.User", "User")
                        .WithMany("UserForms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Form");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MEMOJET.Identity.UserRole", b =>
                {
                    b.HasOne("MEMOJET.Identity.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MEMOJET.Identity.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MEMOJET.Entities.Approval", b =>
                {
                    b.Navigation("ApprovalResponsibilityCentres");

                    b.Navigation("Comments");
                });

            modelBuilder.Entity("MEMOJET.Entities.Comment", b =>
                {
                    b.Navigation("UplodedDocs");
                });

            modelBuilder.Entity("MEMOJET.Entities.Form", b =>
                {
                    b.Navigation("UserForms");
                });

            modelBuilder.Entity("MEMOJET.Entities.ResponsibilityCentre", b =>
                {
                    b.Navigation("ApprovalResponsibilityCentres");
                });

            modelBuilder.Entity("MEMOJET.Entities.UserForm", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("UplodedDocs");
                });

            modelBuilder.Entity("MEMOJET.Identity.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("MEMOJET.Identity.User", b =>
                {
                    b.Navigation("UploadedDocs");

                    b.Navigation("UserForms");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
