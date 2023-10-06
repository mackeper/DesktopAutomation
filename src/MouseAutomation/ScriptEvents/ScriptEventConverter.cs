using Core.Model;
using MouseAutomation.ScriptEvents;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.Persistance
{
    public class ScriptEventConverter : JsonConverter<ScriptEvent>
    {
        private readonly JsonSerializer jsonSerializer;

        public ScriptEventConverter(JsonSerializer jsonSerializer)
        {
            this.jsonSerializer = jsonSerializer;
        }

        public override ScriptEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            var eventType = root.GetProperty("EventType").GetString();
            return eventType switch
            {
                "Keyboard" =>
                    jsonSerializer
                        .Deserialize<KeyboardScriptEvent>(root.GetRawText(), options)
                        .Match(
                            text => text,
                            () => throw new JsonException("Unknown script event type")),
                "Mouse" =>
                    jsonSerializer
                    .Deserialize<MouseScriptEvent>(root.GetRawText(), options)
                    .Match(
                        text => text,
                        () => throw new JsonException("Unknown script event type")),
                _ => throw new JsonException("Unknown script event type"),
            };
        }

        public override void Write(Utf8JsonWriter writer, ScriptEvent value, JsonSerializerOptions options) =>
            jsonSerializer.Serialize(value, options);
    }
}
