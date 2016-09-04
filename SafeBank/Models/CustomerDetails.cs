namespace SafeBank.Models
{
    public class CustomerDetails
    {
        public int id;
        public string GivenName { private get; set; }
        public string FamilyName { private get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string FullName => GivenName + ", " + FamilyName;

    }
}