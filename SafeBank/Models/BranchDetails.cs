namespace SafeBank.Models
{
    public class BranchDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public bool CanDelete { get; set; }
    }
}