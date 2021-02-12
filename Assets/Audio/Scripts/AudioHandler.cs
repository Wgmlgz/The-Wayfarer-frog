using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AudioHandler : MonoBehaviour {
    AudioManager.AudioManager am;
    public UnityEvent on_load;
    private void Start() {
        am = AudioManager.AudioManager.m_instance;
        on_load.Invoke();
    }
    public void playSfx(string s) {
        am.PlaySFX(s);
    }
    public void playMusic(string s) {
        if(am.m_currentMusic == null) am.PlayMusic(s);
    }
}
