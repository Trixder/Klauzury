using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public Color red;
    public static float health = 100;
    public float alpha = 0;
    public bool dir = true;
    public GameObject filtr;

    private void Awake() {
        //TODO 0 remove comment when "facility" (or smth like that) scene is created
        //if (SceneManager.GetActiveScene().name == "facility") return;
        health = 100;
    }

    void Update() {
        if (health == 100) return;
        float hp = 0.5f - (health / 100 / 2);

        if (dir) {
            alpha += Time.deltaTime / 5;
            if (alpha > hp) alpha = hp;
            if (alpha == hp) dir = false;
        } else if (!dir) {
            alpha -= Time.deltaTime / 5;
            if (alpha < 0) alpha = 0;
            if (alpha == 0) dir = true;
        }

        filtr.GetComponent<Image>().color = new Color(red.r, red.g, red.b, alpha);
    }

    public static void ChangeHP(float modifier) {
        health += modifier;
        if (health > 100) health = 100;
        else if (health < 0)  SceneManager.LoadScene("Scenes/DeathScreen", LoadSceneMode.Single);
    }
}