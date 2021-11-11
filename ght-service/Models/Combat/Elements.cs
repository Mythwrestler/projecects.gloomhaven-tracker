using System;
using System.Collections.Generic;
using System.Linq;

namespace GloomhavenTracker.Service.Models.Combat;

public enum ELEMENT_TYPE
{
    fire,
    water,
    air,
    earth,
    light,
    dark
}

public enum ELEMENT_STRENGTH
{
    spent,
    weak,
    strong
}

public class ElementDO
{
    public ELEMENT_TYPE Type { get; set; }
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
        elementKeys.ForEach(key => {
            switch (elements[key])
            {
                case(ELEMENT_STRENGTH.strong):
                    elements[key] = ELEMENT_STRENGTH.weak;
                    break;
                case(ELEMENT_STRENGTH.weak):
                    elements[key] = ELEMENT_STRENGTH.spent;
                    break;
                default:
                    break;
            }
        });
    }

    public Dictionary<ELEMENT_TYPE, ELEMENT_STRENGTH> ElementalStrength => elements.AsEnumerable().ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

    public List<ElementDO> ToDO() => elements.Select(kvp => new ElementDO()
    {
        Type = kvp.Key,
        Strength = kvp.Value
    }).ToList();
}