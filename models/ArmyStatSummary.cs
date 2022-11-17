namespace TMBattle.Models
{
    public class ArmyStatSummary
    {
        public int TotalAttack { get; set; }

        public int TotalDefense { get; set; }

        public int TankAttack { get; set; }

        public int ShockAttack { get; set; }

        public int RangedAttack { get; set; }

        public int CavalryAttack { get; set; }

        public int ZeroRankDefense { get; set; }

        public int FrontRankDefense { get; set; }

        public int MidRankDefense { get; set; }

        public int BackRankDefense { get; set; }


        public void UpdateStats(Unit unit, Rank rank, bool add = true)
        {
            // Rank passed in to possibly support mult-rank units later.

            var attackChange = add ? unit.Attack : -unit.Attack;
            TotalAttack += attackChange;
            switch (unit.Definition.Type)
            {
                case UnitType.Tank:
                TankAttack += attackChange;
                break;
                case UnitType.Shock:
                ShockAttack += attackChange;
                break;
                case UnitType.Ranged:
                RangedAttack += attackChange;
                break;
                case UnitType.Cavalry:
                CavalryAttack += attackChange;
                break;
            }

            var defenseChange = add ? unit.Defense : -unit.Defense;
            TotalDefense += defenseChange;
            switch (rank)
            {
                case Rank.Zero:
                ZeroRankDefense += defenseChange;
                break;
                case Rank.Front:
                FrontRankDefense += defenseChange;
                break;
                case Rank.Mid:
                MidRankDefense += defenseChange;
                break;
                case Rank.Back:
                BackRankDefense += defenseChange;
                break;
            }
        }

    }
}