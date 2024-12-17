using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPv2.Models;
using Microsoft.EntityFrameworkCore;

/// @file UnitService.cs
/// @brief Plik zawiera logikę zarządzania jednostkami miary w systemie ERPv2.

namespace ERPv2.Services
{
    /// @class UnitService
    /// @brief Odpowiada za zarządzanie jednostkami miary.
    public class UnitService
    {
        private readonly AppDbContext _context;

        /// @brief Konstruktor klasy UnitService.
        /// @param context Kontekst bazy danych.
        public UnitService(AppDbContext context)
        {
            _context = context;
        }

        /// @brief Pobiera wszystkie jednostki miary.
        /// @return Lista jednostek miary.
        public async Task<List<Unit>> GetAllAsync()
        {
            return await _context.Units.ToListAsync();
        }

        /// @brief Pobiera jednostkę miary na podstawie identyfikatora.
        /// @param id Identyfikator jednostki miary.
        /// @return Obiekt jednostki miary lub `null`, jeśli nie znaleziono.
        public async Task<Unit?> GetByIdAsync(int id)
        {
            return await _context.Units.FirstOrDefaultAsync(u => u.UmId == id);
        }

        /// @brief Dodaje nową jednostkę miary.
        /// @param unit Obiekt jednostki miary.
        public async Task AddAsync(Unit unit)
        {
            _context.Units.Add(unit);
            await _context.SaveChangesAsync();
        }

        /// @brief Aktualizuje istniejącą jednostkę miary.
        /// @param unit Obiekt jednostki miary.
        public async Task UpdateAsync(Unit unit)
        {
            _context.Units.Update(unit);
            await _context.SaveChangesAsync();
        }

        /// @brief Usuwa jednostkę miary na podstawie identyfikatora.
        /// @param id Identyfikator jednostki miary.
        public async Task DeleteAsync(int id)
        {
            var unit = await GetByIdAsync(id);
            if (unit != null)
            {
                _context.Units.Remove(unit);
                await _context.SaveChangesAsync();
            }
        }

        /// @brief Pobiera wszystkie jednostki miary (metoda dodatkowa).
        /// @return Lista jednostek miary.
        public async Task<List<Unit>> GetAllUnitsAsync()
        {
            return await _context.Units.ToListAsync();
        }
    }
}
