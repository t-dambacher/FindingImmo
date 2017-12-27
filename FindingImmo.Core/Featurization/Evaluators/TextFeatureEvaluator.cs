using FindingImmo.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FindingImmo.Core.Featurization.Evaluators
{
    internal abstract class TextFeatureEvaluator<TResult> : FeatureEvaluator<TResult>
        where TResult : struct, IComparable, IFormattable, IConvertible   // Enum-like constraint
    {
        public sealed override TResult Evaluate(Ad ad)
        {
            if (ad == null)
                throw new ArgumentNullException(nameof(ad));

            return Evaluate(new[] { ad.Title, ad.Description }.Where(s => !string.IsNullOrWhiteSpace(s)));
        }

        protected abstract TResult Evaluate(IEnumerable<string> texts);
    }
}
