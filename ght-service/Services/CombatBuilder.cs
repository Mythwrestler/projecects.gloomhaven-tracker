using System.Collections.Generic;
using GloomhavenTracker.Service.Models;

namespace GloomhavenTracker.Service.Services
{
    public static class CombatBuilder
    {
        public static List<AttackModifier> BaseModDeck => new List<AttackModifier>()
            {
                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Add, Value=1},
                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Add, Value=1},
                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Add, Value=1},
                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Add, Value=1},
                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Add, Value=1},
                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Add, Value=2},
                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Add, Value=2},

                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Add, Value=0},
                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Add, Value=0},
                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Add, Value=0},
                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Add, Value=0},

                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Add, Value=-1},
                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Add, Value=-1},
                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Add, Value=-1},
                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Add, Value=-1},
                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Add, Value=-1},
                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Add, Value=-2},
                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Add, Value=-2},

                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Cancel, Value=0, TriggerShuffle=true},
                new AttackModifier(){Type=ATTACK_MODIFIER_TYPE.Multiply, Value=2, TriggerShuffle=true},
            };
    }
}