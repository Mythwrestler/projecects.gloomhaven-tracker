using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;


    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ATTACK_MODIFIER_TYPE
    {
        [EnumMember(Value = "add")]
        Add,

        [EnumMember(Value = "multiply")]
        Multiply,

        [EnumMember(Value = "cancel")]
        Cancel,
    }

    [Serializable]
    public class AttackModifier : ContentItem
    {
        [JsonPropertyName("type")]
        public ATTACK_MODIFIER_TYPE Type { get; set; }

        [JsonPropertyName("isCurse")]
        public bool IsCurse { get; set; } = false;

        [JsonPropertyName("isBlessing")]
        public bool IsBlessing { get; set; } = false;

        [JsonPropertyName("triggerShuffle")]
        public bool TriggerShuffle { get; set; } = false;

        [JsonPropertyName("value")]
        public int? Value { get; set; }

        [JsonPropertyName("effects")]
        public List<Effect> Effects = new List<Effect>();

        [JsonIgnore]
        public ContentSummary  Summary {
            get {
                return new ContentSummary() {
                    ContentCode = ContentCode,
                    Name = Name,
                    Description = Description
                };
            }
        }
    }