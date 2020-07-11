using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    public Sprite mouseTextureIdle; 
    public Sprite mouseTextureClicking;

    private SpriteRenderer mouseRenderer;
    
    public Vector2 newMousePos, cursorPos,oldMousePos;

    private float xBorder = 5.25f, yBorder = 3.0f;

    private float cursorSpeed = -0.2f;

    void Start()
    {
        Cursor.SetCursor(null, new Vector2(4.5f, 0), CursorMode.Auto);
        Cursor.visible = false;
        mouseRenderer = this.GetComponent<SpriteRenderer>();
        mouseRenderer.sprite = mouseTextureIdle;
    }

    void Update()
    {
        FreeCursorMovement();

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

    void FreeCursorMovement()
    {

        newMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        cursorPos = newMousePos;

        cursorPos.y = Mathf.Clamp(cursorPos.y, -yBorder, yBorder);
        cursorPos.x = Mathf.Clamp(cursorPos.x, -xBorder, xBorder);

        transform.position = cursorPos;

    }

    void ScrollCursorMovement()
    {
        newMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        cursorPos = new Vector2(cursorPos.x + cursorSpeed,newMousePos.y);

        cursorPos.y = Mathf.Clamp(cursorPos.y, -yBorder, yBorder);
        cursorPos.x = Mathf.Clamp(cursorPos.x, -xBorder, xBorder);

        if (cursorPos.x == -xBorder)
            cursorPos.x = xBorder;

        transform.position = cursorPos;
    }

    void CursorMovementRightRestricted()
    {
        newMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var deltaMousePos = oldMousePos - newMousePos;
        cursorPos = cursorPos - deltaMousePos;

        cursorPos.y = Mathf.Clamp(cursorPos.y, -yBorder, yBorder);
        cursorPos.x = Mathf.Clamp(cursorPos.x,-xBorder, Mathf.Min(cursorPos.x, cursorPos.x + deltaMousePos.x) );
        
    
        if(cursorPos.x == -xBorder)
            cursorPos.x = xBorder;

        transform.position = cursorPos;

        oldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    
}
