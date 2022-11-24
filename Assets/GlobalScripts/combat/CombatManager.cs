using System;
using GlobalScripts.entity;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GlobalScripts.combat {

    public class CombatManager  {
        public PlayerScript Player { get; private set; }
        public DustEntity Enemy {  get; private set; }

        public bool PlayerTurn = false;
        private int _turnCounter = 0;
        public PlayerMove SelectedMove;

        public CombatManager(PlayerScript player, DustEntity enemy) {
            Player = player;
            Enemy = enemy;
            enemy.CombatManager = this;
            player.CombatManager = this;
            Debug.Log("Initialized combat between " + player + " and " + enemy);
            player.CombatUI.CombatManager = this;
            UpdateHealth();
            UpdateAmmo();
        }

        public void DoAITurn() {
            Debug.Log("Starting AI turn");
            Player.canAct = false;
            Enemy.CombatAIStep();
        }

        public void FinishAITurn() {
            Player.canAct = true;
            Player.choseTurn = false;
            Player.CurrentStamina = Math.Min(100, Player.CurrentStamina + 50);
            Player.CombatUI.Stamina.GetComponent<Slider>().value = Player.CurrentStamina;
            Debug.Log("Finished AI turn");
            Player.CombatUI.ActionText.GetComponent<Text>().text = "Warte auf Move...";
        }

        public void UpdateHealth() {
            Player.CombatUI.EnemyHP.GetComponent<Slider>().value = Enemy.GetHealth();
            Player.CombatUI.PlayerHP.GetComponent<Slider>().value = Player.GetHealth();
        }

        public void UpdateAmmo() {
            Player.CombatUI.EnemyAmmo.GetComponent<Text>().text = "Munition: " + (Enemy.Weapon.TurnsToReload - Enemy.Weapon.TurnsUsed) + "/" + Enemy.Weapon.TurnsToReload;
            Player.CombatUI.PlayerAmmo.GetComponent<Text>().text = "Munition: " + (Player.Weapon.TurnsToReload - Player.Weapon.TurnsUsed) + "/" + Player.Weapon.TurnsToReload;
        }
        

        public void Shoot(ICombatant shooter, ICombatant target) {
            if (shooter.GetWeapon().TurnsUsed >= shooter.GetWeapon().TurnsToReload) {
                return;
            }
            var shooterPos = shooter.GetGameObject().transform.position;
            var targetPos = target.GetGameObject().transform.position;
            float distance =  Vector3.Distance(shooterPos, targetPos);
            Vector3 direction = targetPos - shooterPos;
            direction = direction.normalized;
            var spawnPos = shooterPos + (direction.normalized * 2);
            var shooterWeapon = shooter.GetWeapon();
            
            //if (!Physics.Linecast(shooterPos, targetPos)) return ; // Wollen wir schießen erlauben auch wenn Ziel ist nicht sichtbar ist?
            
            float offset = 0f;
            var inRange = shooterWeapon.IdealRange < distance;
            if (!inRange) {
                offset += 1;
            }
            Debug.Log("Shooting at " + targetPos + " using direction " + direction);
            for (var i = 0; i < shooterWeapon.Bullets; i++) {
                offset += Random.Range(-shooterWeapon.Accuracy, shooterWeapon.Accuracy);
                direction.x += offset;
                direction.y += offset;
                direction.z += offset;
                spawnPos.y += 0.2f; // Move up so they don't collide with the floor
                var bullet = MonoBehaviour.Instantiate(Player.manager.bullet, spawnPos, shooter.GetGameObject().transform.rotation);
                bullet.GetComponent<BulletScript>().damage = shooterWeapon.DamagePerBullet;
                bullet.GetComponent<Rigidbody>().AddRelativeForce(direction * 1000);
            }
            shooterWeapon.TurnsUsed++;
            UpdateAmmo();
            
            if (shooter == Player) {
                Player.LastCombatActions.Add(new LastCombatAction((PlayerScript) shooter, PlayerMove.Attack, shooterPos, targetPos));
            }
        }

        private void PromptPlayer() {
            // TODO
            PlayerTurn = false;
        }
    }
}