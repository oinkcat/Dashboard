using System;
using System.Collections.Generic;
using System.Linq;

namespace Dashboard.Models
{
    /// <summary>
    /// Элемент данных панели
    /// </summary>
    public class DataItem
    {
        /// <summary>
        /// Идентификатор элемента
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Значение числового показателя
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Элементы списка
        /// </summary>
        public List<string> Items { get; set; }

        /// <summary>
        /// Ряды данных диаграммы
        /// </summary>
        public ChartSeries Series { get; set; }
    }

    /// <summary>
    /// Ряд данных диаграммы
    /// </summary>
    public class ChartSeries
    {
        /// <summary>
        /// Название ряда
        /// </summary>
        public List<string> Labels { get; set; }

        /// <summary>
        /// Значения ряда
        /// </summary>
        public List<double[]> Values { get; set; }
    }
}