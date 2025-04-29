using UnityEngine;

public class CameraSwitch : MonoBehaviour {
    public static GameObject main;
    public static GameObject player;
    public GameObject switchTo;
    void Awake() {
        main = GameObject.Find("MainCamera");
        player = GameObject.Find("Player");
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (Time.timeScale == 0) return;

        if (player == other.gameObject) {
            main.active = false;
            switchTo.active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (Time.timeScale == 0) return;

        if (player == collision.gameObject) {
            main.active = true;
            switchTo.active = false;
        }
    }
}