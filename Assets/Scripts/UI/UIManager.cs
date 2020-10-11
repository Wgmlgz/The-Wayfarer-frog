﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public ZhabaController ZH;
    public GameObject mainUI;
    public GameObject gameUI;
    public GameObject characterSelectionUI;
    public GameObject deathUI;

    public GameObject camIdle;
    public GameObject camCharacterSelection;

    public GameObject[] hearts;
    public int acitveHeart;

    private float hpVisibleTime = 4f;
    private int maxHp;
    public float lastHpActive;
    public TMPro.TextMeshProUGUI redText;
    public Slider speedVis;
    private void Start()
    {
    }
    public void ToGame()
    {
        GetComponent<Animation>().Play();
        deathUI.SetActive(false);
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

    public void SetupHearts(int maxHearts)
    {
        maxHp = maxHearts;
        acitveHeart = maxHearts - 1;
        ShowHp();
    }

    public void ShowHp()
    {
        lastHpActive = hpVisibleTime;
        for (int i = 0; i < 15; ++i)
        {
            hearts[i].SetActive(i <= acitveHeart + 1 && i < maxHp);
        }
    }

    public void HideHp()
    {
        for (int i = 0; i < 15; ++i)
        {
            hearts[i].SetActive(false);
        }
    }
    public void RemoveHeart()
    {
        if (acitveHeart >= 0)
        {
            hearts[acitveHeart].GetComponent<Animation>().Play();
            acitveHeart--;
        }

        ShowHp();
    }
    private void Update()
    {
        lastHpActive -= Time.deltaTime;
        if (lastHpActive < 0f)
        {
            HideHp();
        }

        speedVis.value = (float)(ZH.curSpeed - ZH.minSpeed) / (ZH.maxSpeed - ZH.minSpeed);
    }

    public void SetRedText (string s) {
        redText.gameObject.SetActive(true);
        redText.SetText(s);
    }

    public void HedeRedText () {
        redText.gameObject.SetActive(false);
    }
}