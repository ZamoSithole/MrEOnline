using System.ComponentModel;

namespace MrE.Models
{
    public enum DataOperation
    {
        [Description("Insert")]
        Insert,

        [Description("Update")]
        Update,

        [Description("Select")]
        Select,

        [Description("Delete")]
        Delete
    }
}
