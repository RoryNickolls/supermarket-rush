using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

    public static void PlayOnce(AudioClip clip)
    {
        GameObject tempAudio = new GameObject("AudioClip");
        AudioSource source = tempAudio.AddComponent<AudioSource>();
        source.PlayOneShot(clip);
        Destroy(tempAudio, clip.length);
    }
}
