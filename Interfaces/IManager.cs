using BudgetPlanner.Result;

namespace BudgetPlanner.Interfaces;

public interface IManager<TM, TR> where TM : class 
                                  where TR : BaseResult<TM>
{
    Task<IEnumerable<TM>> LoadAll(int userId);
    Task<TR> Load(int id);
    Task<TR> Save(TM vehicle);
    Task<TR> Delete(int id);
}