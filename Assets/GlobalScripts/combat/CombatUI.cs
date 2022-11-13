using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GlobalScripts.combat {
    public class CombatUI : MonoBehaviour {
        public PlayerScript PlayerScript;

        public GameObject Stamina;
        public GameObject PlayerHP;
        public GameObject EnemyHP;
        public GameObject ActionText;
        public GameObject EnemyAmmo;
        public GameObject PlayerAmmo;

        public CombatManager CombatManager;

        private void Start() {
        }

        public void OnButtonClick() {
            if (!PlayerScript.canAct) {
                return;
            }
            var button = EventSystem.current.currentSelectedGameObject;
            Text text = ActionText.GetComponent<Text>();
            if (button.name.Equals("Attack") && !PlayerScript.choseTurn) {
                text.text = "Angreifen";
                CombatManager.SelectedMove = PlayerMove.Attack;
                CombatManager.Shoot(PlayerScript, CombatManager.Enemy);
                CombatManager.DoAITurn();
            } else if (button.name.Equals("Move")) {
                text.text = "Bewegen";
                CombatManager.SelectedMove = PlayerMove.Move;
            } else if (button.name.Equals("UseItem") && !PlayerScript.choseTurn) {
                text.text = "Nachladen";
                CombatManager.SelectedMove = PlayerMove.Heal;
                PlayerScript.Weapon.TurnsUsed = 0;
                CombatManager.UpdateAmmo();
                CombatManager.DoAITurn();
            } else if (button.name.Equals("NextTurn")) {
                text.text = "Warten...";
                CombatManager.DoAITurn();
            }
        }
    }
}