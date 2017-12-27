using FindingImmo.Core.Domain.Models;
using System;

namespace FindingImmo.Core.Featurization.Evaluators
{
    internal abstract class FeatureEvaluator<TResult>
        where TResult : struct, IComparable, IFormattable, IConvertible   // Enum-like constraint
    {
        public abstract TResult Evaluate(Ad ad);
    }
}
