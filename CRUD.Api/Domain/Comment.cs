namespace CRUD.Api.Domain
{
    public class Comment : Entity
    {
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Caption { get; set; }
        public Comment(string nickName, string email, string caption, Product product)
        {
            NickName = nickName;
            Email = email;
            Caption = caption;
        }
        public Product Product { get; set; }
    }
}