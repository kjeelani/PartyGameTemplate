using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioClip drumroll;
    public static AudioSource audioSrc;
    private void Start() {
        drumroll = Resources.Load<AudioClip>("drumroll");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip) {
        switch (clip) {
            case "drumroll":
                audioSrc.PlayOneShot(drumroll);
                break;
        }
    }
}