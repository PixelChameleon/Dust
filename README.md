# Dust

* Unity 2021.3.11f1 (LTS)
* [Github Desktop](https://desktop.github.com) herunterladen
* [Kurzes Git-Tutorial mit Bildern](https://imgur.com/a/PhLacwV)

### [Projekt zur Organisation](https://github.com/users/Malfrador/projects/3)

## Dokumentation

* **Spieler & Bewegung**
  * Die Spielwelt ist 3D, da sonst die Navigation nicht gut funktioniert. Die Kamera schwebt über dem Spieler und ist orthografisch, es sieht deswegen aus wie 2D.
  * Die Bewegung des Spielers erfolgt über ein Nav-Mesh. Dieses lässt sich in Unity unter `Window -> AI -> Navigation` ansehen und bearbeiten.
  * Der Spieler besitzt ein simples Inventar (Items und Anzahl). Das `PlayerScript` hat verschiedene Methoden, um Items hinzuzufügen und zu entfernen bzw. zu schauen ob der Spieler ein Item besitzt.
* **Klicken**
  * Objekte brauchen das `Clickable`-Tag und einen `Box Collider`-Component (nicht 2D!)
  * Dann einfach ein Script erstellen, das das `IClickableGameObject`-Interface nutzt. In `Scene01 -> ClickyClicky` findet sich ein simples Beispiel. Wichtig ist, das die `onClick`-Methode existiert.
  * Für Items gibt es schon ein fertiges `CollectableItem`-Script, in dem man im Editor einfach das Item eintragen kann. Das Script stellt auch sicher, das man ein Item nur einmal einsammeln kann.
* **Items**
  * Items haben eine ID.
  * Die Definitionen für die ID finden sich aktuell im `DustManager`-Script. In Zukunft sollten Items evtl. aus einer Datei gelesen werden.
* **Szenen**
  * Verschiedene Scenes können über `DustManager` geladen werden. Die IDs lassen sich in Unity unter `File -> Build Settings` festlegen.
  * Scenen lassen sich durch einen Rechtsklick im Unity-Editor (linke Seitenleiste) ausblenden bzw. entladen.
