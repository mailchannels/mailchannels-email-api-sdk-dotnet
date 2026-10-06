using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using MailChannels.EmailApi.Model;

namespace MailChannels.EmailApi.Client
{
    public partial class HostConfiguration
    {
        partial void OnHostConfigurationCreated()
        {
            // List elements use System.Text.Json directly. Its general enum converter
            // uses C# identifiers, not the API's hyphenated wire values.
            _jsonOptions.Converters.Insert(0, new WireEnumConverter<SuppressionEntry.SuppressionTypesEnum>(new()
            {
                ["transactional"] = SuppressionEntry.SuppressionTypesEnum.Transactional,
                ["non-transactional"] = SuppressionEntry.SuppressionTypesEnum.NonTransactional
            }));
            _jsonOptions.Converters.Insert(0, new WireEnumConverter<SuppressionEntryResponse.SuppressionTypesEnum>(new()
            {
                ["transactional"] = SuppressionEntryResponse.SuppressionTypesEnum.Transactional,
                ["non-transactional"] = SuppressionEntryResponse.SuppressionTypesEnum.NonTransactional
            }));
        }
    }

    internal sealed class WireEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        private readonly Dictionary<string, T> _values;
        public WireEnumConverter(Dictionary<string, T> values) => _values = values;
        public override T Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && _values.TryGetValue(reader.GetString()!, out var value)) return value;
            throw new JsonException("Unknown MailChannels enum value.");
        }
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            foreach (var pair in _values)
                if (EqualityComparer<T>.Default.Equals(pair.Value, value)) { writer.WriteStringValue(pair.Key); return; }
            throw new JsonException("Unknown MailChannels enum value.");
        }
    }
}
