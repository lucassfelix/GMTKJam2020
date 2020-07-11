using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private Texture2D mouseTextureIdle;
  
    private Texture2D mouseTextureClicking;

  
    void Awake()
    {
        mouseTextureIdle = Resources.Load("CursorIdle") as Texture2D;

        mouseTextureClicking = Resources.Load("CursorClick") as Texture2D;
    }
    void Start()
    {
        Cursor.SetCursor(mouseTextureIdle, new Vector2(4.5f,0), CursorMode.Auto);
    }

    void Update()
    {


        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;

        if(Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(mouseTextureClicking, new Vector2(4.5f, 0), CursorMode.Auto);
            Debug.Log(mousePos.ToString());
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(mouseTextureIdle, new Vector2(4.5f, 0), CursorMode.Auto);
        }    

    }

    
}
