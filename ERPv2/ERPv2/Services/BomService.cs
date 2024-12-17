using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPv2.Models;
using Microsoft.EntityFrameworkCore;

/// @file BomService.cs
/// @brief Plik zawiera logikę zarządzania zestawieniami materiałowymi (Bill of Materials) w ERPv2.

namespace ERPv2.Services
{
    /// @class BomService
    /// @brief Odpowiada za zarządzanie zestawieniami materiałowymi (BOM).
    public class BomService
    {
        private readonly AppDbContext _context;

        /// @brief Konstruktor klasy BomService.
        /// @param context Kontekst bazy danych.
        public BomService(AppDbContext context)
        {
            _context = context;
        }

        /// @brief Pobiera wszystkie zestawienia BOM.
        /// @return Lista zestawień BOM.
        public async Task<List<Bom>> GetAllAsync()
        {
            return await _context.Boms
                .Include(b => b.BomItems)
                .ThenInclude(bi => bi.Item)
                .ToListAsync();
        }

        /// @brief Pobiera zestawienie BOM na podstawie identyfikatora.
        /// @param bomId Identyfikator zestawienia BOM.
        /// @return Zestawienie BOM lub `null`, jeśli nie znaleziono.
        public async Task<Bom> GetByIdAsync(int bomId)
        {
            return await _context.Boms
                .Include(b => b.BomItems)
                .ThenInclude(bi => bi.Item)
                .FirstOrDefaultAsync(b => b.BomId == bomId);
        }

        /// @brief Dodaje nowe zestawienie BOM.
        /// @param bom Obiekt zestawienia BOM.
        public async Task AddAsync(Bom bom)
        {
            _context.Boms.Add(bom);
            await _context.SaveChangesAsync();
        }

        /// @brief Aktualizuje istniejące zestawienie BOM.
        /// @param bom Obiekt zestawienia BOM.
        public async Task UpdateAsync(Bom bom)
        {
            _context.Boms.Update(bom);
            await _context.SaveChangesAsync();
        }

        /// @brief Usuwa zestawienie BOM na podstawie identyfikatora.
        /// @param id Identyfikator zestawienia BOM.
        public async Task DeleteAsync(int id)
        {
            var bom = await GetByIdAsync(id);
            if (bom != null)
            {
                _context.Boms.Remove(bom);
                await _context.SaveChangesAsync();
            }
        }
    }
}
