using Heraldry.Blazon.Vocabulary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraldryTest.Helpers
{
    /// <summary>
    /// Not an actual Mock, God please forgive me. T__T
    /// </summary>
    class MockVocabulary
    {
        public static BlazonVocabulary Get()
        {
            var sourcesDirectory = Environment.CurrentDirectory + "\\..\\..\\..";

            return VocabularyLoader.LoadFromDirectory(
                blazonDirectory: sourcesDirectory + "\\resources\\en_olde\\",
                numbers: "english",
                verbose: false
                );
        }
    }
}
