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
        
        [Range(0.0f, 10.0f)]
        public float MAX_CLICK_DISTANCE = 3.5f;
        
        private NavMeshAgent _agent;
        public Camera camera;
        public DustManager manager;
        public PlayerConversation conversation;
        public List<ItemStack> Inventory = new(new ItemStack[16]);

        public int MaxHealth = 100;
        public int CurrentHealth;
        public float MaxStamina = 100f;
        public float CurrentStamina;

        public List<LastCombatAction> LastCombatActions = new();
        public bool inCombat = false;
        private bool isCombatMoving = false;
        public CombatUI CombatUI;
        public CombatManager CombatManager;
        public InventoryUI InventoryUI;
        public bool canAct = true;
        public bool canMove = true;
        public bool choseTurn = false;
        public bool inventoryOpen = false; // TODO
        public bool isTalking = false;

        public GameObject Companion;
        public bool CompanionControlMode = false;


        public Weapon Weapon;

        private void Start() {
            _agent = GetComponent<NavMeshAgent>();
            camera = GetComponentInChildren<Camera>();
            manager.player = this; // make sure DustSceneManager always knows about the current player object
            CurrentHealth = MaxHealth;
            CurrentStamina = MaxStamina;
            CombatUI.Stamina.GetComponent<Slider>().value = CurrentStamina;
            CombatUI.PlayerHP.GetComponent<Slider>().value = CurrentHealth;
            LastCombatActions.Add(new LastCombatAction(this, PlayerMove.Move, gameObject.transform.position, gameObject.transform.position));
            conversation = gameObject.GetComponent<PlayerConversation>();
            conversation.player = this;
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
            _agent.updateUpAxis = false;
            _agent.updateRotation = false;
            Cursor.SetCursor(manager.GetCursorTexture(MouseCursorChange.CursorShape.Default), Vector2.zero, CursorMode.ForceSoftware);
        }
    
        private void Update() {
            /*if (Input.GetKeyDown(KeyCode.Tab)) { Permanent inventory for now
                if (inventoryOpen) {
                    InventoryUI.CloseInventory();
                    return;
                }
                InventoryUI.OpenInventory();
                return;
            }*/
            RaycastHit hit;

            if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 100) || UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;
            if (CompanionControlMode && Companion != null) {
                Companion.GetComponent<NavMeshAgent>().destination = hit.point;
                return;
            }
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
            if (inCombat) {
                if (CombatManager.SelectedMove != PlayerMove.Move) return;
                CombatMove(hit);
                choseTurn = true;
                return;
            }

            var hitObject = hit.transform.gameObject;
            Vector2 interact2D = new Vector2(hitObject.transform.position.x, hitObject.transform.position.y);
            Vector2 self2D = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            if (hitObject.CompareTag("Clickable") && Vector2.Distance(interact2D, self2D) <= MAX_CLICK_DISTANCE && !isTalking) {
                var clickable = hitObject.GetComponents<IClickableGameObject>();
                foreach (var click in clickable) {
                    click.OnClick(this);
                }
                return;
            }

            var combatant = hitObject.GetComponent<ICombatant>();
            if (combatant != null && !isTalking && hitObject != gameObject) {
                CombatManager = new CombatManager(this, hitObject.GetComponent<DustEntity>());
                inCombat = !inCombat;
                CombatUI.enabled = inCombat;
                Debug.Log("Combat: " + inCombat + " | Enemy: " + hitObject.name);
                return;
            }
            
            var conversationHolder = hitObject.GetComponent<PlayerConversationHolder>();
            if (conversationHolder != null && Vector2.Distance(interact2D, self2D) <= MAX_CLICK_DISTANCE) {
                conversation.ClickNext(conversationHolder);
                return;
            }

            if (canMove && !isTalking) {
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
        public bool HasItem(int id) {
            foreach (var item in Inventory) {
                if (item == null) {
                    continue;
                }
                if (item.id == id) {
                    return true;
                }
            }

            return false;
        }

        public void AddItem(ItemStack itemStack) {
            if (HasItem(itemStack.id)) {
                return;
            }

            var i = 0;
            foreach (var slot in Inventory) {
                if (slot == null) {
                    Inventory[i] = itemStack;
                    return;
                }
                i++;
            }
            InventoryUI.refreshInventory();
        }

        public void RemoveItem(ItemStack itemStack) {
            Inventory.Remove(itemStack);
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
