using ERPv2.Models;
using ERPv2.Services;
using System;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ERPv2.ConsoleApp
{
    /// @class UserMenu
    /// @brief Klasa obsługująca menu użytkownika w aplikacji konsolowej ERPv2.
    public class UserMenu
    {
        /// @brief Odczytuje hasło z konsoli z zachowaniem bezpieczeństwa (gwiazdki zamiast znaków).
        /// @return Wprowadzone hasło.
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

        /// @brief Konstruktor klasy UserMenu.
        /// @param itemService Serwis obsługujący produkty.
        /// @param bomService Serwis obsługujący zestawienia materiałowe (BOM).
        /// @param bomItemService Serwis obsługujący elementy zestawień materiałowych (BOM).
        /// @param supplierService Serwis obsługujący dostawców.
        /// @param supplyService Serwis obsługujący dostawy.
        /// @param clientService Serwis obsługujący klientów.
        /// @param addressService Serwis obsługujący adresy.
        /// @param orderService Serwis obsługujący zamówienia.
        /// @param orderItemService Serwis obsługujący elementy zamówień.
        /// @param supplyItemService Serwis obsługujący elementy dostaw.
        /// @param inventoryService Serwis obsługujący magazyn.
        /// @param userService Serwis obsługujący użytkowników.
        /// @param roleService Serwis obsługujący role użytkowników.
        /// @param unitService Serwis obsługujący jednostki miary.
        /// @param typeService Serwis obsługujący typy produktów.
        /// @param currencyService Serwis obsługujący waluty.

        protected readonly ItemService _itemService;
        protected readonly BomService _bomService;
        protected readonly BomItemService _bomItemService;
        protected readonly SupplierService _supplierService;
        protected readonly SupplyService _supplyService;
        protected readonly AddressService _addressService;
        protected readonly ClientService _clientService;
        protected readonly OrderService _orderService;
        protected readonly OrderItemService _orderItemService;
        protected readonly InventoryService _inventoryService;
        protected readonly SupplyItemService _supplyItemService;
        protected readonly UserService _userService;
        protected readonly RoleService _roleService;
        protected readonly UnitService _unitService;
        protected readonly TypeService _typeService;
        protected readonly CurrencyService _currencyService;
        private object userService;

        public UserMenu(ItemService itemService, BomService bomService, BomItemService bomItemService, 
            SupplierService supplierService, SupplyService supplyService, ClientService clientService, AddressService addressService,
            OrderService orderService, OrderItemService orderItemService, SupplyItemService supplyItemService, InventoryService inventoryService,
            UserService userService, RoleService roleService, UnitService unitService, TypeService typeService, CurrencyService currencyService)
        {
            _itemService = itemService;
            _bomService = bomService;
            _bomItemService = bomItemService;
            _supplyService = supplyService;
            _supplierService = supplierService;
            _clientService = clientService;
            _addressService = addressService;
            _orderService = orderService;
            _orderItemService = orderItemService;
            _supplyItemService = supplyItemService;
            _inventoryService = inventoryService;
            _userService = userService;
            _roleService = roleService;
            _userService = userService;
            _typeService = typeService;
            _unitService = unitService;
            _currencyService = currencyService;
        }

        /// @brief Sprawdza poprawność wprowadzonego adresu e-mail.
        /// @param email Adres e-mail do sprawdzenia.
        /// @return True, jeśli adres jest poprawny, w przeciwnym razie False.
        static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// @brief Wyświetla główne menu użytkownika.
        public virtual void ShowMenu()
        {
            Console.WriteLine("=== Menu ===");
            Console.WriteLine("1. Produkty");
            Console.WriteLine("  1a. Dodaj nowy produkt");
            Console.WriteLine("  1b. Stwórz zestawienie materiałowe");
            Console.WriteLine("  1c. Wykaz produktów");
            Console.WriteLine("  1d. Usuń przedmiot");
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
            Console.WriteLine("  2f. Modyfikuj dostawcę");
            Console.WriteLine("3. Zamówienia");
            Console.WriteLine("  3a. Dodaj nowego klienta");
            Console.WriteLine("  3b. Zaplanuj zamówienie");
            Console.WriteLine("  3c. Wykaz klientów");
            Console.WriteLine("  3d. Wykaz zamówień");
            Console.WriteLine("  3e. Usuń klienta");
            Console.WriteLine("  3f. Modyfikuj klienta");
            Console.WriteLine("4. Magazyn");
            Console.WriteLine("  4a. Przyjmij dostawę");
            Console.WriteLine("  4b. Wydaj zamówienie");
            Console.WriteLine("  4c. Pokaż stan magazynu");
            Console.WriteLine("5. Użytkownicy");
            Console.WriteLine("  5a. Dodaj nowego użytkownika");
            Console.WriteLine("  5b. Zmień rolę użytkownika");
            Console.WriteLine("  5c. Wykaz użytkowników");
            Console.WriteLine("  5d. Usuń użytkownika");
            Console.WriteLine("6. Mój profil");
            Console.WriteLine("  6a. Zmień dane");
            Console.WriteLine("  6b. Zmień hasło");
            Console.WriteLine("0. Wyloguj");
        }

        /// @brief Obsługuje opcję wybraną przez użytkownika.
        /// @param option Wybrana opcja z menu.
        /// @param loggedUser Obiekt zalogowanego użytkownika.
        public virtual void HandleOption(string option, User loggedUser)
        {
            switch (option)
            {
                case "1a":
                    AddProduct().Wait();
                    break;
                case "1b":
                    CreateBom().Wait();
                    break;
                case "1c":
                    ShowProducts().Wait();
                    break;
                case "1d":
                    DeleteProduct().Wait();
                    break;
                case "1e":
                    UpdateProduct().Wait();
                    break;
                case "1f":
                    DeleteBom().Wait();
                    break;
                case "1g":
                    ModifyBom().Wait();
                    break;
                case "1h":
                    ShowBoms().Wait();
                    break;
                case "2a":
                    AddSupplier().Wait();
                    break;
                case "2b":
                    PlanDelivery().Wait();
                    break;
                case "2c":
                    ShowSuppliers().Wait();
                    break;
                case "2d":
                    ShowDeliveries().Wait();
                    break;
                case "2e":
                    DeleteSupplier().Wait();
                    break;
                case "2f":
                    UpdateSupplier().Wait();
                    break;
                case "3a":
                    AddClient().Wait();
                    break;
                case "3b":
                    PlanOrder().Wait();
                    break;
                case "3c":
                    ShowClients().Wait();
                    break;
                case "3d":
                    ShowOrders().Wait();
                    break;
                case "3e":
                    DeleteClient().Wait();
                    break;
                case "3f":
                    UpdateClient().Wait();
                    break;
                case "4a":
                    ReceiveDelivery().Wait();
                    break;
                case "4b":
                    IssueOrder().Wait();
                    break;
                case "4c":
                    ShowInventory().Wait();
                    break;
                case "5a":
                    AddUser().Wait();
                    break;
                case "5b":
                    ChangeUserRole().Wait();
                    break;
                case "5c":
                    ShowUsers().Wait();
                    break;
                case "5d":
                    DeleteUser().Wait();
                    break;
                case "6a":
                    EditProfile(loggedUser).Wait();
                    break;
                case "6b":
                    ChangePassword(loggedUser).Wait();
                    break;
                case "0":
                    Console.WriteLine("Wylogowano.");
                    break;
                default:
                    Console.WriteLine("Nieznana opcja.");
                    break;
            }
        }

        /// @brief Dodaje nowy produkt do systemu.
        /// @details Pobiera dane produktu od użytkownika, takie jak nazwa, opis, jednostka miary i typ.
        ///          Następnie zapisuje produkt w bazie danych.
        protected async Task AddProduct()
        {
            Console.WriteLine("=== Dodaj nowy produkt ===");

            // Podanie nazwy produktu
            Console.Write("Podaj nazwę produktu: ");
            string name = Console.ReadLine();

            Console.Write("Podaj opis produktu: ");
            string description = Console.ReadLine();

            // Sprawdzanie jednostki miary (UmId)
            Console.WriteLine("Dostępne jednostki miary:");
            var units = await _unitService.GetAllUnitsAsync();
            foreach (var unit in units)
            {
                Console.WriteLine($"ID: {unit.UmId}, Nazwa: {unit.Name}");
            }

            int umId;
            while (true)
            {
                Console.Write("Podaj ID jednostki miary (UmId): ");
                if (int.TryParse(Console.ReadLine(), out umId) && units.Any(u => u.UmId == umId))
                {
                    break;
                }
                Console.WriteLine("Nieprawidłowy ID jednostki. Spróbuj ponownie.");
            }

            // Sprawdzanie typu produktu (ItemType)
            Console.WriteLine("Dostępne typy produktów:");
            var types = await _typeService.GetAllTypesAsync();
            foreach (var type in types)
            {
                Console.WriteLine($"ID: {type.ItemType}, Opis: {type.Description}");
            }

            int itemType;
            while (true)
            {
                Console.Write("Podaj typ produktu (ItemType): ");
                if (int.TryParse(Console.ReadLine(), out itemType) && types.Any(t => t.ItemType == itemType))
                {
                    break;
                }
                Console.WriteLine("Nieprawidłowy ID typu. Spróbuj ponownie.");
            }

            // Dodanie produktu
            await _itemService.AddItemAsync(name, description, umId, itemType);

            Console.WriteLine("Produkt został dodany do bazy danych.");
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }


        /// @brief Tworzy nowe zestawienie materiałowe (BOM).
        /// @details Pobiera nazwę zestawienia materiałowego i elementy wchodzące w jego skład.
        ///          Zapisuje dane w bazie danych za pomocą odpowiednich serwisów.
        protected async Task CreateBom()
        {
            Console.WriteLine("=== Stwórz zestawienie materiałowe (BOM) ===");

            // Pobranie nazwy BOM od użytkownika
            Console.Write("Podaj nazwę BOM: ");
            string bomName = Console.ReadLine();

            // Tworzenie nowego BOM
            var bom = new Bom
            {
                Name = bomName
            };

            // Dodanie BOM do bazy danych za pomocą BomService
            await _bomService.AddAsync(bom);
            Console.WriteLine($"Stworzono BOM: {bomName}");

            // Pobranie ID nowo dodanego BOM (zakładamy, że BomId jest generowany automatycznie)
            bom = (await _bomService.GetAllAsync()).Last(); // Pobiera ostatnio dodany BOM

            Console.WriteLine("Dodawanie przedmiotów do BOM...");
            string addMore;
            do
            {
                // Pobranie szczegółów dotyczących Item
                Console.Write("Podaj ID przedmiotu (ItemId): ");
                int itemId = int.Parse(Console.ReadLine());

                Console.Write("Podaj ilość (Quantity): ");
                decimal quantity = decimal.Parse(Console.ReadLine());

                // Tworzenie BomItem
                var bomItem = new BomItem
                {
                    BomId = bom.BomId,
                    ItemId = itemId,
                    Quantity = quantity
                };

                // Dodanie BomItem do bazy danych za pomocą BomItemService
                await _bomItemService.AddAsync(bomItem);
                Console.WriteLine($"Dodano przedmiot o ID {itemId} w ilości {quantity} do BOM: {bomName}");

                // Pytanie użytkownika, czy chce dodać kolejny przedmiot
                Console.Write("Czy chcesz dodać kolejny przedmiot? (tak/nie): ");
                addMore = Console.ReadLine().ToLower();

            } while (addMore == "tak");

            Console.WriteLine($"Zestawienie materiałowe '{bomName}' zostało zapisane w bazie danych.");
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Wyświetla listę dostępnych produktów.
        /// @details Pozwala na filtrowanie wyników według typu, jednostki miary lub ID.
        ///          Obsługuje paginację, aby wyświetlać wyniki w częściach.
        protected async Task ShowProducts()
        {
            int pageNumber = 1;
            const int pageSize = 10; // Maksymalnie 10 produktów na stronę

            // Filtry ustawiane na początku
            string typeFilter = null;
            int? idFilter = null;
            string unitFilter = null;

            // Ustawienie flagi do kontroli, czy należy pytać o filtry
            bool askForFilters = true;

            while (true)
            {
                Console.WriteLine("\n=== Lista produktów ===");

                // Pytanie o filtry tylko, gdy użytkownik tego zażąda
                if (askForFilters)
                {
                    Console.Write("Filtruj po typie (wpisz nazwę typu lub ENTER, aby pominąć): ");
                    typeFilter = Console.ReadLine();

                    Console.Write("Filtruj po ID produktu (wpisz ID lub ENTER, aby pominąć): ");
                    string idFilterInput = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(idFilterInput))
                    {
                        if (int.TryParse(idFilterInput, out int parsedId))
                        {
                            idFilter = parsedId;
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowy ID produktu. Spróbuj ponownie.");
                            continue;
                        }
                    }


                    Console.Write("Filtruj po jednostce (wpisz nazwę jednostki lub ENTER, aby pominąć): ");
                    unitFilter = Console.ReadLine();

                    // Po ustawieniu filtrów wyłącz flagę
                    askForFilters = false;
                }

                // Pobierz produkty z bazy danych z uwzględnieniem filtrów
                var query = _itemService.GetAllItemsQuery();

                if (!string.IsNullOrEmpty(typeFilter))
                {
                    query = query
                        .Include(i => i.Type)
                        .AsEnumerable() // Wymuszenie na klienta filtrowania po typie
                        .Where(i => i.Type.Description.Contains(typeFilter, StringComparison.OrdinalIgnoreCase))
                        .AsQueryable();
                }

                if (idFilter.HasValue)
                {
                    query = query.Where(i => i.ItemId.ToString().StartsWith(idFilter.Value.ToString()));
                }

                if (!string.IsNullOrEmpty(unitFilter))
                {
                    query = query
                        .Include(i => i.Unit)
                        .AsEnumerable() // Wymuszenie na klienta filtrowania po jednostce
                        .Where(i => i.Unit.Name.Contains(unitFilter, StringComparison.OrdinalIgnoreCase))
                        .AsQueryable();
                }

                // Zastosowanie paginacji
                var totalItems = query.Count();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                var items = query
                    .OrderBy(i => i.ItemId) // Sortowanie po ID
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // Wyświetlenie produktów
                Console.WriteLine($"Strona {pageNumber}/{totalPages} (łącznie: {totalItems} produktów)");
                foreach (var item in items)
                {
                    Console.WriteLine($"ID: {item.ItemId}, Nazwa: {item.Name}, Typ: {item.Type.Description}, Jednostka: {item.Unit.Name}");
                }

                // Wyświetlenie opcji
                Console.WriteLine("\nOpcje:");
                Console.WriteLine("1. Następna strona");
                Console.WriteLine("2. Poprzednia strona");
                Console.WriteLine("3. Zmień filtry");
                Console.WriteLine("0. Wyjście");

                Console.Write("Wybierz opcję: ");
                var option = Console.ReadLine();

                if (option == "1" && pageNumber < totalPages)
                {
                    pageNumber++;
                }
                else if (option == "2" && pageNumber > 1)
                {
                    pageNumber--;
                }
                else if (option == "3")
                {
                    // Ustaw flagę, aby ponownie zapytać o filtry
                    askForFilters = true;
                    pageNumber = 1; // Zresetuj paginację
                }
                else if (option == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Nieznana opcja. Spróbuj ponownie.");
                }
            }
        }

        /// @brief Usuwa wybrany produkt.
        /// @details Pobiera ID produktu od użytkownika, sprawdza jego istnienie,
        ///          a następnie usuwa go z bazy danych.
        protected async Task DeleteProduct()
        {
            Console.WriteLine("\n=== Usuń przedmiot ===");
            Console.Write("Podaj ID przedmiotu do usunięcia: ");
            if (int.TryParse(Console.ReadLine(), out int itemId))
            {
                var item = await _itemService.GetItemByIdAsync(itemId);
                if (item != null)
                {
                    await _itemService.DeleteItemAsync(itemId);
                    Console.WriteLine($"Przedmiot o ID {itemId} został usunięty.");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono przedmiotu o podanym ID.");
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowe ID.");
            }
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Aktualizuje dane istniejącego produktu.
        /// @details Pozwala użytkownikowi zmienić nazwę, opis, jednostkę miary oraz typ produktu.
        ///          Zmiany są zapisywane w bazie danych.
        protected async Task UpdateProduct()
        {
            Console.WriteLine("=== Zmień dane istniejącego produktu ===");

            // Pobranie ID produktu
            Console.Write("Podaj ID produktu do zmiany: ");
            if (!int.TryParse(Console.ReadLine(), out int itemId))
            {
                Console.WriteLine("Nieprawidłowy ID produktu.");
                return;
            }

            // Sprawdzanie, czy produkt istnieje
            var item = await _itemService.GetItemByIdAsync(itemId);
            if (item == null)
            {
                Console.WriteLine("Produkt o podanym ID nie istnieje.");
                return;
            }

            Console.Write($"Aktualna nazwa: {item.Name}, nowa nazwa (ENTER, aby zachować): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                item.Name = newName;
            }

            Console.Write($"Aktualny opis: {item.Description}, nowy opis (ENTER, aby zachować): ");
            string newDescription = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDescription))
            {
                item.Description = newDescription;
            }

            // Sprawdzanie jednostki miary (UmId)
            Console.WriteLine("Dostępne jednostki miary:");
            var units = await _unitService.GetAllUnitsAsync();
            foreach (var unit in units)
            {
                Console.WriteLine($"ID: {unit.UmId}, Nazwa: {unit.Name}");
            }

            Console.Write($"Aktualna jednostka miary: {item.UmId}, nowa (ENTER, aby zachować): ");
            string umInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(umInput))
            {
                if (int.TryParse(umInput, out int umId) && units.Any(u => u.UmId == umId))
                {
                    item.UmId = umId;
                }
                else
                {
                    Console.WriteLine("Nieprawidłowy ID jednostki. Zmiana jednostki pominięta.");
                }
            }

            // Sprawdzanie typu produktu (ItemType)
            Console.WriteLine("Dostępne typy produktów:");
            var types = await _typeService.GetAllTypesAsync();
            foreach (var type in types)
            {
                Console.WriteLine($"ID: {type.ItemType}, Opis: {type.Description}");
            }

            Console.Write($"Aktualny typ: {item.ItemType}, nowy (ENTER, aby zachować): ");
            string typeInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(typeInput))
            {
                if (int.TryParse(typeInput, out int itemType) && types.Any(t => t.ItemType == itemType))
                {
                    item.ItemType = itemType;
                }
                else
                {
                    Console.WriteLine("Nieprawidłowy ID typu. Zmiana typu pominięta.");
                }
            }

            // Aktualizacja danych w bazie
            await _itemService.UpdateItemAsync(item.ItemId, item.Name, item.Description, item.UmId, item.ItemType);

            Console.WriteLine("Dane produktu zostały zaktualizowane.");
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Usuwa zestawienie materiałowe (BOM).
        /// @details Pobiera ID zestawienia materiałowego i usuwa je z bazy danych.
        protected async Task DeleteBom()
        {
            Console.WriteLine("\n=== Usuń zestawienie materiałowe (BOM) ===");
            Console.Write("Podaj ID BOM do usunięcia: ");
            if (int.TryParse(Console.ReadLine(), out int bomId))
            {
                var bom = await _bomService.GetByIdAsync(bomId);
                if (bom != null)
                {
                    await _bomService.DeleteAsync(bomId);
                    Console.WriteLine($"BOM o ID {bomId} został usunięty.");
                }
                else
                {
                    Console.WriteLine("Nie znaleziono BOM o podanym ID.");
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowe ID.");
            }
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Modyfikuje istniejące zestawienie materiałowe (BOM).
        /// @details Pozwala użytkownikowi dodać, usunąć lub zmienić ilość elementów wchodzących w skład BOM.
        protected async Task ModifyBom()
        {
            Console.WriteLine("=== Modyfikacja zestawienia materiałowego (BOM) ===");

            // Pobranie dostępnych BOM-ów
            var boms = await _bomService.GetAllAsync();
            if (!boms.Any())
            {
                Console.WriteLine("Brak dostępnych zestawień materiałowych.");
                return;
            }

            // Wyświetlenie listy BOM-ów
            Console.WriteLine("\nDostępne zestawienia materiałowe:");
            foreach (var bom in boms)
            {
                Console.WriteLine($"ID: {bom.BomId}, Nazwa: {bom.Name}");
            }

            // Wybór BOM-u do modyfikacji
            Console.Write("Podaj ID zestawienia materiałowego do modyfikacji: ");
            if (!int.TryParse(Console.ReadLine(), out int bomId) || !boms.Any(b => b.BomId == bomId))
            {
                Console.WriteLine("Nieprawidłowy ID zestawienia materiałowego.");
                return;
            }

            var bomToModify = await _bomService.GetByIdAsync(bomId);
            if (bomToModify == null)
            {
                Console.WriteLine("Nie znaleziono zestawienia materiałowego o podanym ID.");
                return;
            }

            string action;
            do
            {
                Console.WriteLine($"\nModyfikacja zestawienia materiałowego: {bomToModify.Name}");
                Console.WriteLine("Przedmioty w zestawieniu:");
                foreach (var bomItem in bomToModify.BomItems)
                {
                    Console.WriteLine($"ID Przedmiotu: {bomItem.ItemId}, Nazwa: {bomItem.Item.Name}, Ilość: {bomItem.Quantity}");
                }

                // Opcje modyfikacji
                Console.WriteLine("\nOpcje:");
                Console.WriteLine("1. Dodaj przedmiot do zestawienia");
                Console.WriteLine("2. Zmień ilość przedmiotu w zestawieniu");
                Console.WriteLine("3. Usuń przedmiot z zestawienia");
                Console.WriteLine("0. Zakończ modyfikację");

                Console.Write("Wybierz opcję: ");
                action = Console.ReadLine();

                switch (action)
                {
                    case "1": // Dodawanie przedmiotu
                        Console.Write("Podaj ID przedmiotu do dodania: ");
                        if (!int.TryParse(Console.ReadLine(), out int newItemId))
                        {
                            Console.WriteLine("Nieprawidłowy ID przedmiotu.");
                            break;
                        }

                        // Sprawdzenie, czy przedmiot istnieje w bazie
                        var itemToAdd = await _itemService.GetItemByIdAsync(newItemId);
                        if (itemToAdd == null)
                        {
                            Console.WriteLine("Przedmiot o podanym ID nie istnieje w bazie danych.");
                            break;
                        }

                        // Sprawdzenie, czy przedmiot już istnieje w BOM
                        if (bomToModify.BomItems.Any(bi => bi.ItemId == newItemId))
                        {
                            Console.WriteLine("Przedmiot o podanym ID już istnieje w zestawieniu materiałowym.");
                            break;
                        }

                        // Dodanie nowego przedmiotu do BOM
                        Console.Write("Podaj ilość przedmiotu: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal newQuantity) || newQuantity <= 0)
                        {
                            Console.WriteLine("Nieprawidłowa ilość.");
                            break;
                        }

                        var newBomItem = new BomItem
                        {
                            BomId = bomToModify.BomId,
                            ItemId = newItemId,
                            Quantity = newQuantity
                        };

                        await _bomItemService.AddAsync(newBomItem);
                        bomToModify = await _bomService.GetByIdAsync(bomId); // Odświeżenie danych
                        Console.WriteLine("Przedmiot został dodany do zestawienia materiałowego.");
                        break;

                    case "2": // Zmiana ilości
                        Console.Write("Podaj ID przedmiotu do zmiany ilości: ");
                        if (!int.TryParse(Console.ReadLine(), out int itemToUpdateId) ||
                            !bomToModify.BomItems.Any(bi => bi.ItemId == itemToUpdateId))
                        {
                            Console.WriteLine("Nie znaleziono przedmiotu o podanym ID w zestawieniu materiałowym.");
                            break;
                        }

                        Console.Write("Podaj nową ilość: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal updatedQuantity) || updatedQuantity <= 0)
                        {
                            Console.WriteLine("Nieprawidłowa ilość.");
                            break;
                        }

                        var bomItemToUpdate = bomToModify.BomItems.First(bi => bi.ItemId == itemToUpdateId);
                        bomItemToUpdate.Quantity = updatedQuantity;
                        await _bomItemService.UpdateAsync(bomItemToUpdate);
                        bomToModify = await _bomService.GetByIdAsync(bomId); // Odświeżenie danych
                        Console.WriteLine("Ilość przedmiotu została zaktualizowana.");
                        break;

                    case "3": // Usuwanie przedmiotu
                        Console.Write("Podaj ID przedmiotu do usunięcia: ");
                        if (!int.TryParse(Console.ReadLine(), out int itemToRemoveId) ||
                            !bomToModify.BomItems.Any(bi => bi.ItemId == itemToRemoveId))
                        {
                            Console.WriteLine("Nie znaleziono przedmiotu o podanym ID w zestawieniu materiałowym.");
                            break;
                        }

                        await _bomItemService.DeleteAsync(bomToModify.BomId, itemToRemoveId);
                        bomToModify = await _bomService.GetByIdAsync(bomId); // Odświeżenie danych
                        Console.WriteLine("Przedmiot został usunięty z zestawienia materiałowego.");
                        break;

                    case "0": // Zakończ modyfikację
                        Console.WriteLine("Zakończono modyfikację zestawienia materiałowego.");
                        break;

                    default:
                        Console.WriteLine("Nieznana opcja. Spróbuj ponownie.");
                        break;
                }

            } while (action != "0");
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Wyświetla listę zestawień materiałowych (BOM).
        /// @details Prezentuje szczegółowe informacje o każdym BOM, w tym o elementach wchodzących w jego skład.
        protected async Task ShowBoms()
        {
            try
            {
                Console.WriteLine("\n=== Wykaz zestawień materiałowych (BOM) ===");

                // Pobranie wszystkich BOM-ów z załadowanymi relacjami
                var boms = await _bomService.GetAllAsync();

                if (boms == null || !boms.Any())
                {
                    Console.WriteLine("Brak zestawień materiałowych w systemie.");
                    Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
                    Console.ReadLine();
                    return;
                }

                foreach (var bom in boms)
                {
                    Console.WriteLine($"\nBOM ID: {bom.BomId}, Nazwa: {bom.Name}");

                    if (bom.BomItems != null && bom.BomItems.Any())
                    {
                        Console.WriteLine("Przedmioty w zestawieniu:");
                        foreach (var bomItem in bom.BomItems)
                        {
                            var itemName = bomItem.Item?.Name ?? "Nieznany przedmiot";
                            Console.WriteLine($"- ID Przedmiotu: {bomItem.ItemId}, Nazwa: {itemName}, Ilość: {bomItem.Quantity}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Brak przedmiotów w tym zestawieniu.");
                    }
                }

                Console.WriteLine("\nKoniec wykazu. Naciśnij ENTER, aby kontynuować...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd podczas pobierania zestawień materiałowych:");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
                Console.ReadLine();
            }
        }

        /// @brief Dodaje nowego dostawcę do systemu.
        /// @details Pobiera dane dostawcy i jego adresu od użytkownika, a następnie zapisuje je w bazie danych.
        protected async Task AddSupplier()
        {
            Console.WriteLine("=== Dodaj nowego dostawcę ===");

            // Pobieranie danych adresowych od użytkownika
            Console.Write("Podaj kraj: ");
            string country = Console.ReadLine();

            Console.Write("Podaj kod pocztowy: ");
            string postalCode = Console.ReadLine();

            Console.Write("Podaj miasto: ");
            string city = Console.ReadLine();

            Console.Write("Podaj ulicę: ");
            string street = Console.ReadLine();

            // Tworzenie nowego adresu w bazie danych
            var addressService = new AddressService(new AppDbContext(new DbContextOptions<AppDbContext>()));
            await addressService.AddAddressAsync(country, postalCode, city, street);

            // Pobranie ID nowo dodanego adresu
            var allAddresses = await addressService.GetAllAddressesAsync();
            var address = allAddresses.Last(); // Zakładamy, że ostatni dodany adres jest nasz

            // Pobieranie danych dostawcy od użytkownika
            Console.Write("Podaj nazwę dostawcy: ");
            string supplierName = Console.ReadLine();

            // Tworzenie nowego dostawcy w bazie danych
            var supplierService = new SupplierService(new AppDbContext(new DbContextOptions<AppDbContext>()));
            await supplierService.AddSupplierAsync(supplierName, address.AddressId);

            Console.WriteLine($"Dodano nowego dostawcę: {supplierName} z adresem {street}, {city}, {postalCode}, {country}");
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Planowanie nowej dostawy.
        /// @details Pobiera dane dostawy, takie jak dostawca, waluta i przedmioty.
        ///          Dodaje dostawę do systemu oraz zapisuje jej elementy.
        protected async Task PlanDelivery()
        {
            Console.WriteLine("=== Planowanie nowej dostawy ===");

            var supplierService = new SupplierService(new AppDbContext(new DbContextOptions<AppDbContext>()));
            var currencyService = new CurrencyService(new AppDbContext(new DbContextOptions<AppDbContext>()));
            var itemService = new ItemService(new AppDbContext(new DbContextOptions<AppDbContext>()));
            var supplyService = new SupplyService(new AppDbContext(new DbContextOptions<AppDbContext>()));
            var supplyItemService = new SupplyItemService(new AppDbContext(new DbContextOptions<AppDbContext>()));

            // Pobranie listy dostawców
            var suppliers = await supplierService.GetAllSuppliersAsync();
            if (!suppliers.Any())
            {
                Console.WriteLine("Brak dostępnych dostawców.");
                return;
            }

            Console.WriteLine("Dostępni dostawcy:");
            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"ID: {supplier.SupplierId}, Nazwa: {supplier.Name}");
            }

            // Wybór dostawcy z walidacją
            int supplierId;
            while (true)
            {
                Console.Write("Podaj ID dostawcy: ");
                if (int.TryParse(Console.ReadLine(), out supplierId) && suppliers.Any(s => s.SupplierId == supplierId))
                {
                    break;
                }
                Console.WriteLine("Nieprawidłowy ID dostawcy. Spróbuj ponownie.");
            }

            // Pobranie listy walut
            var currencies = await currencyService.GetAllCurrenciesAsync();
            if (!currencies.Any())
            {
                Console.WriteLine("Brak dostępnych walut.");
                return;
            }

            Console.WriteLine("Dostępne waluty:");
            foreach (var currency in currencies)
            {
                Console.WriteLine($"Kod: {currency.CurrencyId}, Nazwa: {currency.Name}, Symbol: {currency.Symbol}");
            }

            // Wybór waluty z walidacją
            string currencyId;
            while (true)
            {
                Console.Write("Podaj kod waluty: ");
                currencyId = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(currencyId) && currencies.Any(c => c.CurrencyId == currencyId))
                {
                    break;
                }
                Console.WriteLine("Nieprawidłowy kod waluty. Spróbuj ponownie.");
            }

            // Tworzenie nowej dostawy
            await supplyService.AddSupplyAsync(supplierId, currencyId);
            var supplies = await supplyService.GetAllSuppliesAsync();
            var supply = supplies.Last();

            Console.WriteLine($"Zaplanowano nową dostawę z ID: {supply.SupplyId}");

            // Dodawanie przedmiotów do dostawy
            string addMore;
            do
            {
                int itemId;
                decimal quantity, price;

                // Pobranie ID przedmiotu z walidacją
                var items = await itemService.GetAllItemsAsync();
                Console.WriteLine("Dostępne przedmioty:");
                foreach (var item in items)
                {
                    Console.WriteLine($"ID: {item.ItemId}, Nazwa: {item.Name}");
                }

                while (true)
                {
                    Console.Write("Podaj ID przedmiotu: ");
                    if (int.TryParse(Console.ReadLine(), out itemId) && items.Any(i => i.ItemId == itemId))
                    {
                        break;
                    }
                    Console.WriteLine("Nieprawidłowy ID przedmiotu. Spróbuj ponownie.");
                }

                // Pobranie ilości z walidacją
                while (true)
                {
                    Console.Write("Podaj ilość: ");
                    if (decimal.TryParse(Console.ReadLine(), out quantity) && quantity > 0)
                    {
                        break;
                    }
                    Console.WriteLine("Nieprawidłowa ilość. Podaj liczbę dodatnią.");
                }

                // Pobranie ceny z walidacją
                while (true)
                {
                    Console.Write("Podaj cenę za jednostkę: ");
                    if (decimal.TryParse(Console.ReadLine(), out price) && price > 0)
                    {
                        break;
                    }
                    Console.WriteLine("Nieprawidłowa cena. Podaj liczbę dodatnią.");
                }

                // Tworzenie nowego SupplyItem
                var supplyItem = new SupplyItem
                {
                    SupplyId = supply.SupplyId,
                    ItemId = itemId,
                    Quantity = quantity,
                    Price = price
                };

                // Dodanie SupplyItem do bazy danych
                await supplyItemService.AddAsync(supplyItem);
                Console.WriteLine($"Dodano przedmiot ID: {itemId} w ilości: {quantity}, cenie: {price}");

                // Pytanie o kolejne przedmioty
                Console.Write("Czy chcesz dodać kolejny przedmiot? (tak/nie): ");
                addMore = Console.ReadLine()?.ToLower();

            } while (addMore == "tak");

            Console.WriteLine($"Dostawa z ID {supply.SupplyId} została zaplanowana i zapisana w bazie danych.");
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Wyświetla listę dostawców.
        /// @details Pozwala filtrować dostawców według nazwy lub ID.
        ///          Obsługuje paginację.
        protected async Task ShowSuppliers()
        {
            int pageNumber = 1;
            const int pageSize = 10; // Maksymalnie 10 dostawców na stronę

            // Filtry ustawiane na początku
            string nameFilter = null;
            int? supplierIdFilter = null;

            // Flaga do kontroli zapytań o filtry
            bool askForFilters = true;

            while (true)
            {
                Console.WriteLine("\n=== Wykaz dostawców ===");

                // Pytanie o filtry tylko, gdy użytkownik tego zażąda
                if (askForFilters)
                {
                    Console.Write("Filtruj po nazwie dostawcy (wpisz nazwę lub ENTER, aby pominąć): ");
                    nameFilter = Console.ReadLine();

                    Console.Write("Filtruj po ID dostawcy (wpisz ID lub ENTER, aby pominąć): ");
                    string supplierIdInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(supplierIdInput))
                    {
                        if (int.TryParse(supplierIdInput, out int parsedId))
                        {
                            supplierIdFilter = parsedId;
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowy ID klienta. Spróbuj ponownie.");
                            continue;
                        }
                    }

                    // Wyłącz flagę po ustawieniu filtrów
                    askForFilters = false;
                }

                // Pobierz dostawców z bazy danych z uwzględnieniem filtrów
                var query = _supplierService.GetAllSuppliersQuery();

                if (!string.IsNullOrEmpty(nameFilter))
                {
                    query = query.Where(s => s.Name.ToLower().StartsWith(nameFilter.ToLower()));
                }


                if (supplierIdFilter.HasValue)
                {
                    query = query.Where(s => s.SupplierId == supplierIdFilter.Value);
                }

                // Zastosowanie paginacji
                var totalSuppliers = query.Count();
                var totalPages = (int)Math.Ceiling(totalSuppliers / (double)pageSize);

                var suppliers = query
                    .OrderBy(s => s.Name)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // Wyświetlenie dostawców
                Console.WriteLine($"Strona {pageNumber}/{totalPages} (łącznie: {totalSuppliers} dostawców)");
                foreach (var supplier in suppliers)
                {
                    Console.WriteLine($"ID: {supplier.SupplierId}, Nazwa: {supplier.Name}, Adres: {supplier.Address.City}, {supplier.Address.Street}");
                }

                // Wyświetlenie opcji
                Console.WriteLine("\nOpcje:");
                Console.WriteLine("1. Następna strona");
                Console.WriteLine("2. Poprzednia strona");
                Console.WriteLine("3. Zmień filtry");
                Console.WriteLine("0. Wyjście");

                Console.Write("Wybierz opcję: ");
                var option = Console.ReadLine();

                if (option == "1" && pageNumber < totalPages)
                {
                    pageNumber++;
                }
                else if (option == "2" && pageNumber > 1)
                {
                    pageNumber--;
                }
                else if (option == "3")
                {
                    // Ustaw flagę do ponownego zapytania o filtry
                    askForFilters = true;
                    pageNumber = 1; // Resetuj paginację
                }
                else if (option == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Nieznana opcja. Spróbuj ponownie.");
                }
            }
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Wyświetla listę dostaw.
        /// @details Pozwala na filtrowanie dostaw według daty, dostawcy, przedmiotu lub statusu.
        ///          Obsługuje paginację.
        protected async Task ShowDeliveries()
        {
            int pageNumber = 1;
            const int pageSize = 10; // Maksymalnie 10 dostaw na stronę

            // Filtry ustawiane na początku
            DateTime? dateFrom = null;
            DateTime? dateTo = null;
            int? supplierIdFilter = null;
            int? itemIdFilter = null;
            string statusFilter = null;

            // Flaga do kontroli zapytań o filtry
            bool askForFilters = true;

            while (true)
            {
                Console.WriteLine("\n=== Wykaz dostaw ===");

                // Pytanie o filtry tylko, gdy użytkownik tego zażąda
                if (askForFilters)
                {
                    Console.Write("Filtruj od daty (w formacie yyyy-MM-dd lub ENTER, aby pominąć): ");
                    string dateFromInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(dateFromInput))
                    {
                        if (DateTime.TryParse(dateFromInput, out DateTime parsedDate))
                        {
                            dateFrom = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowy format daty. Spróbuj ponownie.");
                            continue;
                        }
                    }

                    Console.Write("Filtruj do daty (w formacie yyyy-MM-dd lub ENTER, aby pominąć): ");
                    string dateToInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(dateToInput))
                    {
                        if (DateTime.TryParse(dateToInput, out DateTime parsedDate))
                        {
                            dateTo = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowy format daty. Spróbuj ponownie.");
                            continue;
                        }
                    }


                Console.Write("Filtruj po ID dostawcy (wpisz ID lub ENTER, aby pominąć): ");
                    string supplierIdInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(supplierIdInput))
                    {
                        if (int.TryParse(supplierIdInput, out int parsedId))
                        {
                            supplierIdFilter = parsedId;
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowy ID dostawcy. Spróbuj ponownie.");
                            continue;
                        }
                    }

                    Console.Write("Filtruj po ID przedmiotu (wpisz ID lub ENTER, aby pominąć): ");
                    string itemIdInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(itemIdInput))
                    {
                        if (int.TryParse(itemIdInput, out int parsedId))
                        {
                            itemIdFilter = parsedId;
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowy ID przedmiotu. Spróbuj ponownie.");
                            continue;
                        }
                    }

                    Console.Write("Filtruj po statusie (wpisz status lub ENTER, aby pominąć): ");
                    statusFilter = Console.ReadLine();

                    // Wyłącz flagę po ustawieniu filtrów
                    askForFilters = false;
                }

                // Pobierz dostawy z bazy danych z uwzględnieniem filtrów
                var query = _supplyService.GetAllSuppliesQuery();

                if (dateFrom.HasValue)
                {
                    query = query.Where(s => s.Date >= dateFrom.Value);
                }

                if (dateTo.HasValue)
                {
                    query = query.Where(s => s.Date <= dateTo.Value);
                }

                if (supplierIdFilter.HasValue)
                {
                    query = query.Where(s => s.SupplierId == supplierIdFilter.Value);
                }

                if (itemIdFilter.HasValue)
                {
                    query = query
                        .Include(s => s.SupplyItems)
                        .Where(s => s.SupplyItems.Any(si => si.ItemId == itemIdFilter.Value));
                }

                if (!string.IsNullOrEmpty(statusFilter))
                {
                    query = query.Where(s => EF.Functions.ILike(s.Status, $"%{statusFilter}%"));
                }

                // Zastosowanie paginacji
                var totalSupplies = query.Count();
                var totalPages = (int)Math.Ceiling(totalSupplies / (double)pageSize);

                var supplies = query
                    .Include(s => s.Currency) // Dołączenie informacji o walucie
                    .OrderBy(s => s.Date) // Sortowanie po dacie
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // Wyświetlenie dostaw
                Console.WriteLine($"Strona {pageNumber}/{totalPages} (łącznie: {totalSupplies} dostaw)");
                foreach (var supply in supplies)
                {
                    Console.WriteLine($"ID: {supply.SupplyId}, Data: {supply.Date:yyyy-MM-dd}, Dostawca: {supply.Supplier.Name}, Status: {supply.Status}");
                    foreach (var item in supply.SupplyItems)
                    {
                        Console.WriteLine($"    Przedmiot: {item.Item.Name}, Ilość: {item.Quantity}, Cena: {item.Price} {supply.Currency.CurrencyId}");
                    }
                }

                // Wyświetlenie opcji
                Console.WriteLine("\nOpcje:");
                Console.WriteLine("1. Następna strona");
                Console.WriteLine("2. Poprzednia strona");
                Console.WriteLine("3. Zmień filtry");
                Console.WriteLine("0. Wyjście");

                Console.Write("Wybierz opcję: ");
                var option = Console.ReadLine();

                if (option == "1" && pageNumber < totalPages)
                {
                    pageNumber++;
                }
                else if (option == "2" && pageNumber > 1)
                {
                    pageNumber--;
                }
                else if (option == "3")
                {
                    // Ustaw flagę do ponownego zapytania o filtry
                    askForFilters = true;
                    pageNumber = 1; // Resetuj paginację
                }
                else if (option == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Nieznana opcja. Spróbuj ponownie.");
                }
            }

            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Usuwa dostawcę.
        /// @details Pobiera ID dostawcy od użytkownika i usuwa go z bazy danych, jeśli istnieje.
        protected async Task DeleteSupplier()
        {
            Console.WriteLine("\n=== Usuń dostawcę ===");

            // Pobierz listę dostawców
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            if (!suppliers.Any())
            {
                Console.WriteLine("Brak dostawców do usunięcia.");
                return;
            }

            // Wyświetl dostawców
            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"ID: {supplier.SupplierId}, Nazwa: {supplier.Name}");
            }

            // Wybór dostawcy
            Console.Write("Podaj ID dostawcy do usunięcia: ");
            if (int.TryParse(Console.ReadLine(), out int supplierId) && suppliers.Any(s => s.SupplierId == supplierId))
            {
                await _supplierService.DeleteSupplierAsync(supplierId);
                Console.WriteLine($"Dostawca o ID {supplierId} został usunięty.");
            }
            else
            {
                Console.WriteLine("Nieprawidłowy ID dostawcy.");
            }
        }

        /// @brief Aktualizuje dane dostawcy.
        /// @details Pozwala zmienić nazwę oraz adres przypisany do dostawcy.
        protected async Task UpdateSupplier()
        {
            Console.WriteLine("\n=== Modyfikuj dostawcę ===");

            // Pobierz listę dostawców
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            if (!suppliers.Any())
            {
                Console.WriteLine("Brak dostawców do modyfikacji.");
                return;
            }

            // Wyświetl dostawców
            Console.WriteLine("Dostępni dostawcy:");
            foreach (var supplier in suppliers)
            {
                Console.WriteLine($"ID: {supplier.SupplierId}, Nazwa: {supplier.Name}");
            }

            // Wybór dostawcy z walidacją
            int supplierId;
            while (true)
            {
                Console.Write("Podaj ID dostawcy do modyfikacji: ");
                if (int.TryParse(Console.ReadLine(), out supplierId) && suppliers.Any(s => s.SupplierId == supplierId))
                {
                    break;
                }
                Console.WriteLine("Nieprawidłowy ID dostawcy. Spróbuj ponownie.");
            }

            var supplierToUpdate = suppliers.First(s => s.SupplierId == supplierId);

            // Aktualizacja nazwy dostawcy
            Console.Write($"Aktualna nazwa: {supplierToUpdate.Name}, nowa nazwa (ENTER, aby zachować): ");
            string newName = Console.ReadLine();
            newName = string.IsNullOrWhiteSpace(newName) ? supplierToUpdate.Name : newName;

            // Pobranie i wyświetlenie dostępnych adresów
            var addresses = await _addressService.GetAllAddressesAsync();
            if (!addresses.Any())
            {
                Console.WriteLine("Brak dostępnych adresów w systemie.");
                return;
            }

            Console.WriteLine("Dostępne adresy:");
            foreach (var address in addresses)
            {
                Console.WriteLine($"ID: {address.AddressId}, Adres: {address.Street}, {address.City}, {address.PostalCode}, {address.Country}");
            }

            // Wybór nowego adresu z walidacją
            int newAddressId;
            while (true)
            {
                Console.Write("Podaj ID nowego adresu (ENTER, aby zachować obecny): ");
                string newAddressIdInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(newAddressIdInput))
                {
                    // Jeśli użytkownik nie podał nowego ID, zachowaj obecny adres
                    newAddressId = supplierToUpdate.AddressId;
                    break;
                }
                else if (int.TryParse(newAddressIdInput, out newAddressId) && addresses.Any(a => a.AddressId == newAddressId))
                {
                    // Jeśli ID jest poprawne i istnieje w bazie adresów
                    break;
                }

                Console.WriteLine("Nieprawidłowy ID adresu. Spróbuj ponownie.");
            }

            // Aktualizacja dostawcy w bazie danych
            await _supplierService.UpdateSupplierAsync(supplierId, newName, newAddressId);

            Console.WriteLine($"Dane dostawcy o ID {supplierId} zostały zaktualizowane.");
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Dodaje nowego klienta do systemu.
        /// @details Pobiera dane klienta i jego adresu od użytkownika, a następnie zapisuje je w bazie danych.
        protected async Task AddClient()
        {
            Console.WriteLine("\n=== Dodaj nowego klienta ===");

            // Pobranie danych adresu od użytkownika
            Console.WriteLine("Podaj dane adresu klienta:");
            Console.Write("Kraj: ");
            string country = Console.ReadLine();

            Console.Write("Kod pocztowy: ");
            string postalCode = Console.ReadLine();

            Console.Write("Miasto: ");
            string city = Console.ReadLine();

            Console.Write("Ulica: ");
            string street = Console.ReadLine();

            // Dodanie adresu do bazy danych
            await _addressService.AddAddressAsync(country, postalCode, city, street);
            var address = (await _addressService.GetAllAddressesAsync()).Last(); // Pobranie ostatnio dodanego adresu

            // Pobranie danych klienta od użytkownika
            Console.WriteLine("\nPodaj dane klienta:");
            Console.Write("Imię i nazwisko / Nazwa firmy: ");
            string name = Console.ReadLine();

            // Dodanie klienta do bazy danych
            await _clientService.AddClientAsync(name, address.AddressId);

            Console.WriteLine($"\nNowy klient \"{name}\" został dodany z adresem: {country}, {postalCode}, {city}, {street}.");
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Planowanie nowego zamówienia.
        /// @details Pobiera dane zamówienia, takie jak klient, waluta i przedmioty.
        ///          Dodaje zamówienie do systemu oraz zapisuje jego elementy.
        protected async Task PlanOrder()
        {
            Console.WriteLine("\n=== Planowanie zamówienia ===");

            // Walidacja ID klienta
            var clients = await _clientService.GetAllClientsAsync();
            if (!clients.Any())
            {
                Console.WriteLine("Brak dostępnych klientów.");
                return;
            }

            Console.WriteLine("Dostępni klienci:");
            foreach (var client in clients)
            {
                Console.WriteLine($"ID: {client.ClientId}, Nazwa: {client.Name}");
            }

            int clientId;
            while (true)
            {
                Console.Write("Podaj ID klienta: ");
                if (int.TryParse(Console.ReadLine(), out clientId) && clients.Any(c => c.ClientId == clientId))
                {
                    break;
                }
                Console.WriteLine("Nieprawidłowy ID klienta. Spróbuj ponownie.");
            }

            // Walidacja waluty
            var currencies = await _currencyService.GetAllCurrenciesAsync();
            if (!currencies.Any())
            {
                Console.WriteLine("Brak dostępnych walut.");
                return;
            }

            Console.WriteLine("Dostępne waluty:");
            foreach (var currency in currencies)
            {
                Console.WriteLine($"Kod: {currency.CurrencyId}, Nazwa: {currency.Name}, Symbol: {currency.Symbol}");
            }

            string currencyId;
            while (true)
            {
                Console.Write("Podaj kod waluty: ");
                currencyId = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(currencyId) && currencies.Any(c => c.CurrencyId == currencyId))
                {
                    break;
                }
                Console.WriteLine("Nieprawidłowy kod waluty. Spróbuj ponownie.");
            }

            // Dodanie zamówienia do bazy danych
            await _orderService.AddOrderAsync(clientId, currencyId);
            var order = (await _orderService.GetAllOrdersAsync()).Last(); // Pobiera ostatnio dodane zamówienie
            Console.WriteLine($"Zamówienie zostało utworzone. ID zamówienia: {order.OrderId}");

            // Dodawanie przedmiotów do zamówienia
            var items = await _itemService.GetAllItemsAsync();
            if (!items.Any())
            {
                Console.WriteLine("Brak dostępnych przedmiotów.");
                return;
            }

            Console.WriteLine("Dostępne przedmioty:");
            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.ItemId}, Nazwa: {item.Name}");
            }

            Console.WriteLine("Dodawanie przedmiotów do zamówienia...");
            string addMore;
            do
            {
                int itemId;
                decimal quantity, price;

                // Walidacja ID przedmiotu
                while (true)
                {
                    Console.Write("Podaj ID przedmiotu (ItemId): ");
                    if (int.TryParse(Console.ReadLine(), out itemId) && items.Any(i => i.ItemId == itemId))
                    {
                        break;
                    }
                    Console.WriteLine("Nieprawidłowy ID przedmiotu. Spróbuj ponownie.");
                }

                // Walidacja ilości
                while (true)
                {
                    Console.Write("Podaj ilość: ");
                    if (decimal.TryParse(Console.ReadLine(), out quantity) && quantity > 0)
                    {
                        break;
                    }
                    Console.WriteLine("Nieprawidłowa ilość. Podaj liczbę dodatnią.");
                }

                // Walidacja ceny
                while (true)
                {
                    Console.Write("Podaj cenę za jednostkę: ");
                    if (decimal.TryParse(Console.ReadLine(), out price) && price > 0)
                    {
                        break;
                    }
                    Console.WriteLine("Nieprawidłowa cena. Podaj liczbę dodatnią.");
                }

                // Dodanie elementu zamówienia do bazy danych
                var orderItem = new OrderItem
                {
                    OrderId = order.OrderId,
                    ItemId = itemId,
                    Quantity = quantity,
                    Price = price
                };
                await _orderItemService.AddAsync(orderItem);

                Console.WriteLine($"Dodano przedmiot ID: {itemId}, Ilość: {quantity}, Cena: {price} do zamówienia.");

                // Pytanie o kolejne przedmioty
                Console.Write("Czy chcesz dodać kolejny przedmiot? (tak/nie): ");
                addMore = Console.ReadLine()?.ToLower();

            } while (addMore == "tak");

            Console.WriteLine($"Zamówienie ID: {order.OrderId} zostało zapisane.");
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Wyświetla listę klientów.
        /// @details Pozwala filtrować klientów według nazwy lub ID.
        ///          Obsługuje paginację.
        protected async Task ShowClients()
        {
            int pageNumber = 1;
            const int pageSize = 10; // Maksymalnie 10 klientów na stronę

            // Filtry ustawiane na początku
            string nameFilter = null;
            int? clientIdFilter = null;

            // Flaga do kontroli zapytań o filtry
            bool askForFilters = true;

            while (true)
            {
                Console.WriteLine("\n=== Wykaz klientów ===");

                // Pytanie o filtry tylko, gdy użytkownik tego zażąda
                if (askForFilters)
                {
                    Console.Write("Filtruj po nazwie klienta (wpisz nazwę lub ENTER, aby pominąć): ");
                    nameFilter = Console.ReadLine();

                    Console.Write("Filtruj po ID klienta (wpisz ID lub ENTER, aby pominąć): ");
                    string clientIdInput = Console.ReadLine();
                    clientIdFilter = string.IsNullOrEmpty(clientIdInput) ? null : int.Parse(clientIdInput);

                    // Wyłącz flagę po ustawieniu filtrów
                    askForFilters = false;
                }

                // Pobierz klientów z bazy danych z uwzględnieniem filtrów
                var query = _clientService.GetAllClientsQuery();

                if (!string.IsNullOrEmpty(nameFilter))
                {
                    query = query.Where(c => c.Name.ToLower().StartsWith(nameFilter.ToLower()));
                }

                if (clientIdFilter.HasValue)
                {
                    query = query.Where(c => c.ClientId == clientIdFilter.Value);
                }

                // Zastosowanie paginacji
                var totalClients = query.Count();
                var totalPages = (int)Math.Ceiling(totalClients / (double)pageSize);

                var clients = query
                    .OrderBy(c => c.Name)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // Wyświetlenie klientów
                Console.WriteLine($"Strona {pageNumber}/{totalPages} (łącznie: {totalClients} klientów)");
                foreach (var client in clients)
                {
                    Console.WriteLine($"ID: {client.ClientId}, Nazwa: {client.Name}, Adres: {client.Address.City}, {client.Address.Street}");
                }

                // Wyświetlenie opcji
                Console.WriteLine("\nOpcje:");
                Console.WriteLine("1. Następna strona");
                Console.WriteLine("2. Poprzednia strona");
                Console.WriteLine("3. Zmień filtry");
                Console.WriteLine("0. Wyjście");

                Console.Write("Wybierz opcję: ");
                var option = Console.ReadLine();

                if (option == "1" && pageNumber < totalPages)
                {
                    pageNumber++;
                }
                else if (option == "2" && pageNumber > 1)
                {
                    pageNumber--;
                }
                else if (option == "3")
                {
                    // Ustaw flagę do ponownego zapytania o filtry
                    askForFilters = true;
                    pageNumber = 1; // Resetuj paginację
                }
                else if (option == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Nieznana opcja. Spróbuj ponownie.");
                }
            }
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Wyświetla listę zamówień.
        /// @details Pozwala na filtrowanie zamówień według daty, klienta, przedmiotu lub statusu.
        ///          Obsługuje paginację.
        protected async Task ShowOrders()
        {
            int pageNumber = 1;
            const int pageSize = 10; // Maksymalnie 10 zamówień na stronę

            // Filtry ustawiane na początku
            DateTime? dateFrom = null;
            DateTime? dateTo = null;
            int? clientIdFilter = null;
            int? itemIdFilter = null;
            string statusFilter = null;

            // Flaga do kontroli zapytań o filtry
            bool askForFilters = true;

            while (true)
            {
                Console.WriteLine("\n=== Wykaz zamówień ===");

                // Pytanie o filtry tylko, gdy użytkownik tego zażąda
                if (askForFilters)
                {
                    Console.Write("Filtruj od daty (w formacie yyyy-MM-dd lub ENTER, aby pominąć): ");
                    string dateFromInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(dateFromInput))
                    {
                        if (DateTime.TryParse(dateFromInput, out DateTime parsedDate))
                        {
                            dateFrom = parsedDate;
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowy format daty. Spróbuj ponownie.");
                            continue;
                        }
                    }

                    Console.Write("Filtruj do daty (w formacie yyyy-MM-dd lub ENTER, aby pominąć): ");
                    string dateToInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(dateToInput))
                    {
                        if (DateTime.TryParse(dateToInput, out DateTime parsedDate))
                        {
                            dateTo = parsedDate;
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowy format daty. Spróbuj ponownie.");
                            continue;
                        }
                    }

                    Console.Write("Filtruj po ID klienta (wpisz ID lub ENTER, aby pominąć): ");
                    string clientIdInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(clientIdInput))
                    {
                        if (int.TryParse(clientIdInput, out int parsedId))
                        {
                            clientIdFilter = parsedId;
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowy ID klienta. Spróbuj ponownie.");
                            continue;
                        }
                    }

                    Console.Write("Filtruj po ID przedmiotu (wpisz ID lub ENTER, aby pominąć): ");
                    string itemIdInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(itemIdInput))
                    {
                        if (int.TryParse(itemIdInput, out int parsedId))
                        {
                            itemIdFilter = parsedId;
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowy ID przedmiotu. Spróbuj ponownie.");
                            continue;
                        }
                    }

                    Console.Write("Filtruj po statusie (wpisz status lub ENTER, aby pominąć): ");
                    statusFilter = Console.ReadLine();

                    // Wyłącz flagę po ustawieniu filtrów
                    askForFilters = false;
                }

                // Pobierz zamówienia z bazy danych z uwzględnieniem filtrów
                var query = _orderService.GetAllOrdersQuery();

                if (dateFrom.HasValue)
                {
                    query = query.Where(o => o.Date >= dateFrom.Value);
                }

                if (dateTo.HasValue)
                {
                    query = query.Where(o => o.Date <= dateTo.Value);
                }

                if (clientIdFilter.HasValue)
                {
                    query = query.Where(o => o.ClientId == clientIdFilter.Value);
                }

                if (itemIdFilter.HasValue)
                {
                    query = query
                        .Include(o => o.OrderItems)
                        .Where(o => o.OrderItems.Any(oi => oi.ItemId == itemIdFilter.Value));
                }

                if (!string.IsNullOrEmpty(statusFilter))
                {
                    query = query.Where(o => EF.Functions.ILike(o.Status, $"%{statusFilter}%"));
                }

                // Zastosowanie paginacji
                var totalOrders = query.Count();
                var totalPages = (int)Math.Ceiling(totalOrders / (double)pageSize);

                var orders = query
                    .Include(o => o.Currency) // Dołączenie informacji o walucie
                    .OrderBy(o => o.Date) // Sortowanie po dacie
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // Wyświetlenie zamówień
                Console.WriteLine($"Strona {pageNumber}/{totalPages} (łącznie: {totalOrders} zamówień)");
                foreach (var order in orders)
                {
                    Console.WriteLine($"ID: {order.OrderId}, Data: {order.Date:yyyy-MM-dd}, Klient: {order.Client.Name}, Status: {order.Status}, Waluta: {order.Currency.CurrencyId}");
                    foreach (var item in order.OrderItems)
                    {
                        Console.WriteLine($"    Przedmiot: {item.Item.Name}, Ilość: {item.Quantity}, Cena: {item.Price} {order.Currency.Symbol}");
                    }
                }

                // Wyświetlenie opcji
                Console.WriteLine("\nOpcje:");
                Console.WriteLine("1. Następna strona");
                Console.WriteLine("2. Poprzednia strona");
                Console.WriteLine("3. Zmień filtry");
                Console.WriteLine("0. Wyjście");

                Console.Write("Wybierz opcję: ");
                var option = Console.ReadLine();

                if (option == "1" && pageNumber < totalPages)
                {
                    pageNumber++;
                }
                else if (option == "2" && pageNumber > 1)
                {
                    pageNumber--;
                }
                else if (option == "3")
                {
                    // Ustaw flagę do ponownego zapytania o filtry
                    askForFilters = true;
                    pageNumber = 1; // Resetuj paginację
                }
                else if (option == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Nieznana opcja. Spróbuj ponownie.");
                }
            }

            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Usuwa klienta.
        /// @details Pobiera ID klienta od użytkownika i usuwa go z bazy danych, jeśli istnieje.
        protected async Task DeleteClient()
        {
            Console.WriteLine("\n=== Usuń klienta ===");

            // Wyświetlenie dostępnych klientów
            var clients = await _clientService.GetAllClientsAsync();
            if (!clients.Any())
            {
                Console.WriteLine("Brak klientów do usunięcia.");
                return;
            }

            Console.WriteLine("Dostępni klienci:");
            foreach (var client in clients)
            {
                Console.WriteLine($"ID: {client.ClientId}, Nazwa: {client.Name}");
            }

            // Wybór klienta do usunięcia
            int clientId;
            while (true)
            {
                Console.Write("Podaj ID klienta do usunięcia: ");
                if (int.TryParse(Console.ReadLine(), out clientId) && clients.Any(c => c.ClientId == clientId))
                {
                    break;
                }
                Console.WriteLine("Nieprawidłowy ID klienta. Spróbuj ponownie.");
            }

            // Usuwanie klienta
            await _clientService.DeleteClientAsync(clientId);
            Console.WriteLine($"Klient o ID {clientId} został usunięty.");
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Aktualizuje dane klienta.
        /// @details Pozwala zmienić nazwę oraz adres przypisany do klienta.
        protected async Task UpdateClient()
        {
            Console.WriteLine("\n=== Modyfikuj dane klienta ===");

            // Wyświetlenie dostępnych klientów
            var clients = await _clientService.GetAllClientsAsync();
            if (!clients.Any())
            {
                Console.WriteLine("Brak klientów do modyfikacji.");
                return;
            }

            Console.WriteLine("Dostępni klienci:");
            foreach (var c in clients)
            {
                Console.WriteLine($"ID: {c.ClientId}, Nazwa: {c.Name}");
            }

            // Wybór klienta do modyfikacji
            int clientId;
            while (true)
            {
                Console.Write("Podaj ID klienta do modyfikacji: ");
                if (int.TryParse(Console.ReadLine(), out clientId) && clients.Any(c => c.ClientId == clientId))
                {
                    break;
                }
                Console.WriteLine("Nieprawidłowy ID klienta. Spróbuj ponownie.");
            }

            var client = clients.First(c => c.ClientId == clientId);

            // Modyfikowanie danych klienta
            Console.WriteLine($"Aktualna nazwa klienta: {client.Name}");
            Console.Write("Podaj nową nazwę klienta (ENTER, aby zachować obecną): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                client.Name = newName;
            }

            // Wyświetlenie obecnego adresu
            Console.WriteLine("Aktualne dane adresowe klienta:");
            Console.WriteLine($"Kraj: {client.Address.Country}, Kod pocztowy: {client.Address.PostalCode}, Miasto: {client.Address.City}, Ulica: {client.Address.Street}");

            // Wyświetlenie dostępnych adresów
            var addresses = await _addressService.GetAllAddressesAsync();
            Console.WriteLine("Dostępne adresy:");
            foreach (var address in addresses)
            {
                Console.WriteLine($"ID: {address.AddressId}, Adres: {address.Street}, {address.City}, {address.PostalCode}, {address.Country}");
            }

            // Wybór nowego adresu z walidacją
            int newAddressId;
            while (true)
            {
                Console.Write("Podaj ID nowego adresu (ENTER, aby zachować obecny): ");
                string newAddressIdInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(newAddressIdInput))
                {
                    // Jeśli użytkownik nie podał nowego ID, zachowaj obecny adres
                    newAddressId = client.AddressId;
                    break;
                }
                else if (int.TryParse(newAddressIdInput, out newAddressId) && addresses.Any(a => a.AddressId == newAddressId))
                {
                    // Jeśli ID jest poprawne i istnieje w bazie adresów
                    break;
                }

                Console.WriteLine("Nieprawidłowy ID adresu. Spróbuj ponownie.");
            }

            // Aktualizacja klienta w bazie danych
            await _clientService.UpdateClientAsync(client.ClientId, client.Name, newAddressId);

            Console.WriteLine($"Dane klienta o ID {client.ClientId} zostały zaktualizowane.");
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Przyjmuje dostawę.
        /// @details Pozwala przyjąć przedmioty z dostawy do magazynu.
        ///          Aktualizuje stan magazynu oraz status dostawy.
        protected async Task ReceiveDelivery()
        {
            Console.WriteLine("=== Przyjmij dostawę ===");

            // Krok 1: Pobierz numer dostawy
            Console.Write("Podaj numer dostawy: ");
            if (!int.TryParse(Console.ReadLine(), out int supplyId))
            {
                Console.WriteLine("Nieprawidłowy numer dostawy.");
                return;
            }

            // Pobierz dostawę i powiązane elementy
            var supply = await _supplyService.GetSupplyByIdAsync(supplyId);
            if (supply == null)
            {
                Console.WriteLine("Nie znaleziono dostawy o podanym numerze.");
                return;
            }

            if (supply.Status == "Dostarczone")
            {
                Console.WriteLine("Ta dostawa została już zakończona.");
                return;
            }

            // Pobierz elementy dostawy
            var supplyItems = await _supplyItemService.GetAllAsync();
            supplyItems = supplyItems.Where(si => si.SupplyId == supplyId).ToList();

            if (!supplyItems.Any())
            {
                Console.WriteLine("Brak elementów w dostawie.");
                return;
            }

            // Krok 2: Wyświetl elementy dostawy i ich ilości
            Console.WriteLine("\nPrzedmioty w dostawie:");
            foreach (var item in supplyItems)
            {
                decimal remainingQuantity = item.Quantity - item.ReceivedQuantity;
                Console.WriteLine($"ID: {item.ItemId}, Nazwa: {item.Item.Name}, Ilość zamówiona: {item.Quantity}, Przyjęta: {item.ReceivedQuantity}, Pozostało: {remainingQuantity}");
            }

            // Krok 3: Przyjmowanie przedmiotów
            bool allAccepted = false;
            do
            {
                // Sprawdź, czy wszystkie przedmioty zostały przyjęte
                bool allItemsReceived = supplyItems.All(si => si.Quantity == si.ReceivedQuantity);
                if (allItemsReceived)
                {
                    Console.WriteLine("Wszystkie przedmioty zostały już przyjęte.");
                    allAccepted = true;
                    break;
                }

                Console.Write("Podaj ID przedmiotu do przyjęcia: ");
                if (!int.TryParse(Console.ReadLine(), out int itemId) || !supplyItems.Any(si => si.ItemId == itemId))
                {
                    Console.WriteLine("Nieprawidłowy ID przedmiotu.");
                    continue;
                }

                var supplyItem = supplyItems.First(si => si.ItemId == itemId);
                decimal remainingQuantity = supplyItem.Quantity - supplyItem.ReceivedQuantity;

                if (remainingQuantity == 0)
                {
                    Console.WriteLine("Wszystkie ilości tego przedmiotu zostały już przyjęte.");
                    continue;
                }

                Console.Write($"Podaj ilość do przyjęcia (pozostało {remainingQuantity}): ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal receivedQuantity) || receivedQuantity <= 0)
                {
                    Console.WriteLine("Nieprawidłowa ilość.");
                    continue;
                }

                if (receivedQuantity > remainingQuantity)
                {
                    Console.WriteLine("Nie możesz przyjąć więcej niż pozostało.");
                    continue;
                }

                // Aktualizacja ilości przyjętej
                supplyItem.ReceivedQuantity += receivedQuantity;
                await _supplyItemService.UpdateAsync(supplyItem);

                // Aktualizacja w Inventory
                var inventory = await _inventoryService.GetAllInventoriesAsync();
                var inventoryItem = inventory.FirstOrDefault(inv => inv.ItemId == itemId);
                if (inventoryItem != null)
                {
                    inventoryItem.Quantity += receivedQuantity;
                    await _inventoryService.UpdateInventoryAsync(inventoryItem.InventoryId, inventoryItem.Quantity);
                }
                else
                {
                    await _inventoryService.AddInventoryAsync(itemId, receivedQuantity);
                }

                Console.WriteLine($"Przyjęto {receivedQuantity} sztuk przedmiotu ID {itemId}.");

                // Sprawdź ponownie, czy wszystkie przedmioty zostały przyjęte
                allItemsReceived = supplyItems.All(si => si.Quantity == si.ReceivedQuantity);
                if (allItemsReceived)
                {
                    Console.WriteLine("Wszystkie przedmioty zostały już przyjęte.");
                    allAccepted = true;
                }
                else
                {
                    Console.Write("Czy chcesz kontynuować przyjmowanie przedmiotów? (tak/nie): ");
                    string answer = Console.ReadLine()?.Trim().ToLower();
                    if (answer == "nie")
                    {
                        allAccepted = true;
                    }
                }
            } while (!allAccepted);

            // Krok 4: Aktualizacja statusu dostawy
            bool allItemsFullyReceived = supplyItems.All(si => si.Quantity == si.ReceivedQuantity);
            supply.Status = allItemsFullyReceived ? "Dostarczone" : "W przygotowaniu";
            await _supplyService.UpdateSupplyAsync(supply.SupplyId, supply.SupplierId, supply.Status, supply.CurrencyId);

            Console.WriteLine($"Dostawa została oznaczona jako '{supply.Status}'.");
            Console.WriteLine("\nPrzyjmowanie dostawy zakończone.");
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Wydaje zamówienie.
        /// @details Pozwala wydać przedmioty z magazynu do zamówienia.
        ///          Aktualizuje stan magazynu oraz status zamówienia.
        protected async Task IssueOrder()
        {
            Console.WriteLine("=== Wydaj zamówienie ===");

            // Krok 1: Pobierz numer zamówienia
            Console.Write("Podaj numer zamówienia: ");
            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine("Nieprawidłowy numer zamówienia.");
                return;
            }

            // Pobierz zamówienie i powiązane elementy
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                Console.WriteLine("Nie znaleziono zamówienia o podanym numerze.");
                return;
            }

            if (order.Status == "Zrealizowane")
            {
                Console.WriteLine("To zamówienie zostało już zrealizowane.");
                return;
            }

            // Pobierz elementy zamówienia
            var orderItems = await _orderItemService.GetAllAsync();
            orderItems = orderItems.Where(oi => oi.OrderId == orderId).ToList();

            if (!orderItems.Any())
            {
                Console.WriteLine("Brak elementów w zamówieniu.");
                return;
            }

            // Krok 2: Wyświetl elementy zamówienia i ich ilości
            Console.WriteLine("\nPrzedmioty w zamówieniu:");
            foreach (var item in orderItems)
            {
                decimal remainingQuantity = item.Quantity - item.DispatchedQuantity;
                Console.WriteLine($"ID: {item.ItemId}, Nazwa: {item.Item.Name}, Ilość zamówiona: {item.Quantity}, Wysłana: {item.DispatchedQuantity}, Pozostało: {remainingQuantity}");
            }

            // Krok 3: Wydawanie przedmiotów
            bool allDispatched = false;
            do
            {
                // Sprawdź, czy wszystkie przedmioty zostały wydane
                bool allItemsDispatched = orderItems.All(oi => oi.Quantity == oi.DispatchedQuantity);
                if (allItemsDispatched)
                {
                    Console.WriteLine("Wszystkie przedmioty zostały już wydane.");
                    allDispatched = true;
                    break;
                }

                Console.Write("Podaj ID przedmiotu do wysłania: ");
                if (!int.TryParse(Console.ReadLine(), out int itemId) || !orderItems.Any(oi => oi.ItemId == itemId))
                {
                    Console.WriteLine("Nieprawidłowy ID przedmiotu.");
                    continue;
                }

                var orderItem = orderItems.First(oi => oi.ItemId == itemId);
                decimal remainingQuantity = orderItem.Quantity - orderItem.DispatchedQuantity;

                // Pobierz informacje o magazynie
                var inventory = await _inventoryService.GetAllInventoriesAsync();
                var inventoryItem = inventory.FirstOrDefault(inv => inv.ItemId == itemId);
                decimal availableInInventory = inventoryItem?.Quantity ?? 0;

                Console.Write($"Podaj ilość do wysłania (pozostało {remainingQuantity}, dostępne w magazynie {availableInInventory}): ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal dispatchQuantity) || dispatchQuantity <= 0)
                {
                    Console.WriteLine("Nieprawidłowa ilość.");
                    continue;
                }

                if (dispatchQuantity > remainingQuantity)
                {
                    Console.WriteLine("Nie możesz wydać więcej niż pozostało w zamówieniu.");
                    continue;
                }

                if (dispatchQuantity > availableInInventory)
                {
                    Console.WriteLine("Nie możesz wydać więcej niż dostępne w magazynie.");
                    continue;
                }

                // Aktualizacja ilości wydanej
                orderItem.DispatchedQuantity += dispatchQuantity;
                await _orderItemService.UpdateAsync(orderItem);

                // Aktualizacja w Inventory
                if (inventoryItem != null)
                {
                    inventoryItem.Quantity -= dispatchQuantity;
                    await _inventoryService.UpdateInventoryAsync(inventoryItem.InventoryId, inventoryItem.Quantity);
                }

                Console.WriteLine($"Wydano {dispatchQuantity} sztuk przedmiotu ID {itemId}.");

                // Sprawdź ponownie, czy wszystkie przedmioty zostały wydane
                allItemsDispatched = orderItems.All(oi => oi.Quantity == oi.DispatchedQuantity);
                if (allItemsDispatched)
                {
                    Console.WriteLine("Wszystkie przedmioty zostały już wydane.");
                    allDispatched = true;
                }
                else
                {
                    Console.Write("Czy chcesz kontynuować wydawanie przedmiotów? (tak/nie): ");
                    string answer = Console.ReadLine()?.Trim().ToLower();
                    if (answer == "nie")
                    {
                        allDispatched = true;
                    }
                }
            } while (!allDispatched);

            // Krok 4: Aktualizacja statusu zamówienia
            bool allItemsFullyDispatched = orderItems.All(oi => oi.Quantity == oi.DispatchedQuantity);
            order.Status = allItemsFullyDispatched ? "Zrealizowane" : "W przygotowaniu";
            await _orderService.UpdateOrderAsync(order.OrderId, order.ClientId, order.Status, order.CurrencyId);

            Console.WriteLine($"Zamówienie zostało oznaczone jako '{order.Status}'.");
            Console.WriteLine("\nWydanie zamówienia zakończone.");
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Wyświetla stan magazynu.
        /// @details Pozwala na filtrowanie według ilości lub ID przedmiotu.
        ///          Obsługuje paginację.
        protected async Task ShowInventory()
        {
            int pageNumber = 1;
            const int pageSize = 10; // Maksymalnie 10 elementów na stronę

            // Filtry ustawiane na początku
            int? itemIdFilter = null;
            decimal? quantityGreaterThanFilter = null;
            decimal? quantityLessThanFilter = null;

            // Flaga do kontroli zapytań o filtry
            bool askForFilters = true;

            while (true)
            {
                Console.WriteLine("\n=== Stan magazynu ===");

                // Pytanie o filtry tylko, gdy użytkownik tego zażąda
                if (askForFilters)
                {
                    Console.Write("Filtruj po ID przedmiotu (wpisz ID lub ENTER, aby pominąć): ");
                    string itemIdInput = Console.ReadLine();
                    itemIdFilter = string.IsNullOrEmpty(itemIdInput) ? null : int.Parse(itemIdInput);

                    Console.Write("Filtruj przedmioty z ilością większą niż (wpisz wartość lub ENTER, aby pominąć): ");
                    string quantityGreaterInput = Console.ReadLine();
                    quantityGreaterThanFilter = string.IsNullOrEmpty(quantityGreaterInput) ? null : decimal.Parse(quantityGreaterInput);

                    Console.Write("Filtruj przedmioty z ilością mniejszą niż (wpisz wartość lub ENTER, aby pominąć): ");
                    string quantityLessInput = Console.ReadLine();
                    quantityLessThanFilter = string.IsNullOrEmpty(quantityLessInput) ? null : decimal.Parse(quantityLessInput);

                    // Wyłącz flagę po ustawieniu filtrów
                    askForFilters = false;
                }

                // Pobierz stan magazynu z bazy danych z uwzględnieniem filtrów
                var query = await _inventoryService.GetAllInventoriesAsync();

                if (itemIdFilter.HasValue)
                {
                    query = query.Where(i => i.ItemId == itemIdFilter.Value).ToList();
                }

                if (quantityGreaterThanFilter.HasValue)
                {
                    query = query.Where(i => i.Quantity > quantityGreaterThanFilter.Value).ToList();
                }

                if (quantityLessThanFilter.HasValue)
                {
                    query = query.Where(i => i.Quantity < quantityLessThanFilter.Value).ToList();
                }

                // Zastosowanie paginacji
                var totalItems = query.Count();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                var inventories = query
                    .OrderBy(i => i.ItemId) // Sortowanie po ID przedmiotu
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // Wyświetlenie stanu magazynu
                Console.WriteLine($"Strona {pageNumber}/{totalPages} (łącznie: {totalItems} przedmiotów)");
                foreach (var inventory in inventories)
                {
                    Console.WriteLine($"ID: {inventory.ItemId}, Nazwa: {inventory.Item.Name}, Ilość: {inventory.Quantity}");
                }

                // Wyświetlenie opcji
                Console.WriteLine("\nOpcje:");
                Console.WriteLine("1. Następna strona");
                Console.WriteLine("2. Poprzednia strona");
                Console.WriteLine("3. Zmień filtry");
                Console.WriteLine("0. Wyjście");

                Console.Write("Wybierz opcję: ");
                var option = Console.ReadLine();

                if (option == "1" && pageNumber < totalPages)
                {
                    pageNumber++;
                }
                else if (option == "2" && pageNumber > 1)
                {
                    pageNumber--;
                }
                else if (option == "3")
                {
                    // Ustaw flagę do ponownego zapytania o filtry
                    askForFilters = true;
                    pageNumber = 1; // Resetuj paginację
                }
                else if (option == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Nieznana opcja. Spróbuj ponownie.");
                }
            }
        }

        /// @brief Dodaje nowego użytkownika.
        /// @details Pobiera dane użytkownika, w tym imię, nazwisko, e-mail, hasło i rolę.
        ///          Sprawdza poprawność e-maila oraz jego unikalność.
        protected async Task AddUser()
        {
            Console.WriteLine("\n=== Dodaj użytkownika ===");

            // Pobierz dane użytkownika
            Console.Write("Podaj imię użytkownika: ");
            string name = Console.ReadLine();

            Console.Write("Podaj nazwisko użytkownika: ");
            string surname = Console.ReadLine();

            Console.Write("Podaj adres email użytkownika: ");
            string email;

            // Walidacja adresu email
            while (true)
            {
                email = Console.ReadLine();

                // Sprawdzamy, czy adres email nie jest pusty i czy ma poprawny format
                if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
                {
                    Console.WriteLine("Nieprawidłowy adres email. Spróbuj ponownie: ");
                    continue;
                }

                // Sprawdzamy, czy email nie jest już używany
                if (await _userService.IsEmailInUseAsync(email))
                {
                    Console.WriteLine("Ten adres email jest już w użyciu. Wprowadź inny: ");
                    continue;
                }

                break;
            }

            Console.Write("Podaj hasło użytkownika: ");
            string password = Console.ReadLine();

            // Pobierz listę ról
            var roles = await _roleService.GetAllRolesAsync(); // Zakładamy, że istnieje metoda w RoleService
            if (!roles.Any())
            {
                Console.WriteLine("Brak dostępnych ról. Nie można dodać użytkownika.");
                return;
            }

            // Wyświetl listę ról
            Console.WriteLine("\nDostępne role:");
            foreach (var role in roles)
            {
                Console.WriteLine($"ID: {role.RoleId}, Nazwa: {role.Name}");
            }

            // Wybierz rolę
            int roleId;
            while (true)
            {
                Console.Write("Wybierz ID roli dla użytkownika: ");
                if (int.TryParse(Console.ReadLine(), out roleId) && roles.Any(r => r.RoleId == roleId))
                {
                    break;
                }
                Console.WriteLine("Nieprawidłowe ID roli. Spróbuj ponownie.");
            }

            // Dodaj użytkownika do bazy danych
            await _userService.AddUserAsync(name, surname, email, password, roleId);

            Console.WriteLine($"Użytkownik {name} {surname} został dodany z rolą ID: {roleId}.");
        }


        /// @brief Zmienia rolę użytkownika.
        /// @details Pozwala przypisać nową rolę wybranemu użytkownikowi.
        protected async Task ChangeUserRole()
        {
            Console.WriteLine("\n=== Zmień rolę użytkownika ===");

            // Pobierz listę użytkowników
            var users = await _userService.GetAllUsersAsync();
            if (!users.Any())
            {
                Console.WriteLine("Brak użytkowników w systemie.");
                return;
            }

            // Wyświetl listę użytkowników
            Console.WriteLine("\nLista użytkowników:");
            Console.WriteLine("\nLista użytkowników:");
            foreach (var u in users) // Zmieniono "user" na "u"
            {
                Console.WriteLine($"ID: {u.UserId}, Imię: {u.Name}, Nazwisko: {u.Surname}, Email: {u.Email}, Rola: {u.Role.Name}");
            }

            // Wybierz użytkownika
            int userId;
            while (true)
            {
                Console.Write("Podaj ID użytkownika, którego rolę chcesz zmienić: ");
                if (int.TryParse(Console.ReadLine(), out userId) && users.Any(u => u.UserId == userId))
                {
                    break;
                }
                Console.WriteLine("Nieprawidłowy ID użytkownika. Spróbuj ponownie.");
            }

            var user = users.First(u => u.UserId == userId);

            // Pobierz listę ról
            var roles = await _roleService.GetAllRolesAsync();
            if (!roles.Any())
            {
                Console.WriteLine("Brak dostępnych ról. Nie można zmienić roli użytkownika.");
                return;
            }

            // Wyświetl listę ról
            Console.WriteLine("\nDostępne role:");
            foreach (var role in roles)
            {
                Console.WriteLine($"ID: {role.RoleId}, Nazwa: {role.Name}");
            }

            // Wybierz nową rolę
            int newRoleId;
            while (true)
            {
                Console.Write($"Podaj ID nowej roli dla użytkownika {user.Name} {user.Surname}: ");
                if (int.TryParse(Console.ReadLine(), out newRoleId) && roles.Any(r => r.RoleId == newRoleId))
                {
                    break;
                }
                Console.WriteLine("Nieprawidłowe ID roli. Spróbuj ponownie.");
            }

            // Zaktualizuj rolę użytkownika w bazie danych
            await _userService.UpdateUserAsync(user.UserId, user.Name, user.Surname, user.Email, user.Password, newRoleId);

            Console.WriteLine($"Rola użytkownika {user.Name} {user.Surname} została zmieniona na rolę ID: {newRoleId}.");
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }


        /// @brief Wyświetla listę użytkowników.
        /// @details Pozwala filtrować użytkowników według roli, imienia lub nazwiska.
        ///          Obsługuje paginację.
        protected async Task ShowUsers()
        {
            int pageNumber = 1;
            const int pageSize = 10; // Maksymalnie 10 użytkowników na stronę

            // Filtry ustawiane na początku
            string roleFilter = null;
            string nameFilter = null;
            string surnameFilter = null;

            // Flaga do kontroli zapytań o filtry
            bool askForFilters = true;

            while (true)
            {
                Console.WriteLine("\n=== Wykaz użytkowników ===");

                // Pytanie o filtry tylko, gdy użytkownik tego zażąda
                if (askForFilters)
                {
                    Console.Write("Filtruj po roli (wpisz nazwę roli lub ENTER, aby pominąć): ");
                    roleFilter = Console.ReadLine()?.ToLower();

                    Console.Write("Filtruj po imieniu użytkownika (wpisz imię lub ENTER, aby pominąć): ");
                    nameFilter = Console.ReadLine()?.ToLower();

                    Console.Write("Filtruj po nazwisku użytkownika (wpisz nazwisko lub ENTER, aby pominąć): ");
                    surnameFilter = Console.ReadLine()?.ToLower();

                    // Wyłącz flagę po ustawieniu filtrów
                    askForFilters = false;
                }

                // Pobierz użytkowników z bazy danych z uwzględnieniem filtrów
                var query = _userService.GetAllUsersAsync().Result.AsQueryable();

                if (!string.IsNullOrEmpty(roleFilter))
                {
                    query = query.Where(u => u.Role.Name.ToLower().Contains(roleFilter));
                }

                if (!string.IsNullOrEmpty(nameFilter))
                {
                    query = query.Where(u => u.Name.ToLower().Contains(nameFilter));
                }

                if (!string.IsNullOrEmpty(surnameFilter))
                {
                    query = query.Where(u => u.Surname.ToLower().Contains(surnameFilter));
                }

                // Zastosowanie paginacji
                var totalUsers = query.Count();
                var totalPages = (int)Math.Ceiling(totalUsers / (double)pageSize);

                var users = query
                    .OrderBy(u => u.UserId) // Sortowanie po ID użytkownika
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // Wyświetlenie użytkowników
                Console.WriteLine($"Strona {pageNumber}/{totalPages} (łącznie: {totalUsers} użytkowników)");
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.UserId}, Imię: {user.Name}, Nazwisko: {user.Surname}, Email: {user.Email}, Rola: {user.Role.Name}");
                }

                // Wyświetlenie opcji
                Console.WriteLine("\nOpcje:");
                Console.WriteLine("1. Następna strona");
                Console.WriteLine("2. Poprzednia strona");
                Console.WriteLine("3. Zmień filtry");
                Console.WriteLine("0. Wyjście");

                Console.Write("Wybierz opcję: ");
                var option = Console.ReadLine();

                if (option == "1" && pageNumber < totalPages)
                {
                    pageNumber++;
                }
                else if (option == "2" && pageNumber > 1)
                {
                    pageNumber--;
                }
                else if (option == "3")
                {
                    // Ustaw flagę do ponownego zapytania o filtry
                    askForFilters = true;
                    pageNumber = 1; // Resetuj paginację
                }
                else if (option == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Nieznana opcja. Spróbuj ponownie.");
                }
            }
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Usuwa użytkownika.
        /// @details Pobiera ID użytkownika i usuwa go z bazy danych, jeśli istnieje.
        protected async Task DeleteUser()
        {
            Console.WriteLine("\n=== Usuń użytkownika ===");

            // Pobierz listę użytkowników
            var users = await _userService.GetAllUsersAsync();
            if (!users.Any())
            {
                Console.WriteLine("Brak użytkowników do usunięcia.");
                return;
            }

            // Wyświetl listę użytkowników
            Console.WriteLine("Dostępni użytkownicy:");
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.UserId}, Imię: {user.Name}, Nazwisko: {user.Surname}, Email: {user.Email}, Rola: {user.Role.Name}");
            }

            // Wybierz użytkownika do usunięcia
            int userId;
            while (true)
            {
                Console.Write("Podaj ID użytkownika do usunięcia: ");
                if (int.TryParse(Console.ReadLine(), out userId) && users.Any(u => u.UserId == userId))
                {
                    break;
                }
                Console.WriteLine("Nieprawidłowy ID użytkownika. Spróbuj ponownie.");
            }

            var userToDelete = users.First(u => u.UserId == userId);

            // Potwierdzenie usunięcia
            Console.WriteLine($"Czy na pewno chcesz usunąć użytkownika: {userToDelete.Name} {userToDelete.Surname} (ID: {userToDelete.UserId})? (tak/nie)");
            string confirmation = Console.ReadLine()?.Trim().ToLower();

            if (confirmation != "tak")
            {
                Console.WriteLine("Anulowano usunięcie użytkownika.");
                return;
            }

            // Usuń użytkownika z bazy danych
            await _userService.DeleteUserAsync(userId);

            Console.WriteLine($"Użytkownik {userToDelete.Name} {userToDelete.Surname} został usunięty.");
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Edytuje dane profilu zalogowanego użytkownika.
        /// @details Pozwala zmienić imię, nazwisko oraz adres e-mail.
        ///          Sprawdza poprawność i unikalność e-maila.
        protected async Task EditProfile(User loggedUser)
        {
            Console.WriteLine("=== Edycja profilu ===");

            // Zmiana imienia
            Console.Write($"Aktualne imię: {loggedUser.Name}, nowe imię (ENTER, aby zachować): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                loggedUser.Name = newName;
            }

            // Zmiana nazwiska
            Console.Write($"Aktualne nazwisko: {loggedUser.Surname}, nowe nazwisko (ENTER, aby zachować): ");
            string newSurname = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newSurname))
            {
                loggedUser.Surname = newSurname;
            }

            // Zmiana adresu email
            Console.Write($"Aktualny email: {loggedUser.Email}, nowy email (ENTER, aby zachować): ");
            string newEmail = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(newEmail))
            {
                // Sprawdzenie poprawności formatu email
                if (!IsValidEmail(newEmail))
                {
                    Console.WriteLine("Nieprawidłowy adres email.");
                    return;
                }

                // Sprawdzenie, czy email jest już używany przez innego użytkownika
                var existingUser = await _userService.GetUserByEmailAsync(newEmail);
                if (existingUser != null && existingUser.UserId != loggedUser.UserId)
                {
                    Console.WriteLine("Podany adres email jest już używany przez innego użytkownika.");
                    return;
                }

                // Jeśli email jest poprawny i dostępny, ustaw go dla zalogowanego użytkownika
                loggedUser.Email = newEmail;
            }
            // Aktualizacja danych w bazie
            await _userService.UpdateUserAsync(loggedUser.UserId, loggedUser.Name, loggedUser.Surname, loggedUser.Email, loggedUser.Password, loggedUser.RoleId);

            Console.WriteLine("Dane profilu zostały zaktualizowane.");
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }

        /// @brief Zmienia hasło zalogowanego użytkownika.
        /// @details Weryfikuje stare hasło oraz sprawdza poprawność i zgodność nowego hasła.
        protected async Task ChangePassword(User loggedUser)
        {
            Console.WriteLine("=== Zmiana hasła ===");

            // Prośba o stare hasło
            Console.Write("Podaj aktualne hasło: ");
            string currentPassword = ReadPassword();

            if (currentPassword != loggedUser.Password)
            {
                Console.WriteLine("Podane hasło jest nieprawidłowe.");
                return;
            }
            // Prośba o nowe hasło
            Console.Write("Podaj nowe hasło: ");
            string newPassword = ReadPassword();

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 8 ||
                !newPassword.Any(char.IsUpper) || !newPassword.Any(char.IsLower))
            {
                Console.WriteLine("Hasło musi mieć co najmniej 8 znaków, w tym jedną dużą i jedną małą literę.");
                return;
            }

            // Prośba o potwierdzenie nowego hasła
            Console.Write("Potwierdź nowe hasło: ");
            string confirmPassword = ReadPassword();

            if (newPassword != confirmPassword)
            {
                Console.WriteLine("Hasła nie pasują do siebie.");
                return;
            }


            // Aktualizacja hasła
            loggedUser.Password = newPassword;
            await _userService.UpdateUserAsync(loggedUser.UserId, loggedUser.Name, loggedUser.Surname, loggedUser.Email, loggedUser.Password, loggedUser.RoleId);

            Console.WriteLine("Hasło zostało zmienione.");
            Console.WriteLine("Naciśnij ENTER, aby kontynuować...");
            Console.ReadLine();
        }
    }
}
