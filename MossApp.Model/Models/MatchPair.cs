#nullable disable

namespace MossApp.Data.Models
{
    public partial class MatchPair
    {
        public int Id { get; set; }

        public string AlphaFileName { get; set; }
        public string BetaFileName { get; set; }
        public int LinesMatched { get; set; }
        public decimal AlphaScore { get; set; }
        public decimal BetaScore { get; set; }
        public string AlphaPassage { get; set; }
        public string BetaPassage { get; set; }
        public string AlphaLines { get; set; }
        public string BetaLines { get; set; }

        public virtual Results Results { get; set; }
    }
}
