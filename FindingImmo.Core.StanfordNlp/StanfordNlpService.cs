using edu.stanford.nlp.ling;
using edu.stanford.nlp.pipeline;
using edu.stanford.nlp.util;
using java.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StanfordCoreNlpPipeline = edu.stanford.nlp.pipeline.StanfordCoreNLP;

namespace FindingImmo.Core.StanfordNlp
{
    /// <remarks>
    /// Help can be found at https://stanfordnlp.github.io/CoreNLP/annotators.html
    /// </remarks>
    public sealed class StanfordNlpService
    {
        /// <summary>
        /// Path to the folder with models extracted from `stanford-corenlp-3.4-models.jar`
        /// </summary>
        private const string JarRootDirectory = @".\..\..\..\StanfordNLP\models\";

        private readonly StanfordCoreNlpPipeline _pipeline;

        public StanfordNlpService()
        {
            _pipeline = CreatePipeline();
        }

        private string JarRoot
        {
            get { return Path.Combine(Environment.CurrentDirectory, JarRootDirectory); }
        }

        private StanfordCoreNlpPipeline CreatePipeline()
        {
            // Create StanfordCoreNLP object properties, with POS tagging
            // (required for lemmatization), and lemmatization
            StanfordCoreNlpPipeline res = null;

            // Annotation pipeline configuration
            var props = new Properties();
            // Use POS tagging, lemmatization, NER, parsing, and coreference resolution
            props.setProperty("annotators", "tokenize, ssplit, pos, lemma, ner, parse, dcoref");
            props.setProperty("sutime.binders", "0");
         //   props.setProperty("tokenize.language", "fr");
       //     props.setProperty("parse.model", "edu/stanford/nlp/models/srparser/frenchSR.beam.ser.gz");
            

            // We should change current directory, so StanfordCoreNLP could find all the model files automatically 
            string currentDirectory = Environment.CurrentDirectory;
            try
            {
                Directory.SetCurrentDirectory(JarRoot);

                /*
                 * This is a pipeline that takes in a string and returns various analyzed linguistic forms. 
                 * The String is tokenized via a tokenizer (such as PTBTokenizerAnnotator), 
                 * and then other sequence model style annotation can be used to add things like lemmas, 
                 * POS tags, and named entities. These are returned as a list of CoreLabels. 
                 * Other analysis components build and store parse trees, dependency graphs, etc. 
                 * 
                 * This class is designed to apply multiple Annotators to an Annotation. 
                 * The idea is that you first build up the pipeline by adding Annotators, 
                 * and then you take the objects you wish to annotate and pass them in and 
                 * get in return a fully annotated object.
                 * 
                 *  StanfordCoreNLP loads a lot of models, so you probably
                 *  only want to do this once per execution
                 */
                res = new StanfordCoreNlpPipeline(props);
            }
            finally
            {
                Directory.SetCurrentDirectory(currentDirectory);
            }

            return res;
        }

        public NlpResult Analyze(string document)
        {
            if (string.IsNullOrWhiteSpace(document))
                throw new ArgumentNullException(nameof(document));

            IList<CoreMap> maps = GetSentences(document).ToList();
            IList<NlpSentence> sentences = new List<NlpSentence>(maps.Count);

            foreach (CoreMap sentence in maps)
            {
                IList<CoreLabel> labels = GetWords(sentence).ToList();
                IList<NlpWord> words = new List<NlpWord>(labels.Count);

                foreach (CoreLabel word in labels)
                {
                    words.Add(new NlpWord(word));
                }

                sentences.Add(new NlpSentence(words));
            }

            return new NlpResult(sentences);
        }

        private IEnumerable<CoreLabel> GetWords(CoreMap sentence)
        {
            // Iterate over all tokens in a sentence
            foreach (object o in sentence.get(typeof(CoreAnnotations.TokensAnnotation)) as ArrayList)
            {
                yield return (CoreLabel)o;
            }
        }

        private IEnumerable<CoreMap> GetSentences(string document)
        {
            // Create an empty Annotation just with the given text
            // Run all Annotators on this text
            Annotation annotation = new Annotation(document);
            this._pipeline.annotate(annotation);

            // These are all the sentences in this document
            // Iterate over all of the sentences found
            foreach (object sentence in (annotation.get(typeof(CoreAnnotations.SentencesAnnotation)) as ArrayList))
            {
                // A CoreMap is essentially a Map that uses class objects as keys and has values with custom types
                yield return sentence as CoreMap;
            }
        }
    }
}
