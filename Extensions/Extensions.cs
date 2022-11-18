using TMBattle.Models;

namespace TMBattle.Extensions
{
    public static class Extensions
    {
        public static UnitType StrongAgainst(this UnitType type)
        {
            switch (type)
            {
                case UnitType.Tank:
                    return UnitType.Cavalry;
                case UnitType.Shock:
                    return UnitType.Tank;
                case UnitType.Ranged:
                    return UnitType.Shock;
                case UnitType.Cavalry:
                    return UnitType.Ranged;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}