using UnityEngine;

public class Portal : MonoBehaviour {
    public static GameObject[] dim = null;
    public static GameObject[] norm = null;
    public static GameObject player = null;
    public GameObject text;

    void Awake() {
        if (player == null) player = GameObject.Find("Player");
        if (dim == null) dim = GameObject.FindGameObjectsWithTag("Dim");
        if (norm == null) norm = GameObject.FindGameObjectsWithTag("Norm");
        for (int i = 0; i < dim.Length; i++) dim[i].SetActive(false);
    }

    void OnDestroy() {
        player = null;
        dim = null;
        norm = null;
    }

    void Update() {
        if (Vector3.Distance(player.transform.position, transform.position) < 1.75f) {
            text.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && !GetComponent<AudioSource>().isPlaying) {
                GetComponent<AudioSource>().Play();
                for (int i = 0; i < dim.Length; i++) dim[i].SetActive(!dim[i].active);
                for (int i = 0; i < norm.Length; i++) norm[i].SetActive(!norm[i].active);
            }
        } else text.SetActive(false);
    }
}