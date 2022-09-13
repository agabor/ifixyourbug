﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IFYB.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IFYB.Entities.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FailedLoginAtemptCount")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Admins");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "gabor@ifixyourbug.com",
                            FailedLoginAtemptCount = 0
                        });
                });

            modelBuilder.Entity("IFYB.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("FailedLoginAtemptCount")
                        .HasColumnType("integer");

                    b.Property<string>("InvoiceAddress")
                        .HasColumnType("text");

                    b.Property<string>("InvoiceName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<DateTime>("RegistrationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TaxNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("IFYB.Entities.ClientError", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserAgent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ClientErrors");
                });

            modelBuilder.Entity("IFYB.Entities.Email", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<string>("Html")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RetryCount")
                        .HasColumnType("integer");

                    b.Property<bool>("Sent")
                        .HasColumnType("boolean");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ToEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("IFYB.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("AdminId")
                        .HasColumnType("integer");

                    b.Property<int?>("ClientId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("LogLevel")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("ClientId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("IFYB.Entities.GitAccess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessMode")
                        .HasColumnType("integer");

                    b.Property<int?>("ClientId")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("GitAccesses");
                });

            modelBuilder.Entity("IFYB.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("OrderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("IFYB.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("FromClient")
                        .HasColumnType("boolean");

                    b.Property<int?>("OrderId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("OrderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("IFYB.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AddressState")
                        .HasColumnType("text");

                    b.Property<long?>("AmountSubtotal")
                        .HasColumnType("bigint");

                    b.Property<long?>("AmountTax")
                        .HasColumnType("bigint");

                    b.Property<long?>("AmountTotal")
                        .HasColumnType("bigint");

                    b.Property<string>("ApplicationUrl")
                        .HasColumnType("text");

                    b.Property<string>("AutomaticTax")
                        .HasColumnType("text");

                    b.Property<string>("BugDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<int>("CreationDay")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Currency")
                        .HasColumnType("text");

                    b.Property<string>("CustomerName")
                        .HasColumnType("text");

                    b.Property<decimal?>("EurPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("EurPriceId")
                        .HasColumnType("text");

                    b.Property<int>("Framework")
                        .HasColumnType("integer");

                    b.Property<int>("GitAccessId")
                        .HasColumnType("integer");

                    b.Property<string>("InvoiceNumber")
                        .HasColumnType("text");

                    b.Property<string>("Line1")
                        .HasColumnType("text");

                    b.Property<string>("Line2")
                        .HasColumnType("text");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("PayedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PaymentToken")
                        .HasColumnType("text");

                    b.Property<string>("PostalCode")
                        .HasColumnType("text");

                    b.Property<string>("SpecificPlatform")
                        .HasColumnType("text");

                    b.Property<string>("SpecificPlatformVersion")
                        .HasColumnType("text");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<string>("StripeCustomerId")
                        .HasColumnType("text");

                    b.Property<string>("StripeSessionId")
                        .HasColumnType("text");

                    b.Property<string>("TaxCountry")
                        .HasColumnType("text");

                    b.Property<string>("TaxId")
                        .HasColumnType("text");

                    b.Property<string>("TaxIdType")
                        .HasColumnType("text");

                    b.Property<string>("TaxState")
                        .HasColumnType("text");

                    b.Property<string>("ThirdPartyTool")
                        .HasColumnType("text");

                    b.Property<decimal?>("UsdPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("UsdPriceId")
                        .HasColumnType("text");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("GitAccessId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("IFYB.Entities.ServerError", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Data")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("File")
                        .HasColumnType("text");

                    b.Property<int>("HResult")
                        .HasColumnType("integer");

                    b.Property<int>("Line")
                        .HasColumnType("integer");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.Property<string>("StackTrace")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ServerErrors");
                });

            modelBuilder.Entity("IFYB.Entities.Event", b =>
                {
                    b.HasOne("IFYB.Entities.Admin", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId");

                    b.HasOne("IFYB.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.Navigation("Admin");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("IFYB.Entities.GitAccess", b =>
                {
                    b.HasOne("IFYB.Entities.Client", null)
                        .WithMany("GitAccesses")
                        .HasForeignKey("ClientId");
                });

            modelBuilder.Entity("IFYB.Entities.Image", b =>
                {
                    b.HasOne("IFYB.Entities.Order", null)
                        .WithMany("Images")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("IFYB.Entities.Message", b =>
                {
                    b.HasOne("IFYB.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IFYB.Entities.Order", "Order")
                        .WithMany("Messages")
                        .HasForeignKey("OrderId");

                    b.Navigation("Client");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("IFYB.Entities.Order", b =>
                {
                    b.HasOne("IFYB.Entities.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IFYB.Entities.GitAccess", "GitAccess")
                        .WithMany()
                        .HasForeignKey("GitAccessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("GitAccess");
                });

            modelBuilder.Entity("IFYB.Entities.Client", b =>
                {
                    b.Navigation("GitAccesses");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("IFYB.Entities.Order", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
