namespace GoodsForPets.Models
{
    record ProductRecord
    (
        string Name,
        double Cost,
        int MaxDiscountAmount,
        int CurrentDiscountAmount,
        int QuantityInStock,
        string Description,
        string Photo,
        string Manufacturer,
        string Supplier,
        string Category
    );
}
