using System.Threading.Tasks;

namespace Ignite.Common.Authentication
{
    public interface IAuthenticator
    {
        Task<string> Authenticate();
    }
}