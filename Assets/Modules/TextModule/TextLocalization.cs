using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLocalization : MonoBehaviour
{
    [TextArea] public string EN;
    [TextArea] public string RU;

    private int language = 0;

    public void Refresh()
    {
        if (PlayerPrefs.HasKey("Language"))
            language = PlayerPrefs.GetInt("Language");
        else
            PlayerPrefs.SetInt("Language", 0);

        if (language == 1)
        {
            SetText(RU);
        }
        else
        {
            SetText(EN);
        }
    }
    void SetText(string text)
    {
        this.gameObject.GetComponent<TMPro.TextMeshProUGUI>().SetText(text);
    }
    void Start()
    {
        Refresh();
    }
}
