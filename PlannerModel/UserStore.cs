using System.Collections.Concurrent;

namespace PlannerModel
{
    public class UserStore
    {
        private ConcurrentDictionary<string, UserWithCredentials> _users = new();
        public UserWithCredentials? GetByUsername(string username) => _users.GetValueOrDefault(username);

        public UserWithCredentials CreateUser(string email)
        {
            var user = new UserWithCredentials
            {
                Email = email,
                Id = Guid.NewGuid().ToString()
            };

            _users[email] = user;
            return user;
        }

        public void AddCredential(string username, CredentialRecord credential)
        {
            if (_users.TryGetValue(username, out var user))
                user.Credentials.Add(credential);
        }
    }
}
