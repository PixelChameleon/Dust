using System;
using System.Collections.Generic;
using System.Linq;
using GlobalScripts.combat;
using GlobalScripts.entity.ai;
using UnityEngine;
using UnityEngine.AI;

namespace GlobalScripts.entity {
    
    public class DustEntity : MonoBehaviour, ICombatant {
        
        public IDictionary<int, AbstractAIGoal> IdleAIGoals = new Dictionary<int, AbstractAIGoal>();
        public List<CombatAIGoal> CombatAIGoals = new();
        private int _currentGoalPrio = 0;
        private AbstractAIGoal _currentIdleGoal;
        public NavMeshAgent Agent;
        public GameObject WeaponAttachPoint;

        public Transform dustEntityPathHolder;

        public int Health = 0;
        public int MaxHealth = 100;
        public bool friendly = false;
        public bool inCombat = false;
        public bool isBusy = false;

        public Weapon Weapon;

        public CombatManager CombatManager;

        public void Start() {
            InvokeRepeating(nameof(TickEntity), 1f, 1f);
            Health = MaxHealth;
            Agent = gameObject.GetComponent<NavMeshAgent>();
        }

        public void Update() {
            if (isBusy && !Agent.hasPath) { // Arrived
                isBusy = false;
                CombatManager.FinishAITurn();
            }
        }

        private void TickEntity() {
            if (!inCombat) {
                AIStep();
            }
        }

        private void AIStep() {
            if (IdleAIGoals.Count == 0) {
                return;
            }
            foreach (var goal in IdleAIGoals) {
                if (goal.Value.ShouldStart() && goal.Key > _currentGoalPrio && _currentIdleGoal != goal.Value) {
                    if (_currentIdleGoal != null) {
                        _currentIdleGoal.OnStop();
                    }
                    _currentIdleGoal = goal.Value;
                    goal.Value.OnStart();
                    _currentGoalPrio = goal.Key;
                }
            }
            _currentIdleGoal.Run();
        }

        public void CombatAIStep() {
            if (CombatAIGoals.Count == 0) {
                CombatManager.FinishAITurn();
                return;
            }

            if (CombatManager.Player.LastCombatActions.Count == 0) {
                CombatManager.FinishAITurn();
                return;
            }
            foreach (var goal in CombatAIGoals) {
                if (!goal.Check(CombatManager.Player.LastCombatActions.Last())) {
                    Debug.Log("Goal " + goal + " skipped.");
                    continue; 
                }

                Debug.Log("Running goal " + goal);
                if (goal.Run()) { // If goal executed successfully, don't execute any further goals
                    break;
                }
            }

            if (!isBusy) {
                CombatManager.FinishAITurn();
            }
        }

        public CombatManager InitCombat(PlayerScript playerScript) {
            CombatManager = new CombatManager(playerScript, this);
            playerScript.CombatManager = CombatManager;
            return CombatManager;
        }

        public void Damage(int amount) {
            Health = Math.Max(0, Health - amount);
            Debug.Log(gameObject.name + " took " + amount + " damage. Health: " + Health);
            CombatManager.UpdateHealth();
        }

        public int GetHealth() {
            return Health;
        }

        public void Die(string msg) {
            Destroy(gameObject);
        }

        public Weapon GetWeapon() {
            return Weapon;
        }

        public GameObject GetGameObject() {
            return gameObject;
        }
    }
}