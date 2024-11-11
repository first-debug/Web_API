namespace Web_API.Domain.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<Phone> Phones { get; set; } = new List<Phone>();

        public void AddPhone(Phone phone)
        {
            Phones.Add(phone);
        }

        public void RemoveAt(int index)
        {
            Phones.RemoveAt(index);
        }

        public int PhoneCount { get { return Phones.Count; } }
    }
}
