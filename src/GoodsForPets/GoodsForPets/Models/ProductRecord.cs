namespace GoodsForPets.Models
{
    public record ProductRecord
    (
        string ArticleNumber,
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
