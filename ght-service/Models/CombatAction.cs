namespace GloomhavenTracker.Service.Models
{


    [Serializable]
    public class CombatActionResult
    {
        public Dictionary<ELEMENT_TYPE, ELEMENT_STRENGTH> ElementalStrength { get; set; } = new Dictionary<ELEMENT_TYPE, ELEMENT_STRENGTH>();

        public ActorsDTO Actors = new ActorsDTO();

    }


    [Serializable]
    public class CombatAction
    {
        public Guid Source { get; set; }

        public Guid Target { get; set; }

        public List<ELEMENT_TYPE> ElementsCharged { get; set; } = new List<ELEMENT_TYPE>();
        public List<ELEMENT_TYPE> ElementSpent { get; set; } = new List<ELEMENT_TYPE>();

        public int Damage { get; set; }

        public int Healing { get; set; }

        public List<ActorEffect> EffectsApplied { get; set; } = new List<ActorEffect>();
    }

    public class CombatInitiative 
    {
        public Guid PlayerId { get; set; }
        public string MonsterType {get; set;} = string.Empty;
        public bool IsLongRest { get; set; }
        public int Initiative { get; set;}
    }

}