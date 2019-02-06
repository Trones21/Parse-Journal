using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ParseJournal
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Entry> JournalEntries = new List<Entry>();
            
            var fullText = File.ReadLines("C:\\Users\\trones\\Documents\\_Projects\\ParseJournal\\ContinuousJournal.txt").ToList<string>();


            foreach (var line in fullText)
            {
                if (Regex.IsMatch(line, "[0-9]{1,2}/[0-9]{1,2}/[0-9]{4}"))
                {
                    var entry = new Entry();

                    entry.stringDate = Regex.Match(line, "[0-9]{1,2}/[0-9]{1,2}/[0-9]{4}").Value;

                    var dt = new DateTime();
                    DateTime.TryParse(entry.stringDate, out dt);
                    entry.ParsedDate = dt;

                    entry.startline = fullText.IndexOf(line);

                    JournalEntries.Add(entry);
                }
            }

            foreach (var entry in JournalEntries)
            {
                entry.AddText(fullText);
                entry.SetLength();
            }
           
            using (var db = new JournalDbContext())
            {
                db.entries.AddRange(JournalEntries);
                db.SaveChanges();

                Console.WriteLine("Finished! Wrote results to: " + db.Database.Connection.ToString());
            }

            Console.WriteLine("Finished");



        }
           
    }
}
