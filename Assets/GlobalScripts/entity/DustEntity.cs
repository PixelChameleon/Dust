using System;
using System.Collections.Generic;
using GlobalScripts.combat;
using GlobalScripts.entity.ai;
using UnityEngine;

namespace GlobalScripts.entity {
    
    public class DustEntity : MonoBehaviour {
        
        public IDictionary<int, AbstractAIGoal> IdleAIGoals = new Dictionary<int, AbstractAIGoal>();
        public IDictionary<int, AbstractAIGoal> CombatAIGoals = new Dictionary<int, AbstractAIGoal>();
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }

        public CombatManager CombatManager { get; private set; }

        private void AIStep() {
            foreach (var goal in IdleAIGoals) {
                goal.Value.Run();
            }
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