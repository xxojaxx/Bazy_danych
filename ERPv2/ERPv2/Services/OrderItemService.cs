using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPv2.Models;
using Microsoft.EntityFrameworkCore;

/// @file OrderItemService.cs
/// @brief Plik zawiera logikę zarządzania pozycjami zamówień w systemie ERPv2.

namespace ERPv2.Services
{
    /// @class OrderItemService
    /// @brief Odpowiada za zarządzanie pozycjami zamówień w aplikacji.
    public class OrderItemService
    {
        private readonly AppDbContext _context;

        /// @brief Konstruktor klasy OrderItemService.
        /// @param context Kontekst bazy danych.
        public OrderItemService(AppDbContext context)
        {
            _context = context;
        }

        /// @brief Pobiera wszystkie pozycje zamówień.
        /// @return Lista pozycji zamówień.
        public async Task<List<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItems.Include(oi => oi.Order).Include(oi => oi.Item).ToListAsync();
        }

        /// @brief Pobiera pozycję zamówienia na podstawie identyfikatorów.
        /// @param orderId Identyfikator zamówienia.
        /// @param itemId Identyfikator przedmiotu.
        /// @return Pozycja zamówienia lub `null`, jeśli nie znaleziono.
        public async Task<OrderItem?> GetByIdAsync(int orderId, int itemId)
        {
            return await _context.OrderItems
                .Include(oi => oi.Order)
                .Include(oi => oi.Item)
                .FirstOrDefaultAsync(oi => oi.OrderId == orderId && oi.ItemId == itemId);
        }

        /// @brief Dodaje nową pozycję zamówienia.
        /// @param orderItem Obiekt pozycji zamówienia.
        public async Task AddAsync(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
        }

        /// @brief Aktualizuje istniejącą pozycję zamówienia.
        /// @param orderItem Obiekt pozycji zamówienia.
        public async Task UpdateAsync(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();
        }

        /// @brief Usuwa pozycję zamówienia na podstawie identyfikatorów.
        /// @param orderId Identyfikator zamówienia.
        /// @param itemId Identyfikator przedmiotu.
        public async Task DeleteAsync(int orderId, int itemId)
        {
            var orderItem = await GetByIdAsync(orderId, itemId);
            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
