using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERPv2.Models;
using Microsoft.EntityFrameworkCore;

/// @file ItemService.cs
/// @brief Plik zawiera logikę zarządzania przedmiotami w systemie ERPv2.

namespace ERPv2.Services
{
    /// @class ItemService
    /// @brief Odpowiada za zarządzanie przedmiotami w aplikacji.
    public class ItemService
    {
        private readonly AppDbContext _context;

        /// @brief Konstruktor klasy ItemService.
        /// @param context Kontekst bazy danych.
        public ItemService(AppDbContext context)
        {
            _context = context;
        }

        /// @brief Dodaje nowy przedmiot.
        /// @param name Nazwa przedmiotu.
        /// @param description Opis przedmiotu.
        /// @param umId Identyfikator jednostki miary.
        /// @param itemType Typ przedmiotu.
        public async Task AddItemAsync(string name, string description, int umId, int itemType)
        {
            var item = new Item
            {
                Name = name,
                Description = description,
                UmId = umId,
                ItemType = itemType
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();
        }

        /// @brief Pobiera wszystkie przedmioty.
        /// @return Lista przedmiotów.
        public async Task<List<Item>> GetAllItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }

        /// @brief Pobiera przedmiot na podstawie identyfikatora.
        /// @param itemId Identyfikator przedmiotu.
        /// @return Przedmiot lub `null`, jeśli nie znaleziono.
        public async Task<Item> GetItemByIdAsync(int itemId)
        {
            return await _context.Items.FindAsync(itemId);
        }

        /// @brief Aktualizuje dane przedmiotu.
        /// @param itemId Identyfikator przedmiotu.
        /// @param name Nazwa przedmiotu.
        /// @param description Opis przedmiotu.
        /// @param umId Identyfikator jednostki miary.
        /// @param itemType Typ przedmiotu.
        public async Task UpdateItemAsync(int itemId, string name, string description, int umId, int itemType)
        {
            var item = await _context.Items.FindAsync(itemId);
            if (item != null)
            {
                item.Name = name;
                item.Description = description;
                item.UmId = umId;
                item.ItemType = itemType;
                await _context.SaveChangesAsync();
            }
        }

        /// @brief Usuwa przedmiot na podstawie identyfikatora.
        /// @param itemId Identyfikator przedmiotu.
        public async Task DeleteItemAsync(int itemId)
        {
            var item = await _context.Items.FindAsync(itemId);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        /// @brief Pobiera zapytanie do wszystkich przedmiotów z załadowanymi powiązaniami.
        /// @return Zapytanie do bazy danych dla przedmiotów.
        public IQueryable<Item> GetAllItemsQuery()
        {
            return _context.Items
                .Include(i => i.Type)
                .Include(i => i.Unit);
        }
    }
}
