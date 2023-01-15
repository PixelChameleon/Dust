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
            if (player.inCombat) {
                Debug.LogError(enemy.name + " tried initialising combat while player is already in combat mode.");
                return;
            }
            if (player.Weapon == null) {
                player.Die("Du wurdest angegriffen und hattest keine Waffe.");
                player.CombatManager = null;
                return;
            }
            Player = player;
            Enemy = enemy;
            enemy.CombatManager = this;
            player.CombatManager = this;
            Debug.Log("Initialized combat between " + player + " and " + enemy);
            player.CombatUI.CombatManager = this;
            player.CombatUI.gameObject.SetActive(true);
            player.inCombat = true;
            enemy.inCombat = true;
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
                if (target is PlayerScript) {
                    Player.CombatUI.ActionText.GetComponent<Text>().text = "Munition alle!";
                }
                return;
            }
            var shooterPos = shooter.GetGameObject().transform.position;
            var targetPos = target.GetGameObject().transform.position;
            shooterPos.y = 0f;
            float distance =  Vector3.Distance(shooterPos, targetPos);
            Vector3 direction = targetPos - shooterPos;
            direction.y = 0;
            direction = direction.normalized;
            var spawnPos = shooterPos + (direction.normalized * 1.2f);
            Debug.Log("Spawn: " + spawnPos);
            var shooterWeapon = shooter.GetWeapon();
            
            //if (!Physics.Linecast(shooterPos, targetPos)) return ; // Wollen wir schießen erlauben auch wenn Ziel nicht sichtbar ist?
            
            float offset = 0f;
            var inRange = shooterWeapon.IdealRange < distance;
            if (!inRange) {
                offset += 0.33f;
            }
            Debug.Log("Shooting at " + targetPos + " using direction " + direction + " from " + spawnPos);
            offset += Random.Range(-shooterWeapon.Accuracy, shooterWeapon.Accuracy);
            shooterWeapon.TurnsUsed++;
            UpdateAmmo();
            var bullet = MonoBehaviour.Instantiate(Player.manager.bullet, spawnPos, Quaternion.identity);
            bullet.GetComponent<BulletScript>().target = targetPos;
            //bullet.GetComponent<Rigidbody>().AddRelativeForce(direction * 100);
            if (!Physics.Linecast(shooterPos, targetPos, bullet.GetComponent<BulletScript>().shooterMask)) {
                target.Damage(shooterWeapon.DamagePerBullet);
            }
            if (shooter == Player) {
                Player.LastCombatActions.Add(new LastCombatAction((PlayerScript) shooter, PlayerMove.Attack, shooterPos, targetPos));
            }
            Player.PlayGunshot();
            /*direction.x += offset;
            direction.y = 0;
            direction.z += offset;
            var bullet = MonoBehaviour.Instantiate(Player.manager.bullet, spawnPos, Quaternion.identity);
            bullet.GetComponent<BulletScript>().damage = shooterWeapon.DamagePerBullet;
            bullet.GetComponent<BulletScript>().shooter = shooter;
            bullet.GetComponent<Rigidbody>().AddRelativeForce(direction * 1000);
        
            shooterWeapon.TurnsUsed++;
            UpdateAmmo();
            
            if (shooter == Player) {
                Player.LastCombatActions.Add(new LastCombatAction((PlayerScript) shooter, PlayerMove.Attack, shooterPos, targetPos));
            }*/
        }

        private void PromptPlayer() {
            // TODO
            PlayerTurn = false;
        }
    }
}