using TMBattle.Models;

namespace TMBattle.Simulators
{
    public class VanillaSimulator
    {
        public ArmyDefinition ArmyDefinitionA { get; set; }

        public ArmyDefinition ArmyDefinitionB { get; set; }

        public VanillaSimulator(ArmyDefinition armyA, ArmyDefinition armyB)
        {
            ArmyDefinitionA = armyA;
            ArmyDefinitionB = armyB;
        }

        public void Simulate()
        {
            var armyA = new Army(ArmyDefinitionA);
            Console.WriteLine("Army A");
            WriteArmyInfo(armyA);
            var armyB = new Army(ArmyDefinitionB);
            Console.WriteLine("Army B");
            WriteArmyInfo(armyB);

            Console.WriteLine();
            var netAttackA = CalculateNetAttack(armyA, armyB);
            Console.WriteLine($"Army A attack adjusted to {netAttackA}");
            var netAttackB = CalculateNetAttack(armyB, armyA);
            Console.WriteLine($"Army B attack adjusted to {netAttackB}");
            Console.WriteLine();

            var remainingAttackA = ProcessAttack(netAttackA, armyB);
            var remainingAttackB = ProcessAttack(netAttackB, armyA);

            Console.WriteLine("Battle Results:");
            Console.WriteLine();
            Console.WriteLine("Army A");
            Console.WriteLine($"{remainingAttackA} surplus Attack");
            Console.WriteLine();
            WriteArmyInfo(armyA);
            Console.WriteLine();
            Console.WriteLine("Army B");
            Console.WriteLine($"{remainingAttackB} surplus Attack");
            Console.WriteLine();
            WriteArmyInfo(armyB);
        }

        private static void WriteArmyInfo(Army army)
        {
            Console.WriteLine($"Active Units:");
            foreach (var (definition, count) in army.RemainingUnits.Values)
            {
                Console.WriteLine($"\t{definition.Name}:\t\t{count}");
            }
            Console.WriteLine();
            if (army.Losses.Any())
            {                
                Console.WriteLine($"Killed Units:");
                foreach (var name in army.Losses.Keys)
                {
                    Console.WriteLine($"\t{name}:\t\t{army.Losses[name]}");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"{army.Stats.TotalAttack} Total Attack");
            Console.WriteLine($"{army.Stats.TotalDefense} Total Defense");
        }

        private static double ProcessAttack(double attackValue, Army defense)
        {
            while (attackValue > 0 && defense.Stats.TotalDefense > 0)
            {
                var (defender, rank, index) = defense.GetNextUnit();
                var preAttackHealth = defender.CurrentHealth;
                defender.CurrentHealth = attackValue >= defender.CurrentHealth ? 0 : defender.CurrentHealth - attackValue;
                attackValue -= preAttackHealth;
                if (attackValue >= 0)
                {
                    defense.MarkLostUnit(defender, rank, index);
                }
            }
            return attackValue;
        }

        private static double CalculateNetAttack(Army attacking, Army defending)
        {            
            var totalAttack = 0.0;
            totalAttack += defending.ContainsUnitType(UnitType.Tank) ? 2.0 * attacking.Stats.ShockAttack : attacking.Stats.ShockAttack;
            totalAttack += defending.ContainsUnitType(UnitType.Shock) ? 2.0 * attacking.Stats.RangedAttack : attacking.Stats.RangedAttack;
            totalAttack += defending.ContainsUnitType(UnitType.Ranged) ? 2.0 * attacking.Stats.CavalryAttack : attacking.Stats.CavalryAttack;
            totalAttack += defending.ContainsUnitType(UnitType.Cavalry) ? 2.0 * attacking.Stats.TankAttack : attacking.Stats.TankAttack;

            return totalAttack;
        }
    }
}