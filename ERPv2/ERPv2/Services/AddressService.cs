using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERPv2.Models;
using Microsoft.EntityFrameworkCore;

/// @file AddressService.cs
/// @brief Plik zawiera logikę zarządzania adresami w systemie ERPv2.

namespace ERPv2.Services
{
    /// @class AddressService
    /// @brief Odpowiada za zarządzanie adresami w aplikacji.
    public class AddressService
    {
        private readonly AppDbContext _context;

        /// @brief Konstruktor klasy AddressService.
        /// @param context Kontekst bazy danych.
        public AddressService(AppDbContext context)
        {
            _context = context;
        }

        /// @brief Dodaje nowy adres do bazy danych.
        /// @param country Kraj.
        /// @param postalCode Kod pocztowy.
        /// @param city Miasto.
        /// @param street Ulica.
        public async Task AddAddressAsync(string country, string postalCode, string city, string street)
        {
            var address = new Address
            {
                Country = country,
                PostalCode = postalCode,
                City = city,
                Street = street
            };

            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
        }

        /// @brief Pobiera wszystkie adresy z bazy danych.
        /// @return Lista wszystkich adresów.
        public async Task<List<Address>> GetAllAddressesAsync()
        {
            return await _context.Addresses.ToListAsync();
        }

        /// @brief Pobiera adres na podstawie identyfikatora.
        /// @param addressId Identyfikator adresu.
        /// @return Adres lub `null`, jeśli nie znaleziono.
        public async Task<Address> GetAddressByIdAsync(int addressId)
        {
            return await _context.Addresses.FindAsync(addressId);
        }

        /// @brief Aktualizuje dane istniejącego adresu.
        /// @param addressId Identyfikator adresu.
        /// @param country Kraj.
        /// @param postalCode Kod pocztowy.
        /// @param city Miasto.
        /// @param street Ulica.
        public async Task UpdateAddressAsync(int addressId, string country, string postalCode, string city, string street)
        {
            var address = await _context.Addresses.FindAsync(addressId);
            if (address != null)
            {
                address.Country = country;
                address.PostalCode = postalCode;
                address.City = city;
                address.Street = street;
                await _context.SaveChangesAsync();
            }
        }

        /// @brief Usuwa adres na podstawie identyfikatora.
        /// @param addressId Identyfikator adresu.
        public async Task DeleteAddressAsync(int addressId)
        {
            var address = await _context.Addresses.FindAsync(addressId);
            if (address != null)
            {
                _context.Addresses.Remove(address);
                await _context.SaveChangesAsync();
            }
        }
    }
}
