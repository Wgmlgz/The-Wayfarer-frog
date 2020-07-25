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

    [Header("Events")]
        public UnityEvent onCoinsValueChanged;

    private void Awake()
    {
        if (storeCoins)
        {
            coins = PlayerPrefs.GetInt("Coins_" + id.ToString());
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
        coins += i;
        ChangeCoins();
    }
    public void ChangeCoins()
    {
        PlayerPrefs.SetInt("Coins_" + id.ToString(), coins);
        onCoinsValueChanged.Invoke();
    }
}
