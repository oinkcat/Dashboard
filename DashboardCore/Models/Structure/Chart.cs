using System;

namespace DashboardCore.Models.Structure;

/// <summary>
/// График/диаграмма
/// </summary>
public class Chart : Indicator
{
    /// <summary>
    /// Ряды данных
    /// </summary>
    public ChartSeries Series { get; set; }

    /// <summary>
    /// Обновить данные диаграммы
    /// </summary>
    /// <param name="data">Новые данные диаграммы</param>
    public override void UpdateData(DataItem data)
    {
        Series = data.Series;
    }
}