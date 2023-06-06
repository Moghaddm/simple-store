using Newtonsoft.Json;

namespace Store.Domain;

public class Product : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Price { get; private set; }
    public double Rate { get; private set; }
    public List<Attachment> Attachments { get; private set; }
    public IReadOnlyList<Comment> Comments { get; private set; }

    public Product(string name, string description, int price)
    {
        Name = name;
        Description = description;
        Price = price;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        Name = name;
    }

    public void SetDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentNullException(nameof(description));
        Description = description;
    }

    public void SetPrice(int price)
    {
        if (price < 0)
            throw new ArgumentException(nameof(price));
        Price = price;
    }

    public void AddAttachment(Byte[] image, string alt)
    {
        if (image is null)
            throw new ArgumentNullException(nameof(image));
        else if (string.IsNullOrWhiteSpace(alt))
            throw new ArgumentException(nameof(alt));
        Attachments.Add(new Attachment(image, alt));
    }

    public void UpdateAttachments(List<Attachment> attachments) => Attachments = attachments;

    public void GiveRate(int rate)
    {
        if (rate < 0)
            throw new ArgumentException(nameof(rate));
        Rate = rate;
    }
}
