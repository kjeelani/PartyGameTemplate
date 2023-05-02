using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioSource audioSrc;

    private void Awake() {
        audioSrc = gameObject.GetComponent<AudioSource>();
    }
    // private void Awake() {
    //     drumroll = Resources.Load<AudioClip>("drumroll");
    //
    //     audioSrc = GetComponent<AudioSource>();
    //     PlaySound("drumroll");
    // }
    //
    // public static void PlaySound(string clip) {
    //     switch (clip) {
    //         case "drumroll":
    //             
    //             audioSrc.PlayOneShot(drumroll);
    //             break;
    //     }
    // }

    public static void playDrumroll(float starting) {
        if (starting < audioSrc.clip.length) {
            audioSrc.time = starting;
            audioSrc.Play();
        }
    }
}