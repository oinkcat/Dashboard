using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace DashboardCore.Models.Structure;

/// <summary>
/// Страница для отображения
/// </summary>
public class DashboardPage
{
    /// <summary>
    /// Идентификатор страницы
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Разделы показателей
    /// </summary>
    public List<Section> Sections { get; set; }

    /// <summary>
    /// Имя источника данных
    /// </summary>
    public string DataSource { get; set; }

    /// <summary>
    /// Посчитать ширину всех элементов страницы
    /// </summary>
    public void ComputeWidths()
    {
        Sections.ForEach(s => s.ComputeWidths());
    }
}