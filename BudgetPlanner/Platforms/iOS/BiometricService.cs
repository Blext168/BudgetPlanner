using BudgetPlanner.Interfaces;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

namespace BudgetPlanner.Platforms.iOS
{
    public sealed class BiometricService : IBiometricService
    {
        public Task<bool> IsAvailableAsync() => CrossFingerprint.Current.IsAvailableAsync();

        public async Task<bool> AuthenticateAsync(string reason)
        {
            var available = await CrossFingerprint.Current.IsAvailableAsync();
            if (!available) return false;

            var request = new AuthenticationRequestConfiguration("Authentifizierung", reason);
            var result = await CrossFingerprint.Current.AuthenticateAsync(request);
            return result.Authenticated;
        }
    }
}
