using FindingImmo.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FindingImmo.Core.Featurization.Evaluators
{
    internal abstract class RegexFeatureEvaluator : TextFeatureEvaluator<BooleanFeatureResult>
    {
        private readonly Regex _regex;

        protected RegexFeatureEvaluator(string regex, RegexOptions options = RegexOptions.None)
        {
            if (string.IsNullOrWhiteSpace(regex))
                throw new ArgumentNullException(regex);

            this._regex = new Regex(regex, options | RegexOptions.Compiled);
        }

        protected override BooleanFeatureResult Evaluate(IEnumerable<string> texts)
        {
            foreach (string text in texts)
            {
                if (this._regex.IsMatch(text))
                    return BooleanFeatureResult.True;
            }

            return BooleanFeatureResult.False;
        }
    }
}
