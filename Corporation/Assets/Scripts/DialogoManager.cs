using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogoManager : MonoBehaviour
{

    

    private Text nameText;
    private Text dialogueText;

    private Animator animator;

    public Animator[] animators;
    public Text[] nameTexts;
    public Text[] dialogueTexts;


    public GameController gameController;

    private Queue<string> sentences;

    // Use this for initialization
    void Start()
    {
        animator = animators[0];
        nameText = nameTexts[0];
        dialogueText = dialogueTexts[0];
        sentences = new Queue<string>();
    }

    void Update() 
    {
        if(Input.anyKeyDown && animator.GetBool("IsOpen"))
        {
            DisplayNextSentence();
        }    
    }
    public void StartDialogue(Dialogo dialogue, int index)
    {
        animator = animators[index];
        nameText = nameTexts[index];
        dialogueText = dialogueTexts[index];

        gameController.DisableButtonInteraction();
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        gameController.EnableButtonInteraction();
    }
}
