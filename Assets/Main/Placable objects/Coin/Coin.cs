using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public int coins = 1;
    [SerializeField] Vector2 collectForce = new Vector2(0, 500f);
    private bool collected;

    public UnityEvent onCollect;
    void Collect()
    {
        //Vibration.VibratePop();
        /*PlayerPrefs.SetInt("coin_" +
            SceneManager.GetActiveScene().name +
            "_" + transform.position.x.ToString() +
            "_" + transform.position.y.ToString(),
            1);*/
        AudioManager.AudioManager.m_instance.PlaySFX("coin");

        gameObject.AddComponent(typeof(Rigidbody2D));
        this.GetComponent<Rigidbody2D>().simulated = true;
        this.GetComponent<Rigidbody2D>().gravityScale = 4f;
        this.GetComponent<Rigidbody2D>().AddRelativeForce(collectForce);
        onCollect.Invoke();
    }
    void VihodVOkno()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CoinCollector>() != null && collected == false)
        {

            collected = true;
            collision.GetComponent<CoinCollector>().AddCoins(coins);

            Collect();
            Invoke("VihodVOkno", 5);
        }
    }
}
