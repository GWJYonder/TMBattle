namespace TMBattle.Models
{
    public class ArmyDefinition
    {
        public IDictionary<string, (UnitDefinition Unit, int Count)> AssignedUnits { get; private set; }

        public ArmyDefinition()
        {
            AssignedUnits = new Dictionary<string, (UnitDefinition, int)>();
        }

        public void SetAssignedUnit(UnitDefinition unit, int count)
        {
            if (count >= 0)
            {
                AssignedUnits[unit.Name] = (unit, count);
            }
            else
            {
                if (AssignedUnits.ContainsKey(unit.Name))
                {
                    AssignedUnits.Remove(unit.Name);
                }
            }
        }

        public void AddAssignedUnit(UnitDefinition unit, int count)
        {
            bool alreadyExists = AssignedUnits.ContainsKey(unit.Name);
            var currentCount = alreadyExists ? AssignedUnits[unit.Name].Count : 0;
            var newCount = currentCount + count;
            if (newCount > 0)
            {
                AssignedUnits[unit.Name] = (unit, newCount);
            }
            else if (alreadyExists)
            {
                AssignedUnits.Remove(unit.Name);
            }
        }
    }
}