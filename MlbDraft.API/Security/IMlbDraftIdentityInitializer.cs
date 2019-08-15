using System.Threading.Tasks;

namespace  MLBDraft.API.Security
{
    public interface IMlbDraftIdentityInitializer
    {
         Task Seed();
    }
}