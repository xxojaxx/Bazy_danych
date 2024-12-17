using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERPv2.Models;
using Microsoft.EntityFrameworkCore;

/// @file SupplierService.cs
/// @brief Plik zawiera logikę zarządzania dostawcami w systemie ERPv2.

namespace ERPv2.Services
{
    /// @class SupplierService
    /// @brief Odpowiada za zarządzanie dostawcami w systemie.
    public class SupplierService
    {
        private readonly AppDbContext _context;

        /// @brief Konstruktor klasy SupplierService.
        /// @param context Kontekst bazy danych.
        public SupplierService(AppDbContext context)
        {
            _context = context;
        }

        /// @brief Dodaje nowego dostawcę.
        /// @param name Nazwa dostawcy.
        /// @param addressId Identyfikator adresu.
        public async Task AddSupplierAsync(string name, int addressId)
        {
            var supplier = new Supplier
            {
                Name = name,
                AddressId = addressId
            };

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
        }

        /// @brief Pobiera wszystkich dostawców.
        /// @return Lista dostawców.
        public async Task<List<Supplier>> GetAllSuppliersAsync()
        {
            return await _context.Suppliers.Include(s => s.Address).ToListAsync();
        }

        /// @brief Pobiera dostawcę na podstawie identyfikatora.
        /// @param supplierId Identyfikator dostawcy.
        /// @return Obiekt dostawcy lub `null`, jeśli nie znaleziono.
        public async Task<Supplier> GetSupplierByIdAsync(int supplierId)
        {
            return await _context.Suppliers.Include(s => s.Address).FirstOrDefaultAsync(s => s.SupplierId == supplierId);
        }

        /// @brief Aktualizuje dane dostawcy.
        /// @param supplierId Identyfikator dostawcy.
        /// @param name Nazwa dostawcy.
        /// @param addressId Identyfikator adresu.
        public async Task UpdateSupplierAsync(int supplierId, string name, int addressId)
        {
            var supplier = await _context.Suppliers.FindAsync(supplierId);
            if (supplier != null)
            {
                supplier.Name = name;
                supplier.AddressId = addressId;
                await _context.SaveChangesAsync();
            }
        }

        /// @brief Usuwa dostawcę na podstawie identyfikatora.
        /// @param supplierId Identyfikator dostawcy.
        public async Task DeleteSupplierAsync(int supplierId)
        {
            var supplier = await _context.Suppliers.FindAsync(supplierId);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                await _context.SaveChangesAsync();
            }
        }

        /// @brief Pobiera zapytanie do wszystkich dostawców z załadowanymi powiązaniami.
        /// @return Zapytanie do bazy danych dla dostawców.
        public IQueryable<Supplier> GetAllSuppliersQuery()
        {
            return _context.Suppliers.Include(s => s.Address);
        }
    }
}
