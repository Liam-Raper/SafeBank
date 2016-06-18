namespace SafeBank.Models
{
    public class OrganisationDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public bool CanDelete { get; set; }
    }
}