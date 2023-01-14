using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GlobalScripts {
    public class MouseCursorChange : MonoBehaviour {
        public enum CursorShape {
            Interact,
            InteractGreen,
            Attack,
            Default,
            Talk
        }

        public CursorShape cursor;
        public string hintText = "";

        private DustManager _manager;
        private PlayerScript _player;


        private void Start() {
            _manager = GameObject.FindGameObjectWithTag("DustManager").GetComponent<DustManager>();
            _player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScript>();
        }

        void OnMouseEnter() {
            var additionalText = "";
            var shape = cursor;
            var isToFar = Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(_player.gameObject.transform.position.x, _player.gameObject.transform.position.y)) > _player.MAX_CLICK_DISTANCE;
            if (isToFar) {
                additionalText = " (Zu weit entfernt)";
            }
            if (shape == CursorShape.Interact && !isToFar) {
                shape = CursorShape.InteractGreen;
            }
            var texture2D = _manager.GetCursorTexture(shape);
            Cursor.SetCursor(texture2D, Vector2.zero, CursorMode.ForceSoftware);
            if (hintText == "") return;
            _manager.interactionHintText.GetComponent<TextMeshProUGUI>().text = hintText + additionalText;
            _manager.interactionHintImage.GetComponent<Image>().sprite = Sprite.Create(texture2D, Rect.MinMaxRect(0, 0, 32, 32), Vector2.zero);
            _manager.interactionHintImage.SetActive(true);
        }

        void OnMouseExit() {
            Cursor.SetCursor(_manager.GetCursorTexture(CursorShape.Default), Vector2.zero, CursorMode.ForceSoftware);
            _manager.interactionHintText.GetComponent<TextMeshProUGUI>().text = "";
            _manager.interactionHintImage.GetComponent<Image>().sprite = null;
            _manager.interactionHintImage.SetActive(false);
        }
    }
}
