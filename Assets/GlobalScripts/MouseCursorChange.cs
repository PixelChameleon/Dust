using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GlobalScripts {
    public class MouseCursorChange : MonoBehaviour {
        public enum CursorShape {
            Interact,
            Attack,
            Default,
            Talk
        }

        public CursorShape cursor;
        public string hintText = "";

        private DustManager _manager;


        private void Start() {
            _manager = GameObject.FindGameObjectWithTag("DustManager").GetComponent<DustManager>();
        }

        void OnMouseEnter() {
            Cursor.SetCursor(_manager.GetCursorTexture(cursor), Vector2.zero, CursorMode.ForceSoftware);
            if (hintText == "") return;
            _manager.interactionHintText.GetComponent<TextMeshProUGUI>().text = hintText;
            _manager.interactionHintImage.GetComponent<Image>().sprite = Sprite.Create(_manager.GetCursorTexture(cursor), Rect.MinMaxRect(0, 0, 32, 32), Vector2.zero);
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
