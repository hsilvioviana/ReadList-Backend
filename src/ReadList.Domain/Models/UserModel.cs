namespace ReadList.Domain.Models
{
    public class UserModel : BaseModel
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public virtual List<BookModel> Books { get; set; }  = new List<BookModel>();
    }
}
