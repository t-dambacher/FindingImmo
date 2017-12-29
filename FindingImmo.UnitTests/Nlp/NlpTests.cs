using FindingImmo.Core.StanfordNlp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FindingImmo.UnitTests.Nlp
{
    [TestClass]
    public class NlpTests : BaseTests
    {
        const string Category = "Stanford NLP";

        private static StanfordNlpService nlpService;

        [TestInitialize]
        public void Test_Initialize()
        {
            nlpService = new StanfordNlpService();
        }

        [TestMethod, TestCategory(Category)]
        public void Try_StanfortNLP_Initialisation()
        {
            if (nlpService == null)
                Assert.Fail();
        }

        [TestMethod, TestCategory(Category)]
        public void Try_StanfortNLP_Lemmatization()
        {
            var service = new NlpService(nlpService);
            var result = service.Lemmatize("Ceci est un test.");

            Assert.AreEqual(5, result.Count());
            Assert.AreEqual("Ceci", result.ElementAt(0));
            Assert.AreEqual("est", result.ElementAt(1));
            Assert.AreEqual("un", result.ElementAt(2));
            Assert.AreEqual("test", result.ElementAt(3));
            Assert.AreEqual(".", result.ElementAt(4));

            result = service.Lemmatize("Ceci est un test. Et encore un autre.");
            Assert.AreEqual(10, result.Count());
        }

        [TestMethod, TestCategory(Category)]
        public void Try_StanfortNLP_Splitting()
        {
            var service = new NlpService(nlpService);
            var result = service.Split("Ceci est un test.");
            Assert.AreEqual(5, result.Count());
        }
    }
}
