namespace ReadList.Domain.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataModificacao { get; set; }
    }
}
