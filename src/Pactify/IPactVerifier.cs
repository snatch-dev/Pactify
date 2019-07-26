using System.Threading.Tasks;

namespace Pactify
{
    public interface IPactVerifier
    {
        Task VerifyAsync();
        void Verify();
    }
}
