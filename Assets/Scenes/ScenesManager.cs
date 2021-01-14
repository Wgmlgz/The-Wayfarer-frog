using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {
    public static void toMapSt()   { SceneManager.LoadScene("Map");   }
    public static void toCargoSt() { SceneManager.LoadScene("Cargo"); }
    public static void toTasksSt() { SceneManager.LoadScene("Tasks"); }
    public static void toMainSt()  { SceneManager.LoadScene("Main");  }

    public void toMap()   { toMapSt();   } 
    public void toCargo() { toCargoSt(); }
    public void toTasks() { toTasksSt(); }
    public void toMain()  { toMainSt();  }
}
