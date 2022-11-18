using TMBattle.Extensions;
using TMBattle.Models;

namespace TMBattle.Simulators
{
    public class CustomSimulator
    {
        public ArmyDefinition ArmyDefinitionA { get; set; }

        public ArmyDefinition ArmyDefinitionB { get; set; }

        public CustomSimulator(ArmyDefinition armyA, ArmyDefinition armyB)
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

            var round = ProcessBattle(armyA, armyB);

            Console.WriteLine("Battle Results:");
            Console.WriteLine($"The battle lasted {round} {(round == 1 ? "round" : "rounds")}");
            Console.WriteLine();
            Console.WriteLine("Army A");
            WriteArmyInfo(armyA);
            Console.WriteLine();
            Console.WriteLine("Army B");
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

        private static int ProcessBattle(Army armyA, Army armyB)
        {
            int round;
            var aDefeated = false;
            var bDefeated = false;
            for (round = 0; round < 4; round++)
            {
                if (aDefeated || bDefeated) break;
                var offenseA = armyA.GetRoundOffensiveUnits();
                var offenseB = armyB.GetRoundOffensiveUnits();

                if (!bDefeated)
                {
                    foreach (var attacker in offenseA)
                    {
                        bDefeated = ProcessAttack(attacker, armyB);
                        if (bDefeated) break;
                    }
                }

                if (!aDefeated)
                {
                    foreach (var attacker in offenseB)
                    {
                        aDefeated = ProcessAttack(attacker, armyA);
                        if (aDefeated) break;
                    }
                }
            }
            return round;
        }

        private static bool ProcessAttack(Unit attacker, Army defense)
        {
            var attackValue = attacker.Attack;
            var strongAgainst = attacker.Definition.Type.StrongAgainst();
            while (attackValue > 0 && defense.Stats.TotalDefense > 0)
            {
                var (defender, rank, index) = defense.GetNextUnit();
                var preAttackHealth = defender.CurrentHealth;
                var factor = strongAgainst == defender.Definition.Type ? 2.0 : 1.0;
                defender.CurrentHealth = attackValue * factor >= defender.CurrentHealth ? 0 : defender.CurrentHealth - attackValue * factor;
                attackValue -= preAttackHealth / factor;
                defender.CurrentHealth = Math.Round(defender.CurrentHealth, 1);
                attackValue = Math.Round(attackValue, 1);
                if (defender.CurrentHealth <= 0)
                {
                    defense.MarkLostUnit(defender, rank, index);
                }
            }
            var enemyDefeated = defense.Stats.TotalDefense <= 0;
            return enemyDefeated;
        }
    }
}