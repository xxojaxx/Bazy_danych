using ERPv2.Models;
using ERPv2.Services;
using System;

namespace ERPv2.ConsoleApp
{
    /// @class WarehousemanMenu
    /// @brief Klasa obsługująca menu magazyniera w aplikacji konsolowej ERPv2.
    public class WarehousemanMenu : UserMenu
    {
        public WarehousemanMenu(InventoryService inventoryService, OrderService orderService, OrderItemService orderItemService, SupplyService supplyService, SupplyItemService supplyItemService, UserService userService, UnitService unitService, TypeService typeService)
            : base(null, null, null, null, supplyService, null, null, orderService, orderItemService, supplyItemService, inventoryService, userService, null, unitService, typeService, null)
        {
        }

        public override void ShowMenu()
        {
            Console.WriteLine("=== Menu Magazyniera ===");
            Console.WriteLine("4. Magazyn");
            Console.WriteLine("  4a. Przyjmij dostawę");
            Console.WriteLine("  4b. Wydaj zamówienie");
            Console.WriteLine("  4c. Pokaż stan magazynu");
            Console.WriteLine("6. Mój profil");
            Console.WriteLine("  6a. Zmień dane");
            Console.WriteLine("  6b. Zmień hasło");
            Console.WriteLine("0. Wyloguj");
        }

        public override void HandleOption(string option, User loggedUser)
        {
            switch (option)
            {
                // Opcje menu 4
                case "4a":
                case "4b":
                case "4c":
                    base.HandleOption(option, loggedUser);
                    break;

                // Opcje menu 6
                case "6a":
                case "6b":
                    base.HandleOption(option, loggedUser);
                    break;

                // Wylogowanie
                case "0":
                    Console.WriteLine("Wylogowano.");
                    break;

                // Nieznana opcja
                default:
                    Console.WriteLine("Nieznana opcja. Spróbuj ponownie.");
                    break;
            }
        }
    }
}

