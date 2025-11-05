using BudgetPlanner.Interfaces;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Windows.Security.Credentials.UI;

namespace BudgetPlanner.Platforms.Windows
{
    public sealed class BiometricService : IBiometricService
    {
        public async Task<bool> IsAvailableAsync()
        {
            var available = await UserConsentVerifier.CheckAvailabilityAsync();
            return available == UserConsentVerifierAvailability.Available;
        }

        public async Task<bool> AuthenticateAsync(string reason)
        {
            var availability = await UserConsentVerifier.CheckAvailabilityAsync();
            if (availability != UserConsentVerifierAvailability.Available)
                return false;

            var result = await UserConsentVerifier.RequestVerificationAsync(reason);
            return result == UserConsentVerificationResult.Verified;
        }
    }
}
