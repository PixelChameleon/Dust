using GlobalScripts;
using TMPro;
using UnityEngine;

public class DeathUI : MonoBehaviour {

    public GameObject DeathMenu;
    public GameObject DeathMessage;
    public GameObject DeathCounter;
    
    private PlayerScript _player;
    
    void Start() {
        _player = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerScript>();
    }

    public void RespawnClick() {
        _player.Respawn();
    }

    public void Refresh(string deathMessage) {
        DeathCounter.GetComponent<TextMeshProUGUI>().text = "Tode: " + PlayerPrefs.GetInt("stat_deaths", 0);
        DeathMessage.GetComponent<TextMeshProUGUI>().text = deathMessage;
    }
    
}
