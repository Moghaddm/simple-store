using CRUD.Api.Domain;
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
        public static Product ToProductDtoMap(AddEditProductDTO product) =>
        new Product()
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Rate = product.Rate,
            Attachments = product.Attachments
        };
}
