using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour
{
	GameManager gameManager;
	// [HideInInspector]
	public GameObject prevDot, currDot;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(!gameManager.loadedLines.Contains(this.gameObject.name))
        {
            if(gameManager.holdingAcurr())
            {
                prevDot = gameManager.prevDot;
                currDot = gameManager.currDot;
            }
            else
            {
                prevDot = gameManager.currDot;
                currDot = gameManager.prevDot;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
    	if(gameManager.dots.Count > 1 && prevDot != null && currDot != null)
    		twoDotsClass.twoDots(prevDot, currDot, this.gameObject, .9f, gameManager.rotationFractions);
    }
}
