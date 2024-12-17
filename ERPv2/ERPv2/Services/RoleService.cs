using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPv2.Models;
using Microsoft.EntityFrameworkCore;

/// @file RoleService.cs
/// @brief Plik zawiera logikę zarządzania rolami w systemie ERPv2.

namespace ERPv2.Services
{
    /// @class RoleService
    /// @brief Odpowiada za zarządzanie rolami użytkowników w systemie.
    public class RoleService
    {
        private readonly AppDbContext _context;

        /// @brief Konstruktor klasy RoleService.
        /// @param context Kontekst bazy danych.
        public RoleService(AppDbContext context)
        {
            _context = context;
        }

        /// @brief Dodaje nową rolę.
        /// @param roleName Nazwa nowej roli.
        public async Task AddRoleAsync(string roleName)
        {
            var role = new Role { Name = roleName };
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        /// @brief Pobiera wszystkie role.
        /// @return Lista ról.
        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        /// @brief Pobiera rolę na podstawie identyfikatora.
        /// @param roleId Identyfikator roli.
        /// @return Obiekt roli lub `null`, jeśli nie znaleziono.
        public async Task<Role> GetRoleByIdAsync(int roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }

        /// @brief Aktualizuje istniejącą rolę.
        /// @param roleId Identyfikator roli.
        /// @param newName Nowa nazwa roli.
        public async Task UpdateRoleAsync(int roleId, string newName)
        {
            var role = await _context.Roles.FindAsync(roleId);
            if (role != null)
            {
                role.Name = newName;
                await _context.SaveChangesAsync();
            }
        }

        /// @brief Usuwa rolę na podstawie identyfikatora.
        /// @param roleId Identyfikator roli.
        public async Task DeleteRoleAsync(int roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
