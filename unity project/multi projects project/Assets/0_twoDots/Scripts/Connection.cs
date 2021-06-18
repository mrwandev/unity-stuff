using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour
{
    GameManager gameManager;
    // [HideInInspector]
    public GameObject prevDot, currDot, cube;

    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(!gameManager.loadedLines.Contains(this.gameObject.name) && !gameManager.customConn)
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
        else
            gameManager.customConn = false;

        if(gameManager.cubes.Count > 0)
            cube = gameManager.cubes[System.Convert.ToInt32(this.gameObject.name.Replace("line", ""))-1];
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.dots.Count > 1 && prevDot != null && currDot != null)
        {
            twoDotsClass.twoDots(prevDot, currDot, this.gameObject, .9f, gameManager.rotationFractions);
            // cube.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -cube.transform.localScale.z/2);
            // cube.transform.eulerAngles = new Vector3(cube.transform.eulerAngles.x, cube.transform.eulerAngles.y, this.gameObject.transform.eulerAngles.z);
            // cube.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, cube.transform.localScale.y, 5f);
        }
    }
}
