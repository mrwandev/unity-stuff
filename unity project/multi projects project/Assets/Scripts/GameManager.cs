using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
	// public Slider slider;
	public GameObject prevDot, currDot, square, holdedDot, holdedLine, dotPrefab, linePrefab, textForLinePrefab, lastHoldedDot;
	public TextMeshProUGUI debugText, unit_T, deleteT_T;
	public TMP_InputField val_I, rT_I, unit_I, mesure_I; 
	public List<GameObject> dots, lines, texts;
	bool hold, holding, moveB, yORx;
	Vector3 pos, touchPos;
	public float timer, val, maxTimerVal, rotationFractions, unit, odrunit, shownUnit;
    public string saveFileDir, loadedLines;
    public Toggle singleDotToggle;

    // Start is called before the first frame update
    void Start()
    {
        SaveFileDir("data.mrwan");

        SetVars();

        if(File.Exists(saveFileDir))
    	   Read();
    }

    void SaveFileDir(string fileName)
    {
        #if UNITY_ANDROID || UNITY_STANDALONE_WIN
            saveFileDir = Application.persistentDataPath + "/" + fileName;
        #endif

        #if UNITY_EDITOR
            saveFileDir = "Assets" + "/" + fileName;
        #endif
    }

    void SetVars()
    {
        // slider.maxValue = 50f;
        // slider.minValue = 10f;
        val = -Camera.main.transform.position.z;
        rotationFractions = 0f;
        unit = 1.09f;
        shownUnit = 1f;

        val_I.text = val.ToString();
        rT_I.text = rotationFractions.ToString();
        unit_I.text = shownUnit.ToString();
        mesure_I.text = "0";

        maxTimerVal = .3f;
    }

    // Update is called once per frame
    void Update()
    {	
        List<GameObject> loadedLinesList = new List<GameObject>();
        string goS = "";
        foreach(char letter in loadedLines)
        {
            if(letter != '&')
                goS += letter.ToString();
            else
            {
                loadedLinesList.Add(GameObject.Find(goS));
                goS = "";
            }
        }
        foreach(GameObject go in loadedLinesList)
        {
            if (!lines.Contains(go))
                loadedLines = loadedLines.Replace(go.name + "&", "");
        }

    	// if(dots.Count > 1)
    	// {
    	// 	for(int i = 0; i < dots.Count-1; i++)
    	// 		twoDotsClass.twoDots(dots[i], dots[i+1], lines[i], .9f, rotationFractions);
    	// }
    	
    	unit_T.text = "1 unit = " + unit.ToString() + "m";

    	if(float.Parse(val_I.text) != 0f)
			val = float.Parse(val_I.text);

    	rotationFractions = float.Parse(rT_I.text);
    	unit = float.Parse(unit_I.text);
    	shownUnit = unit/1.09f;

        // debugText.text = Application.dataPath + "/" + "data.mrwan" + "\n" + 
        //     			  Application.persistentDataPath + "/" + "data.mrwan" + "\n" +
        //     			  Application.streamingAssetsPath + "/" + "data.mrwan";

        debugText.text = saveFileDir;

    	// if(Input.touchCount > 0 && timer < maxTimerVal && !hold || Input.touchCount > 0 && timer < maxTimerVal && !holding)
    	// 	debugText.text = timer.ToString();
    	// else
    	// 	debugText.text = "nothing";

    	// if(Input.touchCount == 1 && !hold || Input.touchCount == 1 && !holding || Input.GetKey(KeyCode.A) && !hold || Input.GetKey(KeyCode.A) && !holding)
    	// {
    	// 	timer += Time.deltaTime;
    	// 	if(timer > maxTimerVal)
    	// 		debugText.text = "move";
    	// }
    	// else
    	// 	timer = 0f;

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
    	// 		twoDotsClass.twoDots(dots[i], dots[i+1], lines[i], .9f, rotationFractions);
    	// }

     	if(dots.Count < 1)
     		return;

     	foreach(GameObject line in lines)
     	{
     		if(GameObject.Find("text"+line.name.Replace("line", "")) == null)
     		{
     			GameObject text = Instantiate(textForLinePrefab);
     			text.name = "text" + (dots.Count-1).ToString();
     			texts.Add(text);
     		}
     		else 
     		{
     			GameObject text = GameObject.Find("text"+line.name.Replace("line", ""));
     			text.transform.position = new Vector3(line.transform.position.x, line.transform.position.y, -0.2f);
     			text.transform.eulerAngles = new Vector3(0f, 0f, line.transform.eulerAngles.z);
     			odrunit = line.transform.localScale.x/unit*10.9f/10f;
     			text.GetComponent<TextMesh>().text = (odrunit/1.09f).ToString("F3");
     			if(line == holdedLine)
     				deleteT_T.text = holdedLine.name + ":" + "\n" + (odrunit/1.09f).ToString("F3");
     		}
     	}

        bool CloseToVector3(Vector3 v1, Vector3 v2)
        {
            return Mathf.Round(v2.x) == Mathf.Round(v1.x) && Mathf.Round(v2.y) == Mathf.Round(v1.y);
        }

 		foreach(GameObject dot in dots)
     	{
     		hold = CloseToVector3(dot.transform.position, pos);

     		if(Input.GetKeyUp(KeyCode.Mouse0) || !hold)
     			dot.GetComponent<SpriteRenderer>().color = Color.white;

     		if(hold)
     			holdedDot = dot;

     		if(Input.GetKeyUp(KeyCode.Mouse0))
     			holding = false;

     		if(hold && Input.GetKey(KeyCode.Mouse0))
     		{
     			prevDot = dot;
     			lastHoldedDot = dot;
     			holding = true;
     		}

     		if(holding && holdedDot!=null)
     		{
     			holdedDot.transform.position = pos;
     			holdedDot.GetComponent<SpriteRenderer>().color = Color.red;
     			return;
     		}
     		
            holdedDot = null;

     		if(hold)
     			dot.GetComponent<SpriteRenderer>().color = Color.green;
     	}

     	foreach(GameObject line in lines)
     	{
     		hold = CloseToVector3(line.transform.position, pos);

     		if(Input.GetKeyUp(KeyCode.Mouse0) || !hold)
     			line.GetComponent<SpriteRenderer>().color = Color.white;

     		if(hold && Input.GetKey(KeyCode.Mouse0))
     			holdedLine = line;

     		if(Input.GetKeyUp(KeyCode.Mouse0))
     			holding = false;

     		if(hold && Input.GetKey(KeyCode.Mouse0))
     			holding = true;

     		if(holding)
     		{
     			holdedLine.GetComponent<SpriteRenderer>().color = Color.red;
     			break;
     		}

     		if(hold)
     			line.GetComponent<SpriteRenderer>().color = Color.green;
     	}
    }

    public void CreateDot()
    {
    	GameObject newDot = Instantiate(dotPrefab);
    	newDot.name = "dot"+ dots.Count;
    	dots.Add(newDot);

    	if(dots.Count == 1)
    		lastHoldedDot = newDot;

    	if(dots.Count > 1 && !singleDotToggle.isOn)
    	{
    		newDot.transform.position = new Vector3(dots[dots.Count-2].transform.position.x + 3f, dots[dots.Count-2].transform.position.y + 0f, 0f);
    		currDot = newDot;
    		if(prevDot == currDot || prevDot == null)
    			prevDot = dots[System.Convert.ToInt32(newDot.name.Replace("dot", ""))-1];
    		GameObject newLine = Instantiate(linePrefab);
    		newLine.name = "line"+ (dots.Count-1).ToString();
    		newLine.AddComponent<Connection>();
    		lines.Add(newLine);
    		holdedLine = newLine;
    	}
    }

    public void CreateDot(float x, float y)
    {
    	GameObject newDot = Instantiate(dotPrefab);
    	newDot.name = "dot"+ dots.Count;
    	dots.Add(newDot);
    	newDot.transform.position = new Vector3(x, y, 0f);
    }

    public void CreateConnection(GameObject _prevDot, GameObject _currDot, string linename)
    {
        if(dots.Count > 1)
        {
            GameObject newLine = Instantiate(linePrefab);
            newLine.name = linename;
            newLine.AddComponent<Connection>();
            newLine.GetComponent<Connection>().prevDot = _prevDot;
            newLine.GetComponent<Connection>().currDot = _currDot;
            lines.Add(newLine);
            holdedLine = newLine;

            foreach(GameObject line in lines)
            {
                if(GameObject.Find("text" + line.name.Replace("line", "")) == null)
                {
                    GameObject text = Instantiate(textForLinePrefab);
                    text.name = "text" + linename.Replace("line", "").ToString();
                    texts.Add(text);
                }
                else
                {
                    GameObject text = GameObject.Find("text" + line.name.Replace("line", ""));
                    text.transform.position = new Vector3(line.transform.position.x, line.transform.position.y, -0.2f);
                    text.transform.eulerAngles = new Vector3(0f, 0f, line.transform.eulerAngles.z);
                    odrunit = line.transform.localScale.x / unit * 10.9f / 10f;
                    text.GetComponent<TextMesh>().text = (odrunit / 1.09f).ToString("F3");

                    if(line == holdedLine)
                        deleteT_T.text = holdedLine.name + ":\n" + (odrunit / 1.09f).ToString("F3");
                }
            }
        }
    }

    // bool noLineHasTwoDots(GameObject dot1, GameObject dot2)
    // {
    //     int yee = 0;
    //     foreach (GameObject line in lines)
    //     {
    //         if(line.GetComponent<Connection>().currDot != dot1 && line.GetComponent<Connection>().prevDot != dot2 && 
    //            line.GetComponent<Connection>().currDot != dot2 && line.GetComponent<Connection>().prevDot != dot1)
    //             yee++;
    //     }
    //     return yee == lines.Count;
    // }

    bool noLineHasThisDot(GameObject dot1)
    {
        int yee = 0;
        foreach(GameObject line in lines)
        {
            if(line.GetComponent<Connection>().prevDot != dot1 && line.GetComponent<Connection>().currDot != dot1)
                yee++;
        }
        return yee == lines.Count;
    }

    public void DeleteLine()
    {
        lines.Remove(holdedLine);
        Destroy(holdedLine);

        // if(noLineHasTwoDots(holdedLine.GetComponent<Connection>().currDot, holdedLine.GetComponent<Connection>().prevDot))
        // {
        //     dots.Remove(holdedLine.GetComponent<Connection>().prevDot);
        //     Destroy(holdedLine.GetComponent<Connection>().prevDot);

        //     dots.Remove(holdedLine.GetComponent<Connection>().currDot);
        //     Destroy(holdedLine.GetComponent<Connection>().currDot);
        // }
        if(noLineHasThisDot(holdedLine.GetComponent<Connection>().prevDot))
        {
            dots.Remove(holdedLine.GetComponent<Connection>().prevDot);
            Destroy(holdedLine.GetComponent<Connection>().prevDot);
        }
        if(noLineHasThisDot(holdedLine.GetComponent<Connection>().currDot))
        {
            dots.Remove(holdedLine.GetComponent<Connection>().currDot);
            Destroy(holdedLine.GetComponent<Connection>().currDot);
        }
        Destroy(GameObject.Find("text" + holdedLine.name.Replace("line", "")));
        texts.Remove(GameObject.Find("text" + holdedLine.name.Replace("line", "")));
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
    	Vector3 v1 = holdedLine.GetComponent<Connection>().currDot.transform.position;
    	Vector3 v2 = holdedLine.GetComponent<Connection>().prevDot.transform.position;
    	if(yORx)
    		holdedLine.GetComponent<Connection>().currDot.transform.position = floatToCordsy(v1, v2, float.Parse(mesure_I.text));
    	else
    		holdedLine.GetComponent<Connection>().currDot.transform.position = floatToCordsx(v1, v2, float.Parse(mesure_I.text));
    }

    public static string indexCharStart(string word, int index)
    {
    	string newWord = "";

    	for(int i = 0; i < index; i++)
    		newWord += word[i].ToString();

    	return newWord;
    }

    public static string indexCharEnd(string word, int index)
    {
        string newWord = "";
        int j = 0;

        for(int i = 0; i < index; i++)
        {
            j = word.Length - i - 1;
            newWord += word[j].ToString();
        }

        return flipWord(newWord);
    }

    public static string flipWord(string word)
    {
        string newWord = "";
        int j = 0;

        for(int i = 0; i < word.Length; i++)
        {
            j = word.Length - i - 1;
            newWord += word[j].ToString();
        }

        return newWord;
    }

    void ClearFile(string fileDir)
    {
        string fileContent = File.ReadAllText(fileDir);

        if(fileContent != "")
            fileContent = fileContent.Replace(fileContent, "");
    }

    public void Save()
    {
	    if(File.Exists(saveFileDir))
    		ClearFile(saveFileDir);

        // File.Delete(saveFileDir);

    	string fileContent = "";

    	foreach(GameObject dot in dots)
    		fileContent += dot.transform.position.x.ToString() +"\n" + dot.transform.position.y.ToString() +"\n";

        foreach(GameObject line in lines)
            fileContent += line.name + "=" + line.GetComponent<Connection>().prevDot.name + "+" + line.GetComponent<Connection>().currDot.name + "\n";
            
            // if(line == lines[lines.Count-1])
            //     fileContent = indexCharStart(fileContent, fileContent.Length-1); // dont have to use cuz u ca&n remove \n in mesure value bellow

        fileContent += "v: " + rotationFractions.ToString() + "\n";
        fileContent += "v: " + val.ToString() + "\n";
        fileContent += "v: " + unit.ToString() + "\n";
        fileContent += "v: " + mesure_I.text;
    	
    	File.WriteAllText(saveFileDir, fileContent);  
    }

    void Read()
    {
    	string[] fileContent = File.ReadAllLines(saveFileDir);
        int coordsMax = 0;

        for(int i = 0; i < fileContent.Length; i++)
        {
            if(fileContent[i].IndexOf("line") != 0 && fileContent[i].IndexOf("v") != 0)
                coordsMax++;
        }
        for(int i = 0; i < fileContent.Length; i++)
        {
            if(i < coordsMax / 2)
                CreateDot(float.Parse(fileContent[i * 2]), float.Parse(fileContent[(i * 2) + 1]));
        }

        List<string> vals = new List<string>();

        for(int i = 0; i < fileContent.Length; i++)
        {
            if(fileContent[i].IndexOf("line") == 0)
            {
                string linename = indexCharStart(fileContent[i], fileContent[i].IndexOf("=")) + "=";
                loadedLines += linename.Replace("=", "&");
                GameObject dot1 = GameObject.Find(indexCharStart(fileContent[i], fileContent[i].IndexOf("+")).Replace(linename, ""));
                GameObject dot2 = GameObject.Find(fileContent[i].Replace(indexCharStart(fileContent[i], fileContent[i].IndexOf("+")) + "+", "").Replace(linename, ""));
                CreateConnection(dot1, dot2, linename.Replace("=", ""));
            }
            else if(fileContent[i].IndexOf("v") == 0)
                vals.Add(fileContent[i].Replace("v: ", "").Replace("\n", ""));
        }

        rT_I.text = vals[0];
        val_I.text = vals[1];
        unit_I.text = vals[2];
        mesure_I.text = vals[3];
    }
}
