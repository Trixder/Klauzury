using UnityEngine;
using UnityEngine.SceneManagement;

public class FacilityEnter : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") SceneManager.LoadScene("Scenes/Facility", LoadSceneMode.Single);
    }
}
