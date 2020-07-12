using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogoTrigger: MonoBehaviour
{

    public Dialogo dialogue;

    public void TriggerDialogue(int index)
    {
        FindObjectOfType<DialogoManager>().StartDialogue(dialogue,index);
    }
}