using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour {
    public Transform player;

    public GameObject door;
    public AudioSource genSound;

    public Transform portal;
    public GameObject text;
    public float dist;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player" && !door.active) {
            door.SetActive(true);
            genSound.Stop();
            GetComponent<AudioSource>().Play();
            door.GetComponent<AudioSource>().Play();
        }
    }

    void Update() {
        if (Vector3.Distance(player.position, portal.position) < dist) {
            text.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) SceneManager.LoadScene("Scenes/End", LoadSceneMode.Single);
        } else text.SetActive(false);
    }
}
