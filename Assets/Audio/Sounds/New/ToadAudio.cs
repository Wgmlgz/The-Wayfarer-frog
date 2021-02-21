using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToadAudio : MonoBehaviour {
    public ZhabaController zh;
    public AudioSource sand;
    public AudioSource wind;
    public AudioSource wind2;
    public AudioSource sand_drop;
    public AudioSource sand_up;
    public AudioSource death;
    public AudioSource coin;
    public AudioSource hit;
    private bool last_clip_mode;
    float vol;
    // Start is called before the first frame update
    void Start() {
        vol = PlayerPrefs.GetFloat("sfx_volume");
        sand.volume = vol;
        wind.volume = vol;
        wind2.volume = vol;
        sand_drop.volume = vol;
        sand_up.volume = vol;
        hit.volume = vol;
        coin.volume = vol;
        death.volume = vol;
        last_clip_mode = zh.clipMode;
    }

    public void hitHHp() {
        hit.Play();
    }
    void Update() {
        //sand.mute = !zh.clipMode;
        
        if (last_clip_mode != zh.clipMode) {
            if (zh.clipMode) {
                sand_drop.Play();
                sand.Play();

            } else {
                sand_up.Play();
                sand.Stop();
            }
        }
        if (zh.clipMode) {
            float tar = Mathf.Pow((zh.curSpeed - zh.minSpeed) / (zh.maxXSpeed - zh.minSpeed), 0.6f) * vol;
            if (zh.gameObject.GetComponent<Rigidbody2D>().gravityScale.Equals(2f)) tar = 1 * vol;
            wind.volume = Mathf.Lerp(wind.volume, tar, Time.deltaTime * 2);
            wind2.volume = wind.volume;
        } else if (zh.gameObject.GetComponent<Rigidbody2D>().gravityScale.Equals(2f)) {
            float tar = 1 * vol;
            wind.volume = Mathf.Lerp(wind.volume, tar, Time.deltaTime * 2);
            wind2.volume = wind.volume;

        }

        last_clip_mode = zh.clipMode;
    }
}
