using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPv2.Models;
using Microsoft.EntityFrameworkCore;

/// @file TypeService.cs
/// @brief Plik zawiera logikę zarządzania typami produktów w systemie ERPv2.

namespace ERPv2.Services
{
    /// @class TypeService
    /// @brief Odpowiada za zarządzanie typami produktów w systemie.
    public class TypeService
    {
        private readonly AppDbContext _context;

        /// @brief Konstruktor klasy TypeService.
        /// @param context Kontekst bazy danych.
        public TypeService(AppDbContext context)
        {
            _context = context;
        }

        /// @brief Pobiera wszystkie typy produktów.
        /// @return Lista typów produktów.
        public async Task<List<Models.Type>> GetAllAsync()
        {
            return await _context.Types.ToListAsync();
        }

        /// @brief Pobiera typ produktu na podstawie identyfikatora.
        /// @param id Identyfikator typu produktu.
        /// @return Obiekt typu produktu lub `null`, jeśli nie znaleziono.
        public async Task<Models.Type?> GetByIdAsync(int id)
        {
            return await _context.Types.FirstOrDefaultAsync(t => t.ItemType == id);
        }

        /// @brief Dodaje nowy typ produktu.
        /// @param type Obiekt typu produktu.
        public async Task AddAsync(Models.Type type)
        {
            _context.Types.Add(type);
            await _context.SaveChangesAsync();
        }

        /// @brief Aktualizuje istniejący typ produktu.
        /// @param type Obiekt typu produktu.
        public async Task UpdateAsync(Models.Type type)
        {
            _context.Types.Update(type);
            await _context.SaveChangesAsync();
        }

        /// @brief Usuwa typ produktu na podstawie identyfikatora.
        /// @param id Identyfikator typu produktu.
        public async Task DeleteAsync(int id)
        {
            var type = await GetByIdAsync(id);
            if (type != null)
            {
                _context.Types.Remove(type);
                await _context.SaveChangesAsync();
            }
        }

        /// @brief Pobiera wszystkie typy produktów (metoda dodatkowa).
        /// @return Lista typów produktów.
        public async Task<List<Models.Type>> GetAllTypesAsync()
        {
            return await _context.Types.ToListAsync();
        }
    }
}
