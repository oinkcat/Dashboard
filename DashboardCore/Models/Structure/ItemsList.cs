using System.Collections.Generic;

namespace DashboardCore.Models.Structure;

/// <summary>
/// Список текстовых элементов
/// </summary>
public class ItemsList : Indicator
{
    /// <summary>
    /// Элементы списка
    /// </summary>
    public List<string> Items { get; set; }

    /// <summary>
    /// Обновить элементы списка
    /// </summary>
    /// <param name="data">Элемент с новыми пунктами списка</param>
    public override void UpdateData(DataItem data)
    {
        Items = data.Items;
    }
}
