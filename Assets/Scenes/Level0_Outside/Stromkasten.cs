using System;
using System.Collections;
using System.Collections.Generic;
using GlobalScripts;
using UnityEngine;

public class Stromkasten : MonoBehaviour, IClickableGameObject {

    public GameObject parentPopup;
    public GameObject cableTexture;
    public Color firstColor;
    public Color secondColor;
    public Color thirdColor;
    public static float COLOR_ACCURACY = 0.3f;
    private int successCounter = 0;
    
    public void OnClick(PlayerScript player) {
        RaycastHit hit;
        Physics.Raycast(player.camera.ScreenPointToRay(Input.mousePosition), out hit, 100);
        
        // Color of clicked pixel on sprite
        Debug.Log("Hit at " + hit.point.x + " : " + hit.point.z);
        Sprite sprite = cableTexture.GetComponent<SpriteRenderer>().sprite;
        Rect rect = sprite.textureRect;
        float x = hit.point.x - cableTexture.transform.position.x;
        float z = hit.point.z - cableTexture.transform.position.z;
        x *= sprite.pixelsPerUnit;
        z *= sprite.pixelsPerUnit;
        x += rect.width / 2;
        z += rect.height /2;
        x += rect.x;
        z += rect.y;
        Color pixel = sprite.texture.GetPixel(Mathf.FloorToInt(x), Mathf.FloorToInt(z));
        Debug.Log("--- Tex Coords: " + x + " / " + z);
        Debug.Log("Pixel: " + pixel);
        Debug.Log("Color: " + firstColor);

        if (pixel.r < 0.2f && pixel.g <= 0.2f && pixel.g <= 0.2f) {
            Debug.Log("transparent");
            return;
        }
        
        
        if (successCounter == 0) {
            if (Math.Abs(firstColor.r - pixel.r) < COLOR_ACCURACY && Math.Abs(firstColor.g - pixel.g) < COLOR_ACCURACY && Math.Abs(firstColor.b - pixel.b) < COLOR_ACCURACY) {
                successCounter = 1;
                Debug.Log("First color success.");
                return;
            }
            Debug.Log("Failed first");
            // TODO: Kill the player
        }
        if (successCounter == 1) {
            if (Math.Abs(secondColor.r - pixel.r) < COLOR_ACCURACY && Math.Abs(secondColor.g - pixel.g) < COLOR_ACCURACY && Math.Abs(secondColor.b - pixel.b) < COLOR_ACCURACY) {
                successCounter = 2;
                Debug.Log("Second color success.");
                return;
            }
            Debug.Log("Failed second");
            // TODO: Kill the player
            
        }
        if (successCounter == 2) {
            if (Math.Abs(thirdColor.r - pixel.r) < COLOR_ACCURACY && Math.Abs(thirdColor.g - pixel.g) < COLOR_ACCURACY && Math.Abs(thirdColor.b - pixel.b) < COLOR_ACCURACY) {
                successCounter = 3;
                Debug.Log("Done");
                player.canMove = true;
                parentPopup.SetActive(false);
                return;
            }
            Debug.Log("Failed last");
            // TODO: Kill the player
        }
        
    }
}
