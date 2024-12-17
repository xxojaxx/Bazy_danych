using ERPv2.Services;

namespace ERPv2.ConsoleApp
{
    /// @class ManagerMenu
    /// @brief Klasa obsługująca menu manadżera w aplikacji konsolowej ERPv2.
    public class ManagerMenu : UserMenu
    {
        public ManagerMenu(ItemService itemService, BomService bomService, BomItemService bomItemService,
            SupplierService supplierService, SupplyService supplyService, ClientService clientService, AddressService addressService,
            OrderService orderService, OrderItemService orderItemService, SupplyItemService supplyItemService, InventoryService inventoryService,
            UserService userService, RoleService roleService, UnitService unitService, TypeService typeService, CurrencyService currencyService)
    : base(itemService, bomService, bomItemService, supplierService, supplyService, clientService, addressService, orderService, orderItemService, supplyItemService, inventoryService, userService, roleService, unitService, typeService, currencyService)
        {

        }
    }
}
