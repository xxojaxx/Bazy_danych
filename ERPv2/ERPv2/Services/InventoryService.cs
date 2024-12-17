using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERPv2.Models;
using Microsoft.EntityFrameworkCore;

/// @file InventoryService.cs
/// @brief Plik zawiera logikę zarządzania stanami magazynowymi w systemie ERPv2.

namespace ERPv2.Services
{
    /// @class InventoryService
    /// @brief Odpowiada za zarządzanie stanami magazynowymi w aplikacji.
    public class InventoryService
    {
        private readonly AppDbContext _context;

        /// @brief Konstruktor klasy InventoryService.
        /// @param context Kontekst bazy danych.
        public InventoryService(AppDbContext context)
        {
            _context = context;
        }

        /// @brief Dodaje nowy stan magazynowy.
        /// @param itemId Identyfikator przedmiotu.
        /// @param quantity Ilość.
        public async Task AddInventoryAsync(int itemId, decimal quantity)
        {
            var inventory = new Inventory
            {
                ItemId = itemId,
                Quantity = quantity
            };

            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
        }

        /// @brief Pobiera wszystkie stany magazynowe.
        /// @return Lista stanów magazynowych.
        public async Task<List<Inventory>> GetAllInventoriesAsync()
        {
            return await _context.Inventories.Include(i => i.Item).ToListAsync();
        }

        /// @brief Pobiera stan magazynowy na podstawie identyfikatora.
        /// @param inventoryId Identyfikator stanu magazynowego.
        /// @return Stan magazynowy lub `null`, jeśli nie znaleziono.
        public async Task<Inventory> GetInventoryByIdAsync(int inventoryId)
        {
            return await _context.Inventories.Include(i => i.Item).FirstOrDefaultAsync(i => i.InventoryId == inventoryId);
        }

        /// @brief Aktualizuje ilość w stanie magazynowym.
        /// @param inventoryId Identyfikator stanu magazynowego.
        /// @param quantity Nowa ilość.
        public async Task UpdateInventoryAsync(int inventoryId, decimal quantity)
        {
            var inventory = await _context.Inventories.FindAsync(inventoryId);
            if (inventory != null)
            {
                inventory.Quantity = quantity;
                await _context.SaveChangesAsync();
            }
        }

        /// @brief Usuwa stan magazynowy na podstawie identyfikatora.
        /// @param inventoryId Identyfikator stanu magazynowego.
        public async Task DeleteInventoryAsync(int inventoryId)
        {
            var inventory = await _context.Inventories.FindAsync(inventoryId);
            if (inventory != null)
            {
                _context.Inventories.Remove(inventory);
                await _context.SaveChangesAsync();
            }
        }
    }
}
