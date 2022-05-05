namespace ReadList.Domain.Models
{
    public class UserModel : BaseModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual List<BookModel> Books { get; set; }
    }
}
