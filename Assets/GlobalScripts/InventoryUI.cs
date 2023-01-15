using System;
using System.Collections.Generic;
using GlobalScripts.combat;
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
        public ItemStack PickedItem = null;
        public GameObject CursorFollowObjectPrefab;
        private GameObject _follower;
        
        private Color _activeColor = new(255, 255, 255, 1);
        private Color _inactiveColor = new(255, 255, 255, 0.0f);
        

        private void Start() {
            foreach (Transform child in gameObject.transform) {
                if (!child.CompareTag("InvSlot")) {
                    continue;
                }
                _buttons.Add(child.gameObject);
            }
            refreshInventory();
        }

        public void refreshInventory() {
            if (PlayerScript.Inventory.Count == 0) {
                return;
            }
            int i = 0;
            foreach (var button in _buttons) {
                button.GetComponent<InventorySlot>().slotID = i;
                if (i == 8) {
                    if (PlayerScript.Inventory[8] == null) {
                        PlayerScript.Weapon = null;
                    }
                    else if (PlayerScript.Inventory[8] is Weapon) {
                        PlayerScript.Weapon = (Weapon) button.GetComponent<InventorySlot>().Item;
                    }
                }
                if (i > PlayerScript.Inventory.Count - 1 || PlayerScript.Inventory[i] == null) {
                    button.GetComponent<Image>().sprite = null;
                    var colorBlock = button.GetComponent<Button>().colors;
                    colorBlock.normalColor = _inactiveColor;
                    colorBlock.highlightedColor = _inactiveColor;
                    colorBlock.pressedColor = _inactiveColor;
                    button.GetComponent<Button>().colors = colorBlock;
                    i++;
                    continue;
                }
                button.GetComponent<InventorySlot>().Item = PlayerScript.Inventory[i];
                button.GetComponent<Image>().sprite = button.GetComponent<InventorySlot>().Item.Sprite;
                var colorBlock2 = button.GetComponent<Button>().colors;
                colorBlock2.normalColor = _activeColor;
                colorBlock2.highlightedColor = _activeColor;
                colorBlock2.pressedColor = _activeColor;
                button.GetComponent<Button>().colors = colorBlock2;
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
            PickedItem = stack;
        }

        public void OnButtonClick() {
            refreshInventory();
            var button = EventSystem.current.currentSelectedGameObject;
            InventorySlot slot = button.GetComponent<InventorySlot>();
            if (slot.Item != null && PickedItem == null) {
                Pickup(slot.Item);
                slot.Item = null;
                PlayerScript.Inventory[slot.slotID] = null;
                refreshInventory();
            }
            else if (_follower != null) {
                if (slot.Item != null && PickedItem.CanCombineWith != null && slot.Item == PickedItem.CanCombineWith) {
                    slot.Item = PickedItem.CombinationResult;
                    PlayerScript.Inventory[slot.slotID] = slot.Item;
                    Destroy(_follower);
                    refreshInventory();
                    PickedItem = null;
                    Debug.Log("Crafted " + slot.Item.Name);
                    return;
                }
                if (slot.Item == null) {
                    if (slot.slotID == 8 && PickedItem is not Weapon) { // Only allow weapons in slot 8
                        return;
                    }
                    slot.Item = PickedItem;
                    PlayerScript.Inventory[slot.slotID] = slot.Item;
                    Destroy(_follower);
                    refreshInventory();
                    PickedItem = null;
                }
            }
        }
    }
}