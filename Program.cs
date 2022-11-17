// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using TMBattle.Models;
using TMBattle.Simulators;

var armyDefinitionA = new ArmyDefinition();
armyDefinitionA.SetAssignedUnit(UnitDefinition.Spearman, 20);
armyDefinitionA.SetAssignedUnit(UnitDefinition.Mercenary, 20);
armyDefinitionA.SetAssignedUnit(UnitDefinition.ManAtArms, 20);
armyDefinitionA.SetAssignedUnit(UnitDefinition.Crossbowman, 30);

var armyDefinitionB = new ArmyDefinition();
armyDefinitionB.SetAssignedUnit(UnitDefinition.Spearman, 10);
armyDefinitionB.SetAssignedUnit(UnitDefinition.Warrior, 20);
armyDefinitionB.SetAssignedUnit(UnitDefinition.Archer, 20);

var simulator = new VanillaSimulator(armyDefinitionA, armyDefinitionB);
simulator.Simulate();

