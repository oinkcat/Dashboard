namespace DashboardCore.Models.Structure;

/// <summary>
/// Числовой показатель
/// </summary>
public class Gauge : Indicator
{
    /// <summary>
    /// Значение показателя
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Обновить значение показателя
    /// </summary>
    /// <param name="data">Элемент новых данных</param>
    public override void UpdateData(DataItem data)
    {
        Value = data.Value;
    }
}
