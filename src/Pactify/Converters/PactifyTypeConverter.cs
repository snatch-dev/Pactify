using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Pactify.Definitions.Http;

namespace Pactify.Converters
{
    public class PactifyTypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => serializer.Serialize(writer, value);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<HashSet<HttpCouplingDefinition>>(reader);
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
