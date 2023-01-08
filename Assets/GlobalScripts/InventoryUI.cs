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
            _buttons[1].GetComponent<InventorySlot>().Item = DustManager.ItemRegistry[0];
            refreshInventory();
        }

        public void refreshInventory() {
            foreach (var button in _buttons) {
                if (button.GetComponent<InventorySlot>().Item is null) {
                    button.GetComponent<Image>().sprite = null;
                    continue;
                }
                button.GetComponent<Image>().sprite = button.GetComponent<InventorySlot>().Item.Sprite;
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
            if (!PlayerScript.inventoryOpen) {
                
                Debug.Log("Huh");
                return;
            }
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
            else if (slot.Item == null && _follower != null) {
                Debug.Log("Dropped item");
                slot.Item = _pickedItem;
                Destroy(_follower);
                refreshInventory();
            }
        }
    }
}