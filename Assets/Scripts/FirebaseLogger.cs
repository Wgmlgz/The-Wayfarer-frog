using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;

public class FirebaseLogger : MonoBehaviour {
    public GameObject zh;
    int coins_from_game_start;
    private void Awake() {
        zh = GameObject.FindGameObjectWithTag("Player");
    }
    public void NewGame() {
        coins_from_game_start = zh.GetComponent<CoinCollector>().coins;
        FirebaseAnalytics.LogEvent(
          "start_play",
          new Parameter(
            "coins", zh.GetComponent<CoinCollector>().coins),
          new Parameter(
            "level", zh.GetComponent<Levels>().level)
        );
    }
    public void Death() {
        FirebaseAnalytics.LogEvent(
          "death",
          new Parameter(
            "coins", zh.GetComponent<CoinCollector>().coins - coins_from_game_start),
          new Parameter(
            "distance", zh.GetComponent<Score>().GetDist())
        );
    }

    public void Log(string s) {
        FirebaseAnalytics.LogEvent(s);
    }
    public void Log(string s, string par, string val) {
        FirebaseAnalytics.LogEvent(s, par, val);
    }
    public void Log(string s, string par, int val) {
        FirebaseAnalytics.LogEvent(s, par, val);
    }
    //public void 
}
