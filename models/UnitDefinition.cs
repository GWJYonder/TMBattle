namespace TMBattle.Models
{
    public class UnitDefinition
    {
        public string Name { get; set; }

        public UnitType Type { get; set; }

        public Rank Rank { get; set; }

        public int BaseAttack { get; set; }

        public int BaseDefense { get; set; }

        public UnitDefinition (string name, UnitType type, Rank rank,
                        int baseAttack, int baseDefense)
        {
            Name = name;
            Type = type;
            Rank = rank;
            BaseAttack = baseAttack;
            BaseDefense = baseDefense;
        }

        public static readonly UnitDefinition Spearman = new("Spearman", UnitType.Tank, Rank.Front, 2, 7);
        public static readonly UnitDefinition Monk = new("Monk", UnitType.Tank, Rank.Front, 3, 13);
        public static readonly UnitDefinition Golem = new("Golem", UnitType.Tank, Rank.Front, 8, 22);
        public static readonly UnitDefinition Cleric = new("Cleric", UnitType.Tank, Rank.Mid, 8, 24);

        public static readonly UnitDefinition Mercenary = new("Mercenary", UnitType.Shock, Rank.Front, 12, 8);
        public static readonly UnitDefinition Warrior = new("Warrior", UnitType.Shock, Rank.Mid, 8, 8);
        public static readonly UnitDefinition Heavy = new("Heavy", UnitType.Shock, Rank.Mid, 12, 12);
        public static readonly UnitDefinition ManAtArms = new("ManAtArms", UnitType.Shock, Rank.Mid, 21, 16);
        public static readonly UnitDefinition Infantry = new("Infantry", UnitType.Shock, Rank.Mid, 34, 22);
        public static readonly UnitDefinition Commander = new("Commander", UnitType.Shock, Rank.Back, 24, 22);
        public static readonly UnitDefinition General = new("General", UnitType.Shock, Rank.Back, 60, 90);

        public static readonly UnitDefinition Archer = new("Archer", UnitType.Ranged, Rank.Back, 3, 2);
        public static readonly UnitDefinition Catapult = new("Catapult", UnitType.Ranged, Rank.Back, 14, 2);
        public static readonly UnitDefinition Crossbowman = new("Crossbowman", UnitType.Ranged, Rank.Back, 11, 6);
        public static readonly UnitDefinition Trebuchet = new("Trebuchet", UnitType.Ranged, Rank.Back, 28, 3);
        public static readonly UnitDefinition Arquebusier = new("Arquebusier", UnitType.Ranged, Rank.Back, 16, 7);
        public static readonly UnitDefinition Bombard = new("Bombard", UnitType.Ranged, Rank.Back, 42, 4);
        public static readonly UnitDefinition Cannon = new("Cannon", UnitType.Ranged, Rank.Back, 88, 8);
        
        public static readonly UnitDefinition Cavalry = new("Cavalry", UnitType.Cavalry, Rank.Mid, 10, 4);
        public static readonly UnitDefinition Knight = new("Knight", UnitType.Cavalry, Rank.Mid, 22, 22);
        public static readonly UnitDefinition Cuirassier = new("Cuirassier", UnitType.Cavalry, Rank.Mid, 36, 28);
    }
}