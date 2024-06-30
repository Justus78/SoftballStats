using System.ComponentModel.DataAnnotations;

namespace SoftballStats.Models
{
    public class Stats
    {
        [Key]
        public int StatsID { get; set; }
        public int? GamesPlayed { get; set; } = 0;
        public int? AtBats { get; set; } = 0;
        public int? Hits { get; set; } = 0;
        public int? Runs { get; set; } = 0;
        public int? RBIs { get; set; } = 0;
        public int? Walks { get; set; } = 0;
        public int? Strikeouts { get; set; } = 0;
        public int? StolenBases { get; set; } = 0;
        public int? Errors { get; set; } = 0;
        public int? Singles { get; set; } = 0;
        public int? Doubles { get; set; } = 0;
        public int? Triples { get; set; } = 0;
        public int? HomeRuns { get; set; } = 0;
        public int? HitByPitch { get; set; } = 0;
        public int? SacrificeFly { get; set; } = 0;
        public int?  SacrificeBunt { get; set; } = 0;
    } // end class Stats
} // end namespace SoftballStats.Models
