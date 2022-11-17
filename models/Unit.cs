namespace TMBattle.Models
{
    public class Unit
    {
        public UnitDefinition Definition { get; }

        public int Attack { get; }
        public int Defense { get; }

        public double CurrentHealth { get; set; }

        public Unit(UnitDefinition definition)
        {
            Definition = definition;
            // Currently no tech/spells/etc implemented
            Attack = definition.BaseAttack;
            Defense = definition.BaseDefense;

            CurrentHealth = definition.BaseDefense;
        }
    }
}