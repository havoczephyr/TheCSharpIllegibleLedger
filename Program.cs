using System.Text;
using System.Text.RegularExpressions;
namespace TheCSharpIllegibleLedger
{


    static class Program
    {
        ///
        /// <summary>
        /// sends out a <c>WriteLine</c> to notify the user the application is running, then perform
        /// line by line reads of pre-curated.tsv, if the file exists. 
        public static void Main(String[] args)
        {
            Console.WriteLine("\n The Legible Unintelligible Ledger: C# Edition!");
            Console.WriteLine("\n by Giovanni D'Amico");
            // string test = @"<RSE time=""00:00:00.000-00:00:00.000""></RSE>";
            // Replacer redactedSensitive = new Replacer(new System.Text.RegularExpressions.Regex(@"(<[/]*)RSE([^>]*>)"), "RedactedSensitive");
            // Console.WriteLine(redactedSensitive.Replace(test));
            List<Replacer> replacers = CreateReplacers();
            List<string> replacedLines = new List<string>();
            try
            {
                using (var file = File.Open("pre-curated.tsv", FileMode.Open))
                {
                    using (var reader = new System.IO.StreamReader(file))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            foreach (Replacer replacer in replacers)
                            {
                                line = replacer.Replace(line);
                            }
                            replacedLines.Add(line);
                        }

                    }

                }
                File.WriteAllText("curated.tsv", string.Join('\n', replacedLines));
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("Sorry! Couldn't find a pre-curated.tsv file here.\n please run me in a directory with a pre-curated.tsv file!");
            }




        }
        private static List<Replacer> CreateReplacers()
        {
            return new List<Replacer>{
                new Replacer(@"(\[)UA(\])", "Unintelligible:Articulation"),
                new Replacer(@"(\[)UC(\])", "Unintelligible:Crosstalk"),
                new Replacer(@"(\[)BS(\])", "BackgroundSpeech"),
                new Replacer(@"(<[/]*)RN([^>]*>)", "RedactedName"),
                new Replacer(@"(<[/]*)RSE([^>]*>)", "RedactedSensitive"),
                new Replacer(@"(<[/]*)RSP([^>]*>)", "RedactedSpeaker")
            };
        }

    }
}
