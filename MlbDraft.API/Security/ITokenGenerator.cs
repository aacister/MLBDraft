using System.Threading.Tasks;

namespace  MLBDraft.API.Security
{
    public interface ITokenGenerator
    {
        string CreateToken(string username);
    }
}