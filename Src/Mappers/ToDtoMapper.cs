using Store.Domain;
using DTOs;
using Newtonsoft.Json;

namespace Mappers;

public class ToDtoMapper
{
    public static AddEditProductDTO ToProductDtoMap(Product product) =>
        new AddEditProductDTO()
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Rate = product.Rate,
            Attachments = product.Attachments
        };
    public static Product ToProductDtoMap(AddEditProductDTO product)
    {
        var newProduct =new Product(product.Name, product.Description, product.Price);
        newProduct.GiveRate((int)product.Rate);
        newProduct.UpdateAttachments(product.Attachments);
        return newProduct;
    }
}
