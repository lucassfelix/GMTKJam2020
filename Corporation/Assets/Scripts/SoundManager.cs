using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager: MonoBehaviour
{
    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }
    public SoundAudioClip[] effectsArray;

    public enum Sound {
        ElevatorMovement,
        DoorOpening,
        DoorClosing,
        FloorJingle,
        Speaking,
        MenuPercussion,
        MenuLoop,
        MenuTransitionLevel1,
        Level1Loop
    }

    public AudioSource PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
        return audioSource;
    }

    public AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAudioClip item in effectsArray)
        {
            if(item.sound == sound)
                return item.audioClip;   
        }
        Debug.Log("Error");
        return null;
    }

}
