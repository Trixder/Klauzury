using UnityEngine;

public class EnemyVisibility : MonoBehaviour {
    public float alpha = .25f;
    public float minAlpha = .25f;
    public bool inLight = false;
    private static Transform player;
    public Material mat;

    void Awake() {
        player = GameObject.Find("Player").transform;
        mat = gameObject.GetComponent<Renderer>().material;
        mat.color = new Color(1f, 1f, 1f, 0.25f);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (Time.timeScale == 0) return;

        Renderer rend = gameObject.GetComponent<SpriteRenderer>();
        inLight = true;
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (Time.timeScale == 0) return;

        Renderer rend = gameObject.GetComponent<SpriteRenderer>();
        inLight = false;
    }

    private void Update() {
        if (Time.timeScale == 0) return;

        if (inLight && alpha < 1) {
            alpha += (Time.deltaTime / Vector3.Distance(transform.position, player.position)) * 10;
            if (alpha > 1) alpha = 1;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, alpha);
        } else if (!inLight && alpha > minAlpha) {
            alpha -= (Time.deltaTime / Vector3.Distance(transform.position, player.position)) * 10;
            if (alpha < minAlpha) alpha = minAlpha;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, alpha);
        }
    }
}