using ERPv2.Models;
using ERPv2.Services;
using System;

namespace ERPv2.ConsoleApp
{
    /// @class PlannerMenu
    /// @brief Klasa obsługująca menu planisty w aplikacji konsolowej ERPv2.
    public class PlannerMenu : UserMenu
    {
        public PlannerMenu(ItemService itemService, BomService bomService, BomItemService bomItemService, SupplierService supplierService, SupplyService supplyService, SupplyItemService supplyItemService, AddressService addressService, UserService userService, UnitService unitService, TypeService typeService, CurrencyService currencyService)
            : base(itemService, bomService, bomItemService, supplierService, supplyService, null, addressService, null, null, supplyItemService, null, userService, null, unitService, typeService, currencyService)
        {
        }

        public override void ShowMenu()
        {
            Console.WriteLine("=== Menu Planisty ===");
            Console.WriteLine("1. Produkty");
            Console.WriteLine("  1a. Dodaj nowy produkt");
            Console.WriteLine("  1b. Stwórz zestawienie materiałowe");
            Console.WriteLine("  1c. Wykaz produktów");
            Console.WriteLine("  1d. Wykaz zestawień materiałowych");
            Console.WriteLine("  1e. Zmień dane przedmiotu");
            Console.WriteLine("  1f. Usuń zestawienie materiałowe (BOM)");
            Console.WriteLine("  1g. Modyfikuj zestawienie materiałowe (BOM)");
            Console.WriteLine("  1h. Wykaz zestawień materiałowych");
            Console.WriteLine("2. Dostawy");
            Console.WriteLine("  2a. Dodaj nowego dostawcę");
            Console.WriteLine("  2b. Zaplanuj dostawę");
            Console.WriteLine("  2c. Wykaz dostawców");
            Console.WriteLine("  2d. Wykaz dostaw");
            Console.WriteLine("  2e. Usuń dostawcę");
            Console.WriteLine("3. Zamówienia");
            Console.WriteLine("  3a. Dodaj nowego klienta");
            Console.WriteLine("  3b. Zaplanuj zamówienie");
            Console.WriteLine("  3c. Wykaz klientów");
            Console.WriteLine("  3d. Wykaz zamówień");
            Console.WriteLine("  3e. Usuń klienta");
            Console.WriteLine("  3f. Modyfikuj klienta");
            Console.WriteLine("  2f. Modyfikuj dostawcę");
            Console.WriteLine("6. Mój profil");
            Console.WriteLine("  6a. Zmień dane");
            Console.WriteLine("  6b. Zmień hasło");
            Console.WriteLine("0. Wyloguj");
        }

        public override void HandleOption(string option, User loggedUser)
        {
            switch (option)
            {
                // Opcje menu 1
                case "1a":
                case "1b":
                case "1c":
                case "1d":
                case "1e":
                case "1f":
                case "1g":
                case "1h":
                    base.HandleOption(option, loggedUser);
                    break;

                // Opcje menu 2
                case "2a":
                case "2b":
                case "2c":
                case "2d":
                case "2e":
                case "2f":
                    base.HandleOption(option, loggedUser);
                    break;
                // Opcje menu 3
                case "3a":
                case "3b":
                case "3c":
                case "3d":
                case "3e":
                case "3f":
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
