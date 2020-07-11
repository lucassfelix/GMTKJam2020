using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    [SerializeField]
    private Texture2D mouseTexture;

    void Start()
    {
        Cursor.SetCursor(mouseTexture,Vector2.zero, CursorMode.Auto);
    }

    
}
