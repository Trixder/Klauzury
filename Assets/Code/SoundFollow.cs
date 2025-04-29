using UnityEngine;

public class SoundFollow : MonoBehaviour {
    public GameObject track;

    public bool trackX;
    public bool trackY;

    public bool soundByDistance;

    public float minDist = 1;
    public float maxDist = 400;

    void Update() {
        if (trackX) transform.position = new Vector3(track.transform.position.x, transform.position.y, 0);
        if (trackY) transform.position = new Vector3(transform.position.x, track.transform.position.y, 0);

        if (!soundByDistance) return;

        float dist = Vector3.Distance(transform.position, track.transform.position);

        if (dist < minDist) GetComponent<AudioSource>().volume = 1;
        else if (dist > maxDist) GetComponent<AudioSource>().volume = 0;
        else GetComponent<AudioSource>().volume = 0.75f - (((dist - minDist) / (maxDist - minDist)) / 4 * 3);
    }
}