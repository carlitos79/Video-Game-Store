﻿// <auto-generated />
using GameStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace GameStore.Migrations
{
    [DbContext(typeof(GameStoreContext))]
    partial class GameStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameStore.Models.Cart", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CanAdd");

                    b.Property<int>("Count");

                    b.Property<decimal>("FinalTotal");

                    b.Property<int>("GameId");

                    b.Property<string>("GameTitle");

                    b.Property<DateTime>("OrderDate");

                    b.Property<int?>("OrderId");

                    b.Property<int>("Quantity");

                    b.Property<string>("ShoppingCartId")
                        .IsRequired();

                    b.Property<decimal>("Total");

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("CartId");

                    b.HasIndex("GameId");

                    b.HasIndex("OrderId");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("GameStore.Models.Game", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("GameImage");

                    b.Property<string>("Genre");

                    b.Property<decimal>("Price");

                    b.Property<string>("Title");

                    b.Property<int>("UnitsInStock");

                    b.HasKey("ID");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("GameStore.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("IdentityNo");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("OrderCreationDate");

                    b.Property<string>("OrderShoppingCartId");

                    b.Property<string>("Phone");

                    b.Property<string>("PostalCode");

                    b.Property<decimal>("Total");

                    b.HasKey("OrderId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("GameStore.Models.Cart", b =>
                {
                    b.HasOne("GameStore.Models.Game", "GameInfo")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GameStore.Models.Order")
                        .WithMany("CartsInOrder")
                        .HasForeignKey("OrderId");
                });
#pragma warning restore 612, 618
        }
    }
}
