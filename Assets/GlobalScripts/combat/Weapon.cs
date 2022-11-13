namespace GlobalScripts.combat {
    public class Weapon {
        public string Name { get; private set; }
        public int DamagePerBullet { get; private set; }
        public int Bullets { get; private set; }
        public int TurnsToReload { get; private set; }
        public float Accuracy { get; private set; }
        public float IdealRange { get; private set; }

        public int TurnsUsed = 0;

        public Weapon(string name, int bullets, int damagePerBullet, int turnsToReload, float accuracy, float idealRange) {
            Name = name;
            DamagePerBullet = damagePerBullet;
            Bullets = bullets;
            TurnsToReload = turnsToReload;
            Accuracy = accuracy;
            IdealRange = idealRange;
        }
    }
}