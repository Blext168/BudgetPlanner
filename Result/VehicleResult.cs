using BudgetPlanner.Model;

namespace BudgetPlanner.Result;

public sealed class VehicleResult : BaseResult<Vehicle>
{
    public int ItemId { get; set; }
    public override Vehicle? Item { get; set; }
    
    public VehicleResult(bool successful, Vehicle vehicle)
    {
        Successful = successful;
        Item = vehicle;
        ItemId = vehicle.Id;
    }
    
    public VehicleResult(bool successful, int itemId)
    {
        Successful = successful;
        ItemId = itemId;
    }
    
    public VehicleResult(string errorMessage, int itemId)
    {
        Successful = false;
        ItemId = itemId;
        ErrorMessage = errorMessage;
    }
}