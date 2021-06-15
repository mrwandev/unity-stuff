using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // public Slider slider;
    public GameObject debugUnit, dot1, dot2, dotPrefab, cubePrefab, linePrefab, textForLinePrefab, arrows, hideButtonGo, hideTButtonGo;
    public TextMeshProUGUI debugText, unit_Text, deleteLine_Text, holdedDot_Text;
    public TMP_InputField val_Input, roTFrac_Input, unit_Input, mesure_Input;
    public string saveFileDir, loadedLines;
    public Toggle singleDotToggle;
    public List<GameObject> dots, lines, texts, uiStuff, cubes;
    [HideInInspector]
    public GameObject prevDot, currDot, square, holdedDot, holdedLine, lastHoldedDot;
    [HideInInspector]
    public float timer, arrowsFloat, val, maxTimerVal, rotationFractions, unit, odrunit, shownUnit;
    bool hold, holding, moveB, yORx, hide, link;
    // [HideInInspector]
    public bool ctrl, customConn;
    Vector3 pos, touchPos;
    int d_keycode;

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
        #if UNITY_STANDALONE_WIN
        //  saveFileDir = Application.persistentDataPath + "/" + fileName;
            saveFileDir = Application.dataPath + "/" + fileName;
        #endif

        #if UNITY_ANDROID
            saveFileDir = Application.persistentDataPath + "/" + fileName;
            // saveFileDir = "/storage/emulated/0/" + fileName;
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

        val_Input.text = val.ToString();
        roTFrac_Input.text = rotationFractions.ToString();
        unit_Input.text = shownUnit.ToString();
        mesure_Input.text = "0";

        maxTimerVal = .3f;
    }

    public bool holdingAcurr()
    {
        bool yee = false;
        foreach(GameObject line in lines)
        {
            if(yee == false)
                yee = line.GetComponent<Connection>().currDot == prevDot;
        }
        return yee;
    }

    public void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
            d_keycode++;

        if(d_keycode == 1)
            debugUnit.SetActive(true);

        if(d_keycode == 2)
        {
            debugUnit.SetActive(false);
            d_keycode = 0;
        }

        if(dot2 != null)
            dot2.GetComponent<SpriteRenderer>().color = Color.yellow;

        dot1 = lastHoldedDot;

        if(link)
        {
            if(dot1 == dot2)
                return;

            GameObject dot1L = null;
            GameObject dot2L = null;
            GameObject lineToDel = null;

            foreach(GameObject line in lines)
            {
                if(line.GetComponent<Connection>().currDot == dot1 && line.GetComponent<Connection>().prevDot == dot2 || 
                   line.GetComponent<Connection>().currDot == dot2 && line.GetComponent<Connection>().prevDot == dot1)
                    lineToDel = line;
            }

            if(lineToDel != null)
            {
                if(lineToDel.GetComponent<Connection>().prevDot == dot1 && lineToDel.GetComponent<Connection>().currDot == dot2)
                {
                    foreach(GameObject line in lines)
                    {
                        if(line.GetComponent<Connection>().currDot == lineToDel.GetComponent<Connection>().prevDot)
                            dot1L = line;

                        if(line.GetComponent<Connection>().prevDot == lineToDel.GetComponent<Connection>().currDot)
                            dot2L = line;
                    }
                }
                else if(lineToDel.GetComponent<Connection>().currDot == dot1 && lineToDel.GetComponent<Connection>().prevDot == dot2)
                {
                    foreach(GameObject line in lines)
                    {
                        if(line.GetComponent<Connection>().currDot == lineToDel.GetComponent<Connection>().prevDot)
                            dot2L = line;

                        if(line.GetComponent<Connection>().prevDot == lineToDel.GetComponent<Connection>().currDot)
                            dot1L = line;
                    }
                }

                if(lineToDel.GetComponent<Connection>().prevDot == dot1 && lineToDel.GetComponent<Connection>().currDot == dot2)
                    dot2L.GetComponent<Connection>().prevDot = dot1L.GetComponent<Connection>().currDot;

                else if(lineToDel.GetComponent<Connection>().currDot == dot1 && lineToDel.GetComponent<Connection>().prevDot == dot2)
                    dot1L.GetComponent<Connection>().prevDot = dot2L.GetComponent<Connection>().currDot;

                DeleteLine(lineToDel);
            }
            else
            {
                foreach(GameObject line in lines)
                {
                    if(line.GetComponent<Connection>().currDot == dot1 || line.GetComponent<Connection>().prevDot == dot1)
                        dot1L = line;

                    if(line.GetComponent<Connection>().prevDot == dot2 || line.GetComponent<Connection>().currDot == dot2)
                        dot2L = line;
                }

                if(dot1L != null && dot2L != null)
                {
                    if(dot1L.GetComponent<Connection>().currDot == dot1 && dot2L.GetComponent<Connection>().prevDot == dot2)
                    {
                        dots.Remove(dot2L.GetComponent<Connection>().prevDot);
                        Destroy(dot2L.GetComponent<Connection>().prevDot);  

                        dot2L.GetComponent<Connection>().prevDot = dot1L.GetComponent<Connection>().currDot;
                    }
                    else
                    {
                        dots.Remove(dot1L.GetComponent<Connection>().prevDot);
                        Destroy(dot1L.GetComponent<Connection>().prevDot);  

                        dot1L.GetComponent<Connection>().prevDot = dot2L.GetComponent<Connection>().currDot;
                    }
                }
            }

            if(lines.Count > 0)
                CreateConnection(dot1, dot2, "line"+(System.Convert.ToInt32(lines[lines.Count-1].name.Replace("line", ""))+1).ToString(), 1);
            else if(lines.Count == 0)
                CreateConnection(dot1, dot2, "line0", 1);

            dot2 = null;
            link = false;
        }

        // if(!GameObject.Find("text" + holdedLine.name.Replace("line", "")).GetComponent<MeshRenderer>().enabled)
        //  hideTButtonGo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Show text";
        // else
        //  hideTButtonGo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Hide text";

        if(Input.GetKey(KeyCode.RightArrow) || arrowsFloat == 4f)
            Camera.main.transform.position += new Vector3(.05f, 0f, 0f);

        if(Input.GetKey(KeyCode.LeftArrow) || arrowsFloat == 3f)
            Camera.main.transform.position += new Vector3(-.05f, 0f, 0f);

        if(Input.GetKey(KeyCode.UpArrow) || arrowsFloat == 1f)
            Camera.main.transform.position += new Vector3(0f, .05f, 0f);

        if(Input.GetKey(KeyCode.DownArrow) || arrowsFloat == 2f)
            Camera.main.transform.position += new Vector3(0f, -.05f, 0f);

        Vector3 camz = Camera.main.transform.position;
        Camera.main.transform.position = new Vector3(camz.x, camz.y, -val);

        foreach(GameObject thing in uiStuff)
            thing.SetActive(!hide);

        if(hide)
        {
            hideButtonGo.GetComponent<RectTransform>().anchoredPosition = new Vector3(106.9f, -81.3f, 0f);
            hideButtonGo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Show";
        }
        else
        {
            hideButtonGo.GetComponent<RectTransform>().anchoredPosition = new Vector3(106.9f, -209.8f, 0f);
            hideButtonGo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Hide";
        }

        arrows.SetActive(hide);

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
        //  for(int i = 0; i < dots.Count-1; i++)
        //      twoDotsClass.twoDots(dots[i], dots[i+1], lines[i], .9f, rotationFractions);
        // }

        unit_Text.text = "1 unit = " + unit.ToString() + "m";

        if(float.Parse(val_Input.text) != 0f)
            val = float.Parse(val_Input.text);

        rotationFractions = float.Parse(roTFrac_Input.text);
        unit = float.Parse(unit_Input.text);
        shownUnit = unit/1.09f;

        // debugText.text = Application.dataPath + "/" + "data.mrwan" + "\n" +
        //                Application.persistentDataPath + "/" + "data.mrwan" + "\n" +
        //                Application.streamingAssetsPath + "/" + "data.mrwan";

        // debugText.text = saveFileDir;

        // if(Input.touchCount > 0 && timer < maxTimerVal && !hold || Input.touchCount > 0 && timer < maxTimerVal && !holding)
        //  debugText.text = timer.ToString();
        // else
        //  debugText.text = "nothing";

        // if(Input.touchCount == 1 && !hold || Input.touchCount == 1 && !holding || Input.GetKey(KeyCode.A) && !hold || Input.GetKey(KeyCode.A) && !holding)
        // {
        //  timer += Time.deltaTime;
        //  if(timer > maxTimerVal)
        //      debugText.text = "move";
        // }
        // else
        //  timer = 0f;

        // if(Input.touchCount > 1)
        // {
            // if(Input.GetTouch(0).)
            // {
            //  val = Vector3.Distance(,);
            // }
            // square.transform.position = pos;
        // }

        Vector3 mousePos = Input.mousePosition;
        pos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -Camera.main.transform.position.z));

     //     if(Input.touchCount > 0)
     //     {
        //  if(timer > maxTimerVal)
        //      moveB = true;
        //  else
        //      moveB = false;
     //     }

     //     if(moveB)
        //  Camera.main.transform.position = new Vector3(pos.x*0.5f, pos.y*0.5f, -val);
        // else
        //  Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -val);

        // if(dots.Count > 1)
        // {
        //  for(int i = 0; i < dots.Count-1; i++)
        //      twoDotsClass.twoDots(dots[i], dots[i+1], lines[i], .9f, rotationFractions);
        // }

        if(dots.Count < 1)
            return;

        foreach(GameObject line in lines)
        {
            // if(GameObject.Find("cube"+line.name.Replace("line", "")) == null)
            // {
            //  GameObject cube = Instantiate(cubePrefab);
            //  cube.name = "cube" + (System.Convert.ToInt32(dots[dots.Count-1].name.Replace("dot", ""))).ToString();
            //  cubes.Add(cube);
            // }

            if(GameObject.Find("text"+line.name.Replace("line", "")) == null)
            {
                GameObject text = Instantiate(textForLinePrefab);
                text.name = "text" + (System.Convert.ToInt32(dots[dots.Count-1].name.Replace("dot", ""))).ToString();
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
                    deleteLine_Text.text = holdedLine.name + ":" + "\n" + (odrunit/1.09f).ToString("F3");
            }
        }

        foreach(GameObject dot in dots)
        {
            hold = CloseToVector3(dot.transform.position, pos);

            if(Input.GetKeyUp(KeyCode.Mouse0) && dot != dot2 || !hold && dot != dot2)
                dot.GetComponent<SpriteRenderer>().color = Color.white;

            if(hold)
                holdedDot = dot;

            if(hold && ctrl && Input.GetKey(KeyCode.Mouse1))
            {
                dot2 = dot;
                dot2.GetComponent<SpriteRenderer>().color = Color.yellow;
            }

            if(Input.GetKeyUp(KeyCode.Mouse0))
                holding = false;

            if(hold && Input.GetKey(KeyCode.Mouse0))
            {
                prevDot = dot;
                lastHoldedDot = dot;
                holdedDot_Text.text = lastHoldedDot.name;
                holding = true;

                // if(link)
                //  newLastHoldedDot = dot;
            }

            if(hold && Input.GetKey(KeyCode.Mouse1) && !ctrl)
            {
                prevDot = dot;
                lastHoldedDot = dot;
                holdedDot_Text.text = lastHoldedDot.name;
                // if(link)
                //  newLastHoldedDot = dot;
            }
            
            if(lastHoldedDot!=null)
                lastHoldedDot.GetComponent<SpriteRenderer>().color = Color.yellow;

            if(holding && holdedDot!=null)
            {
                holdedDot.transform.position = pos;
                holdedDot.GetComponent<SpriteRenderer>().color = Color.red;
                return;
            }

            holdedDot = null;

            if(hold && !ctrl)
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

            if(Input.GetKeyUp(KeyCode.Mouse1) && !ctrl)
                dot2 = null;
        }
    }

    public void LinkTwoDots()
    {
        link = !link;
    }

    bool CloseToVector3(Vector3 v1, Vector3 v2)
    {
        return Mathf.Round(v2.x) == Mathf.Round(v1.x) && Mathf.Round(v2.y) == Mathf.Round(v1.y);
    }

    public void CreateDot()
    {
        GameObject newDot = Instantiate(dotPrefab);

        if(dots.Count > 0)
            newDot.name = "dot" + (System.Convert.ToInt32(dots[dots.Count-1].name.Replace("dot", "")) + 1).ToString();
        else
            newDot.name = "dot0";
        
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
            newLine.name = "line" + (System.Convert.ToInt32(dots[dots.Count-1].name.Replace("dot", ""))).ToString();
            newLine.AddComponent<Connection>();
            lines.Add(newLine);
            holdedLine = newLine;
        }
    }

    public void CreateDot(float x, float y, int dotIndex)
    {
        GameObject newDot = Instantiate(dotPrefab);
        newDot.name = "dot"+ dotIndex.ToString();
        dots.Add(newDot);
        newDot.transform.position = new Vector3(x, y, 0f);
    }

    public void CreateConnection(GameObject _prevDot, GameObject _currDot, string linename, int YES)
    {
        customConn = YES == 1;

        if(dots.Count > 1 || YES == 1)
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
                        deleteLine_Text.text = holdedLine.name + ":\n" + (odrunit / 1.09f).ToString("F3");
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

    public void DeleteDot()
    {
        List<GameObject> linesToDel = new List<GameObject>();

        foreach(GameObject line in lines)
        {
            if(line.GetComponent<Connection>().currDot == lastHoldedDot || line.GetComponent<Connection>().prevDot == lastHoldedDot)
                linesToDel.Add(line);
        }

        foreach(GameObject line in linesToDel)
        {
            lines.Remove(line);
            Destroy(line);

            texts.Remove(GameObject.Find("text" + line.name.Replace("line", "")));
            Destroy(GameObject.Find("text" + line.name.Replace("line", "")));
        }


        dots.Remove(lastHoldedDot);
        Destroy(lastHoldedDot);
    } 

    public void DeleteLine(GameObject line)
    {
        lines.Remove(line);
        Destroy(line);

        // if(noLineHasTwoDots(holdedLine.GetComponent<Connection>().currDot, holdedLine.GetComponent<Connection>().prevDot))
        // {
        //     dots.Remove(holdedLine.GetComponent<Connection>().prevDot);
        //     Destroy(holdedLine.GetComponent<Connection>().prevDot);

        //     dots.Remove(holdedLine.GetComponent<Connection>().currDot);
        //     Destroy(holdedLine.GetComponent<Connection>().currDot);
        // }
        if(noLineHasThisDot(line.GetComponent<Connection>().prevDot))
        {
            dots.Remove(line.GetComponent<Connection>().prevDot);
            Destroy(line.GetComponent<Connection>().prevDot);
        }
        if(noLineHasThisDot(line.GetComponent<Connection>().currDot))
        {
            dots.Remove(line.GetComponent<Connection>().currDot);
            Destroy(line.GetComponent<Connection>().currDot);
        }
        Destroy(GameObject.Find("text" + line.name.Replace("line", "")));
        texts.Remove(GameObject.Find("text" + line.name.Replace("line", "")));
    }

    public void Mesure(string dir)
    {
        Vector3 v1 = holdedLine.GetComponent<Connection>().currDot.transform.position;
        Vector3 v2 = holdedLine.GetComponent<Connection>().prevDot.transform.position;
        if(dir == "r")
            holdedLine.GetComponent<Connection>().currDot.transform.position = new Vector3(v2.x+.9f+float.Parse(mesure_Input.text), v2.y, v1.z);
        if(dir == "l")
            holdedLine.GetComponent<Connection>().currDot.transform.position = new Vector3(+v2.x-.9f-float.Parse(mesure_Input.text), v2.y, v1.z);
        if(dir == "u")
            holdedLine.GetComponent<Connection>().currDot.transform.position = new Vector3(v2.x, v2.y+float.Parse(mesure_Input.text)+.9f, v1.z);
        if(dir == "d")
            holdedLine.GetComponent<Connection>().currDot.transform.position = new Vector3(v2.x, v2.y-float.Parse(mesure_Input.text)-.9f, v1.z);
    }

    public void HideText()
    {
        MeshRenderer text = null;
        text = GameObject.Find("text" + holdedLine.name.Replace("line", "")).GetComponent<MeshRenderer>();

        GameObject.Find("text" + holdedLine.name.Replace("line", "")).GetComponent<MeshRenderer>().enabled = !text.enabled;

        if(!text.enabled)
            hideTButtonGo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Show text";
        else
            hideTButtonGo.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Hide text";
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
            fileContent += "i: " + dot.name.Replace("dot", "")+"\n";

        foreach(GameObject dot in dots)
            fileContent += dot.transform.position.x.ToString() +"\n" + dot.transform.position.y.ToString() +"\n";

        foreach(GameObject line in lines)
            fileContent += line.name + "=" + line.GetComponent<Connection>().prevDot.name + "+" + line.GetComponent<Connection>().currDot.name + "\n";

            // if(line == lines[lines.Count-1])
            //     fileContent = indexCharStart(fileContent, fileContent.Length-1); // dont have to use cuz u ca&n remove \n in mesure value bellow

        fileContent += "v: " + rotationFractions.ToString() + "\n";
        fileContent += "v: " + val.ToString() + "\n";
        fileContent += "v: " + unit.ToString() + "\n";
        fileContent += "v: " + mesure_Input.text + "\n";
        fileContent += "v: " + hide.ToString() + "\n";
        fileContent += "v: " + Camera.main.transform.position.x.ToString() + "\n";
        fileContent += "v: " + Camera.main.transform.position.y.ToString();

        File.WriteAllText(saveFileDir, fileContent);
    }

    public void Arrows(string dir)
    {
        if(dir == "u")
            arrowsFloat = 1f;
        if(dir == "d")
            arrowsFloat = 2f;
        if(dir == "l")
            arrowsFloat = 3f;
        if(dir == "r")
            arrowsFloat = 4f;
    }

    public void Hide()
    {
        hide = !hide;
    }

    void Read()
    {
        string[] fileContent = File.ReadAllLines(saveFileDir);
        int coordsMax = 0;

        List<int> indexes = new List<int>();


        for(int i = 0; i < fileContent.Length; i++)
        {
            if(fileContent[i].IndexOf("i") == 0)
                indexes.Add(System.Convert.ToInt32(fileContent[i].Replace("i: ", "").Replace("\n", "")));
        }

        for(int i = 0; i < fileContent.Length; i++)
        {
            if(fileContent[i].IndexOf("line") != 0 && fileContent[i].IndexOf("v") != 0 && fileContent[i].IndexOf("i") != 0)
                coordsMax++;
        }

        for(int i = 0; i < fileContent.Length; i++)
        {
            if(i < coordsMax / 2)
                CreateDot(float.Parse(fileContent[i * 2 + indexes.Count]), float.Parse(fileContent[i * 2 + 1 + indexes.Count]), indexes[i]);
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
                CreateConnection(dot1, dot2, linename.Replace("=", ""), 0);
            }
            else if(fileContent[i].IndexOf("v") == 0)
                vals.Add(fileContent[i].Replace("v: ", "").Replace("\n", ""));
        }

        roTFrac_Input.text = vals[0];
        val_Input.text = vals[1];
        unit_Input.text = vals[2];
        mesure_Input.text = vals[3];
        hide = vals[4] == "True";
        Camera.main.transform.position = new Vector3(float.Parse(vals[5]), float.Parse(vals[6]), Camera.main.transform.position.z);
    }
}
