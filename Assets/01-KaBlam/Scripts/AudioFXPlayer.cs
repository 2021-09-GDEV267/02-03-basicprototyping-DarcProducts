using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFXPlayer : MonoBehaviour
{
    [SerializeField] List<AudioClip> audioFXList = new List<AudioClip>(0);
    [SerializeField] AudioSource fXSource;

    public void PlayAudioEffect(int index)
    {
        if (fXSource == null || index > audioFXList.Count)
            return;
        if (audioFXList[index] != null)
            fXSource.PlayOneShot(audioFXList[index]);
    }
}
