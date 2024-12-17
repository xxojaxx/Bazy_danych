using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reactive;
using System;
using Tmds.DBus.Protocol;
using System.Collections.Generic;

namespace ERPv2.Models
{
    /// <summary>
    /// Główny kontekst bazy danych dla aplikacji ERP.
    /// Zawiera definicje tabel i konfigurację mapowania modeli.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Inicjalizuje nowy obiekt AppDbContext z opcjami konfiguracji.
        /// </summary>
        /// <param name="options">Opcje konfiguracji DbContext.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Tabela użytkowników.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Tabela ról.
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Tabela adresów.
        /// </summary>
        public DbSet<Address> Addresses { get; set; }

        /// <summary>
        /// Tabela klientów.
        /// </summary>
        public DbSet<Client> Clients { get; set; }

        /// <summary>
        /// Tabela dostawców.
        /// </summary>
        public DbSet<Supplier> Suppliers { get; set; }

        /// <summary>
        /// Tabela przedmiotów.
        /// </summary>
        public DbSet<Item> Items { get; set; }

        /// <summary>
        /// Tabela stanu magazynowego.
        /// </summary>
        public DbSet<Inventory> Inventories { get; set; }

        /// <summary>
        /// Tabela zamówień.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Tabela elementów zamówień.
        /// </summary>
        public DbSet<OrderItem> OrderItems { get; set; }

        /// <summary>
        /// Tabela dostaw.
        /// </summary>
        public DbSet<Supply> Supplies { get; set; }

        /// <summary>
        /// Tabela elementów dostaw.
        /// </summary>
        public DbSet<SupplyItem> SupplyItems { get; set; }

        /// <summary>
        /// Tabela zestawień materiałowych (BOM).
        /// </summary>
        public DbSet<Bom> Boms { get; set; }

        /// <summary>
        /// Tabela elementów zestawień materiałowych (BOM).
        /// </summary>
        public DbSet<BomItem> BomItems { get; set; }

        /// <summary>
        /// Tabela jednostek miary.
        /// </summary>
        public DbSet<Unit> Units { get; set; }

        /// <summary>
        /// Tabela typów przedmiotów.
        /// </summary>
        public DbSet<Type> Types { get; set; }

        /// <summary>
        /// Tabela walut.
        /// </summary>
        public DbSet<Currency> Currencies { get; set; }

        /// <summary>
        /// Konfiguruje opcje dla kontekstu bazy danych.
        /// </summary>
        /// <param name="optionsBuilder">Obiekt konfiguracji opcji bazy danych.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=ERP DB;Username=postgres;Password=password");
        }

        /// <summary>
        /// Konfiguruje mapowania modeli do tabel w bazie danych.
        /// </summary>
        /// <param name="modelBuilder">Obiekt konfiguracji modeli.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapowanie tabeli User
            modelBuilder.Entity<User>()
                .ToTable("User")
                .HasKey(u => u.UserId);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            // Mapowanie tabeli Role
            modelBuilder.Entity<Role>()
                .ToTable("Role")
                .HasKey(r => r.RoleId);

            // Mapowanie tabeli Address
            modelBuilder.Entity<Address>()
                .ToTable("Address")
                .HasKey(a => a.AddressId);

            // Mapowanie tabeli Client
            modelBuilder.Entity<Client>()
                .ToTable("Client")
                .HasKey(c => c.ClientId);
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Address)
                .WithMany(a => a.Clients)
                .HasForeignKey(c => c.AddressId);

            // Mapowanie tabeli Supplier
            modelBuilder.Entity<Supplier>()
                .ToTable("Supplier")
                .HasKey(s => s.SupplierId);
            modelBuilder.Entity<Supplier>()
                .HasOne(s => s.Address)
                .WithMany(a => a.Suppliers)
                .HasForeignKey(s => s.AddressId);

            // Mapowanie tabeli Item
            modelBuilder.Entity<Item>()
                .ToTable("Item")
                .HasKey(i => i.ItemId);
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Unit)
                .WithMany(u => u.Items)
                .HasForeignKey(i => i.UmId);
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Type)
                .WithMany(t => t.Items)
                .HasForeignKey(i => i.ItemType)
                .OnDelete(DeleteBehavior.Restrict);

            // Mapowanie tabeli Inventory
            modelBuilder.Entity<Inventory>()
                .ToTable("Inventory")
                .HasKey(inv => inv.InventoryId);
            modelBuilder.Entity<Inventory>()
                .HasOne(inv => inv.Item)
                .WithMany(i => i.Inventory)
                .HasForeignKey(inv => inv.ItemId);

            // Mapowanie tabeli Order
            modelBuilder.Entity<Order>()
                .ToTable("Order")
                .HasKey(o => o.OrderId);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Currency)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CurrencyId);

            // Mapowanie tabeli OrderItem
            modelBuilder.Entity<OrderItem>()
                .ToTable("OrderItem")
                .HasKey(oi => new { oi.OrderId, oi.ItemId });
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Item)
                .WithMany(i => i.OrderItems)
                .HasForeignKey(oi => oi.ItemId);

            // Mapowanie tabeli Supply
            modelBuilder.Entity<Supply>()
                .ToTable("Supply")
                .HasKey(s => s.SupplyId);
            modelBuilder.Entity<Supply>()
                .HasOne(s => s.Supplier)
                .WithMany(su => su.Supplies)
                .HasForeignKey(s => s.SupplierId);
            modelBuilder.Entity<Supply>()
                .HasOne(s => s.Currency)
                .WithMany(c => c.Supplies)
                .HasForeignKey(s => s.CurrencyId);

            // Mapowanie tabeli SupplyItem
            modelBuilder.Entity<SupplyItem>()
                .ToTable("SupplyItem")
                .HasKey(si => new { si.SupplyId, si.ItemId });
            modelBuilder.Entity<SupplyItem>()
                .HasOne(si => si.Supply)
                .WithMany(s => s.SupplyItems)
                .HasForeignKey(si => si.SupplyId);
            modelBuilder.Entity<SupplyItem>()
                .HasOne(si => si.Item)
                .WithMany(i => i.SupplyItems)
                .HasForeignKey(si => si.ItemId);

            // Mapowanie tabeli Bom
            modelBuilder.Entity<Bom>()
                .ToTable("Bom")
                .HasKey(b => b.BomId);

            // Mapowanie tabeli BomItem
            modelBuilder.Entity<BomItem>()
                .ToTable("BomItem")
                .HasKey(bi => new { bi.BomId, bi.ItemId });
            modelBuilder.Entity<BomItem>()
                .HasOne(bi => bi.Bom)
                .WithMany(b => b.BomItems)
                .HasForeignKey(bi => bi.BomId);
            modelBuilder.Entity<BomItem>()
                .HasOne(bi => bi.Item)
                .WithMany(i => i.BomItems)
                .HasForeignKey(bi => bi.ItemId);

            // Mapowanie tabeli Unit
            modelBuilder.Entity<Unit>()
                .ToTable("Unit")
                .HasKey(u => u.UmId);

            // Mapowanie tabeli Type
            modelBuilder.Entity<Type>()
                .ToTable("Type")
                .HasKey(t => t.ItemType);

            // Mapowanie tabeli Currency
            modelBuilder.Entity<Currency>()
                .ToTable("Currency")
                .HasKey(c => c.CurrencyId);
        }

    }
        /// <summary>
        /// Reprezentuje zamówienie w systemie.
        /// </summary>
        public class Order
        {
            /// <summary>
            /// Identyfikator zamówienia.
            /// </summary>
            public int OrderId { get; set; }

            /// <summary>
            /// Identyfikator klienta, który złożył zamówienie.
            /// </summary>
            public int ClientId { get; set; }

            /// <summary>
            /// Data złożenia zamówienia.
            /// </summary>
            public DateTime Date { get; set; } = DateTime.UtcNow;

            /// <summary>
            /// Status zamówienia.
            /// </summary>
            public string Status { get; set; } = string.Empty;

            /// <summary>
            /// Identyfikator waluty użytej w zamówieniu.
            /// </summary>
            public string CurrencyId { get; set; } = string.Empty;

            /// <summary>
            /// Powiązany klient.
            /// </summary>
            public Client Client { get; set; } = null!;

            /// <summary>
            /// Powiązana waluta.
            /// </summary>
            public Currency Currency { get; set; } = null!;

            /// <summary>
            /// Elementy zamówienia.
            /// </summary>
            public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        }

        /// <summary>
        /// Reprezentuje użytkownika w systemie.
        /// </summary>
        public class User
        {
            public int UserId { get; set; }

            /// <summary>
            /// Imię użytkownika.
            /// </summary>
            public string Name { get; set; } = string.Empty;

            /// <summary>
            /// Nazwisko użytkownika.
            /// </summary>
            public string Surname { get; set; } = string.Empty;

            /// <summary>
            /// Adres email użytkownika.
            /// </summary>
            public string Email { get; set; } = string.Empty;

            /// <summary>
            /// Hasło użytkownika.
            /// </summary>
            public string Password { get; set; } = string.Empty;

            /// <summary>
            /// Identyfikator roli użytkownika.
            /// </summary>
            public int RoleId { get; set; }

            /// <summary>
            /// Powiązana rola użytkownika.
            /// </summary>
            public Role Role { get; set; } = null!;
        }

        /// <summary>
        /// Reprezentuje adres w systemie.
        /// </summary>
        public class Address
        {
            public int AddressId { get; set; }

            /// <summary>
            /// Kraj adresu.
            /// </summary>
            public string Country { get; set; } = string.Empty;

            /// <summary>
            /// Kod pocztowy adresu.
            /// </summary>
            public string PostalCode { get; set; } = string.Empty;

            /// <summary>
            /// Miasto adresu.
            /// </summary>
            public string City { get; set; } = string.Empty;

            /// <summary>
            /// Ulica adresu.
            /// </summary>
            public string Street { get; set; } = string.Empty;

            /// <summary>
            /// Klienci powiązani z adresem.
            /// </summary>
            public ICollection<Client> Clients { get; set; } = new List<Client>();

            /// <summary>
            /// Dostawcy powiązani z adresem.
            /// </summary>
            public ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
        }

        /// <summary>
        /// Reprezentuje zestawienie materiałowe (BOM).
        /// </summary>
        public class Bom
        {
            public int BomId { get; set; }

            /// <summary>
            /// Nazwa zestawienia materiałowego.
            /// </summary>
            public string Name { get; set; } = string.Empty;

            /// <summary>
            /// Elementy zestawienia materiałowego.
            /// </summary>
            public ICollection<BomItem> BomItems { get; set; } = new List<BomItem>();
        }

        /// <summary>
        /// Reprezentuje element zestawienia materiałowego (BOM).
        /// </summary>
        public class BomItem
        {
            public int BomId { get; set; }

            public int ItemId { get; set; }

            /// <summary>
            /// Ilość elementu w zestawieniu materiałowym.
            /// </summary>
            public decimal Quantity { get; set; }

            public Bom Bom { get; set; } = null!;
            public Item Item { get; set; } = null!;
        }

        /// <summary>
        /// Reprezentuje klienta w systemie.
        /// </summary>
        public class Client
        {
            public int ClientId { get; set; }

            /// <summary>
            /// Nazwa klienta lub nazwa firmy.
            /// </summary>
            public string Name { get; set; } = string.Empty;

            public int AddressId { get; set; }

            public Address Address { get; set; } = null!;
            public ICollection<Order> Orders { get; set; } = new List<Order>();
        }

        /// <summary>
        /// Reprezentuje walutę w systemie.
        /// </summary>
        public class Currency
        {
            public string CurrencyId { get; set; } = string.Empty;

            /// <summary>
            /// Nazwa waluty.
            /// </summary>
            public string Name { get; set; } = string.Empty;

            /// <summary>
            /// Symbol waluty.
            /// </summary>
            public string Symbol { get; set; } = string.Empty;

            /// <summary>
            /// Kurs wymiany waluty.
            /// </summary>
            public decimal ExchangeRate { get; set; }

            public ICollection<Order> Orders { get; set; } = new List<Order>();
            public ICollection<Supply> Supplies { get; set; } = new List<Supply>();
        }

        /// <summary>
        /// Reprezentuje stan magazynowy w systemie.
        /// </summary>
        public class Inventory
        {
            public int InventoryId { get; set; }
            public int ItemId { get; set; }

            /// <summary>
            /// Ilość dostępna w magazynie.
            /// </summary>
            public decimal Quantity { get; set; }

            public Item Item { get; set; } = null!;
        }

        /// <summary>
        /// Reprezentuje przedmiot w systemie.
        /// </summary>
        public class Item
        {
            public int ItemId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
            public int UmId { get; set; }
            public int ItemType { get; set; }

            public Unit Unit { get; set; } = null!;
            public Type Type { get; set; } = null!;
            public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
            public ICollection<SupplyItem> SupplyItems { get; set; } = new List<SupplyItem>();
            public ICollection<BomItem> BomItems { get; set; } = new List<BomItem>();
            public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();
        }

        /// <summary>
        /// Reprezentuje element zamówienia w systemie.
        /// </summary>
        public class OrderItem
        {
            public int OrderId { get; set; }
            public int ItemId { get; set; }
            public decimal Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal DispatchedQuantity { get; set; } = 0;

            public Order Order { get; set; } = null!;
            public Item Item { get; set; } = null!;
        }

        /// <summary>
        /// Reprezentuje rolę użytkownika w systemie.
        /// </summary>
        public class Role
        {
            public int RoleId { get; set; }
            public string Name { get; set; } = string.Empty;

            public ICollection<User> Users { get; set; } = new List<User>();
        }

        /// <summary>
        /// Reprezentuje dostawcę w systemie.
        /// </summary>
        public class Supplier
        {
            public int SupplierId { get; set; }
            public string Name { get; set; } = string.Empty;
            public int AddressId { get; set; }

            public Address Address { get; set; } = null!;
            public ICollection<Supply> Supplies { get; set; } = new List<Supply>();
        }

        /// <summary>
        /// Reprezentuje dostawę w systemie.
        /// </summary>
        public class Supply
        {
            public int SupplyId { get; set; }
            public int SupplierId { get; set; }
            public DateTime Date { get; set; } = DateTime.UtcNow;
            public string Status { get; set; } = string.Empty;
            public string CurrencyId { get; set; } = string.Empty;

            public Supplier Supplier { get; set; } = null!;
            public Currency Currency { get; set; } = null!;
            public ICollection<SupplyItem> SupplyItems { get; set; } = new List<SupplyItem>();
        }

        /// <summary>
        /// Reprezentuje element dostawy w systemie.
        /// </summary>
        public class SupplyItem
        {
            public int SupplyId { get; set; }
            public int ItemId { get; set; }
            public decimal Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal ReceivedQuantity { get; set; } = 0;

            public Supply Supply { get; set; } = null!;
            public Item Item { get; set; } = null!;
        }

        /// <summary>
        /// Reprezentuje typ przedmiotu w systemie.
        /// </summary>
        public class Type
        {
            public int ItemType { get; set; }
            public string Description { get; set; } = string.Empty;

            public ICollection<Item> Items { get; set; } = new List<Item>();
        }

        /// <summary>
        /// Reprezentuje jednostkę miary w systemie.
        /// </summary>
        public class Unit
        {
            public int UmId { get; set; }
            public string Name { get; set; } = string.Empty;

            public ICollection<Item> Items { get; set; } = new List<Item>();
        }
    }





