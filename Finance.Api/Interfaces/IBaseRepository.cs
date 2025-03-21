namespace Finance.Api.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> Delete();
    }
}
