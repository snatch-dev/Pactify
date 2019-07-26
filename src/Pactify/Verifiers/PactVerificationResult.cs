using System.Collections.Generic;
using System.Linq;

namespace Pactify.Verifiers
{
    internal sealed class PactVerificationResult
    {
        public bool IsSuccessful => !Errors.Any();
        public IEnumerable<string> Errors => _errors;

        private readonly List<string> _errors = new List<string>();

        public PactVerificationResult()
        {
        }

        public PactVerificationResult(IEnumerable<string> errors)
        {
            _errors = errors.ToList();
        }

        public static PactVerificationResult operator &(PactVerificationResult left, PactVerificationResult right)
        {
            var errors = left.Errors.Concat(right.Errors);
            return new PactVerificationResult(errors);
        }
    }
}
