using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Levels : MonoBehaviour {
    [Header("data")]
    public int hp;
    public int level;
    public List<int> hpVals;
    [Header("ui")]
    public Slider sl;
    public TMPro.TextMeshProUGUI hpOld, hpCur, hpNext, lvlOld, lvlNext, feedText;

    private void Awake() {
        hp = PlayerPrefs.GetInt("hp");
        level = PlayerPrefs.GetInt("level");
        
        if (PlayerPrefs.HasKey("last_feed") == false) PlayerPrefs.SetString("last_feed", new DateTime(10, 10, 10).ToString());
        UpdateUI();
    }
    public void NextLevel() {
        ++level;
        if (hp < hpVals[level]) {
            hp = hpVals[level];
        }
        PlayerPrefs.SetInt("level", level);
        UpdateUI();
    }
    public void AddHp(int hpToAdd) {
        hp += hpToAdd;
        PlayerPrefs.SetInt("hp", hp);
        if (hp >= hpVals[level + 1]) {
            NextLevel();
            AddHp(0);
        }
        UpdateUI();
    }
    void UpdateUI() {
        sl.value = ((float)(hp - hpVals[level])) / (hpVals[level + 1] - hpVals[level]);
        hpOld.SetText(hpVals[level].ToString());
        hpCur.SetText(hp.ToString());
        hpNext.SetText(hpVals[level + 1].ToString());
        lvlOld.SetText(level.ToString());
        lvlNext.SetText((level + 1).ToString());

        long time_since_feed = TimeSinceFeed();
        if (time_since_feed <= 0) {
            feedText.SetText("Feed toad");
            feedText.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        } else {
            TimeSpan elapsedSpan = new TimeSpan(TimeSinceFeed());
            feedText.SetText((elapsedSpan.Hours) + ":" + (elapsedSpan.Minutes) + ":" + (elapsedSpan.Seconds));
            feedText.color = new Color(0.6603774f, 0.6603774f, 0.6603774f);

        }
    }
    public long TimeSinceFeed() {
        DateTime end = DateTime.Parse(PlayerPrefs.GetString("last_feed")).AddSeconds(5);
        DateTime now = DateTime.Now;

        long elapsedTicks = end.Ticks - now.Ticks;
        TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);

        return elapsedTicks;
    }
    public void FeedToad() {
        if (TimeSinceFeed() <= 0) {
            PlayerPrefs.SetString("last_feed", DateTime.Now.ToString());
            AddHp(100);
        } else {
            return;
        }
    }
    private void Update() {
        UpdateUI();
    }
}
