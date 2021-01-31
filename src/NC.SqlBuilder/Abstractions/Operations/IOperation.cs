using NC.SqlBuilder.Models;

namespace NC.SqlBuilder.Abstractions.Operations
{

    public interface IOperation
    {
        string Field { get; set; }
        Operator Operator { get; set; }
    }
}
