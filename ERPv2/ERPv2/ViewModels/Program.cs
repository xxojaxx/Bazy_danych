using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ERPv2.Models;
using ERPv2.Services;
using ERPv2.ConsoleApp;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ERPv2
{
    class Program
    {
        private static string ReadPassword()
        {
            StringBuilder passwordBuilder = new StringBuilder();
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey(intercept: true);

                if (keyInfo.Key == ConsoleKey.Backspace && passwordBuilder.Length > 0)
                {
                    passwordBuilder.Remove(passwordBuilder.Length - 1, 1);
                    Console.Write("\b \b"); // Usuń ostatnią gwiazdkę z konsoli
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    passwordBuilder.Append(keyInfo.KeyChar);
                    Console.Write("*"); // Wyświetl gwiazdkę dla każdego znaku
                }

            } while (keyInfo.Key != ConsoleKey.Enter); // Koniec po naciśnięciu Enter

            Console.WriteLine();
            return passwordBuilder.ToString();
        }

        static async Task Main(string[] args)
        {
            // Konfiguracja DI i DbContext
            var serviceProvider = new ServiceCollection()
                .AddDbContext<AppDbContext>(options =>
                {
                    options.UseNpgsql("Host=localhost;Database=ERP DB;Username=postgres;Password=password");
                })
                .AddScoped<UserService>() // Rejestracja UserService
                .AddScoped<ItemService>() // Rejestracja ItemService dla ManagerMenu
                .AddScoped<BomService>()
                .AddScoped<BomItemService>()
                .AddScoped<UnitService>()
                .AddScoped<TypeService>()
                .AddScoped<RoleService>()
                .AddScoped<AddressService>()
                .AddScoped<ClientService>()
                .AddScoped<OrderService>()
                .AddScoped<OrderItemService>()
                .AddScoped<SupplierService>()
                .AddScoped<SupplyItemService>()
                .AddScoped<SupplyService>()
                .AddScoped<InventoryService>()
                .AddScoped<CurrencyService>()

                .AddScoped<UserMenu>()
                .AddScoped<ManagerMenu>() // Rejestracja ManagerMenu w DI
                .AddScoped<PlannerMenu>()
                .AddScoped<WarehousemanMenu>()
                .AddScoped<ConsultantMenu>()

                .BuildServiceProvider();

            var userService = serviceProvider.GetRequiredService<UserService>();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Logowanie do systemu ERP ---");
                Console.WriteLine("Wciśnij ESC, aby zakończyć program.");
                Console.Write("Podaj email: ");
                var email = Console.ReadLine();

                if (string.IsNullOrEmpty(email)) continue;

                Console.Write("Podaj hasło: ");
                var password = ReadPassword();

                var loggedUser = await userService.AuthenticateUserAsync(email, password);

                if (loggedUser != null)
                {
                    Console.WriteLine($"\nZalogowano jako: {loggedUser.Name} {loggedUser.Surname}");
                    await ShowUserMenu(loggedUser, serviceProvider);
                }
                else
                {
                    Console.WriteLine("Nieprawidłowe dane logowania. Spróbuj ponownie.");
                }

                Console.WriteLine("\nNaciśnij ESC, aby zakończyć, lub dowolny klawisz, aby spróbować ponownie.");
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

        private static async Task ShowUserMenu(User loggedUser, ServiceProvider serviceProvider)
        {
            string option;

            do
            {
                Console.Clear();

                if (loggedUser.RoleId == 1) // Menadżer
                {
                    var managerMenu = serviceProvider.GetRequiredService<ManagerMenu>();
                    managerMenu.ShowMenu();
                    Console.Write("\nWybierz opcję: ");
                    option = Console.ReadLine();
                    managerMenu.HandleOption(option, loggedUser);
                }
                else if (loggedUser.RoleId == 2) // Planista
                {
                    var plannerMenu = serviceProvider.GetRequiredService<PlannerMenu>();
                    plannerMenu.ShowMenu();
                    Console.Write("\nWybierz opcję: ");
                    option = Console.ReadLine();
                    plannerMenu.HandleOption(option, loggedUser);
                }
                else if (loggedUser.RoleId == 3) // Magazynier
                {
                    var warehousemanMenu = serviceProvider.GetRequiredService<WarehousemanMenu>();
                    warehousemanMenu.ShowMenu();
                    Console.Write("\nWybierz opcję: ");
                    option = Console.ReadLine();
                    warehousemanMenu.HandleOption(option, loggedUser);
                }
                else if (loggedUser.RoleId == 4) // Konsultant
                {
                    var consultantMenu = serviceProvider.GetRequiredService<ConsultantMenu>();
                    consultantMenu.ShowMenu();
                    Console.Write("\nWybierz opcję: ");
                    option = Console.ReadLine();
                    consultantMenu.HandleOption(option, loggedUser);
                }
                else
                {
                    Console.WriteLine("BŁĄD. Niedostępna rola.");
                    option = "0";
                }

                if (option == "0")
                {
                    Console.WriteLine("\nWylogowano. Wciśnij ESC, aby zakończyć program, lub dowolny klawisz, aby zalogować się ponownie.");
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }
                }

            } while (option != "0");
        }
    }
}
