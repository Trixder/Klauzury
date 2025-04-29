using UnityEngine;

public class SwitchPuzzle : MonoBehaviour {
    private static int completed = 0;
    private static GameObject player;
    public GameObject door = null;
    private static int[] selected = new int[4];
    private static int[] solution = {2, 1, 3, 4};
    public float dist;
    public int order;
    private static float stay = 3f;
    private static int toClear = 3;
    public GameObject text;

    public AudioSource sound;
    public AudioClip[] sounds = new AudioClip[3];

    public void Awake() {
        player = GameObject.Find("Player");
        gameObject.GetComponent<Renderer>().material.color = new Color(68f / 255f, 71f / 255f, 68f / 255f);
        completed = 0;
        selected = new int[4];
        toClear = 3;
    }

    void Update() {
        if (order == 0 | Time.timeScale == 0) return;

        if (completed == 0 && Vector3.Distance(player.transform.position, transform.position) < dist) {
            text.SetActive(true);
            for (int i = 0; i < selected.Length; i++) {
                if (selected[i] == order) {
                    text.SetActive(false);
                    i = selected.Length;
                }
            }
        } else text.SetActive(false);

        if (completed == 0 && Input.GetKeyDown(KeyCode.E) && Vector3.Distance(player.transform.position, transform.position) < dist) {
            for (int i = 0; i < selected.Length; i++) {
                if (selected[i] == order) return;
                if (selected[i] == 0) {
                    if (!sound.isPlaying) sound.Play();
                    gameObject.GetComponent<Renderer>().material.color = new Color(14f / 255f, 17f / 255f, 68f / 255f);
                    toClear++;
                    selected[i] = order;
                    if (i == selected.Length - 1) Check();
                    i = selected.Length;
                }
            }
        } else if (completed == 1) {
            order = 0;
            gameObject.GetComponent<Renderer>().material.color = new Color(29f / 255f, 149f / 255f, 30f / 255f);
            if (door != null) door.transform.position = new Vector3(100, 100, 0);
            sound.clip = sounds[1];
            sound.Play();
        } else if (completed == 2) {
            if (stay < 0) {
                sound.clip = sounds[0];
                gameObject.GetComponent<Renderer>().material.color = new Color(68f / 255f, 71f / 255f, 68f / 255f);
                if (toClear == 0) {
                    stay = 3f;
                    completed = 0;
                } toClear--;
            } else {
                sound.clip = sounds[2];
                if (!sound.isPlaying) sound.Play();
                gameObject.GetComponent<Renderer>().material.color = new Color(68f / 255f, 17f / 255f, 14f / 255f);
                stay -= Time.deltaTime;
            }
;        }
    }

    public static void Check() {
        bool ok = true;
        for (int i = 0; i < solution.Length; i++) if (solution[i] != selected[i]) ok = false;

        if (ok) completed = 1;
        else {
            completed = 2;
            toClear = 3;
            for (int i = 0; i < selected.Length; i++) selected[i] = 0;
        }
    }
}