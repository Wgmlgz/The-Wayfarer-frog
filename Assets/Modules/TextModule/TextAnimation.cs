using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManager;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] public float charTime = 0.05f;
    [SerializeField] public string charSound = "DefaultCharSound";
    [SerializeField] public int charsToSound = 15;
    private string text;

    private float animTime = 0f;
    public bool anim = false;
    public bool start = false;
    private int lastSound = 0;

    public void StartAnimation(string newText = null, float newCharTime = 0.02f, string sound = "DefaultCharSound")
    {
        charTime = newCharTime;
        charSound = sound;
        animTime = 0f;
        lastSound = 0;
        if (newText == null)
        {
            text = this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().text;
        }
        else
        {
            text = newText;
        }
        anim = true;
        //AudioManager.AudioManager.m_instance.PlaySFX(charSound);
    }

    void SetText(string text)
    {
        this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().SetText(text);
    }

    private void Start()
    {
        StartAnimation(null, 0.005f);
    }
    void Update()
    {
        if (anim)
        {
            animTime += Time.deltaTime;

            int charsToShow = Mathf.RoundToInt(animTime / charTime);
            if (charsToShow > text.Length)
            {
                anim = false;
                SetText(text);
            }
            else
            {
                string showText = text.Substring(0, charsToShow);
                if (charsToShow - lastSound >= charsToSound)
                {
                    //AudioManager.AudioManager.m_instance.PlaySFX(charSound);
                    lastSound = charsToShow;
                }
                SetText(showText);
            }
        }
    }
}
