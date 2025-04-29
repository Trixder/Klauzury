using Unity.VisualScripting;
using UnityEngine;

public class PuzzleTwo : MonoBehaviour {
    public Transform player;
    public bool fuel = false;
    public static bool gen0 = false, gen1 = false;
    public float dist;
    public ParticleSystem el;

    public Transform genTrans0;
    public GameObject textGen0;

    public Transform genTrans1;
    public GameObject textGen1;

    public Transform fuelDeposit;
    public GameObject textFuel;


    public BoxCollider2D[] water;
    public GameObject[] doors0;
    public GameObject[] doors1;


    void Update() {
        if (!fuel & Vector3.Distance(player.position, fuelDeposit.position) < dist) {
            textFuel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) {
                fuelDeposit.gameObject.GetComponent<AudioSource>().Play();
                fuel = true;
            }
        } else textFuel.SetActive(false);

        if (fuel & Vector3.Distance(player.position, genTrans0.position) < dist) {
            textGen0.SetActive(true);
            if (fuel && Input.GetKeyDown(KeyCode.E)) {
                fuel = false;
                gen0 = true;
                genTrans0.gameObject.GetComponent<AudioSource>().Play(0);
                el.Play();
                for (int i = 0; i < water.Length; i++) {
                    water[i].GetComponent<AudioSource>().Play(0);
                    water[i].enabled = true;
                }
                for (int i = 0; i < doors0.Length; i++) doors0[i].SetActive(false);
            }
        } else textGen0.SetActive(false);

        if (fuel & Vector3.Distance(player.position, genTrans1.position) < dist) {
            textGen1.SetActive(true);
            if (fuel && Input.GetKeyDown(KeyCode.E)) {
                fuel = false;
                gen1 = true;
                genTrans1.gameObject.GetComponent<AudioSource>().Play(0);
                for (int i = 0; i < doors1.Length; i++) doors1[i].SetActive(false);
            }
        } else textGen1.SetActive(false);
    }
}
