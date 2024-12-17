using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPv2.Models;
using Microsoft.EntityFrameworkCore;

/// @file SupplyItemService.cs
/// @brief Plik zawiera logikę zarządzania pozycjami dostaw w systemie ERPv2.

namespace ERPv2.Services
{
    /// @class SupplyItemService
    /// @brief Odpowiada za zarządzanie pozycjami dostaw w systemie.
    public class SupplyItemService
    {
        private readonly AppDbContext _context;

        /// @brief Konstruktor klasy SupplyItemService.
        /// @param context Kontekst bazy danych.
        public SupplyItemService(AppDbContext context)
        {
            _context = context;
        }

        /// @brief Pobiera wszystkie pozycje dostaw.
        /// @return Lista pozycji dostaw.
        public async Task<List<SupplyItem>> GetAllAsync()
        {
            return await _context.SupplyItems.Include(si => si.Supply).Include(si => si.Item).ToListAsync();
        }

        /// @brief Pobiera pozycję dostawy na podstawie identyfikatorów.
        /// @param supplyId Identyfikator dostawy.
        /// @param itemId Identyfikator przedmiotu.
        /// @return Pozycja dostawy lub `null`, jeśli nie znaleziono.
        public async Task<SupplyItem?> GetByIdAsync(int supplyId, int itemId)
        {
            return await _context.SupplyItems
                .Include(si => si.Supply)
                .Include(si => si.Item)
                .FirstOrDefaultAsync(si => si.SupplyId == supplyId && si.ItemId == itemId);
        }

        /// @brief Dodaje nową pozycję dostawy.
        /// @param supplyItem Obiekt pozycji dostawy.
        public async Task AddAsync(SupplyItem supplyItem)
        {
            _context.SupplyItems.Add(supplyItem);
            await _context.SaveChangesAsync();
        }

        /// @brief Aktualizuje istniejącą pozycję dostawy.
        /// @param supplyItem Obiekt pozycji dostawy.
        public async Task UpdateAsync(SupplyItem supplyItem)
        {
            _context.SupplyItems.Update(supplyItem);
            await _context.SaveChangesAsync();
        }

        /// @brief Usuwa pozycję dostawy na podstawie identyfikatorów.
        /// @param supplyId Identyfikator dostawy.
        /// @param itemId Identyfikator przedmiotu.
        public async Task DeleteAsync(int supplyId, int itemId)
        {
            var supplyItem = await GetByIdAsync(supplyId, itemId);
            if (supplyItem != null)
            {
                _context.SupplyItems.Remove(supplyItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
