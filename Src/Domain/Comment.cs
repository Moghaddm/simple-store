namespace Store.Domain
{
    public class Comment : Entity
    {
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Caption { get; set; }
        public Comment(string nickName, string email, string caption)
        {
            if (string.IsNullOrWhiteSpace(nickName))
                throw new ArgumentNullException(nameof(nickName));
            if (string.IsNullOrWhiteSpace(Email))
                throw new ArgumentNullException(nameof(Email));
            if (string.IsNullOrWhiteSpace(Caption))
                throw new ArgumentNullException(nameof(Caption));
            NickName = nickName;
            Email = email;
            Caption = caption;
        }
        public Product Product { get; set; }
    }
}