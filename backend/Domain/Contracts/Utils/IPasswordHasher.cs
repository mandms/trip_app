namespace Domain.Contracts.Utils
{
    public interface IPasswordHasher
    {
        public string Hash(string password);
        public bool Verify(string password, string hashedPassword);
    }
}
