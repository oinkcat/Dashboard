using DashboardCore.Models.Structure;
using System.Collections.Generic;
using System.Linq;

namespace DashboardCore.Models.Structure;

/// <summary>
/// Раздел - группа показателей
/// </summary>
public class Section
{
    // Ширина всей строки раздела
    private const int RowWidth = 12;

    /// <summary>
    /// Пропорции ширины
    /// </summary>
    public string WidthRatios { get; set; }

    /// <summary>
    /// Заголовок раздела
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Показатели в разделе
    /// </summary>
    public List<Indicator> Indicators { get; set; }

    /// <summary>
    /// Посчитать ширину всех элементов раздела
    /// </summary>
    public void ComputeWidths()
    {
        string widthRatios = WidthRatios ?? "1";

        var ratios = widthRatios.Split('/').Select(n => int.Parse(n)).ToArray();
        int summ = ratios.Sum();

        int totalWidth = 0;

        for (int i = 0; i < Indicators.Count; i++)
        {
            int itemWidth = ratios[i] * RowWidth / summ;
            Indicators[i].Width = itemWidth;
            totalWidth += itemWidth;
        }

        if (totalWidth < RowWidth)
        {
            int rest = RowWidth - totalWidth;
            Indicators[0].Width += rest;
        }
    }
}