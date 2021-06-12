using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	GameManager gameManager;
	public string dir;

    // Start is called before the first frame update
    void Start()
    {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
	{
	    gameManager.Arrows(dir);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
	    gameManager.arrowsFloat = 0f;
	}
}
