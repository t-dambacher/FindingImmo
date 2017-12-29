using edu.stanford.nlp.ling;
using edu.stanford.nlp.util;
using System;

namespace FindingImmo.Core.StanfordNlp
{
    public sealed class NlpWord
    {
        public string Value { get; }
        public string Text { get; }
        public string OriginalText { get; }
        public string CharacterOffsetBegin { get; }
        public string CharacterOffsetEnd { get; }
        public string Before { get; }
        public string After { get; }
        public string Index { get; }
        public string SentenceIndex { get; }
        public string PartOfSpeech { get; }
        public string Lemma { get; }
        public string TokenBegin { get; }
        public string TokenEnd { get; }
        public string NamedEntityTag { get; }
        public string BeginIndex { get; }
        public string EndIndex { get; }
        public string Utterance { get; }
        public string Paragraph { get; }
        public string Speaker { get; }

        internal NlpWord(CoreLabel label)
        {
            if (label == null)
                throw new ArgumentNullException(nameof(label));

            this.Value = GetValue<CoreAnnotations.ValueAnnotation>(label);
            this.Text = GetValue<CoreAnnotations.TextAnnotation>(label);
            this.OriginalText = GetValue<CoreAnnotations.OriginalTextAnnotation>(label);
            this.CharacterOffsetBegin = GetValue<CoreAnnotations.CharacterOffsetBeginAnnotation>(label);
            this.CharacterOffsetEnd = GetValue<CoreAnnotations.CharacterOffsetEndAnnotation>(label);
            this.Before = GetValue<CoreAnnotations.BeforeAnnotation>(label);
            this.After = GetValue<CoreAnnotations.AfterAnnotation>(label);
            this.Index = GetValue<CoreAnnotations.IndexAnnotation>(label);
            this.SentenceIndex = GetValue<CoreAnnotations.SentenceIndexAnnotation>(label);
            this.PartOfSpeech = GetValue<CoreAnnotations.PartOfSpeechAnnotation>(label);
            this.Lemma = GetValue<CoreAnnotations.LemmaAnnotation>(label);
            this.TokenBegin = GetValue<CoreAnnotations.TokenBeginAnnotation>(label);
            this.TokenEnd = GetValue<CoreAnnotations.TokenEndAnnotation>(label);
            this.NamedEntityTag = GetValue<CoreAnnotations.NamedEntityTagAnnotation>(label);
            this.BeginIndex = GetValue<CoreAnnotations.BeginIndexAnnotation>(label);
            this.EndIndex = GetValue<CoreAnnotations.EndIndexAnnotation>(label);
            this.Utterance = GetValue<CoreAnnotations.UtteranceAnnotation>(label);
            this.Paragraph = GetValue<CoreAnnotations.ParagraphAnnotation>(label);
            this.Speaker = GetValue<CoreAnnotations.SpeakerAnnotation>(label);
        }

        static private string GetValue<T>(CoreLabel label)
            where T : CoreAnnotation, TypesafeMap.Key
        {
            return (label.get(typeof(T)) ?? string.Empty).ToString();
        }
    }
}
