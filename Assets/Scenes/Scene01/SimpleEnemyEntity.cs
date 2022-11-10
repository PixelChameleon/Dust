using System;
using GlobalScripts.entity;
using GlobalScripts.entity.ai;

namespace Scenes.Scene01 {
    public class SimpleEnemyEntity : DustEntity{
        private void Start() {
            IdleAIGoals.Add(0, new RandomWalkGoal(this));
            InvokeRepeating("AIStep", 5.0f, 5.0f);
        }
    }
}