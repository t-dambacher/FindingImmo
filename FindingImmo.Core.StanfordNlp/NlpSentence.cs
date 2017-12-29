using System;
using System.Collections.Generic;

namespace FindingImmo.Core.StanfordNlp
{
    public sealed class NlpSentence
    {
        public IEnumerable<NlpWord> Words { get; }

        internal NlpSentence(IEnumerable<NlpWord> words)
        {
            if (words == null)
                throw new ArgumentNullException(nameof(words));

            this.Words = words;
        }
    }
}
