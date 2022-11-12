using System;
using System.Collections.Generic;
using GlobalScripts.combat;
using GlobalScripts.entity.ai;
using UnityEngine;

namespace GlobalScripts.entity {
    
    public class DustEntity : MonoBehaviour {
        
        public IDictionary<int, AbstractAIGoal> IdleAIGoals = new Dictionary<int, AbstractAIGoal>();
        public IDictionary<int, AbstractAIGoal> CombatAIGoals = new Dictionary<int, AbstractAIGoal>();
        private int _currentGoalPrio = 0;
        private AbstractAIGoal _currentGoal;
        
        public Transform pathHolder;
        
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public bool friendly = false;
        public bool inCombat = false;

        public CombatManager CombatManager { get; private set; }

        public void Start() {
            InvokeRepeating(nameof(TickEntity), 1f, 1f);
        }

        private void TickEntity() {
            Debug.Log("Entity tick for " + gameObject.name);
            if (!inCombat) {
                AIStep();
            }
        }

        private void AIStep() {
            foreach (var goal in IdleAIGoals) {
                if (goal.Value.ShouldStart() && goal.Key > _currentGoalPrio && _currentGoal != goal.Value) {
                    if (_currentGoal != null) {
                        _currentGoal.OnStop();
                    }
                    _currentGoal = goal.Value;
                    goal.Value.OnStart();
                    _currentGoalPrio = goal.Key;
                }
            }
            _currentGoal.Run();
        }

        public void CombatAIStep() {
            foreach (var goal in CombatAIGoals) {
                goal.Value.Run();
            }
        }

        public CombatManager InitCombat(PlayerScript playerScript) {
            CombatManager = new CombatManager(playerScript, this);
            return CombatManager;
        }
    }
}