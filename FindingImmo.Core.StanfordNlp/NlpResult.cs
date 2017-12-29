using System;
using System.Collections.Generic;

namespace FindingImmo.Core.StanfordNlp
{
    public sealed class NlpResult
    {
        public IEnumerable<NlpSentence> Sentences { get; }

        internal NlpResult(IEnumerable<NlpSentence> sentences)
        {
            if (sentences == null)
                throw new ArgumentNullException(nameof(sentences));

            this.Sentences = sentences;
        }
    }
}
