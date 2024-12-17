using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPv2.Models;
using Microsoft.EntityFrameworkCore;

/// @file BomItemService.cs
/// @brief Plik zawiera logikę zarządzania elementami zestawienia materiałowego (Bill of Materials) w ERPv2.

namespace ERPv2.Services
{
    /// @class BomItemService
    /// @brief Odpowiada za zarządzanie elementami zestawienia materiałowego (BOM).
    public class BomItemService
    {
        private readonly AppDbContext _context;

        /// @brief Konstruktor klasy BomItemService.
        /// @param context Kontekst bazy danych.
        public BomItemService(AppDbContext context)
        {
            _context = context;
        }

        /// @brief Pobiera wszystkie elementy BOM.
        /// @return Lista elementów BOM.
        public async Task<List<BomItem>> GetAllAsync()
        {
            return await _context.BomItems.Include(bi => bi.Item).Include(bi => bi.Bom).ToListAsync();
        }

        /// @brief Pobiera element BOM na podstawie identyfikatorów.
        /// @param bomId Identyfikator zestawienia BOM.
        /// @param itemId Identyfikator przedmiotu.
        /// @return Element BOM lub `null`, jeśli nie znaleziono.
        public async Task<BomItem?> GetByIdAsync(int bomId, int itemId)
        {
            return await _context.BomItems
                .Include(bi => bi.Item)
                .Include(bi => bi.Bom)
                .FirstOrDefaultAsync(bi => bi.BomId == bomId && bi.ItemId == itemId);
        }

        /// @brief Dodaje nowy element BOM.
        /// @param bomItem Obiekt elementu BOM.
        public async Task AddAsync(BomItem bomItem)
        {
            _context.BomItems.Add(bomItem);
            await _context.SaveChangesAsync();
        }

        /// @brief Aktualizuje istniejący element BOM.
        /// @param bomItem Obiekt elementu BOM.
        public async Task UpdateAsync(BomItem bomItem)
        {
            _context.BomItems.Update(bomItem);
            await _context.SaveChangesAsync();
        }

        /// @brief Usuwa element BOM na podstawie identyfikatorów.
        /// @param bomId Identyfikator zestawienia BOM.
        /// @param itemId Identyfikator przedmiotu.
        public async Task DeleteAsync(int bomId, int itemId)
        {
            var bomItem = await GetByIdAsync(bomId, itemId);
            if (bomItem != null)
            {
                _context.BomItems.Remove(bomItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
