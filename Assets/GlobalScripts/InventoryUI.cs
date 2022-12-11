using System;
using System.Collections.Generic;
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
        

        private void Start() {
            foreach (Transform child in gameObject.transform) {
                Debug.Log(child.name);
                _buttons.Add(child.gameObject);
            }
            Debug.Log("Buttons: " + _buttons.Count);
            _buttons[1].GetComponent<InventorySlot>().Item = DustManager.ItemRegistry[0];
            refreshInventory();
        }

        public void refreshInventory() {
            foreach (var button in _buttons) {
                button.GetComponent<Image>().sprite = button.GetComponent<InventorySlot>().Item.Sprite;
            }
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
            }
            else if (slot.Item == null && _follower != null) {
                Debug.Log("Dropped item");
                slot.Item = _pickedItem;
                Destroy(_follower);
            }
        }
    }
}