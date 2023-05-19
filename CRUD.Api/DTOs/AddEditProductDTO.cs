using CRUD.Api.Domain;
using Newtonsoft.Json;

namespace DTOs;
public class AddEditProductDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public double Rate { get; set; }
    public List<Attachment> Attachments { get; set; }
}