using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Analytics;
public class ScenesManager : MonoBehaviour {
    public static void toMapSt()   { SceneManager.LoadScene("Map");   }
    public static void toCargoSt() { SceneManager.LoadScene("Cargo"); }
    public static void toTasksSt() { SceneManager.LoadScene("Tasks"); }
    public static void toMainSt()  { SceneManager.LoadScene("Main");  }

    public void toMap()   { toMapSt();   } 
    public void toCargo() { toCargoSt(); }
    public void toTasks() { toTasksSt(); }
    public void toMain()  { toMainSt();  }

    public static void arriveToCitySt() {
        FirebaseAnalytics.LogEvent(
          "arrive_to_city",
          new Parameter(
            "current", PlayerPrefs.GetString("CurrentCity")),
          new Parameter(
            "target", PlayerPrefs.GetString("TargetCity"))
        );
        PlayerPrefs.SetString("CurrentCity", PlayerPrefs.GetString("TargetCity"));
        toMapSt();
    }
    public void arriveToCity() {
        arriveToCitySt();
    }
}
