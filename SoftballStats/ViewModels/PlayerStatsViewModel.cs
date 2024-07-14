using SoftballStats.Models;

namespace SoftballStats.ViewModels
{
    public class PlayerStatsViewModel
    {
        public Player Player { get; set; }
        public IEnumerable<GameStats> Stats { get; set; }

        public int? TotalAtBats { get {  return Stats.Sum(s => (int)s.AtBats); } } // end TotalAtBats get
        public int? TotalHits { get { return Stats.Sum(s => (int)s.Hits); } } // end TotalHits get
        public int? TotalRuns { get { return Stats.Sum(s => (int)s.Runs); } } // end TotalRuns get
        public int? TotalRBIs { get { return Stats.Sum(s => (int)s.RBIs); } } // end TotalRBIs get
        public int? TotalWalks { get { return Stats.Sum(s => (int)s.Walks); } } // end TotalWalks get
        public int? TotalStrikeouts { get { return Stats.Sum(s => (int)s.Strikeouts); } } // end TotalStrikeouts get
        public int? TotalStolenBases { get { return Stats.Sum(s => (int)s.StolenBases); } } // end TotalStolenBases get
        public int? TotalErrors { get { return Stats.Sum(s => (int)s.Errors); } } // end TotalErrors get
        public int? TotalSingles { get { return Stats.Sum(s => (int)s.Singles); } } // end TotalSingles get
        public int? TotalHomeRuns { get { return Stats.Sum(s => (int)s.HomeRuns); } } // end TotalHomeRuns get
        public int? TotalDoubles { get { return Stats.Sum(s => (int)s.Doubles); } } // end TotalDoubles get
        public int? TotalTriples { get { return Stats.Sum(s => (int)s.Triples); } } // end TotalTriples get
        public double? OnBasePercentage { get { return (double)TotalHits / (TotalAtBats + TotalWalks); } } // end OnBasePercentage get
        public double? SluggingPercentage { get { return (double)(TotalSingles + (2 * TotalDoubles) + (3 * TotalTriples) + (4 * TotalHomeRuns)) / TotalAtBats; } } // end SluggingPercentage get
        public double? BattingAverage { get { return (double)TotalHits / TotalAtBats; } } // end BattingAverage get
    } // end view model
} // end namespace
