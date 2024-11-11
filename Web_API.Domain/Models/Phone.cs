namespace Web_API.Domain.Models
{
    public class Phone
    {
        public Guid Id { get; set; }
        public string PhoneType { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public Guid PersonId { get; set; }
        public Person Person { get; set; } = null!;
    }
}
