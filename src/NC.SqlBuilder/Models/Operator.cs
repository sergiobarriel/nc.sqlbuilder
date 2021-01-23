using System.ComponentModel;

namespace NC.SqlBuilder.Models
{
    public enum Operator
    {
        [Description("Equals")]
        Equals = 0,

        [Description("Less than")]
        LessThan,

        [Description("Less than or equal")]
        LessThanOrEqual,

        [Description("Greater than")]
        GreaterThan,

        [Description("Greater than or equal")]
        GreaterThanOrEqual,

        [Description("Between")]
        Between
    }
}
