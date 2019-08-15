using System.Threading.Tasks;

namespace  MLBDraft.API.Security
{
    public interface ITokenGenerator
    {
        Task<string> CreateToken(string username);
    }
}