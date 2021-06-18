using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoDotsClass : MonoBehaviour
{
    // public GameObject dot1, dot2, dot3, ting1, ting2;
    // public float cos, sin, angle, thickness;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // twoDotsFunction(dot1, dot2, ting1, thickness);
        // twoDotsFunction(dot2, dot3, ting2, thickness);
    }

    public static void twoDots(GameObject dot1, GameObject dot2, GameObject ting, float thickness, float flipRatio)
    {
        Vector3 dot1v = dot1.transform.position;
        Vector3 dot2v = dot2.transform.position;
        Vector3 dot3v = new Vector3(dot2v.x, dot1v.y, 0f);

        if(dot1v == dot2v)
            return;

        // dot3.transform.position = dot3v;

        float dist1n2 = Vector3Distance(dot1v, dot2v);

        float cos = Vector3.Distance(dot1v, dot3v)/dist1n2;
        float sin = Vector3.Distance(dot2v, dot3v)/dist1n2;
        float angle = acos(cos);
        float properAngle = 0f;

        properAngle = angle;

        if(dot2v.x < dot1v.x) { // if dot 2 is on dot 1's right
            cos = -cos;
            angle = -angle;

            properAngle = 180f+angle;

            if(dot2v.y < dot1v.y) { // if dot 2 is on dot 1's right and dot 2 is bellow dot 1
                sin = -sin;
                angle = -angle;

                properAngle = 180f+angle;
            }
        }
        else { // if dot 2 is on dot 1's left
            if(dot2v.y < dot1v.y) { // if dot 2 is on dot 1's left and dot 2 is bellow dot 1
                sin = -sin;
                angle = -angle;
                properAngle = 360f+angle;
            }
        }

        bool yes = false;

        

        for(int i = 0; i < flipRatio+1; i++)
        {
            if(Mathf.Round(properAngle) > 360/flipRatio*i && Mathf.Round(properAngle) < 360/flipRatio*(i+1))
                yes = true;
        }

        if(!yes)
        {
            ting.transform.position = new Vector3(-(dist1n2/2)*cos+dot2v.x, -(dist1n2/2)*sin+dot2v.y, 0f); // the hard thing
            Vector3 tingS = ting.transform.localScale;
            ting.transform.localScale = new Vector3(dist1n2-thickness, tingS.y, tingS.z);
            ting.transform.eulerAngles = new Vector3(0f, 0f, angle);
        }
    }

    public static float Vector3Distance(Vector3 v1, Vector3 v2)
    {
        return Mathf.Sqrt(Mathf.Pow((v2.x-v1.x), 2)+Mathf.Pow((v2.y-v1.y), 2));
    }

    public static float acos(float cos)
    {
        float rad = 180/Mathf.PI;
        return Mathf.Acos(cos)*rad;
    }

    public static void print(float f)
    {
        Debug.Log(f);
    }
}
