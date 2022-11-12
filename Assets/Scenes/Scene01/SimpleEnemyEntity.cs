using System;
using GlobalScripts.entity;
using GlobalScripts.entity.ai;

namespace Scenes.Scene01 {
    public class SimpleEnemyEntity : DustEntity{
        private void Start() {
            base.Start();
            //IdleAIGoals.Add(1, new FollowPathGoal(this));
        }
    }
}