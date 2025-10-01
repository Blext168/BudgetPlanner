namespace BudgetPlanner.Result;

public abstract class BaseResult<TM> where TM : class
{
    public bool Successful { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public virtual TM? Item { get; set; }

    public BaseResult() { }
}