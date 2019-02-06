using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace ParseJournal
{
    public class Entry
    {
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public DateTime ParsedDate { get; set; }
        public string stringDate { get; set; }
        public string text { get; set; }

        public int startline;
        public int length { get; set; }

        public void AddText(List<string> fullText)
        {
            //We don't want text before the entry starts
            var partialText = new List<string>();
            foreach (var line in fullText)
            {
                if(fullText.IndexOf(line) > startline)
                {
                    partialText.Add(" \n " + line);
                }
            }

            //We don't want text after the next entry begins
            foreach (var line in partialText)
            {
                if (!Regex.IsMatch(line, "[0-9]{1,2}/[0-9]{1,2}/[0-9]{4}"))
                {
                    this.text = this.text + line;
                }
                else
                {
                    break;
                }
            }
        }

        public void SetLength()
        {
            this.length = text.Length;
        }
    }
}
