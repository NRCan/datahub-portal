﻿// <auto-generated />
using System;
using Datahub.Metadata.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Datahub.Metadata.Migrations
{
    [DbContext(typeof(MetadataDbContext))]
    [Migration("20220809181757_AddingCatalogObjectGroupingIndex")]
    partial class AddingCatalogObjectGroupingIndex
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Datahub.Metadata.Model.ApprovalForm", b =>
                {
                    b.Property<int>("ApprovalFormId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApprovalFormId"), 1L, 1);

                    b.Property<bool>("Approval_InSitu_FLAG")
                        .HasColumnType("bit");

                    b.Property<bool>("Approval_Other_FLAG")
                        .HasColumnType("bit");

                    b.Property<string>("Approval_Other_TXT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Authority_To_Release_FLAG")
                        .HasColumnType("bit");

                    b.Property<int>("Branch_ID")
                        .HasColumnType("int");

                    b.Property<string>("Branch_NAME")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("Can_Be_Released_For_Free_FLAG")
                        .HasColumnType("bit");

                    b.Property<bool>("Collection_Of_Datasets_FLAG")
                        .HasColumnType("bit");

                    b.Property<bool>("Copyright_Restrictions_FLAG")
                        .HasColumnType("bit");

                    b.Property<string>("Dataset_Title_TXT")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Department_NAME")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Division_NAME")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Email_EMAIL")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<bool>("Localized_Metadata_FLAG")
                        .HasColumnType("bit");

                    b.Property<bool>("Machine_Readable_FLAG")
                        .HasColumnType("bit");

                    b.Property<string>("Name_NAME")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("Non_Propietary_Format_FLAG")
                        .HasColumnType("bit");

                    b.Property<bool>("Not_Clasified_Or_Protected_FLAG")
                        .HasColumnType("bit");

                    b.Property<string>("Phone_TXT")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<bool>("Private_Personal_Information_FLAG")
                        .HasColumnType("bit");

                    b.Property<bool>("Requires_Blanket_Approval_FLAG")
                        .HasColumnType("bit");

                    b.Property<string>("Section_NAME")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("Sector_ID")
                        .HasColumnType("int");

                    b.Property<string>("Sector_NAME")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("Subject_To_Exceptions_Or_Eclusions_FLAG")
                        .HasColumnType("bit");

                    b.Property<string>("Type_Of_Data_TXT")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<bool>("Updated_On_Going_Basis_FLAG")
                        .HasColumnType("bit");

                    b.HasKey("ApprovalFormId");

                    b.ToTable("ApprovalForms", (string)null);
                });

            modelBuilder.Entity("Datahub.Metadata.Model.CatalogObject", b =>
                {
                    b.Property<long>("CatalogObjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CatalogObjectId"), 1L, 1);

                    b.Property<int>("Branch_NUM")
                        .HasColumnType("int");

                    b.Property<byte>("Classification_Type")
                        .HasColumnType("tinyint");

                    b.Property<string>("Contact_TXT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("DataType")
                        .HasColumnType("tinyint");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Language")
                        .HasColumnType("int");

                    b.Property<string>("Location_TXT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name_French_TXT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name_TXT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ObjectMetadataId")
                        .HasColumnType("bigint");

                    b.Property<string>("Search_English_TXT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Search_French_TXT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sector_NUM")
                        .HasColumnType("int");

                    b.Property<string>("SecurityClass_TXT")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Unclassified");

                    b.Property<string>("Url_English_TXT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url_French_TXT")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CatalogObjectId");

                    b.HasIndex("GroupId");

                    b.HasIndex("ObjectMetadataId");

                    b.ToTable("CatalogObjects", (string)null);
                });

            modelBuilder.Entity("Datahub.Metadata.Model.FieldChoice", b =>
                {
                    b.Property<int>("FieldChoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FieldChoiceId"), 1L, 1);

                    b.Property<string>("Cascading_Value_TXT")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("FieldDefinitionId")
                        .HasColumnType("int");

                    b.Property<string>("Label_English_TXT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Label_French_TXT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value_TXT")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("FieldChoiceId");

                    b.HasIndex("FieldDefinitionId");

                    b.ToTable("FieldChoices", (string)null);
                });

            modelBuilder.Entity("Datahub.Metadata.Model.FieldDefinition", b =>
                {
                    b.Property<int>("FieldDefinitionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FieldDefinitionId"), 1L, 1);

                    b.Property<int?>("CascadeParentId")
                        .HasColumnType("int");

                    b.Property<bool>("Custom_Field_FLAG")
                        .HasColumnType("bit");

                    b.Property<string>("Default_Value_TXT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("English_DESC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Field_Name_TXT")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("French_DESC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MetadataVersionId")
                        .HasColumnType("int");

                    b.Property<bool>("MultiSelect_FLAG")
                        .HasColumnType("bit");

                    b.Property<string>("Name_English_TXT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name_French_TXT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Required_FLAG")
                        .HasColumnType("bit");

                    b.Property<int>("Sort_Order_NUM")
                        .HasColumnType("int");

                    b.Property<string>("Validators_TXT")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FieldDefinitionId");

                    b.HasIndex("MetadataVersionId");

                    b.HasIndex("Field_Name_TXT", "MetadataVersionId")
                        .IsUnique();

                    b.ToTable("FieldDefinitions", (string)null);
                });

            modelBuilder.Entity("Datahub.Metadata.Model.Keyword", b =>
                {
                    b.Property<int>("KeywordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KeywordId"), 1L, 1);

                    b.Property<string>("English_TXT")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("French_TXT")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<string>("Source")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("KeywordId");

                    b.HasIndex("English_TXT")
                        .IsUnique()
                        .HasFilter("[English_TXT] IS NOT NULL");

                    b.HasIndex("French_TXT")
                        .IsUnique()
                        .HasFilter("[French_TXT] IS NOT NULL");

                    b.ToTable("Keywords", (string)null);
                });

            modelBuilder.Entity("Datahub.Metadata.Model.MetadataProfile", b =>
                {
                    b.Property<int>("ProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProfileId"), 1L, 1);

                    b.Property<string>("Name")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("ProfileId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Profiles", (string)null);
                });

            modelBuilder.Entity("Datahub.Metadata.Model.MetadataSection", b =>
                {
                    b.Property<int>("SectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SectionId"), 1L, 1);

                    b.Property<string>("Name_English_TXT")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name_French_TXT")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.HasKey("SectionId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Sections", (string)null);
                });

            modelBuilder.Entity("Datahub.Metadata.Model.MetadataVersion", b =>
                {
                    b.Property<int>("MetadataVersionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MetadataVersionId"), 1L, 1);

                    b.Property<DateTime>("Last_Update_DT")
                        .HasColumnType("datetime2");

                    b.Property<string>("Source_TXT")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Version_Info_TXT")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("MetadataVersionId");

                    b.ToTable("MetadataVersions", (string)null);
                });

            modelBuilder.Entity("Datahub.Metadata.Model.ObjectFieldValue", b =>
                {
                    b.Property<long>("ObjectMetadataId")
                        .HasColumnType("bigint");

                    b.Property<int>("FieldDefinitionId")
                        .HasColumnType("int");

                    b.Property<string>("Value_TXT")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ObjectMetadataId", "FieldDefinitionId");

                    b.HasIndex("FieldDefinitionId");

                    b.ToTable("ObjectFieldValues", (string)null);
                });

            modelBuilder.Entity("Datahub.Metadata.Model.ObjectMetadata", b =>
                {
                    b.Property<long>("ObjectMetadataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ObjectMetadataId"), 1L, 1);

                    b.Property<int>("MetadataVersionId")
                        .HasColumnType("int");

                    b.Property<string>("ObjectId_TXT")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("ObjectMetadataId");

                    b.HasIndex("MetadataVersionId");

                    b.HasIndex("ObjectId_TXT")
                        .IsUnique();

                    b.ToTable("ObjectMetadata", (string)null);
                });

            modelBuilder.Entity("Datahub.Metadata.Model.SectionField", b =>
                {
                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.Property<int>("FieldDefinitionId")
                        .HasColumnType("int");

                    b.Property<bool>("Required_FLAG")
                        .HasColumnType("bit");

                    b.HasKey("SectionId", "FieldDefinitionId");

                    b.HasIndex("FieldDefinitionId");

                    b.ToTable("SectionFields", (string)null);
                });

            modelBuilder.Entity("Datahub.Metadata.Model.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubjectId"), 1L, 1);

                    b.Property<string>("Subject_TXT")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("SubjectId");

                    b.HasIndex("Subject_TXT")
                        .IsUnique()
                        .HasFilter("[Subject_TXT] IS NOT NULL");

                    b.ToTable("Subjects", (string)null);
                });

            modelBuilder.Entity("Datahub.Metadata.Model.SubSubject", b =>
                {
                    b.Property<int>("SubSubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubSubjectId"), 1L, 1);

                    b.Property<string>("Name_English_TXT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name_French_TXT")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubSubjectId");

                    b.ToTable("SubSubjects", (string)null);
                });

            modelBuilder.Entity("SubSubjectSubject", b =>
                {
                    b.Property<int>("SubSubjectsSubSubjectId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectsSubjectId")
                        .HasColumnType("int");

                    b.HasKey("SubSubjectsSubSubjectId", "SubjectsSubjectId");

                    b.HasIndex("SubjectsSubjectId");

                    b.ToTable("SubSubjectSubject");
                });

            modelBuilder.Entity("Datahub.Metadata.Model.CatalogObject", b =>
                {
                    b.HasOne("Datahub.Metadata.Model.ObjectMetadata", "ObjectMetadata")
                        .WithMany("CatalogObjects")
                        .HasForeignKey("ObjectMetadataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ObjectMetadata");
                });

            modelBuilder.Entity("Datahub.Metadata.Model.FieldChoice", b =>
                {
                    b.HasOne("Datahub.Metadata.Model.FieldDefinition", "FieldDefinition")
                        .WithMany("Choices")
                        .HasForeignKey("FieldDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FieldDefinition");
                });

            modelBuilder.Entity("Datahub.Metadata.Model.FieldDefinition", b =>
                {
                    b.HasOne("Datahub.Metadata.Model.MetadataVersion", "MetadataVersion")
                        .WithMany("Definitions")
                        .HasForeignKey("MetadataVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MetadataVersion");
                });

            modelBuilder.Entity("Datahub.Metadata.Model.MetadataSection", b =>
                {
                    b.HasOne("Datahub.Metadata.Model.MetadataProfile", "Profile")
                        .WithMany("Sections")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Datahub.Metadata.Model.ObjectFieldValue", b =>
                {
                    b.HasOne("Datahub.Metadata.Model.FieldDefinition", "FieldDefinition")
                        .WithMany("FieldValues")
                        .HasForeignKey("FieldDefinitionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Datahub.Metadata.Model.ObjectMetadata", "ObjectMetadata")
                        .WithMany("FieldValues")
                        .HasForeignKey("ObjectMetadataId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FieldDefinition");

                    b.Navigation("ObjectMetadata");
                });

            modelBuilder.Entity("Datahub.Metadata.Model.ObjectMetadata", b =>
                {
                    b.HasOne("Datahub.Metadata.Model.MetadataVersion", "MetadataVersion")
                        .WithMany("Objects")
                        .HasForeignKey("MetadataVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MetadataVersion");
                });

            modelBuilder.Entity("Datahub.Metadata.Model.SectionField", b =>
                {
                    b.HasOne("Datahub.Metadata.Model.FieldDefinition", "FieldDefinition")
                        .WithMany("SectionFields")
                        .HasForeignKey("FieldDefinitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Datahub.Metadata.Model.MetadataSection", "Section")
                        .WithMany("Fields")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FieldDefinition");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("SubSubjectSubject", b =>
                {
                    b.HasOne("Datahub.Metadata.Model.SubSubject", null)
                        .WithMany()
                        .HasForeignKey("SubSubjectsSubSubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Datahub.Metadata.Model.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsSubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Datahub.Metadata.Model.FieldDefinition", b =>
                {
                    b.Navigation("Choices");

                    b.Navigation("FieldValues");

                    b.Navigation("SectionFields");
                });

            modelBuilder.Entity("Datahub.Metadata.Model.MetadataProfile", b =>
                {
                    b.Navigation("Sections");
                });

            modelBuilder.Entity("Datahub.Metadata.Model.MetadataSection", b =>
                {
                    b.Navigation("Fields");
                });

            modelBuilder.Entity("Datahub.Metadata.Model.MetadataVersion", b =>
                {
                    b.Navigation("Definitions");

                    b.Navigation("Objects");
                });

            modelBuilder.Entity("Datahub.Metadata.Model.ObjectMetadata", b =>
                {
                    b.Navigation("CatalogObjects");

                    b.Navigation("FieldValues");
                });
#pragma warning restore 612, 618
        }
    }
}
