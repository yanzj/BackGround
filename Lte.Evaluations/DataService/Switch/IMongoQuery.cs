namespace Lte.Evaluations.DataService.Switch
{
    public interface IMongoQuery<out T>
    {
        T Query();
    }
}
