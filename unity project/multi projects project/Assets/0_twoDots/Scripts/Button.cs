using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    GameManager gameManager;
    public bool camMovement, ctrl;
    public string dir;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
            gameManager.ctrl = true;
        if(Input.GetKeyUp(KeyCode.LeftControl))
            gameManager.ctrl = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(camMovement && !ctrl)
            gameManager.Arrows(dir);
        else if(!camMovement && !ctrl)
            gameManager.Mesure(dir);
        else if(ctrl)
            gameManager.ctrl = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(camMovement && !ctrl)
            gameManager.arrowsFloat = 0f;
        else if(ctrl)
            gameManager.ctrl = false;
    }
}
