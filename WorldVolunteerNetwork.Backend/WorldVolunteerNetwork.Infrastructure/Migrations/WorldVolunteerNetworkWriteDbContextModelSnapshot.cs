﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WorldVolunteerNetwork.Infrastructure.DbContexts;

#nullable disable

namespace WorldVolunteerNetwork.Infrastructure.Migrations
{
    [DbContext(typeof(WorldVolunteerNetworkWriteDbContext))]
    partial class WorldVolunteerNetworkWriteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WorldVolunteerNetwork.Domain.Entities.Organizer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("ActsBehalfCharitableOrganization")
                        .HasColumnType("boolean")
                        .HasColumnName("acts_behalf_charitable_organization");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int>("YearsVolunteeringExperience")
                        .HasColumnType("integer")
                        .HasColumnName("years_volunteering_experience");

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "WorldVolunteerNetwork.Domain.Entities.Organizer.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("first_name");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("last_name");

                            b1.Property<string>("Patronymic")
                                .HasColumnType("text")
                                .HasColumnName("patronymic");
                        });

                    b.HasKey("Id")
                        .HasName("pk_organizers");

                    b.ToTable("organizers", (string)null);
                });

            modelBuilder.Entity("WorldVolunteerNetwork.Domain.Entities.OrganizerPhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean")
                        .HasColumnName("is_main");

                    b.Property<Guid>("OrganizerId")
                        .HasColumnType("uuid")
                        .HasColumnName("organizer_id");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("path");

                    b.HasKey("Id")
                        .HasName("pk_organizer_photos");

                    b.HasIndex("OrganizerId")
                        .HasDatabaseName("ix_organizer_photos_organizer_id");

                    b.ToTable("organizer_photos", (string)null);
                });

            modelBuilder.Entity("WorldVolunteerNetwork.Domain.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("DateCreate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_create");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("duration");

                    b.Property<string>("Employment")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("employment");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid>("OrganizerId")
                        .HasColumnType("uuid")
                        .HasColumnName("organizer_id");

                    b.Property<float?>("Payment")
                        .IsRequired()
                        .HasColumnType("real")
                        .HasColumnName("payment");

                    b.Property<string>("Restriction")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("restriction");

                    b.Property<float?>("Reward")
                        .IsRequired()
                        .HasColumnType("real")
                        .HasColumnName("reward");

                    b.Property<DateTimeOffset>("SubmissionDeadline")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("submission_deadline");

                    b.ComplexProperty<Dictionary<string, object>>("ContactNumber", "WorldVolunteerNetwork.Domain.Entities.Post.ContactNumber#PhoneNumber", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("contact_number");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Location", "WorldVolunteerNetwork.Domain.Entities.Post.Location#Location", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Building")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("building");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("city");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("country");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("postalcode");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("street");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Requirement", "WorldVolunteerNetwork.Domain.Entities.Post.Requirement#Requirement", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Age")
                                .HasColumnType("text")
                                .HasColumnName("age");

                            b1.Property<string>("Gender")
                                .HasColumnType("text")
                                .HasColumnName("gender");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Status", "WorldVolunteerNetwork.Domain.Entities.Post.Status#PostStatus", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("status");
                        });

                    b.HasKey("Id")
                        .HasName("pk_posts");

                    b.HasIndex("OrganizerId")
                        .HasDatabaseName("ix_posts_organizer_id");

                    b.ToTable("posts", (string)null);
                });

            modelBuilder.Entity("WorldVolunteerNetwork.Domain.Entities.PostPhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean")
                        .HasColumnName("is_main");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("path");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid")
                        .HasColumnName("post_id");

                    b.HasKey("Id")
                        .HasName("pk_post_photos");

                    b.HasIndex("PostId")
                        .HasDatabaseName("ix_post_photos_post_id");

                    b.ToTable("post_photos", (string)null);
                });

            modelBuilder.Entity("WorldVolunteerNetwork.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string[]>("Permissions")
                        .IsRequired()
                        .HasColumnType("text[]")
                        .HasColumnName("permissions");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role_name");

                    b.HasKey("Id")
                        .HasName("pk_roles");

                    b.HasIndex("RoleName")
                        .HasDatabaseName("ix_roles_role_name");

                    b.ToTable("roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("b8c09528-1648-4921-b493-a66669854796"),
                            Permissions = new[] { "volunteer.applications.read", "volunteer.applications.update", "organizers.create", "organizers.read", "organizers.delete", "posts.read", "posts.delete" },
                            RoleName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("abefff7f-c4af-40a4-8a57-e540a85f71a3"),
                            Permissions = new[] { "posts.create", "posts.read", "posts.update", "posts.delete", "organizers.read" },
                            RoleName = "ORGANIZER"
                        });
                });

            modelBuilder.Entity("WorldVolunteerNetwork.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasColumnName("role_id");

                    b.ComplexProperty<Dictionary<string, object>>("Email", "WorldVolunteerNetwork.Domain.Entities.User.Email#Email", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("email");
                        });

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_users_role_id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("WorldVolunteerNetwork.Domain.Entities.Vaccination", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("Applied")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("applied");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid?>("PostId")
                        .HasColumnType("uuid")
                        .HasColumnName("post_id");

                    b.HasKey("Id")
                        .HasName("pk_vaccinations");

                    b.HasIndex("PostId")
                        .HasDatabaseName("ix_vaccinations_post_id");

                    b.ToTable("vaccinations", (string)null);
                });

            modelBuilder.Entity("WorldVolunteerNetwork.Domain.Entities.VolunteerApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("ExperienceDescription")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("experience_description");

                    b.Property<bool>("IsMemberOfOrganization")
                        .HasColumnType("boolean")
                        .HasColumnName("is_member_of_organization");

                    b.Property<string>("NameOfOrganization")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name_of_organization");

                    b.Property<int>("YearsVolunteeringExperience")
                        .HasColumnType("integer")
                        .HasColumnName("years_volunteering_experience");

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "WorldVolunteerNetwork.Domain.Entities.VolunteerApplication.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("first_name");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("last_name");

                            b1.Property<string>("Patronymic")
                                .HasColumnType("text")
                                .HasColumnName("patronymic");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("StatusApplication", "WorldVolunteerNetwork.Domain.Entities.VolunteerApplication.StatusApplication#StatusApplication", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("status");
                        });

                    b.HasKey("Id")
                        .HasName("pk_volunteer_application");

                    b.ToTable("volunteer_application", (string)null);
                });

            modelBuilder.Entity("WorldVolunteerNetwork.Domain.Entities.Organizer", b =>
                {
                    b.OwnsMany("WorldVolunteerNetwork.Domain.Entities.SocialMedia", "SocialMedias", b1 =>
                        {
                            b1.Property<Guid>("OrganizerId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            b1.Property<string>("Link")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Social")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("OrganizerId", "Id");

                            b1.ToTable("organizers");

                            b1.ToJson("social_medias");

                            b1.WithOwner()
                                .HasForeignKey("OrganizerId")
                                .HasConstraintName("fk_organizers_organizers_organizer_id");
                        });

                    b.Navigation("SocialMedias");
                });

            modelBuilder.Entity("WorldVolunteerNetwork.Domain.Entities.OrganizerPhoto", b =>
                {
                    b.HasOne("WorldVolunteerNetwork.Domain.Entities.Organizer", null)
                        .WithMany("Photos")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_organizer_photos_organizers_organizer_id");
                });

            modelBuilder.Entity("WorldVolunteerNetwork.Domain.Entities.Post", b =>
                {
                    b.HasOne("WorldVolunteerNetwork.Domain.Entities.Organizer", null)
                        .WithMany("Posts")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_posts_organizers_organizer_id");
                });

            modelBuilder.Entity("WorldVolunteerNetwork.Domain.Entities.PostPhoto", b =>
                {
                    b.HasOne("WorldVolunteerNetwork.Domain.Entities.Post", null)
                        .WithMany("Photos")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_post_photos_posts_post_id");
                });

            modelBuilder.Entity("WorldVolunteerNetwork.Domain.Entities.User", b =>
                {
                    b.HasOne("WorldVolunteerNetwork.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_users_roles_role_id");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("WorldVolunteerNetwork.Domain.Entities.Vaccination", b =>
                {
                    b.HasOne("WorldVolunteerNetwork.Domain.Entities.Post", null)
                        .WithMany("Vaccinations")
                        .HasForeignKey("PostId")
                        .HasConstraintName("fk_vaccinations_posts_post_id");
                });

            modelBuilder.Entity("WorldVolunteerNetwork.Domain.Entities.Organizer", b =>
                {
                    b.Navigation("Photos");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("WorldVolunteerNetwork.Domain.Entities.Post", b =>
                {
                    b.Navigation("Photos");

                    b.Navigation("Vaccinations");
                });
#pragma warning restore 612, 618
        }
    }
}
