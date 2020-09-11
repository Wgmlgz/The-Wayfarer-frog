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
        public TMPro.TextMeshProUGUI indicator;
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
        coins = i;
        ChangeCoins();
    }
    public void AddCoins(int i = 1)
    {
        coins += i * coinMult;
        ChangeCoins();
    }
    public void ChangeCoins()
    {
        PlayerPrefs.SetInt("Coins_" + id.ToString(), coins);
        indicator.SetText(coins.ToString());
        timeLeft = timeLeftMax;
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
