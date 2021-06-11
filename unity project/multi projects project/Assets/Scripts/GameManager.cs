using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
	// public Slider slider;
	public GameObject prevDot, currDot, square, holdedDot, holdedThing, dotPrefab, thingPrefab, textForThingPrefab, lastHoldedDot;
	public TextMeshProUGUI debugText, unit_T, deleteT_T;
	public TMP_InputField val_I, rT_I, unit_I, mesure_I; 
	public List<GameObject> dots, things, texts, dotsOrder;
	public bool hold, holding, moveB, yORx, reading, read;
	Vector3 pos, touchPos;
	public float timer, val, maxTimerVal, rotationFractions, unit, odrunit, shownUnit;

    // Start is called before the first frame update
    void Start()
    {
    	#if UNITY_ANDROID
	    	saveFile = "jar:file://" + Application.dataPath +"/data.txt";
	    #endif

	    #if UNITY_STANDALONE_WIN
	    	saveFile = Application.dataPath +"/data.txt";
	    #endif

	    #if UNITY_EDITOR
	    	saveFile = "Assets/data.txt";
	    #endif

    	// slider.maxValue = 50f;
    	// slider.minValue = 10f;
    	val = -Camera.main.transform.position.z;
    	rotationFractions = 0f;
    	unit = 1.09f;
    	shownUnit = 1f;

    	val_I.text = val.ToString();
    	rT_I.text = rotationFractions.ToString();
    	unit_I.text = shownUnit.ToString();

    	maxTimerVal = .3f;

    	if(read)
    		Read();
    }

    // Update is called once per frame
    void Update()
    {	
    	// if(dots.Count > 1)
    	// {
    	// 	for(int i = 0; i < dots.Count-1; i++)
    	// 		twoDotsClass.twoDots(dots[i], dots[i+1], things[i], .9f, rotationFractions);
    	// }
    	
    	unit_T.text = "1 unit = " + unit.ToString() + "m";

    	if(float.Parse(val_I.text) != 0f)
			val = float.Parse(val_I.text);

    	rotationFractions = float.Parse(rT_I.text);
    	unit = float.Parse(unit_I.text);
    	shownUnit = unit/1.09f;

    	if(Input.touchCount > 0 && timer < maxTimerVal && !hold || Input.touchCount > 0 && timer < maxTimerVal && !holding)
    		debugText.text = timer.ToString();
    	else
    		debugText.text = "nothing";

    	if(Input.touchCount == 1 && !hold || Input.touchCount == 1 && !holding || Input.GetKey(KeyCode.A) && !hold || Input.GetKey(KeyCode.A) && !holding)
    	{
    		timer += Time.deltaTime;
    		if(timer > maxTimerVal)
    			debugText.text = "move";
    	}
    	else
    		timer = 0f;

    	// if(Input.touchCount > 1)
    	// {
    		// if(Input.GetTouch(0).)
    		// {
    		// 	val = Vector3.Distance(,);
    		// }
     		// square.transform.position = pos;
    	// }

     	Vector3 mousePos = Input.mousePosition;
     	pos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -Camera.main.transform.position.z));

     	if(Input.touchCount > 0)
     	{
    		if(timer > maxTimerVal)
	    		moveB = true;
	    	else
	    		moveB = false;
     	}

     	if(moveB)
   			Camera.main.transform.position = new Vector3(pos.x*0.5f, pos.y*0.5f, -val);
   		else
    		Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -val);

    	// if(dots.Count > 1)
    	// {
    	// 	for(int i = 0; i < dots.Count-1; i++)
    	// 		twoDotsClass.twoDots(dots[i], dots[i+1], things[i], .9f, rotationFractions);
    	// }

     	if(dots.Count < 1)
     		return;

     	foreach(GameObject thing in things)
     	{
     		if(GameObject.Find("text"+thing.name.Replace("thing", "")) == null)
     		{
     			GameObject text = Instantiate(textForThingPrefab);
     			text.name = "text" + (dots.Count-1).ToString();
     			texts.Add(text);
     		}
     		else 
     		{
     			GameObject text = GameObject.Find("text"+thing.name.Replace("thing", ""));
     			text.transform.position = new Vector3(thing.transform.position.x, thing.transform.position.y, -0.2f);
     			text.transform.eulerAngles = new Vector3(0f, 0f, thing.transform.eulerAngles.z);
     			odrunit = ((thing.transform.localScale.x/unit)*10.9f)/10f;
     			text.GetComponent<TextMesh>().text = (odrunit/1.09f).ToString("F3");
     			if(thing == holdedThing)
     				deleteT_T.text = holdedThing.name + ":" + "\n" + (odrunit/1.09f).ToString("F3");
     		}
     	}

 		foreach(GameObject dot in dots)
     	{
     		if(closeTo(dot.transform.position, pos))
     			hold = true;
     		else
     			hold = false;

     		if(Input.GetKeyUp(KeyCode.Mouse0) || !hold)
     			dot.GetComponent<SpriteRenderer>().color = Color.white;

     		if(hold)
     		{
     			holdedDot = dot;
     			prevDot = dot;
     		}

     		if(Input.GetKeyUp(KeyCode.Mouse0))
     			holding = false;

     		if(hold && Input.GetKey(KeyCode.Mouse0))
     		{
     			lastHoldedDot = dot;
     			holding = true;
     		}

     		if(holding && holdedDot!=null)
     		{
     			holdedDot.transform.position = pos;
     			holdedDot.GetComponent<SpriteRenderer>().color = Color.red;
     			return;
     		}
     		else
     			holdedDot = null;

     		if(hold)
     			dot.GetComponent<SpriteRenderer>().color = Color.green;
     	}

     	foreach(GameObject thing in things)
     	{
     		if(closeTo(thing.transform.position, pos))
     			hold = true;
     		else
     			hold = false;

     		if(Input.GetKeyUp(KeyCode.Mouse0) || !hold)
     			thing.GetComponent<SpriteRenderer>().color = Color.white;

     		if(hold && Input.GetKey(KeyCode.Mouse0))
     			holdedThing = thing;

     		if(Input.GetKeyUp(KeyCode.Mouse0))
     			holding = false;

     		if(hold && Input.GetKey(KeyCode.Mouse0))
     			holding = true;

     		if(holding)
     		{
     			// holdedThing.transform.position = pos;
     			holdedThing.GetComponent<SpriteRenderer>().color = Color.red;
     			return;
     		}
     		// else
     		// 	holdedThing = null;

     		if(hold)
     			thing.GetComponent<SpriteRenderer>().color = Color.green;
     	}
    }

    bool closeTo(Vector3 v1, Vector3 v2)
    {
    	if(Mathf.Round(v2.x) == Mathf.Round(v1.x) && Mathf.Round(v2.y) == Mathf.Round(v1.y))
    		return true;
    	else
    		return false;
    }

    public void CreateDot()
    {
    	GameObject newDot = Instantiate(dotPrefab);
    	newDot.name = "dot"+ dots.Count;
    	dots.Add(newDot);

    	if(dots.Count == 1)
    		lastHoldedDot = newDot;

    	dotsOrder.Add(lastHoldedDot);

    	if(dots.Count > 1 && !reading)
    	{
    		newDot.transform.position = new Vector3(dots[dots.Count-2].transform.position.x + 3f, dots[dots.Count-2].transform.position.y + 0f, 0f);
    		currDot = newDot;
    		if(prevDot == currDot || prevDot == null)
    			prevDot = dots[System.Convert.ToInt32(newDot.name.Replace("dot", ""))-1];
    		GameObject newThing = Instantiate(thingPrefab);
    		newThing.name = "thing"+ (dots.Count-1).ToString();
    		newThing.AddComponent<Connection>();
    		things.Add(newThing);
    		holdedThing = newThing;
    	}
    }

    public void CreateConnection(GameObject _prevDot, GameObject _currDot, string thingname)
    {
    	if(dots.Count > 1)
    	{
    		GameObject newThing = Instantiate(thingPrefab);
    		newThing.name = thingname;
    		newThing.AddComponent<Connection>();
    		newThing.GetComponent<Connection>().prevDot = _prevDot;
    		newThing.GetComponent<Connection>().currDot = _currDot;
    		things.Add(newThing);
    		holdedThing = newThing;

			foreach(GameObject thing in things)
	     	{
	     		if(GameObject.Find("text"+thing.name.Replace("thing", "")) == null)
	     		{
	     			GameObject text = Instantiate(textForThingPrefab);
	     			text.name = "text" + thingname.Replace("thing", "").ToString();
	     			texts.Add(text);
	     		}
	     		else 
	     		{
	     			GameObject text = GameObject.Find("text"+thing.name.Replace("thing", ""));
	     			text.transform.position = new Vector3(thing.transform.position.x, thing.transform.position.y, -0.2f);
	     			text.transform.eulerAngles = new Vector3(0f, 0f, thing.transform.eulerAngles.z);
	     			odrunit = ((thing.transform.localScale.x/unit)*10.9f)/10f;
	     			text.GetComponent<TextMesh>().text = (odrunit/1.09f).ToString("F3");
	     			if(thing == holdedThing)
	     				deleteT_T.text = holdedThing.name + ":" + "\n" + (odrunit/1.09f).ToString("F3");
	     		}
	     	}
    	}
    }

    public void CreateDot(float x, float y)
    {
    	GameObject newDot = Instantiate(dotPrefab);
    	newDot.name = "dot"+ dots.Count;
    	dots.Add(newDot);
    	newDot.transform.position = new Vector3(x, y, 0f);

		// if(dots.Count > 1)
  //   	{
  //   		currDot = newDot;
		// 	if(prevDot == currDot || prevDot == null)
		// 		prevDot = dots[System.Convert.ToInt32(newDot.name.Replace("dot", ""))-1];
		// 	GameObject newThing = Instantiate(thingPrefab);
		// 	newThing.name = "thing"+ (dots.Count-1).ToString();
		// 	newThing.AddComponent<Connection>();
		// 	newThing.GetComponent<Connection>().prevDot = dotsOrder[dots.Count-1];
		// 	newThing.GetComponent<Connection>().currDot = dotsOrder[dots.Count];
		// 	things.Add(newThing);
		// 	holdedThing = newThing;

		// 	foreach(GameObject thing in things)
	 //     	{
	 //     		if(GameObject.Find("text"+thing.name.Replace("thing", "")) == null)
	 //     		{
	 //     			GameObject text = Instantiate(textForThingPrefab);
	 //     			text.name = "text" + (dots.Count-1).ToString();
	 //     			texts.Add(text);
	 //     		}
	 //     		else 
	 //     		{
	 //     			GameObject text = GameObject.Find("text"+thing.name.Replace("thing", ""));
	 //     			text.transform.position = new Vector3(thing.transform.position.x, thing.transform.position.y, -0.2f);
	 //     			text.transform.eulerAngles = new Vector3(0f, 0f, thing.transform.eulerAngles.z);
	 //     			odrunit = ((thing.transform.localScale.x/unit)*10.9f)/10f;
	 //     			text.GetComponent<TextMesh>().text = (odrunit/1.09f).ToString("F3");
	 //     			if(thing == holdedThing)
	 //     				deleteT_T.text = holdedThing.name + ":" + "\n" + (odrunit/1.09f).ToString("F3");
	 //     		}
	 //     	}
		// }
    }

    public void DeleteThing()
    {
    	things.Remove(holdedThing);
    	Destroy(holdedThing);

    	if(GameObject.Find("thing"+(System.Convert.ToInt32(holdedThing.name.Replace("thing", ""))+1).ToString())!=null)
    	{
    		dots.Remove(holdedThing.GetComponent<Connection>().prevDot);
    		Destroy(holdedThing.GetComponent<Connection>().prevDot);
    	}
    	else
    	{
    		dots.Remove(holdedThing.GetComponent<Connection>().currDot);
    		Destroy(holdedThing.GetComponent<Connection>().currDot);
    	}

    	Destroy(GameObject.Find("text"+holdedThing.name.Replace("thing", "")));
    	texts.Remove(GameObject.Find("text"+holdedThing.name.Replace("thing", "")));
    }

    Vector3 floatToCordsx(Vector3 v1, Vector3 v2, float x)
    {
        return new Vector3(v2.x + x + .9f, v2.y, v1.z);
    }

    Vector3 floatToCordsy(Vector3 v1, Vector3 v2, float x)
    {
        return new Vector3(v2.x, v2.y + x + .9f, v1.z);
    }

    public void yORxF(bool y)
    {
    	if(y)
    		yORx = true;
    	else
    		yORx = false;
    }


    public void Mesure()
    {
    	Vector3 v1 = holdedThing.GetComponent<Connection>().currDot.transform.position;
    	Vector3 v2 = holdedThing.GetComponent<Connection>().prevDot.transform.position;
    	if(yORx)
    		holdedThing.GetComponent<Connection>().currDot.transform.position = floatToCordsy(v1, v2, float.Parse(mesure_I.text));
    	else
    		holdedThing.GetComponent<Connection>().currDot.transform.position = floatToCordsx(v1, v2, float.Parse(mesure_I.text));
    }

    public static string indexChar(string word, int index)
    {
    	string newWord = "";

    	for(int i = 0; i < index; i++)
    		newWord += word[i].ToString();

    	return newWord;
    }

    string saveFile;

    public void Save()
    {
	    if(File.Exists(saveFile))
	    {
	    	string str = File.ReadAllText(saveFile);
			str = str.Replace(str, "");
    		// File.Delete(saveFile);
	    }

    	string thingy = "";

    	foreach(GameObject dot in dots)
    	{
    		thingy += dot.transform.position.x.ToString()+"\n";
			thingy += dot.transform.position.y.ToString()+"\n";
    	}
    	foreach(GameObject thing in things)
    		thingy += thing.name + "=" + thing.GetComponent<Connection>().prevDot.name + "+" + thing.GetComponent<Connection>().currDot.name+"\n";
    	
    	File.WriteAllText(saveFile, thingy);  
    }

    void Read()
    {
    	string[] thingy = File.ReadAllLines(saveFile);

    	int cordsMax = 0;

    	for(int i = 0; i < thingy.Length; i++)
    	{
    		if(thingy[i].IndexOf("thing")!=0)
    			cordsMax++;
    	}

    	for(int i = 0; i < thingy.Length; i++)
    	{
    		if(i < cordsMax/2)
    			CreateDot(float.Parse(thingy[i*2]), float.Parse(thingy[(i*2)+1]));
    	}

    	string thingname = "";
    	GameObject dot1 = null;
    	GameObject dot2 = null;

    	for(int i = 0; i < thingy.Length; i++)
    	{
    		if(thingy[i].IndexOf("thing")==0)
    		{
    			thingname = indexChar(thingy[i], thingy[i].IndexOf("=")) + "=";
    			dot1 = GameObject.Find(indexChar(thingy[i], thingy[i].IndexOf("+")).Replace(thingname, ""));
    			dot2 = GameObject.Find(thingy[i].Replace(indexChar(thingy[i], thingy[i].IndexOf("+")) + "+", "").Replace(thingname, ""));
	    		CreateConnection(dot1, dot2, thingname.Replace("=", ""));
    		}
	    }

		reading = true;
    }
}
