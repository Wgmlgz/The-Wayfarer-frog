using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject gameUI;
    public GameObject characterSelectionUI;
    public GameObject deathUI;

    public GameObject camIdle;
    public GameObject camCharacterSelection;

    private void Start()
    {
    }
    public void ToGame()
    {
        GetComponent<Animation>().Play();
    }
    public void ToCharacterSelect()
    {
        camIdle.gameObject.SetActive(false);
        camCharacterSelection.gameObject.SetActive(true);
    }
    public void FromCharacterSelect()
    {
        camIdle.gameObject.SetActive(true);
        camCharacterSelection.gameObject.SetActive(false);
    }
    public void ToDeath()
    {
        deathUI.SetActive(true);
    }
}
