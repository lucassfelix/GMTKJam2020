using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CursorController : MonoBehaviour
{

    public Sprite mouseTextureIdle; 
    public Sprite mouseTextureClicking;
    public GameObject stamp;

    private SpriteRenderer mouseRenderer;

    private Vector2 newMousePos, cursorPos,oldMousePos;

    private float xBorder = 5.25f, yBorder = 3.0f;

    private float cursorSpeed = -0.2f;

    public GameObject documento;
    private bool stampMode = false;

    public GameController gameController;
    private int mouseMode = 0;

    void Start()
    {
        Cursor.SetCursor(null, new Vector2(4.5f, 0), CursorMode.Auto);
        Cursor.visible = false;
        mouseRenderer = this.GetComponent<SpriteRenderer>();
        mouseRenderer.sprite = mouseTextureIdle;
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(Input.GetMouseButtonDown(0))
        {
            var pointer = new PointerEventData(EventSystem.current);
            var butt = other.transform.GetComponent<Button>();
            if(butt.enabled)
            {
                ExecuteEvents.Execute(butt.gameObject, pointer, ExecuteEvents.pointerDownHandler);
                ExecuteEvents.Execute(butt.gameObject, pointer, ExecuteEvents.pointerUpHandler);
            }
        }
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
            case 5:
                SlowFlappy();
                break;
            default:
                break;
        }

        //Inputs
        if(Input.GetMouseButtonDown(0))
        {
            if(stampMode)
            {
                GameObject newStamp = new GameObject("Stamp");
                newStamp.transform.position = cursorPos;
                var aux = newStamp.AddComponent<SpriteRenderer>();
                aux.sprite = mouseRenderer.sprite;
                newStamp.transform.parent = documento.transform;


                stampMode = false;
                mouseRenderer.sprite = mouseTextureIdle;
                gameController.ChangeCondition("stampMode",false);
            }
            else
            {
                mouseRenderer.sprite = mouseTextureClicking;
            }
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            if(!stampMode)
            {
                mouseRenderer.sprite = mouseTextureIdle;
            }
            else
            {

            }
        }    
    }

    public void StampMode(Sprite stampSprite)
    {
        documento.SetActive(true);
        stampMode = true;
        mouseRenderer.sprite = stampSprite;
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

    private void SlowFlappy()
    {
        newMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        cursorPos = new Vector2(cursorPos.x + cursorSpeed, 0);

        cursorPos.y = Mathf.Clamp(cursorPos.y, -yBorder, yBorder);
        cursorPos.x = Mathf.Clamp(cursorPos.x, -xBorder, xBorder);

        if (cursorPos.x == -xBorder)
            cursorPos.x = xBorder;

        transform.position = cursorPos;
    }

    
}
