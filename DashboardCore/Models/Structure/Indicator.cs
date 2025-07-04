using System.Collections.Generic;
using System;

namespace DashboardCore.Models.Structure;

/// <summary>
/// Показатель панели
/// </summary>
public abstract class Indicator
{
    /// <summary>
    /// Ширина поля
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// Подпись
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Имя шаблона для отображения
    /// </summary>
    public string TemplateName { get; set; }

    /// <summary>
    /// Имя элемента данных
    /// </summary>
    public string DataItem { get; set; }

    /// <summary>
    /// Дополнительный стиль элемента
    /// </summary>
    public string Style { get; set; }

    /// <summary>
    /// Дополнительные свойства
    /// </summary>
    public Dictionary<string, object> Props { get; set; }

    /// <summary>
    /// Обновить данные элемента
    /// </summary>
    /// <param name="data">Новые данные элемента</param>
    public abstract void UpdateData(DataItem data);

    /// <summary>
    /// Создать показатель панели по его типу
    /// </summary>
    /// <param name="type">Тип создаваемого элемента</param>
    /// <returns>Новый элемент указанного типа</returns>
    public static Indicator CreateWithType(string type)
    {
        return type switch
        {
            "Label" => new Label(),
            "Gauge" => new Gauge(),
            "List" => new ItemsList(),
            "Graph" => new Chart(),
            _ => throw new ArgumentException("Неизвестный тип показателя"),
        };
    }
}
