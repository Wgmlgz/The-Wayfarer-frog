using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinCollector : MonoBehaviour
{
    public int coins;
    
    [Header("Storage")]
        // Coins_id
        public bool storeCoins;
        public int id;
    [Header("Main")]
        public int coinMult = 1;
    [Header("UI")]
        public TMPro.TextMeshProUGUI[] indicators;
        public float timeLeftMax = 5f;
    [Header("Events")]
        public UnityEvent onCoinsValueChanged;
        public UnityEvent onTimeLeft;

    private float timeLeft;
    private void Awake()
    {
        if (storeCoins)
        {
            coins = PlayerPrefs.GetInt("Coins_" + id.ToString());
            ChangeCoins();
        }
    }

    public int GetCoins()
    {
        return coins;
    }
    public void SetCoins(int i = 0)
    {
        ChangeCoins(i);
    }
    public void AddCoins(int i = 1)
    {
        if (i < 0) {
            ChangeCoins(i);
        } else {
            ChangeCoins(i * coinMult);
        }
    }
    public void ChangeCoins(int c = 0)
    {
        coins += c;
        PlayerPrefs.SetInt("Coins_" + id.ToString(), coins);
        
        foreach (var i in indicators)
        {
            i.SetText(coins.ToString());
        }
        
        timeLeft = timeLeftMax;
        if (Time.timeSinceLevelLoad < 1f) {
            timeLeft = -1f;
        }
        onCoinsValueChanged.Invoke();
    }
    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft < 0)
        {
            onTimeLeft.Invoke();
        }
    }
}
