using System.Collections.Generic;
using System.Linq;
using GlobalScripts.combat;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

namespace GlobalScripts {
    public class PlayerScript : MonoBehaviour {
        private NavMeshAgent _agent;
        public Camera camera;
        public DustManager manager;
        private IDictionary<ItemStack, int> _inventory = new Dictionary<ItemStack, int>();
        
        protected LastCombatAction[] LastActions;

        private void Start() {
            _agent = GetComponent<NavMeshAgent>();
            camera = GetComponentInChildren<Camera>();
            manager.player = this; // make sure DustSceneManager always knows about the current player object
            Debug.LogError("Initialized player");
        }
    
        private void Update() {
            if (!Input.GetMouseButtonDown(0)) return;
            RaycastHit hit;

            if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 100)) return;
            if (hit.transform.gameObject.CompareTag("Clickable")) {
                var clickable = hit.transform.gameObject.GetComponent<IClickableGameObject>();
                clickable.OnClick(this);
                return;
            }
            _agent.destination = hit.point;
        }
        
        //
        // Inventory
        //

        // Check if the player has required amount of an item. See DustManager for item ids.
        public bool HasItem(int id, int amount) {
            return _inventory.Any(item => (item.Key.id == id) && item.Value >= amount);
        }

        public void AddItem(ItemStack itemStack) {
            if (_inventory.ContainsKey(itemStack)) {
                _inventory[itemStack]++;
                return;
            }
            _inventory.Add(itemStack, 1);
        }
        
        public void AddItem(ItemStack itemStack, int amount) {
            if (_inventory.ContainsKey(itemStack)) {
                _inventory[itemStack] += amount;
                return;
            }
            _inventory.Add(itemStack, amount);
        }

        public void RemoveItem(ItemStack itemStack) {
            _inventory.Remove(itemStack);
        }

        public void RemoveItem(ItemStack itemStack, int amount) {
            if (!_inventory.ContainsKey(itemStack)) return;
            var currentAmount = _inventory[itemStack];
            _inventory[itemStack] = amount - currentAmount;
            if (_inventory[itemStack] <= 0) {
                _inventory.Remove(itemStack);
            }
        }
    }
}
