using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Combat;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ELEMENT_TYPE
{
    [EnumMember(Value = "fire")]
    fire,

    [EnumMember(Value = "water")]
    water,

    [EnumMember(Value = "air")]
    air,

    [EnumMember(Value = "earth")]
    earth,

    [EnumMember(Value = "light")]
    light,

    [EnumMember(Value = "dark")]
    dark
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ELEMENT_STRENGTH
{
    [EnumMember(Value = "spent")]
    spent,

    [EnumMember(Value = "weak")]
    weak,

    [EnumMember(Value = "strong")]
    strong
}

[Serializable]
public class ElementDO
{
    [JsonPropertyName("type")]
    public ELEMENT_TYPE Type { get; set; }

    [JsonPropertyName("strength")]
    public ELEMENT_STRENGTH Strength { get; set; }
}
[Serializable]
public class ElementDTO
{
    [JsonPropertyName("type")]
    public ELEMENT_TYPE Type { get; set; }

    [JsonPropertyName("strength")]
    public ELEMENT_STRENGTH Strength { get; set; }
}

public class Elements
{

    private readonly Dictionary<ELEMENT_TYPE, ELEMENT_STRENGTH> elements = new Dictionary<ELEMENT_TYPE, ELEMENT_STRENGTH>();

    public Elements()
    {
        Enum.GetValues<ELEMENT_TYPE>().ToList().ForEach(et => elements.Add(et, 0));
    }

    public Elements(List<ElementDO> elements)
    {
        this.elements = elements.DistinctBy(element => element.Type).ToDictionary(element => element.Type, element => element.Strength);
    }

    public void ChargeElement(ELEMENT_TYPE elementToCharge)
    {
        elements[elementToCharge] = ELEMENT_STRENGTH.strong;
    }

    public void SpendElement(ELEMENT_TYPE elementToSpend)
    {
        elements[elementToSpend] = ELEMENT_STRENGTH.spent;
    }

    private void ReduceAllCharges()
    {
        List<ELEMENT_TYPE> elementKeys = elements.Keys.ToList();
        elementKeys.ForEach(key =>
        {
            switch (elements[key])
            {
                case (ELEMENT_STRENGTH.strong):
                    elements[key] = ELEMENT_STRENGTH.weak;
                    break;
                case (ELEMENT_STRENGTH.weak):
                    elements[key] = ELEMENT_STRENGTH.spent;
                    break;
                default:
                    break;
            }
        });
    }

    public Dictionary<ELEMENT_TYPE, ELEMENT_STRENGTH> ElementalStrength => elements.AsEnumerable().ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

    public List<ElementDO> DataObject
    {
        get
        {
            return elements.Select(kvp => new ElementDO()
            {
                Type = kvp.Key,
                Strength = kvp.Value
            }).ToList();
        }
    }

    public List<ElementDTO> DataTransferObject
    {
        get
        {
            return elements.Select(kvp => new ElementDTO()
            {
                Type = kvp.Key,
                Strength = kvp.Value
            }).ToList();
        }
    }


}