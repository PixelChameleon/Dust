using GlobalScripts;
using UnityEngine;

public class SpawnCompanion : MonoBehaviour, IClickableGameObject {

    public GameObject CompanionPrefab;
    public bool alreadySpawned = false;

    public void OnClick(PlayerScript player) {
        if (player.InventoryUI.PickedItem != DustManager.ItemRegistry[7]) {
            GetComponent<InvestigateObjectScript>().SpawnBox("Sieht aus als würde hier ein Roboter draufpassen...");
            return;
        }

        if (alreadySpawned) {
            return;
        }
        var comp = Instantiate(CompanionPrefab, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Quaternion.identity);
        player.Companion = comp;
        player.CompanionButton.SetActive(true);
        comp.GetComponent<CompanionScript>().Talk("Beep-Beep. Gu-ten Ta-g", 5);
        player.RemoveItem(DustManager.ItemRegistry[7]);
    }
}
