using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERPv2.Models;
using Microsoft.EntityFrameworkCore;

/// @file OrderService.cs
/// @brief Plik zawiera logikę zarządzania zamówieniami w systemie ERPv2.

namespace ERPv2.Services
{
    /// @class OrderService
    /// @brief Odpowiada za zarządzanie zamówieniami w aplikacji.
    public class OrderService
    {
        private readonly AppDbContext _context;

        /// @brief Konstruktor klasy OrderService.
        /// @param context Kontekst bazy danych.
        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        /// @brief Dodaje nowe zamówienie.
        /// @param clientId Identyfikator klienta.
        /// @param currencyId Identyfikator waluty.
        public async Task AddOrderAsync(int clientId, string currencyId)
        {
            var order = new Order
            {
                ClientId = clientId,
                Status = "Oczekujące",
                CurrencyId = currencyId,
                Date = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        /// @brief Pobiera wszystkie zamówienia.
        /// @return Lista zamówień.
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        /// @brief Pobiera zamówienie na podstawie identyfikatora.
        /// @param orderId Identyfikator zamówienia.
        /// @return Zamówienie lub `null`, jeśli nie znaleziono.
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders.FindAsync(orderId);
        }

        /// @brief Aktualizuje dane zamówienia.
        /// @param orderId Identyfikator zamówienia.
        /// @param clientId Identyfikator klienta.
        /// @param status Status zamówienia.
        /// @param currencyId Identyfikator waluty.
        public async Task UpdateOrderAsync(int orderId, int clientId, string status, string currencyId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.ClientId = clientId;
                order.Status = status;
                order.CurrencyId = currencyId;
                await _context.SaveChangesAsync();
            }
        }

        /// @brief Usuwa zamówienie na podstawie identyfikatora.
        /// @param orderId Identyfikator zamówienia.
        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        /// @brief Pobiera zapytanie do wszystkich zamówień z załadowanymi powiązaniami.
        /// @return Zapytanie do bazy danych dla zamówień.
        public IQueryable<Order> GetAllOrdersQuery()
        {
            return _context.Orders
                .Include(o => o.Client)         // Ładowanie klienta.
                .Include(o => o.Currency)      // Ładowanie waluty.
                .Include(o => o.OrderItems)    // Ładowanie pozycji zamówienia.
                .ThenInclude(oi => oi.Item);   // Ładowanie szczegółów przedmiotów.
        }
    }
}
