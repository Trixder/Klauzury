using UnityEngine;

public class Switch : MonoBehaviour {
    public Transform player;

    public GameObject[] doors;
    public GameObject text;
    public float dist;

    void Update() {
        if (PuzzleTwo.gen0 && Vector3.Distance(player.position, transform.position) < dist) {
            text.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && !GetComponent<AudioSource>().isPlaying) {
                GetComponent<AudioSource>().Play();
                for (int i = 0; i < doors.Length; i++) doors[i].SetActive(!doors[i].active);
            }
        } else text.SetActive(false);
    }
}
