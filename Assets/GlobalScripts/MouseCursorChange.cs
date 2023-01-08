using System;
using TMPro;
using UnityEngine;

namespace GlobalScripts {
    public class MouseCursorChange : MonoBehaviour {
        public enum CursorShape {
            Interact,
            Attack,
            Open,
            Talk
        }

        public CursorShape cursor;
        public string hintText = "";

        private DustManager _manager;
        private GameObject _object;


        private void Start() {
            _manager = GameObject.FindGameObjectWithTag("DustManager").GetComponent<DustManager>();
        }

        private void Update() {
            if (_object != null) {
                Physics.Raycast(_manager.player.camera.ScreenPointToRay(Input.mousePosition), out var hit, 100);
                Vector3 worldPosition = hit.transform.position;
                worldPosition.z = 5f;
                _object.transform.position = worldPosition;
                _object.transform.rotation.Set(90, 0, 0, 0);
            }
        }

        void OnMouseEnter() {
            Cursor.SetCursor(_manager.GetCursorTexture(cursor), Vector2.zero, CursorMode.ForceSoftware);
            if (hintText == "") return;
            Physics.Raycast(_manager.player.camera.ScreenPointToRay(Input.mousePosition), out var hit, 100);
            Vector3 worldPosition = hit.transform.position;
            worldPosition.z = 5f;
            _object = Instantiate(_manager.cursorPrefab, worldPosition, Quaternion.identity);
            _object.GetComponent<TextMeshPro>().text = hintText;
            _object.transform.rotation.Set(90, 0, 0, 0);
        }

        void OnMouseExit() {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
            if (_object != null) {
                Destroy(_object);
            }
        }
    }
}
