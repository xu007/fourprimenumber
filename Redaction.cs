using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace PingarCodeChallenge
{

    /// <summary>
    /// Background:
    /// We'd like to redact a document by replacing certain substrings (names, dates, etc.) 
    /// with non-identifying place-holders: 
    /// e.g., replacing every instance of "Neil Armstrong" with "John Doe", 
    /// or replacing every instance of a date with "XX/XX/XXXX". 
    /// 
    /// In the resulting redacted document, 
    /// we need to be able to tell where each redaction was made, 
    /// so it can be highlighted later when the redacted document is displayed.
    /// </summary>
    public static class Redactor
    {
        public static void DoTextRedaction()
        {
            var document = "Neil123Neil456Neil789";
            var regex = "Neil";
            var replacementString = "Xu";
            IReadOnlyCollection<ReplacementLocation> replacementLocations;

            var result = Redact(document, regex, replacementString, out replacementLocations);

            Console.WriteLine(document);
            Console.WriteLine(result);
            Console.WriteLine();
            foreach (var location in replacementLocations)
            {
                Console.WriteLine("location: {0}  {1}", location.startIndex, location.endIndex);
            }

            Console.WriteLine("Press any key to end...");
            Console.ReadKey();
        }

        /// <summary>
        /// Redacts a document by replacing all instances of regex with replacementString, and outputs where each replacement is within the resulting redacted text.
        /// </summary>
        /// <param name="document">The document text to redact</param>
        /// <param name="regex">A regular expression representing all substrings to replace</param>
        /// <param name="replacementString">Text to replace each regex match with</param>
        /// <param name="replacementLocations">Positions of replacements within the resulting redacted document</param>
        /// <returns>The redacted text</returns>
        public static string Redact(
            string document, 
            string regex, 
            string replacementString, 
            out IReadOnlyCollection<ReplacementLocation> replacementLocations)
        {
            var len = replacementString.Length;
            var list = new List<ReplacementLocation>();
            var reg = new Regex(regex);

            while (true)
            {
                Match match = Regex.Match(document, regex);
                if (!match.Success) break;

                list.Add(new ReplacementLocation(match.Index, match.Index + len));
                document = reg.Replace(document, replacementString, 1);
            }

            replacementLocations = new ReadOnlyCollection<ReplacementLocation>(list);
            return document;
        }
    }

    // Do not alter this class
    public class ReplacementLocation
    {
        public readonly int startIndex;
        public readonly int endIndex; //Note: by convention, endIndex is startIndex + length; that is, the index of the last character of the replacement + 1

        /// <param name="endIndex">By convention, endIndex is startIndex + length</param>
        public ReplacementLocation(int startIndex, int endIndex)
        {
            this.startIndex = startIndex;
            this.endIndex = endIndex;
        }
    }
}

