using UnityEngine;

namespace GlobalScripts.combat {
    public class Weapon : ItemStack {
        public int DamagePerBullet { get; private set; }
        public int Bullets { get; private set; }
        public int TurnsToReload { get; private set; }
        public float Accuracy { get; private set; }
        public float IdealRange { get; private set; }

        public int TurnsUsed = 0;

        public Weapon(int id, string name, Sprite sprite, int bullets, int damagePerBullet, int turnsToReload, float accuracy, float idealRange) : base(id, name, sprite) {
            Name = name;
            DamagePerBullet = damagePerBullet;
            Bullets = bullets;
            TurnsToReload = turnsToReload;
            Accuracy = accuracy;
            IdealRange = idealRange;
        }
    }
}