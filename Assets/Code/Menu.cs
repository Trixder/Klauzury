using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public GameObject cnv;
    public int running = 1;
    public string[] changeTo;

    void Update() { if (Input.GetKeyDown(KeyCode.Escape) && cnv != null) CloseOpen(); }

    public void TestButton() {
        Debug.Log("Works");
    }

    public void ChangeScene(int which) {
        Time.timeScale = 1;
        SceneManager.LoadScene(changeTo[which], LoadSceneMode.Single);
    }

    public void CloseOpen() {
        if (SceneManager.GetActiveScene().name == "menu") return;
        cnv.SetActive(!cnv.active);

        if (running == 1) running = 0;
        else running = 1;

        Time.timeScale = running;
    }

    public void Quit() { Application.Quit(); }
}