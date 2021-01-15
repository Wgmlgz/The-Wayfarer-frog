using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapU : MonoBehaviour {
    public City cur_city;
    public City target_city;
    public TMPro.TextMeshProUGUI distance_text;
    public RectTransform map;
    public float cur_city_offset;

    public void changeCargo() {
        PlayerPrefs.SetString("CurrentCity", cur_city.gameObject.name);
        ScenesManager.toCargoSt();
    }
    void LateUpdate() {
        if (cur_city == target_city) {
            var t = FindObjectsOfType<City>();
            int index = 0;
            for (int i = 0; i < t.Length; ++i)
                if (t[i] == target_city)
                        index = i;
            int tt = Random.Range(0, t.Length);
            while (tt == index) tt = Random.Range(0, t.Length);
            t[tt].targetSelf();
        }
        if (cur_city != null) {
            Vector2 city_pos = cur_city.gameObject.GetComponent<RectTransform>().localPosition;
            GetComponent<RectTransform>().localPosition = new Vector3(city_pos.x, city_pos.y - cur_city_offset, 0);
        }
    }
}
