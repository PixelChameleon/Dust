using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GlobalScripts {
    public class InventoryUI : MonoBehaviour {

        public PlayerScript PlayerScript;
        public DustManager DustManager;
        private List<GameObject> _buttons = new();
        private ItemStack _pickedItem = null;
        public GameObject CursorFollowObjectPrefab;
        private GameObject _follower;
        
        private Color _activeColor = new(255, 255, 255, 1);
        private Color _inactiveColor = new(255, 255, 255, 0.3f);
        

        private void Start() {
            foreach (Transform child in gameObject.transform) {
                Debug.Log(child.name);
                if (!child.CompareTag("InvSlot")) {
                    continue;
                }
                _buttons.Add(child.gameObject);
            }
            Debug.Log("Buttons: " + _buttons.Count);
            refreshInventory();
        }

        public void refreshInventory() {
            if (PlayerScript.Inventory.Count == 0) {
                return;
            }
            int i = 0;
            foreach (var button in _buttons) {
                Debug.Log("Player inventory size: " + PlayerScript.Inventory.Count);
                if (PlayerScript.Inventory[i] == null) {
                    button.GetComponent<Image>().sprite = null;
                    button.GetComponent<Image>().color = _inactiveColor;
                    i++;
                    continue;
                }
                button.GetComponent<InventorySlot>().Item = PlayerScript.Inventory[i];
                button.GetComponent<Image>().sprite = button.GetComponent<InventorySlot>().Item.Sprite;
                button.GetComponent<Image>().color = _activeColor;
                i++;
            }
        }

        public void OpenInventory() {
            gameObject.SetActive(true);
            PlayerScript.inventoryOpen = true;
            refreshInventory();
        }
        
        public void CloseInventory() {
            if (_follower != null) {
                return;
            }
            gameObject.SetActive(false);
            PlayerScript.inventoryOpen = false;
        }

        private void Update() {
           // Set cursor item
           if (_follower != null) {
               _follower.transform.position = Input.mousePosition;
           }
        }

        private void Pickup(ItemStack stack) {
            if (_follower == null) {
                _follower = Instantiate(CursorFollowObjectPrefab, gameObject.transform);
            }
            _follower.GetComponent<Image>().sprite = stack.Sprite;
            _follower.GetComponentInChildren<TextMeshProUGUI>().text = stack.Name;
            _pickedItem = stack;
        }

        public void OnButtonClick() {
            Debug.Log("Inv click");
            refreshInventory();
            var button = EventSystem.current.currentSelectedGameObject;
            InventorySlot slot = button.GetComponent<InventorySlot>();
            if (slot.Item != null) {
                Pickup(slot.Item);
                Debug.Log("Picked up item");
                slot.Item = null;
                refreshInventory();
            }
            else if (_follower != null) {
                if (slot.Item == null) {
                    slot.Item = _pickedItem;
                    Destroy(_follower);
                    refreshInventory();
                    Debug.Log("Dropped item");
                } else if (_pickedItem.CanCombineWith != null && slot.Item == _pickedItem.CanCombineWith) {
                    slot.Item = _pickedItem.CombinationResult;
                    Destroy(_follower);
                    refreshInventory();
                    Debug.Log("Crafted " + slot.Item.Name);
                }
            }
        }
    }
}