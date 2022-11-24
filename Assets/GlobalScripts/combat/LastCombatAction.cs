using UnityEngine;

namespace GlobalScripts.combat {
    public struct LastCombatAction {

        public PlayerScript Player;
        public PlayerMove Move;
        public Vector3 StartLocation;
        public Vector3 EndLocation;
        
        public LastCombatAction(PlayerScript player, PlayerMove move, Vector3 startLocation, Vector3 endLocation) {
            Player = player;
            Move = move;
            StartLocation = startLocation;
            EndLocation = endLocation;
        }
    }
}