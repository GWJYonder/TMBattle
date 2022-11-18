namespace TMBattle.Models
{
    public class Army
    {
        public ArmyDefinition Definition { get; }

        public IList<Unit> ZeroRank { get; set; }
        public IList<Unit> FrontRank { get; }
        public IList<Unit> MidRank { get; }
        public IList<Unit> BackRank { get; }

        public IDictionary<string, int> Losses { get; }
        // RemainingUnits means both combat-ready and Wounded
        public IDictionary<string, (UnitDefinition, int)> RemainingUnits { get; }
        public IList<Unit> WoundedUnits { get; }

        public ArmyStatSummary Stats { get; }

        private readonly Random _random = new();

        public Army(ArmyDefinition definition)
        {
            Definition = definition;
            Stats = new ArmyStatSummary();

            Losses = new Dictionary<string, int>();
            RemainingUnits = new Dictionary<string, (UnitDefinition, int)>();
            WoundedUnits = new List<Unit>();

            ZeroRank = CreateShuffledRank(Rank.Zero);
            FrontRank = CreateShuffledRank(Rank.Front);
            MidRank = CreateShuffledRank(Rank.Mid);
            BackRank = CreateShuffledRank(Rank.Back);
        }

        public bool ContainsUnitType(UnitType unitType)
        {
            return Definition.AssignedUnits.Values.Any(au => au.Count > 0 && au.Unit.Type == unitType);
        }

        public void MarkLostUnit(Unit lost, Rank rank, int i)
        {
            if (Losses.ContainsKey(lost.Definition.Name))
            {
                Losses[lost.Definition.Name]++;
            }
            else
            {
                Losses[lost.Definition.Name] = 1;
            }

            GetRankList(rank).RemoveAt(i);
            Stats.UpdateStats(lost, rank, false);
            RemoveFromRemainingUnits(lost);
        }

        public IList<Unit> GetRoundOffensiveUnits()
        {
            var offense = new List<Unit>();
            foreach (var unit in ZeroRank)
            {
                offense.Add(Unit.Clone(unit));
            }
            foreach (var unit in FrontRank)
            {
                offense.Add(Unit.Clone(unit));
            }
            foreach (var unit in MidRank)
            {
                offense.Add(Unit.Clone(unit));
            }
            foreach (var unit in BackRank)
            {
                offense.Add(Unit.Clone(unit));
            }

            return offense;
        }

        private void RemoveFromRemainingUnits(Unit removed)
        {
            var (definition, remaining) = RemainingUnits[removed.Definition.Name];
            remaining--;
            if (remaining <= 0)
            {
                RemainingUnits.Remove(removed.Definition.Name);
            }
            else
            {
                RemainingUnits[removed.Definition.Name] = (definition, remaining);
            }
        }

        public IList<Unit> GetRankList(Rank rank)
        {
            return rank switch
            {
                Rank.Zero => ZeroRank,
                Rank.Front => FrontRank,
                Rank.Mid => MidRank,
                Rank.Back => BackRank,
                _ => throw new NotImplementedException(),
            };
        }

        public (Unit unit, Rank rank, int index) GetNextUnit(int breakthrough = 0)
        {
            var rankUnits = new List<(Rank rank, IList<Unit> units)>();
            if (ZeroRank.Any())
            {
                rankUnits.Add((Rank.Zero, ZeroRank));
            }
            if (FrontRank.Any())
            {
                rankUnits.Add((Rank.Front, FrontRank));
            }
            if (MidRank.Any())
            {
                rankUnits.Add((Rank.Mid, MidRank));
            }
            if (BackRank.Any())
            {
                rankUnits.Add((Rank.Back, BackRank));
            }

            if (!rankUnits.Any())
            {
                throw new InvalidOperationException();
            }
            if (breakthrough > rankUnits.Count - 1)
            {
                breakthrough = rankUnits.Count - 1;
            }
            var (rank, units) = rankUnits[breakthrough];
            var index = _random.Next(units.Count);
            return (units[index], rank, index);
        }

        private IList<Unit> CreateShuffledRank(Rank rank)
        {
            var rankUnitDefs = Definition.AssignedUnits.Values.Where(au => au.Count >= 0 && au.Unit.Rank == rank);

            // technically we could do something more fancy with decrementing counts rather than making two full size lists
            // but I don't think performance on this will be too bad.
            var orderedUnits = new List<Unit>();
            foreach (var (def, count) in rankUnitDefs)
            {
                RemainingUnits[def.Name] = (def, count);
                for (int i = 0; i < count; i++)
                {
                    var unit = new Unit(def);
                    orderedUnits.Add(unit);
                    Stats.UpdateStats(unit, rank);
                }
            }
            var shuffledUnits = new List<Unit>();
            while (orderedUnits.Any())
            {
                var index = _random.Next(orderedUnits.Count);
                shuffledUnits.Add(orderedUnits[index]);
                orderedUnits.RemoveAt(index);
            }

            return shuffledUnits;
        }
    }
}