namespace Lte.Domain.LinqToCsv.Test
{
    public interface IAssertable<in T>
    {
        void AssertEqual(T other);
    }
}
