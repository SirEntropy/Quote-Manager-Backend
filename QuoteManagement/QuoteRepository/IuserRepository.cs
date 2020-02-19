using QuoteModels;

namespace QuoteRepository
{
    public interface IuserRepository:IGenericRepository<user>
    {
        bool CheckCredentials(string username, string password);
        bool CheckUserExists(string username);
    }
}