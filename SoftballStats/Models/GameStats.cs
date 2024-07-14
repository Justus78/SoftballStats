using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftballStats.Models
{
    public class GameStats
    {
        [Key]
        public int StatsID { get; set; }

        [ForeignKey("Player")]
        public int? PlayerID { get; set; }
        [ValidateNever]
        public Player? Player { get; set; }

        public string? Opponent { get; set; }

        [DataType(DataType.Date)]
        public DateTime GameDate { get; set; }

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
        public int? SacrificeBunt { get; set; } = 0;
    }
}
