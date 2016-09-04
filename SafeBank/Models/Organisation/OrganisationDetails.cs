namespace SafeBank.Models.Organisation
{
    public class OrganisationDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public bool CanDelete { get; set; }
    }
}