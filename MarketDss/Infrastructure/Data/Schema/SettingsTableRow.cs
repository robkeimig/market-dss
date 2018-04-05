using System.ComponentModel.DataAnnotations.Schema;

namespace MarketDss.Infrastructure.Data.Schema
{
    [Table("Settings")]
    internal class SettingsTableRow
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
