using BudgetPlanner.Interfaces;
using BudgetPlanner.Model;
using BudgetPlanner.Result;
using Microsoft.EntityFrameworkCore;

namespace BudgetPlanner.Manager;

public class VehicleManager : IVehicleManager
{
    public async Task<IEnumerable<Vehicle>> LoadAll(int userId)
    {
        await using DbModel context = DbModel.GetContext();
        List<Vehicle> vehicles = await context.Vehicle.Where(w => w.UserId == userId).ToListAsync();
        return vehicles;
        // return vehicles.Select(vehicle => new VehicleResult(true, vehicle)).ToList();
    }
    
    public async Task<VehicleResult> Load(int id)
    {
        await using DbModel context = DbModel.GetContext();
        var vehicle = await context.Vehicle.FirstOrDefaultAsync(w => w.Id == id);
        return vehicle is null ? 
                   new VehicleResult("Fahrzeug konnte nicht geladen werden.", id) : 
                   new VehicleResult(true, vehicle);
    }

    public async Task<VehicleResult> Save(Vehicle vehicle)
    {
        await using DbModel context = DbModel.GetContext();

        try
        {
            if (vehicle.Id is 0)
                await context.Vehicle.AddAsync(vehicle);
            else
                context.Vehicle.Update(vehicle);
            
            await context.SaveChangesAsync();
            return new VehicleResult(true, vehicle.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new VehicleResult("Es ist ein unerwarteter Fehler aufgetreten: " + e.Message, vehicle.Id);
        }
    }
    
    public async Task<VehicleResult> Delete(int id)
    {
        await using DbModel context = DbModel.GetContext();
        var vehicle = await context.Vehicle.FirstOrDefaultAsync(w => w.Id == id);
        if (vehicle is null)
            return new VehicleResult("Fahrzeug konnte nicht gefunden werden.", id);
        
        context.Vehicle.Remove(vehicle);
        await context.SaveChangesAsync();
        return new VehicleResult(true, id);
    }
}