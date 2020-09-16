﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [Header("main")]
        public Transform target;
        public int finalScore;
        public int score;
        public int highScore;
        public int highDist;

    [Header("calculation")]
        public float distance;
        public float distanceFactor = 1f;
        public Vector3 startPos;

    [Header("UI")]
        public TMPro.TextMeshProUGUI scoreText;
        public TMPro.TextMeshProUGUI distanceText;
        public GameObject addScoreExample;
        public GameObject scoreDed;
        public List<GameObject> aaaa;

        public float scoreTimer;
        public float cleanScoreTime;

    void Start()
    {
        startPos = target.transform.position;
        highScore = PlayerPrefs.GetInt("HighScore");
        highDist = PlayerPrefs.GetInt("HighDist");
    }
    void Update()
    {
        if (scoreTimer > cleanScoreTime)
        {
            GameObject t = aaaa[0];
            aaaa.RemoveAt(0);
            Destroy(t);
            foreach (var i in aaaa)
            {
                i.GetComponent<RectTransform>().localPosition = new Vector2(0, i.GetComponent<RectTransform>().localPosition.y + 20);
            }
            scoreTimer = 0;
        }
        else if (aaaa.Count > 0)
        {
            scoreTimer += Time.deltaTime;
        }

        distance = GetDist();
        finalScore = GetDist() + score;

        scoreText.SetText(finalScore.ToString());

        distanceText.SetText("distance: " + Mathf.RoundToInt(distance).ToString());
    }

    public void RefreshHighScore()
    {
        if(highScore < finalScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        if (highDist < GetDist())
        {
            highDist = GetDist();
            PlayerPrefs.SetInt("HighDist", highDist);
        }
    }
    private void LateUpdate()
    {

    }
    public int GetDist(Transform t = null)
    {
        if(t == null)
        {
            t = target.transform;
        }
        return Mathf.RoundToInt((t.position - startPos).magnitude * distanceFactor);
    }
    public void AddScore(int i, string comment = "")
    {
        score += i;

        //i = Random.Range(0, 54);
        GameObject tmp = Instantiate(addScoreExample);
        tmp.SetActive(true);

        tmp.GetComponent<TMPro.TextMeshProUGUI>().SetText("+ " + i.ToString() + (comment == "" ? "" : " (" + comment + ")"));

        tmp.transform.SetParent(scoreDed.transform);
        tmp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);


        if (aaaa.Count > 0)
        {
            tmp.GetComponent<RectTransform>().localPosition = new Vector2(0, aaaa[aaaa.Count - 1].GetComponent<RectTransform>().localPosition.y - 20);
        }
        else
        {
            tmp.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        }

        aaaa.Add(tmp);
    }
    public void ClearTable()
    {
        for (int j = 0; j < aaaa.Count; j++)
        {
            GameObject i = aaaa[j];
            aaaa.Remove(i);
            Destroy(i);
        }
    }
}
