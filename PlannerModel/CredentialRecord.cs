namespace PlannerModel
{
    public class CredentialRecord
    {
        public string CredentialId { get; set; } = string.Empty; // base64
        public byte[] PublicKey { get; set; } = [];
        public uint SignCount { get; set; }
    }
}
