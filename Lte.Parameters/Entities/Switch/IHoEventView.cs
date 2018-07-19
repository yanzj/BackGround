namespace Lte.Parameters.Entities.Switch
{
    public interface IHoEventView
    {
        int Hysteresis { get; set; }

        int TimeToTrigger { get; set; }
    }
}