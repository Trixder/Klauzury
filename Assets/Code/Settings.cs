using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour {
    [SerializeField] private AudioListener audioListener;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject backTo;

    void Start() {
        AudioListener.volume = volumeSlider.value;
    }

    public void Awake() {
        if (AudioListener.volume != volumeSlider.value) volumeSlider.value = AudioListener.volume;
    }

    public void Update() {
        if (!Input.GetKeyDown(KeyCode.Escape) || !settings.active || SceneManager.GetActiveScene().name == "menu") return;
        settings.SetActive(false);
        backTo.SetActive(false);
    }

    public void ChangeSettings() {
        AudioListener.volume = volumeSlider.value;
    }

    public void Open(GameObject current) {
        backTo = current;
        backTo.gameObject.SetActive(false);
        settings.gameObject.SetActive(true);
    }

    public void Back() {
        settings.gameObject.SetActive(false);
        backTo.gameObject.SetActive(true);
    }
}