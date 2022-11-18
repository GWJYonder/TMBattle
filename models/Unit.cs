namespace TMBattle.Models
{
    public class Unit
    {
        public UnitDefinition Definition { get; }

        public double Attack { get; }
        public int Defense { get; }

        public double CurrentHealth { get; set; }

        public Unit(UnitDefinition definition)
        {
            Definition = definition;
            // Currently no tech/spells/etc implemented
            Attack = definition.BaseAttack / 2.0;
            Defense = definition.BaseDefense;

            CurrentHealth = definition.BaseDefense;
        }

        public static Unit Clone(Unit unit)
        {
            var clone = new Unit(unit.Definition)
            {
                CurrentHealth = unit.CurrentHealth
            };
            return clone;
        }
    }
}