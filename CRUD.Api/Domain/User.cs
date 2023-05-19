namespace CRUD.Api.Domain
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<Comment> Comments { get; set; }
    }
}