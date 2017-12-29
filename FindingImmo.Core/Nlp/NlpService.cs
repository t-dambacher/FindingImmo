using FindingImmo.Core.StanfordNlp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FindingImmo.Core.Nlp
{
    public sealed class NlpService : INlpService
    {
        private readonly StanfordNlpService _nlpService;

        public NlpService(StanfordNlpService nlpService)
        {
            this._nlpService = nlpService;
        }

        public IEnumerable<string> Split(string document)
        {
            if (string.IsNullOrWhiteSpace(document))
                throw new ArgumentNullException(nameof(document));

            return this._nlpService.Analyze(document)
                ?.Sentences
                .SelectMany(s => s.Words)
                .Select(w => w.Value)
                .ToList();
        }

        public IEnumerable<string> Lemmatize(string document)
        {
            if (string.IsNullOrWhiteSpace(document))
                throw new ArgumentNullException(nameof(document));

            return this._nlpService.Analyze(document)
                ?.Sentences
                .SelectMany(s => s.Words)
                .Select(w => w.Lemma)
                .ToList();
        }
    }
}
