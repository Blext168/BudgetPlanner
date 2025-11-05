using BudgetPlanner.Interfaces;

namespace BudgetPlanner.Classes
{
    public sealed class DummyBiometricService : IBiometricService
    {
        public Task<bool> IsAvailableAsync() => Task.FromResult(false);

        public Task<bool> AuthenticateAsync(string reason) => Task.FromResult(false);
    }
}
