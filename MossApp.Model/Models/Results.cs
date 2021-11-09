
using System;
using System.Collections.Generic;

#nullable disable

namespace MossApp.Data.Models
{
    public class Results
    {

        public int Id { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string Options { get; set; }

        public ICollection<MatchPair> MatchPairs { get; set; }

    }
}
