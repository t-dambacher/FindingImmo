using System.Collections.Generic;

namespace FindingImmo.Core.Nlp
{
    public interface INlpService
    {
        IEnumerable<string> Split(string document);
        IEnumerable<string> Lemmatize(string document);
    }
}
