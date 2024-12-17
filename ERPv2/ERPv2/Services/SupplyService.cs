using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERPv2.Models;
using Microsoft.EntityFrameworkCore;

/// @file SupplyService.cs
/// @brief Plik zawiera logikę zarządzania dostawami w systemie ERPv2.

namespace ERPv2.Services
{
    /// @class SupplyService
    /// @brief Odpowiada za zarządzanie dostawami w systemie.
    public class SupplyService
    {
        private readonly AppDbContext _context;

        /// @brief Konstruktor klasy SupplyService.
        /// @param context Kontekst bazy danych.
        public SupplyService(AppDbContext context)
        {
            _context = context;
        }

        /// @brief Dodaje nową dostawę.
        /// @param supplierId Identyfikator dostawcy.
        /// @param currencyId Kod waluty.
        public async Task AddSupplyAsync(int supplierId, string currencyId)
        {
            var supply = new Supply
            {
                SupplierId = supplierId,
                Status = "Oczekujące",
                CurrencyId = currencyId,
                Date = DateTime.UtcNow
            };

            _context.Supplies.Add(supply);
            await _context.SaveChangesAsync();
        }

        /// @brief Pobiera wszystkie dostawy.
        /// @return Lista dostaw.
        public async Task<List<Supply>> GetAllSuppliesAsync()
        {
            return await _context.Supplies
                .Include(s => s.Supplier)
                .Include(s => s.Currency)
                .ToListAsync();
        }

        /// @brief Pobiera dostawę na podstawie identyfikatora.
        /// @param supplyId Identyfikator dostawy.
        /// @return Obiekt dostawy lub `null`, jeśli nie znaleziono.
        public async Task<Supply> GetSupplyByIdAsync(int supplyId)
        {
            return await _context.Supplies
                .Include(s => s.Supplier)
                .Include(s => s.Currency)
                .FirstOrDefaultAsync(s => s.SupplyId == supplyId);
        }

        /// @brief Aktualizuje istniejącą dostawę.
        /// @param supplyId Identyfikator dostawy.
        /// @param supplierId Identyfikator dostawcy.
        /// @param status Status dostawy.
        /// @param currencyId Kod waluty.
        public async Task UpdateSupplyAsync(int supplyId, int supplierId, string status, string currencyId)
        {
            var supply = await _context.Supplies.FindAsync(supplyId);
            if (supply != null)
            {
                supply.SupplierId = supplierId;
                supply.Status = status;
                supply.CurrencyId = currencyId;
                await _context.SaveChangesAsync();
            }
        }

        /// @brief Usuwa dostawę na podstawie identyfikatora.
        /// @param supplyId Identyfikator dostawy.
        public async Task DeleteSupplyAsync(int supplyId)
        {
            var supply = await _context.Supplies.FindAsync(supplyId);
            if (supply != null)
            {
                _context.Supplies.Remove(supply);
                await _context.SaveChangesAsync();
            }
        }

        /// @brief Pobiera zapytanie do wszystkich dostaw z załadowanymi powiązaniami.
        /// @return Zapytanie do bazy danych dla dostaw.
        public IQueryable<Supply> GetAllSuppliesQuery()
        {
            return _context.Supplies
                .Include(s => s.Supplier)         // Ładowanie dostawcy
                .Include(s => s.Currency)         // Ładowanie waluty
                .Include(s => s.SupplyItems)      // Ładowanie elementów dostawy
                .ThenInclude(si => si.Item);      // Ładowanie szczegółów przedmiotu w SupplyItems
        }
    }
}
