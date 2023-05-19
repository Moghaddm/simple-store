namespace CRUD.Api.Domain
{
    public class Comment : Entity
    {
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Caption { get; set; }
        
        public Product Product { get; set; }
    }
}