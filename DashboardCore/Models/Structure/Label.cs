namespace DashboardCore.Models.Structure;

/// <summary>
/// Текстовая метка
/// </summary>
public class Label : Indicator
{
    /// <summary>
    /// Текст метки
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Обновить данные (не используется)
    /// </summary>
    /// <param name="data"></param>
    public override void UpdateData(DataItem data)
    { }
}
