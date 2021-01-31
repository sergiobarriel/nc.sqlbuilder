namespace NC.SqlBuilder.Abstractions.Operations
{
    public interface ISimpleOperation : IOperation
    {
        string Value { get; set; }
    }
}