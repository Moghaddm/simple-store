using Newtonsoft.Json;

namespace CRUD.Api.Domain;

public class Product : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public double Rate { get; set; }
    public List<Attachment> Attachments { get; set; }
    public List<Comment> Comments { get; set; }
}
