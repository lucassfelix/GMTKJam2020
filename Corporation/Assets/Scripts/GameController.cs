using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Animator menuAnimator;
    public Animator rDoorAnimator;
    public Animator lDoorAnimator;

    public Text floorIndicator;

    public CursorController cursor;

    public AudioSource musicSource;
 
    public SoundManager soundManager;

    public Button[] elevatorButton;

    public DialogoTrigger primeiroDialogo;

    private bool loopMenu = true, loopLevel1 = true;
    void Start()
    {
        StartCoroutine(MusicLoopOrganizer());
    }

    IEnumerator MusicLoopOrganizer()
    {
        musicSource.PlayOneShot(soundManager.GetAudioClip(SoundManager.Sound.MenuPercussion));
        yield return new WaitWhile(() => musicSource.isPlaying);
        musicSource.loop = false;
        while(loopMenu)
        {
            musicSource.PlayOneShot(soundManager.GetAudioClip(SoundManager.Sound.MenuLoop));
            yield return new WaitWhile(() => musicSource.isPlaying);
        }


    }

    IEnumerator TransitionBetweenMusic()
    {
        loopMenu = false;
        yield return new WaitWhile(() => musicSource.isPlaying);
        musicSource.PlayOneShot(soundManager.GetAudioClip(SoundManager.Sound.MenuTransitionLevel1));
        yield return new WaitWhile(() => musicSource.isPlaying);
        while (loopLevel1){
            musicSource.PlayOneShot(soundManager.GetAudioClip(SoundManager.Sound.Level1Loop));
            yield return new WaitWhile(() => musicSource.isPlaying);
        }

    }

    public void BeginGame()
    {
        menuAnimator.SetBool("GameHasBegun", true);
        floorIndicator.text = "1";
        StartCoroutine(WaitJingleToOpenDoors());
        StartCoroutine(TransitionBetweenMusic());
        elevatorButton[0].onClick = null;
    }

    IEnumerator WaitJingleToOpenDoors()
    {
        var audioSource = soundManager.PlaySound(SoundManager.Sound.FloorJingle);
        yield return new WaitWhile(() => audioSource.isPlaying);
        AbrirPortas();
        primeiroDialogo.TriggerDialogue();
    }

    public void AbrirPortas()
    {
        rDoorAnimator.SetBool("PortaAberta", true);
        lDoorAnimator.SetBool("PortaAberta" ,true);
    }

    void Update()
    {
        
    }
}
