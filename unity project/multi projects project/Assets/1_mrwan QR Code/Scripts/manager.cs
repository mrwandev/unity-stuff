using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class manager : MonoBehaviour
{
    public Texture2D texture;
    public Material mat;
    public List<string> chars, alphabets, specialChars, odrz;
    public TMP_InputField input;
    public TextMeshProUGUI output;
    public Toggle toggle;

    public List<string> newcharsL;
    public string word;

    // Start is called before the first frame update
    void Start()
    {
        output.text = "";
    }

    public void ToggleChanged()
    {
        output.text = "";
        input.text = "";
        newcharsL.Clear();
        word = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(toggle.isOn)
        {
            Write();
            toggle.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "chars to code";
        }
        else
        {
            Read();
            toggle.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "code to chars";
        }
    }

    void Write()
    {
        output.text = "";

        foreach(char _char in input.text)
        {
            for(int a = 0; a < chars.Count; a++)
            {
                if(_char.ToString().ToLower() == chars[a])
                {
                    if(alphabets.Contains(_char.ToString().ToLower()))
                    {
                        output.text+=odrz[a]+"1";

                        if(alphabets.Contains(_char.ToString()))
                            output.text+="0";
                        else
                            output.text+="1";
                    }
                    else if(specialChars.Contains(_char.ToString()))
                    {
                        output.text+=odrz[a-26]+"0";
                        output.text+="0";
                    }
                }
            }
        }
        
        newcharsL.Clear();
        for(int i = 0; i < output.text.Length/8; i++)
            newcharsL.Add(indexCharStartFrom(output.text, 8*(i+1), 8*i));

        if(newcharsL.Count <= 8)
        {
            texture = new Texture2D(8, 8);
            texture.filterMode = FilterMode.Point;
            Color color = Color.black;

            for(int y = 0; y < texture.width; y++)
            {
                for(int x = 0; x < texture.height; x++)
                {
                    if(y < newcharsL.Count)
                    {
                        if(newcharsL[y][x] == '1')
                            color = Color.black;
                        else
                            color = Color.white;
                        
                        texture.SetPixel(x, 7-y, color);
                    }
                }
            }
        }
        if(newcharsL.Count > 8)
        {
            // texture = null;
            texture = new Texture2D(16, 16);
            texture.filterMode = FilterMode.Point;
            Color color = Color.black;

            for(int y = 0; y < texture.width; y++)
            {
                for(int x = 0; x < texture.height; x++)
                {
                    if(y*2 < newcharsL.Count && x < 8)
                    {
                        if(newcharsL[y*2][x] == '1')
                            color = Color.black;
                        else
                            color = Color.white;
                        
                        texture.SetPixel(x, 15-y, color);
                    }
                    if(y*2+1 < newcharsL.Count && x < 8)
                    {
                        if(newcharsL[y*2+1][x] == '1')
                            color = Color.black;
                        else
                            color = Color.white;
                        
                        texture.SetPixel(x+8, 15-y, color);
                    }
                }
            }
        }

        // if(newcharsL.Count > 8)
        // {
        texture.Apply();
        GetComponent<SpriteRenderer>().sprite = 
        Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
        mat.mainTexture = texture;
        // }
    }

    void Read()
    {
        if(input.text == null || word.Length == newcharsL.Count && word.Length > 0)
            return;

        if(newcharsL.Count != input.text.Length/8)
        {
            for(int i = 0; i < input.text.Length/8; i++)
                newcharsL.Add(indexCharStartFrom(input.text, 8*(i+1), 8*i));
        }

        for(int newchar = 0; newchar < newcharsL.Count; newchar++)
        {
            for(int _char = 0; _char < odrz.Count; _char++)
            {
                if(indexCharStart(newcharsL[newchar], 6) == odrz[_char])
                {
                    if(!specialChars.Contains(chars[_char]))
                        word+=chars[_char];
                    else
                        word+=chars[_char+26];
                }
            }
        }
        output.text = word;
    }

    public void copy()
    {
        GUIUtility.systemCopyBuffer = output.text;
    }

    public static string indexCharStartFrom(string word, int index, int fromIndex)
    {
        string newWord = "";

        for(int i = fromIndex; i < index; i++)
            newWord += word[i].ToString();

        return newWord;
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
}
