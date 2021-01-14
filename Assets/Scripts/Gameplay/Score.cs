using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {
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
    public GameObject scoreBodyExample;
    public GameObject scoreDed;
    public List<GameObject> aaaa;

    public float scoreTimer;
    public float cleanScoreTime;

    float target_distance;

    void Start() {
        startPos = target.transform.position;
        highScore = PlayerPrefs.GetInt("HighScore");
        highDist = PlayerPrefs.GetInt("HighDist");
        target_distance = PlayerPrefs.GetInt("DistanceToTargetCity");
    }
    void Update() {
        if (scoreTimer > cleanScoreTime) {
            GameObject t = aaaa[0];
            aaaa.RemoveAt(0);
            Destroy(t);
            foreach (var i in aaaa) {
                i.GetComponent<RectTransform>().localPosition = new Vector2(0, i.GetComponent<RectTransform>().localPosition.y + 20);
            }
            scoreTimer = 0;
        } else if (aaaa.Count > 0) {
            scoreTimer += Time.deltaTime;
        }

        distance = GetDist();
        finalScore = GetDist() + score;

        scoreText.SetText(finalScore.ToString());
        distanceText.SetText("distance: " + Mathf.RoundToInt(distance).ToString() +
            " / " +
            target_distance.ToString());
        if (distance >= target_distance) {
            PlayerPrefs.SetString("CurrentCity", PlayerPrefs.GetString("TargetCity"));
            ScenesManager.toMapSt();
        }
    }

    public void RefreshHighScore() {
        if (highScore < finalScore) {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        if (highDist < GetDist()) {
            highDist = GetDist();
            PlayerPrefs.SetInt("HighDist", highDist);
        }
    }
    private void LateUpdate() {

    }
    public int GetDist(Transform t = null) {
        if (t == null) {
            t = target.transform;
        }
        return Mathf.RoundToInt((t.position - startPos).magnitude * distanceFactor);
    }
    public void AddScore(int i, string comment = "") {
        score += i;

        //i = Random.Range(0, 54);
        GameObject tmp = Instantiate(addScoreExample);
        tmp.SetActive(true);

        tmp.GetComponent<TMPro.TextMeshProUGUI>().SetText("+ " + i.ToString() + (comment == "" ? "" : " (" + comment + ")"));

        tmp.transform.SetParent(scoreDed.transform);
        tmp.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);


        if (aaaa.Count > 0) {
            tmp.GetComponent<RectTransform>().localPosition = new Vector2(0, aaaa[aaaa.Count - 1].GetComponent<RectTransform>().localPosition.y - 20);
        } else {
            tmp.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        }

        aaaa.Add(tmp);

        GameObject tmp1 = Instantiate(scoreBodyExample);
        tmp1.SetActive(true);
        tmp1.transform.rotation = Quaternion.Euler(0, 0, Random.Range(-20f, 20f));
        tmp1.transform.position = gameObject.transform.position;
        tmp1.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        tmp1.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(1500f, 1500f));
        tmp1.GetComponentInChildren<TMPro.TextMeshProUGUI>().GetComponent<TMPro.TextMeshProUGUI>().SetText("+ " + i.ToString());
    }
    public void ClearTable() {
        for (int j = 0; j < aaaa.Count; j++) {
            GameObject i = aaaa[j];
            aaaa.Remove(i);
            Destroy(i);
        }
    }
}
