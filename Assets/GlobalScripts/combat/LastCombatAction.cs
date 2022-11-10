using UnityEngine;

namespace GlobalScripts.combat {
    public struct LastCombatAction {

        public long Timestamp;
        public PlayerMove Move;
        public Vector3 StartLocation;
        public Vector3 EndLocation;
        
        public LastCombatAction(long timestamp, PlayerMove move, Vector3 startLocation, Vector3 endLocation) {
            Timestamp = timestamp;
            Move = move;
            StartLocation = startLocation;
            EndLocation = endLocation;
        }
    }
}