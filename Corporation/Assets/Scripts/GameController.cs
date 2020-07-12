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
    public DialogoTrigger[] dialogos;
    private GameObject documento;
    public Sprite[] stamps;
    public Dictionary<string,bool> condicoes;
    private bool loopMenu = true, loopLevel1 = true;
    public SpriteRenderer backgroundFloor;
    public Sprite[] floors;

    public int difficulty = 0;

    private float timeLeft = 120f;

    void Start()
    {
        documento = cursor.documento;
        StartCoroutine(MusicLoopOrganizer());
        condicoes = new Dictionary<string, bool>();
        condicoes.Add("andar5",false);
        condicoes.Add("andar8", false);
        condicoes.Add("andar3", false);
        condicoes.Add("andar7", false);
        condicoes.Add("andar4", false);
        condicoes.Add("andar2", false);
        condicoes.Add("andar9", false);
        condicoes.Add("andar10", false);
        condicoes.Add("andar1", false);
        condicoes.Add("aux", false);
        condicoes.Add("stampMode", false);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(condicoes["stampMode"] == false)
            {
                //var audio = soundManager.PlaySound(SoundManager.Sound.MouseClick);
            }
            else
            {
                //AUDIO CARIMBADA
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (condicoes["stampMode"] == false && condicoes["aux"] == false)
                documento.SetActive(!documento.activeSelf);
        }

        timeLeft -= Time.deltaTime;


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

    IEnumerator FloorTransition(int destination)
    {
        FecharPortas();
        yield return new WaitForSeconds(0.5f);
        int current =  int.Parse(floorIndicator.text);
        backgroundFloor.sprite = floors[destination-1];
        while (current != destination)
        {
            if(current > destination)
            {
                current--;
            }
            else
            {
                current++;
            }
            floorIndicator.text = current.ToString();
            yield return new WaitForSeconds(0.5f);
        }
        var audioSource = soundManager.PlaySound(SoundManager.Sound.FloorJingle);
        yield return new WaitWhile(() => audioSource.isPlaying);
        rDoorAnimator.SetBool("PortaAberta", true);
        lDoorAnimator.SetBool("PortaAberta", true);
        yield return new WaitForSeconds(0.5f);
        Destroy(audioSource);
        EnableButtonInteraction();
    }
    public void BeginGame()
    {
        menuAnimator.SetBool("GameHasBegun", true);
        StartCoroutine(TriggerDialogue(1));
        StartCoroutine(TransitionBetweenMusic());
        condicoes["andar5"] = true;
    }


    IEnumerator TriggerDialogue(int index)
    {
        if(index == 6)
        {
            yield return StartCoroutine(FloorTransition(1));
            dialogos[6].TriggerDialogue(6);
        }
        else
        {
            yield return StartCoroutine(FloorTransition(index));
            dialogos[index - 1].TriggerDialogue(index-1);
        }

    }

    IEnumerator NewStamp(int index)
    {
        yield return StartCoroutine(TriggerDialogue(index));
        condicoes["aux"] = true;
        yield return new WaitUntil(() => condicoes["aux"] == false);
        condicoes["stampMode"] = true;
        cursor.StampMode(stamps[index-1]);
        yield return new WaitUntil(() => condicoes["stampMode"] == false);
        IncreaseDifficulty();
    }

    private void IncreaseDifficulty()
    {
        difficulty++;
        switch (difficulty)
        {
            case 1:
                cursor.SetMouseMovement(3);
                break;
            case 2:
                cursor.SetMouseMovement(4);
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            default:
                break;
        }
    }

    public void ChangeCondition(string name, bool value)
    {
        condicoes[name] = value;
    }

    public void ButtonClick(int buttonNumber)
    {
        DisableButtonInteraction();
        var audio = soundManager.PlaySound(SoundManager.Sound.ButtonClick);

        switch (buttonNumber)
        {
            case 0:
                if (!menuAnimator.GetBool("GameHasBegun"))
                {
                    BeginGame();
                }
                else if(condicoes["andar1"])
                {
                    StartCoroutine(TriggerDialogue(6));
                }
                else
                {
                    StartCoroutine(FloorTransition(buttonNumber +1));
                }
                break;

            case 1:
                if (condicoes["andar2"])
                {
                    StartCoroutine(NewStamp(buttonNumber + 1));
                    condicoes["andar2"] = false;
                    condicoes["andar9"] = true;

                }
                else
                {
                    StartCoroutine(FloorTransition(buttonNumber + 1));
                }
                break;


            case 2:
                if (condicoes["andar3"])
                {
                    StartCoroutine(NewStamp(buttonNumber + 1));
                    condicoes["andar3"] = false;
                    condicoes["andar7"] = true;

                }
                else
                {
                    StartCoroutine(FloorTransition(buttonNumber + 1));
                }

                break;


            case 3:
                if (condicoes["andar4"])
                {
                    StartCoroutine(FloorTransition(buttonNumber + 1));
                    condicoes["andar4"] = false;
                    condicoes["andar2"] = true;

                }
                else
                {
                    StartCoroutine(FloorTransition(buttonNumber + 1));
                }
                break;

            case 4:
                if (condicoes["andar5"])
                {
                    StartCoroutine(NewStamp(buttonNumber + 1));
                    condicoes["andar5"] = false;
                    condicoes["andar8"] = true;
                }
                else
                {
                    StartCoroutine(FloorTransition(buttonNumber + 1));
                }
                break;

            case 5:
                StartCoroutine(FloorTransition(buttonNumber + 1));
                break;

            case 6:
                if (condicoes["andar7"])
                {
                    StartCoroutine(NewStamp(buttonNumber + 1));
                    condicoes["andar7"] = false;
                    condicoes["andar4"] = true;

                }
                else
                {
                    StartCoroutine(FloorTransition(buttonNumber + 1));
                }
                break;

            case 7:
                if (condicoes["andar8"])
                {
                    StartCoroutine(NewStamp(buttonNumber + 1));
                    condicoes["andar8"] = false;
                    condicoes["andar3"] = true;
                }
                else
                {
                    StartCoroutine(FloorTransition(buttonNumber + 1));
                }
                break;

            case 8:
                if (condicoes["andar9"])
                {
                    StartCoroutine(NewStamp(buttonNumber + 1));
                    condicoes["andar9"] = false;
                    condicoes["andar10"] = true;

                }
                else
                {
                    StartCoroutine(FloorTransition(buttonNumber + 1));
                }
                break;

            case 9:
                if (condicoes["andar10"])
                {
                    StartCoroutine(NewStamp(buttonNumber + 1));
                    condicoes["andar10"] = false;
                    condicoes["andar1"] = true;

                }
                else
                {
                    StartCoroutine(FloorTransition(buttonNumber + 1));
                }
                break;

            default:
                break;
        }

    }

    public void DisableButtonInteraction()
    {
        foreach (Button item in elevatorButton)
        {
            item.enabled = false;
        }
    }

    public void EnableButtonInteraction()
    {
        foreach (Button item in elevatorButton)
        {
            item.enabled = true;
        }
        condicoes["aux"] = false;
    }


    public void FecharPortas()
    {
        rDoorAnimator.SetBool("PortaAberta", false);
        lDoorAnimator.SetBool("PortaAberta", false);
    }
}
