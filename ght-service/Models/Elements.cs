using System;
using System.Collections.Generic;
using System.Linq;

namespace GloomhavenTracker.Service.Models;

public enum ELEMENT_TYPE {
    Fire,
    Water,
    Air,
    Earth,
    Light,
    Dark
}

public enum ELEMENT_STRENGTH {
    Spent,
    Weak,
    Strong
}

public class Elements {

    private readonly Dictionary<ELEMENT_TYPE, ELEMENT_STRENGTH> _elements = new Dictionary<ELEMENT_TYPE, ELEMENT_STRENGTH>();

    public Elements() {
        Enum.GetValues<ELEMENT_TYPE>().ToList().ForEach(et => _elements.Add(et, 0));
    }

    public Elements(Dictionary<ELEMENT_TYPE, ELEMENT_STRENGTH> elements)
    {
        _elements = elements;
    }

    public ELEMENT_STRENGTH ChargeElement(ELEMENT_TYPE element)
    {
        _elements[element] = ELEMENT_STRENGTH.Strong;
        return _elements[element];
    }

    public ELEMENT_STRENGTH SpendElementCharge(ELEMENT_TYPE element)
    {
        switch (_elements[element])
        {
            case(ELEMENT_STRENGTH.Spent):
                throw new ArgumentException($"Element {Enum.GetName(element)} is spent and cannot be used.");
            case(ELEMENT_STRENGTH.Strong): 
                _elements[element] = ELEMENT_STRENGTH.Weak;
                break;
            case(ELEMENT_STRENGTH.Weak):
                _elements[element] = ELEMENT_STRENGTH.Spent;
                break;
            default:
                break;
        }

        return _elements[element];
    }

    public void ReduceAllElementCharges()
    {
        _elements.Keys.ToList().ForEach(k => {
            _elements[k]--;
            if(_elements[k] < 0) _elements[k] = 0;
        });
    } 

    public Dictionary<ELEMENT_TYPE, ELEMENT_STRENGTH> ElementalStrength => _elements.AsEnumerable().ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

}