namespace PlannerModel
{
    public class UserWithCredentials
    {
        public string Id { get; set; } = string.Empty; // base64 oder GUID
        public string Email { get; set; } = string.Empty;
        public List<CredentialRecord> Credentials { get; set; } = new();
    }
}