using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    public Sprite mouseTextureIdle; 
    public Sprite mouseTextureClicking;
    public GameObject stamp;

    private SpriteRenderer mouseRenderer;
    
    public Vector2 newMousePos, cursorPos,oldMousePos;

    private float xBorder = 5.25f, yBorder = 3.0f;

    private float cursorSpeed = -0.2f;

    public GameObject documento;

    private bool stampMode = false;

    private int mouseMode = 0;

    void Start()
    {
        Cursor.SetCursor(null, new Vector2(4.5f, 0), CursorMode.Auto);
        Cursor.visible = false;
        mouseRenderer = this.GetComponent<SpriteRenderer>();
        mouseRenderer.sprite = mouseTextureIdle;
    }

    public void SetMouseMovement(int mode)
    {
        mouseMode = mode;
    }

    void Update()
    {

        switch (mouseMode)
        {
            case 0:
                FreeCursorMovement();
                break;
            case 1:
                RightSideCursorMovement();
                break;
            case 2:
                LeftSideCursorMovement();
                break;
            case 3:
                CursorMovementRightRestricted();
                break;
            case 4:
                ScrollCursorMovement();
                break;
            default:
                break;
        }

        //Inputs
        if(Input.GetMouseButtonDown(0))
        {
            if(stampMode)
            {
                GameObject newStamp = Instantiate(stamp,cursorPos,Quaternion.identity);
                newStamp.transform.parent = documento.transform;
            }
            else
            {
                mouseRenderer.sprite = mouseTextureClicking;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            documento.SetActive(!documento.activeSelf);
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            mouseRenderer.sprite = mouseTextureIdle;
        }    
    }

    void RightSideCursorMovement()
    {

        newMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        cursorPos = newMousePos;

        cursorPos.y = Mathf.Clamp(cursorPos.y, -yBorder, yBorder);
        cursorPos.x = Mathf.Clamp(cursorPos.x, 0, xBorder);

        transform.position = cursorPos;

    }

    void LeftSideCursorMovement()
    {

        newMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        cursorPos = newMousePos;

        cursorPos.y = Mathf.Clamp(cursorPos.y, -yBorder, yBorder);
        cursorPos.x = Mathf.Clamp(cursorPos.x, -xBorder, 0);

        transform.position = cursorPos;

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
