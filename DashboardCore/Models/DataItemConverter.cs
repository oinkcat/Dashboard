using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DashboardCore.Models;

public class DataItemConverter : JsonConverter
{
    /// <summary>
    /// Можно ли произвести преобразование
    /// </summary>
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(DataItem);
    }

    /// <summary>
    /// Произвести конвертирование из JSON
    /// </summary>
    public override object ReadJson(JsonReader reader, Type objectType, 
                                    object existingValue, JsonSerializer serializer)
    {
        var obj = JObject.ReadFrom(reader);
        var jsonSeries = obj["Series"];

        if (jsonSeries != null)
        {
            var dataSeries = new ChartSeries()
            {
                Labels = obj["Labels"].Values<string>().ToList()
            };

            if(jsonSeries[0].Type == JTokenType.Array)
            {
                // Несколько рядов данных
                dataSeries.Values = jsonSeries.Select(jsonRow => {
                    return jsonRow.Values<double>().ToArray();
                }).ToList();
            }
            else
            {
                // Один ряд данных
                var valuesRow = jsonSeries.Values<double>().ToArray();
                dataSeries.Values = new List<double[]> { valuesRow };
            }

            return new DataItem()
            {
                Id = obj["Id"].Value<string>(),
                Series = dataSeries
            };
        }
        else
        {
            var convertedItem = new DataItem();
            serializer.Populate(obj.CreateReader(), convertedItem);
            return convertedItem;
        }

    }

    public override void WriteJson(JsonWriter writer, object value,
                                   JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}