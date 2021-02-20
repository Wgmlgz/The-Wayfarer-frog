using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
    public string bool_name;

    public void PauseGame() {
        Time.timeScale = 0;
        Debug.Log("Pause Game");
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        Debug.Log("Resume Game");
    }

    public void tryShow() {
        Debug.Log("DA");
        if (PlayerPrefs.GetInt(bool_name) == 1) {
            return;
        } else {
            show();
        }
    }
    public void show() {
        PlayerPrefs.SetInt(bool_name, 1);
        GetComponent<Canvas>().enabled = true;
        PauseGame();
    }
    public void hide() {
        ResumeGame();
        GetComponent<Canvas>().enabled = false;
    }
}
