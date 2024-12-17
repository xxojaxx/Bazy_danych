using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERPv2.Models;
using Microsoft.EntityFrameworkCore;

/// @file ClientService.cs
/// @brief Plik zawiera logikę zarządzania klientami w systemie ERPv2.

namespace ERPv2.Services
{
    /// @class ClientService
    /// @brief Odpowiada za zarządzanie klientami w aplikacji.
    public class ClientService
    {
        private readonly AppDbContext _context;

        /// @brief Konstruktor klasy ClientService.
        /// @param context Kontekst bazy danych.
        public ClientService(AppDbContext context)
        {
            _context = context;
        }

        /// @brief Dodaje nowego klienta do bazy danych.
        /// @param name Nazwa klienta.
        /// @param addressId Identyfikator adresu klienta.
        public async Task AddClientAsync(string name, int addressId)
        {
            var client = new Client
            {
                Name = name,
                AddressId = addressId
            };

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        /// @brief Pobiera wszystkich klientów.
        /// @return Lista klientów.
        public async Task<List<Client>> GetAllClientsAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        /// @brief Pobiera klienta na podstawie identyfikatora.
        /// @param clientId Identyfikator klienta.
        /// @return Klient lub `null`, jeśli nie znaleziono.
        public async Task<Client> GetClientByIdAsync(int clientId)
        {
            return await _context.Clients.FindAsync(clientId);
        }

        /// @brief Aktualizuje dane klienta.
        /// @param clientId Identyfikator klienta.
        /// @param name Nowa nazwa klienta.
        /// @param addressId Nowy adres klienta.
        public async Task UpdateClientAsync(int clientId, string name, int addressId)
        {
            var client = await _context.Clients.FindAsync(clientId);
            if (client != null)
            {
                client.Name = name;
                client.AddressId = addressId;
                await _context.SaveChangesAsync();
            }
        }

        /// @brief Usuwa klienta na podstawie identyfikatora.
        /// @param clientId Identyfikator klienta.
        public async Task DeleteClientAsync(int clientId)
        {
            var client = await _context.Clients.FindAsync(clientId);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }

        /// @brief Pobiera zapytanie do wszystkich klientów z załadowanymi adresami.
        /// @return Zapytanie do bazy danych dla klientów.
        public IQueryable<Client> GetAllClientsQuery()
        {
            return _context.Clients.Include(c => c.Address);
        }
    }
}
