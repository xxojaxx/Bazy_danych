using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERPv2.Models;
using Microsoft.EntityFrameworkCore;

/// @file UserService.cs
/// @brief Plik zawiera logikę zarządzania użytkownikami w systemie ERPv2.

namespace ERPv2.Services
{
    /// @class UserService
    /// @brief Odpowiada za zarządzanie użytkownikami w systemie.
    public class UserService
    {
        private readonly AppDbContext _context;

        /// @brief Konstruktor klasy UserService.
        /// @param context Kontekst bazy danych.
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        /// @brief Pobiera użytkownika na podstawie adresu email.
        /// @param email Adres email użytkownika.
        /// @return Obiekt użytkownika lub `null`, jeśli nie znaleziono.
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        /// @brief Dodaje nowego użytkownika.
        /// @param name Imię użytkownika.
        /// @param surname Nazwisko użytkownika.
        /// @param email Adres email użytkownika.
        /// @param password Hasło użytkownika (w postaci zahaszowanej).
        /// @param roleId Identyfikator roli użytkownika.
        public async Task AddUserAsync(string name, string surname, string email, string password, int roleId)
        {
            var user = new User
            {
                Name = name,
                Surname = surname,
                Email = email,
                Password = password,
                RoleId = roleId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        /// @brief Pobiera wszystkich użytkowników.
        /// @return Lista użytkowników.
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }

        /// @brief Pobiera użytkownika na podstawie identyfikatora.
        /// @param userId Identyfikator użytkownika.
        /// @return Obiekt użytkownika lub `null`, jeśli nie znaleziono.
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        /// @brief Aktualizuje istniejącego użytkownika.
        /// @param userId Identyfikator użytkownika.
        /// @param name Imię użytkownika.
        /// @param surname Nazwisko użytkownika.
        /// @param email Adres email użytkownika.
        /// @param password Hasło użytkownika (w postaci zahaszowanej).
        /// @param roleId Identyfikator roli użytkownika.
        public async Task UpdateUserAsync(int userId, string name, string surname, string email, string password, int roleId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.Name = name;
                user.Surname = surname;
                user.Email = email;
                user.Password = password;
                user.RoleId = roleId;

                await _context.SaveChangesAsync();
            }
        }

        /// @brief Usuwa użytkownika na podstawie identyfikatora.
        /// @param userId Identyfikator użytkownika.
        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        /// @brief Uwierzytelnia użytkownika na podstawie adresu email i hasła.
        /// @param email Adres email użytkownika.
        /// @param password Hasło użytkownika.
        /// @return Obiekt użytkownika, jeśli uwierzytelnienie powiedzie się, lub `null` w przeciwnym razie.
        public async Task<User> AuthenticateUserAsync(string email, string password)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        /// @brief Sprawdza, czy adres email jest już używany.
        /// @param email Adres email do sprawdzenia.
        /// @return `true`, jeśli adres email jest już używany, w przeciwnym razie `false`.
        public async Task<bool> IsEmailInUseAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Adres email nie może być pusty.", nameof(email));
            }

            return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }
    }
}
