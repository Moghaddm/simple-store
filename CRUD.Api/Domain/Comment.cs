namespace CRUD.Api.Domain
{
    public class Comment : Entity
    {
        public string Caption { get; set; }
        
        public Product Product { get; set; }
        public User User { get; set; }
    }
}