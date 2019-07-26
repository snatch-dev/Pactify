using System;
using System.Threading.Tasks;

namespace Pactify
{
    public interface IPactVerifier
    {
        IPactVerifier Before(Func<Task> onBefore);
        IPactVerifier After(Func<Task> onAfter);
        Task VerifyAsync();
        void Verify();
    }
}
