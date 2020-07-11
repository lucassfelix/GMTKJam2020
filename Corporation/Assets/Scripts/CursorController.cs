using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    public Sprite mouseTextureIdle; 
    public Sprite mouseTextureClicking;

    private SpriteRenderer mouseRenderer;
    
    private Vector2 newMousePos, cursorPos,oldMousePos;

    void Start()
    {
        Cursor.SetCursor(null, new Vector2(4.5f, 0), CursorMode.Auto);
        Cursor.visible = false;
        mouseRenderer = this.GetComponent<SpriteRenderer>();
        mouseRenderer.sprite = mouseTextureIdle;
    }

    void Update()
    {
        CursorMovement();


        //Inputs
        if(Input.GetMouseButtonDown(0))
        {
            mouseRenderer.sprite = mouseTextureClicking;
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            mouseRenderer.sprite = mouseTextureIdle;
        }    

        

    }

    void CursorMovement()
    {
        newMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var deltaMousePos = oldMousePos - newMousePos;

        cursorPos = cursorPos - deltaMousePos;
        
        cursorPos.x = Mathf.Clamp(cursorPos.x,-5, Mathf.Min(cursorPos.x, cursorPos.x + deltaMousePos.x) );

        transform.position = cursorPos;

        oldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    
}
