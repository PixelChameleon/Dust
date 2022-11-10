using GlobalScripts.entity;
using UnityEngine;

namespace GlobalScripts.combat {
    
    /*
     * Spieler und Gegner sind abwechselnd an der Reihe (PlayerTurn).
     * Gegner nutzen DustEntity und AI-Ziele, die von AbstractAIGoal abgeleitet werden.
     * DustEntity's besitzen CombatAIGoals und IdleAIGoals. Die letzteren werden alle 5 Sekunden aktualisiert und sind
     * z.B. zum zufällig umher laufen etc. AIGoals haben Prioritäten, die aktuell noch nicht genutzt werden.
     *
     * CombatAIGoals können sich auf LastCombatAction beziehen, um basierend auf den letzten Aktionen des Spielers etwas zu tun.
     * 
     */
    
    public class CombatManager {
        public PlayerScript Player { get; private set; }
        public DustEntity Enemy {  get; private set; }

        public bool PlayerTurn = false;
        private int _turnCounter = 0;

        public CombatManager(PlayerScript player, DustEntity enemy) {
            Player = player;
            Enemy = enemy;
            Debug.Log("Initialized combat between " + player + " and " + enemy);
        }

        public void Turn() {
            _turnCounter++;
            if (!PlayerTurn) {
                Enemy.CombatAIStep();
                PlayerTurn = true;
            }
            else {
                PromptPlayer();
            }
        }

        private void PromptPlayer() {
            // TODO
            PlayerTurn = false;
        }
    }
}