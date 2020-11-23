using System;
using System.Collections.Generic;
using System.Linq;

namespace DashboardCore.Models.Structure
{
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

            for(int i = 0; i < Indicators.Count; i++)
            {
                int itemWidth = ratios[i] * RowWidth / summ;
                Indicators[i].Width = itemWidth;
                totalWidth += itemWidth;
            }

            if(totalWidth < RowWidth)
            {
                int rest = RowWidth - totalWidth;
                Indicators[0].Width += rest;
            }
        }
    }

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
            switch(type)
            {
                case "Label":
                    return new Label();
                case "Gauge":
                    return new Gauge();
                case "List":
                    return new ItemsList();
                case "Graph":
                    return new Chart();
                default:
                    throw new ArgumentException("Неизвестный тип показателя");
            }
        }
    }

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
}