using System;
using System.Collections.Generic;
using System.Linq;
using GlobalScripts.combat;
using GlobalScripts.entity;
using UnityEngine;
using UnityEngine.AI;
using Slider = UnityEngine.UI.Slider;

namespace GlobalScripts {
    public class PlayerScript : MonoBehaviour, ICombatant {

        private static float MAX_CLICK_DISTANCE = 5.0f;
        
        private NavMeshAgent _agent;
        public Camera camera;
        public DustManager manager;
        private IDictionary<ItemStack, int> _inventory = new Dictionary<ItemStack, int>();

        public int MaxHealth = 100;
        public int CurrentHealth;
        public float MaxStamina = 100f;
        public float CurrentStamina;

        public List<LastCombatAction> LastCombatActions = new();
        public bool inCombat = false;
        private bool isCombatMoving = false;
        public CombatUI CombatUI;
        public CombatManager CombatManager;
        public bool canAct = true;
        public bool canMove = true;
        public bool choseTurn = false;
        public bool inventoryOpen = false; // TODO

        public Weapon Weapon = new("Pistole", 1, 5, 3, 0.2f, 4.0f);

        private void Start() {
            _agent = GetComponent<NavMeshAgent>();
            camera = GetComponentInChildren<Camera>();
            manager.player = this; // make sure DustSceneManager always knows about the current player object
            Debug.LogError("Initialized player");
            CurrentHealth = MaxHealth;
            CurrentStamina = MaxStamina;
            CombatUI.Stamina.GetComponent<Slider>().value = CurrentStamina;
            CombatUI.PlayerHP.GetComponent<Slider>().value = CurrentHealth;
            LastCombatActions.Add(new LastCombatAction(this, PlayerMove.Move, gameObject.transform.position, gameObject.transform.position));
        }
    
        private void Update() {
            if (!canAct) {
                return;
            }
            if (isCombatMoving && !_agent.hasPath) {
                isCombatMoving = false;
                return;
            } 
            if (inCombat && isCombatMoving && _agent.hasPath) {
                if (CurrentStamina <= 0) {
                    _agent.isStopped = true;
                    isCombatMoving = false;
                    CombatManager.DoAITurn();
                } 
                CurrentStamina = Mathf.Clamp(CurrentStamina - (20.0f * Time.deltaTime), 0.0f, MaxStamina);
                CombatUI.Stamina.GetComponent<Slider>().value = CurrentStamina;
            }
            if (!Input.GetMouseButtonDown(0)) return;
            RaycastHit hit;

            if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 100) || UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;
            if (inCombat) {
                if (CombatManager.SelectedMove != PlayerMove.Move) return;
                CombatMove(hit);
                choseTurn = true;
                return;
            }

            var hitObject = hit.transform.gameObject;
            if (hitObject.CompareTag("Clickable") && Vector3.Distance(hitObject.transform.position, this.transform.position) <= MAX_CLICK_DISTANCE) {
                var clickable = hitObject.GetComponent<IClickableGameObject>();
                clickable.OnClick(this);
                return;
            }

            var combatant = hitObject.GetComponent<ICombatant>();
            if (combatant != null) {
                CombatManager = new CombatManager(this, hitObject.GetComponent<DustEntity>());
                inCombat = !inCombat;
                CombatUI.enabled = inCombat;
                Debug.Log("Combat: " + inCombat + " | Enemy: " + hitObject.name);
                return;
            }

            if (canMove) {
                _agent.destination = hit.point;
            }
        }

        private void CombatMove(RaycastHit hit) {
            //var distance = Vector3.Distance(gameObject.transform.position, hit.point);
            if (isCombatMoving || !canAct) {
                return;
            }
            _agent.destination = hit.point;
            isCombatMoving = true;
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

        public void Damage(int amount) {
            CurrentHealth = Math.Max(0, CurrentHealth - amount);
            Debug.Log(gameObject.name + " took " + amount + " damage. Health: " + CurrentHealth);
            CombatManager.UpdateHealth();
        }

        public int GetHealth() {
            return CurrentHealth;
        }
        
        public void Die() {
            // Deaded.
        }
        
        public Weapon GetWeapon() {
            return Weapon;
        }
        
        public GameObject GetGameObject() {
            return gameObject;
        }
    }
}
