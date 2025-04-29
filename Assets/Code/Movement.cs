using System;
using UnityEngine;

public class Movement : MonoBehaviour {
    [SerializeField] AudioSource sound;
    [SerializeField] Transform cam;

    [SerializeField] Collider2D col0;
    [SerializeField] Collider2D col1;

    private Vector3 mousePos;
    private Vector3 mov;
    public float walkSpeed = 5;
    public float speed = 4;

    private float rotationZ;
    private float rot = 0;
    public float rotSpeed = 150;
    private float rotationSpeed = 0;
    public float minSpeedMod = 0;
    public float acceleration;
    private bool rotDir = false;

    public AudioClip[] soundsGrass = new AudioClip[3];
    public AudioClip[] soundsWood = new AudioClip[3];

    public void Awake() { Physics2D.IgnoreCollision(col0, col1, true); }

    void OnGUI() {
        if (Time.timeScale == 0) return;

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        mousePos.x = Event.current.mousePosition.x;
        mousePos.y = cam.gameObject.GetComponent<Camera>().pixelHeight - Event.current.mousePosition.y;
        cam.localPosition = new Vector3(transform.position.x + mousePos.x / Screen.width - 0.5f, transform.position.y + mousePos.y / Screen.height - 0.5f, -10);

        Vector3 difference = cam.gameObject.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.allCameras[0].gameObject.GetComponent<Camera>().nearClipPlane)) - transform.position;

        float distX = Camera.allCameras[0].transform.position.x - transform.position.x;
        float distY = Camera.allCameras[0].transform.position.y - transform.position.y;

        float offSetX = 0;
        float offSetY = 0;

        if (Camera.allCameras[0].name == "MainCamera") {
            rotSpeed = 120;
            offSetX = (mousePos.x / Screen.width - 0.5f);
            offSetY = (mousePos.y / Screen.height - 0.5f);
        } else rotSpeed = 40;

        difference.x = difference.x + (distX - offSetX);
        difference.y = difference.y + (distY - offSetY);

        if (Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg > 0) rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        else if (Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg < 0) rotationZ = 360 + (Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg);
        else if (Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg == 0 || Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg == 360) rotationZ = 0;


        float dir = Mathf.DeltaAngle(rotationZ, rot);

        if (rotDir != dir > 0) rotationSpeed = minSpeedMod;
        if (rotationSpeed < 1) rotationSpeed += Time.deltaTime / acceleration;

        rotDir = dir > 0;
        if (rotDir) rot -= Time.deltaTime * rotSpeed * rotationSpeed;
        else rot += Time.deltaTime * rotSpeed * rotationSpeed;

        if (rot < 0) rot = 360;
        if (rot > 360) rot = 0;

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rot);
    }

    //TODO 3 player moves slightly when there isnt input (probablly waste of time (Untiy issue))

    void Update() {
        if (Time.timeScale == 0) return;

        if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical")) {
            if (mov.x == 0 && mov.y == 0) return;

            if (Math.Abs(mov.x) > 0) mov.x -= Time.deltaTime * speed * (mov.x / Math.Abs(mov.x));
            if (Math.Abs(mov.x) < 0.001) mov.x = 0;

            if (Math.Abs(mov.y) > 0) mov.y -= Time.deltaTime * speed * (mov.y / Math.Abs(mov.y));
            if (Math.Abs(mov.y) < 0.001) mov.y = 0;

            float mod = Time.deltaTime * 5;
            transform.position += new Vector3(mov.x * mod, mov.y * mod, 0);
        } else if (Input.GetButton("Horizontal") | Input.GetButton("Vertical")) {
            AudioClip[] currentSounds = ChooseSounds();
            if (!sound.isPlaying) sound.clip = currentSounds[UnityEngine.Random.Range(0, currentSounds.Length)];
            if (!sound.isPlaying) sound.Play();

            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            mov = Vector3.Normalize(new Vector3(x, y, 0));

            float mod = Time.deltaTime * walkSpeed;
            float mod1 = 1;
            if (Input.GetKey(KeyCode.LeftShift)) mod1 = 1.25f;
            transform.position += new Vector3(mov.x * mod * mod1, mov.y * mod * mod1, 0);
        }
    }

    public AudioClip[] ChooseSounds() {
        AudioClip[] currentSounds;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, 0), 2, LayerMask.GetMask("Walkable"));
        Debug.DrawRay(transform.position, new Vector2(0, 0), Color.red);

        if (hit.collider != null) {
            switch (hit.collider.transform.name) {
                case "Wood": case "Floor":
                    //TODO 10 Works but should be checked (return in for)
                    for (int i = 0; i < soundsWood.Length; i++) {
                        currentSounds = new AudioClip[soundsWood.Length];
                        currentSounds[i] = soundsWood[i];
                        return currentSounds;
                    } break;
                default:
                    currentSounds = new AudioClip[soundsGrass.Length];
                    for (int i = 0; i < soundsGrass.Length; i++) currentSounds[i] = soundsGrass[i];
                    return currentSounds;
            }
        } else {
            currentSounds = new AudioClip[soundsGrass.Length];
            for (int i = 0; i < soundsGrass.Length; i++) currentSounds[i] = soundsGrass[i];
            return currentSounds;
        }

        currentSounds = new AudioClip[soundsGrass.Length];
        for (int i = 0; i < soundsGrass.Length; i++) currentSounds[i] = soundsGrass[i];
        return currentSounds;
    }
}