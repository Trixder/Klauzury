using UnityEngine;
using UnityEngine.UI;

public class Dark : MonoBehaviour {
    public Image overlay;
    public float offSet, len;

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag != "Player") return;

        if ((collision.transform.position.x > (transform.position.x + GetComponent<BoxCollider2D>().offset.x) - GetComponent<BoxCollider2D>().size.x / 2) && (collision.transform.position.x < (transform.position.x + GetComponent<BoxCollider2D>().offset.x) + GetComponent<BoxCollider2D>().size.x / 2)) {
            overlay.color = new Color(0, 0, 0, (collision.transform.position.x - offSet) / len);
        } else overlay.color = new Color(0, 0, 0, 0);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag != "Player") return;
        overlay.color = new Color(0, 0, 0, 0);
    }
}
