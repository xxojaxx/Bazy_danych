using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERPv2.Models;
using Microsoft.EntityFrameworkCore;

/// @file CurrencyService.cs
/// @brief Plik zawiera logikę zarządzania walutami w systemie ERPv2.

namespace ERPv2.Services
{
    /// @class CurrencyService
    /// @brief Odpowiada za zarządzanie walutami w aplikacji.
    public class CurrencyService
    {
        private readonly AppDbContext _context;

        /// @brief Konstruktor klasy CurrencyService.
        /// @param context Kontekst bazy danych.
        public CurrencyService(AppDbContext context)
        {
            _context = context;
        }

        /// @brief Dodaje nową walutę do bazy danych.
        /// @param currencyCode Kod waluty.
        /// @param name Nazwa waluty.
        /// @param symbol Symbol waluty.
        /// @param exchangeRate Kurs wymiany waluty.
        public async Task AddCurrencyAsync(string currencyCode, string name, string symbol, decimal exchangeRate)
        {
            var currency = new Currency
            {
                CurrencyId = currencyCode,
                Name = name,
                Symbol = symbol,
                ExchangeRate = exchangeRate
            };

            _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();
        }

        /// @brief Pobiera wszystkie waluty z bazy danych.
        /// @return Lista walut.
        public async Task<List<Currency>> GetAllCurrenciesAsync()
        {
            return await _context.Currencies.ToListAsync();
        }

        /// @brief Pobiera walutę na podstawie kodu waluty.
        /// @param currencyCode Kod waluty.
        /// @return Waluta lub `null`, jeśli nie znaleziono.
        public async Task<Currency> GetCurrencyByCodeAsync(string currencyCode)
        {
            return await _context.Currencies.FindAsync(currencyCode);
        }

        /// @brief Aktualizuje dane waluty.
        /// @param currencyCode Kod waluty.
        /// @param name Nazwa waluty.
        /// @param symbol Symbol waluty.
        /// @param exchangeRate Kurs wymiany waluty.
        public async Task UpdateCurrencyAsync(string currencyCode, string name, string symbol, decimal exchangeRate)
        {
            var currency = await _context.Currencies.FindAsync(currencyCode);
            if (currency != null)
            {
                currency.Name = name;
                currency.Symbol = symbol;
                currency.ExchangeRate = exchangeRate;
                await _context.SaveChangesAsync();
            }
        }

        /// @brief Usuwa walutę na podstawie kodu waluty.
        /// @param currencyCode Kod waluty.
        public async Task DeleteCurrencyAsync(string currencyCode)
        {
            var currency = await _context.Currencies.FindAsync(currencyCode);
            if (currency != null)
            {
                _context.Currencies.Remove(currency);
                await _context.SaveChangesAsync();
            }
        }
    }
}
