using System;
using GlobalScripts.combat;
using GlobalScripts.entity;
using GlobalScripts.entity.ai;

namespace Scenes.Scene01 {
    public class SimpleEnemyEntity : DustEntity{
        private new void Start() {
            base.Start();
            Weapon = new Weapon("Minigun", 1, 10, 3, 0.2f, 4.0f);
            //IdleAIGoals.Add(1, new FollowPathGoal(this));
            CombatAIGoals.Add(new CombatHideGoal(this));
            CombatAIGoals.Add(new CombatReloadGoal(this));
            CombatAIGoals.Add(new CombatShootGoal(this));
            CombatAIGoals.Add(new CombatGetInShootingRangeGoal(this));
        }
    }
}