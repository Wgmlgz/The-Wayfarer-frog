using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {
    public static void toMapSt() { SceneManager.LoadScene("Map"); }
    public static void toCargoSt() { SceneManager.LoadScene("Cargo"); }
    public static void toMainSt() { SceneManager.LoadScene("Tasks"); }
    public static void toTasksSt() { }

    public void toMap() { SceneManager.LoadScene("Map"); }
    public void toCargo() { SceneManager.LoadScene("Cargo"); }
    public void toTasks() { SceneManager.LoadScene("Tasks"); }
    public void toMain() { }
}
