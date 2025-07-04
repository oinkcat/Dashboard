using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DashboardCore.Models.Structure;

/// <summary>
/// Конвертер строкового представления разметки страницы панели
/// </summary>
public class PanelStructureConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        var baseIndicatorType = typeof(Indicator);

        return objectType.Equals(baseIndicatorType) ||
               objectType.IsSubclassOf(baseIndicatorType);
    }

    /// <summary>
    /// Конвертировать JSON в объект разметки
    /// </summary>
    public override object ReadJson(JsonReader reader, Type objectType, 
                                    object existingValue, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);
        string elementType = (string)jsonObject["Type"];

        var panelElement = Indicator.CreateWithType(elementType);
        serializer.Populate(jsonObject.CreateReader(), panelElement);

        return panelElement;
    }

    public override void WriteJson(JsonWriter writer, object value, 
                                   JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}