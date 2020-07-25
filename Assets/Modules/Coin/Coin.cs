using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
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
        //AudioManager.AudioManager.m_instance.PlaySFX(1);

        this.gameObject.AddComponent(typeof(Rigidbody2D));
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
        if (collision.GetComponent<CoinCollector>() != null)
        {

            collected = true;
            collision.GetComponent<CoinCollector>().AddCoins();

            Collect();
            Invoke("VihodVOkno", 5);
        }
    }
}
