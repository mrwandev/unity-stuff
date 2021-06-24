using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using TMPro;

public class Main : MonoBehaviour
{
    public TMP_InputField darijaInputField;
    public ArabicText arabicTextComponent;
    public List<string> availableCharactersDA, availableCharactersAR, charactersInWrittenPhrase, nonoChars;
    string jomlaBl3arbya; 
    bool canAddCharacters = true;

    void Start()
    {
    	string readFromFilePathDA = Application.streamingAssetsPath + "/darija.txt";
    	string readFromFilePathAR = Application.streamingAssetsPath + "/3arbya.txt";

        List<string> fileLinesDA = File.ReadAllLines(readFromFilePathDA).ToList();
        List<string> fileLinesAR = File.ReadAllLines(readFromFilePathAR).ToList();

        foreach(string line in fileLinesDA)
            availableCharactersDA.Add(line);

	    foreach(string line in fileLinesAR)
	    	availableCharactersAR.Add(line);

    	Screen.SetResolution(800, 400, false);
    }

    void Update()
    {
    	string jomlaBDarija = darijaInputField.text;

    	// make l3arabya lfos7a mode as a toggle and ar9am 3arabya toggle

    	if(canAddCharacters && jomlaBDarija.Length != charactersInWrittenPhrase.Count)
    	{
    		charactersInWrittenPhrase.Clear();
    		nonoChars.Clear();

	    	foreach(char _7rf in jomlaBDarija)
	    	{
	    	 	charactersInWrittenPhrase.Add(_7rf.ToString());

	    	 	if(!availableCharactersDA.Contains(_7rf.ToString()))
		    		nonoChars.Add(_7rf.ToString());

        		if(nonoChars.Count > 0)
        		   	jomlaBDarija = jomlaBDarija.Replace(nonoChars[0], "");
	    	}
    	}

    	if(jomlaBDarija.Length == charactersInWrittenPhrase.Count)
    	 	canAddCharacters = false;
    	else
    	 	canAddCharacters = true;

    	if(!Input.anyKeyDown)
	    	canAddCharacters = true;

	    darijaInputField.text = jomlaBDarija.Replace("''", "'");


	    // for(int i = 0; i < 44; i++)
		// {
		//     jomlaBDarija = jomlaBDarija.Replace(availableCharactersDA[i], availableCharactersAR[i])
		//     						   .Replace(availableCharactersDA[44], "");
		// }

		// jomlaBl3arbya = jomlaBDarija;

        jomlaBl3arbya = jomlaBDarija.Replace(availableCharactersDA[0], availableCharactersAR[0])
                                    .Replace(availableCharactersDA[1], availableCharactersAR[1])
                                    .Replace(availableCharactersDA[2], availableCharactersAR[2])
                                    .Replace(availableCharactersDA[3], availableCharactersAR[3])
                                    .Replace(availableCharactersDA[4], availableCharactersAR[4])
                                    .Replace(availableCharactersDA[5], availableCharactersAR[5])
                                    .Replace(availableCharactersDA[6], availableCharactersAR[6])
                                    .Replace(availableCharactersDA[7], availableCharactersAR[7])
                                    .Replace(availableCharactersDA[8], availableCharactersAR[8])
                                    .Replace(availableCharactersDA[9], availableCharactersAR[9])
                                    .Replace(availableCharactersDA[10], availableCharactersAR[10])
                                    .Replace(availableCharactersDA[11], availableCharactersAR[11])
                                    .Replace(availableCharactersDA[12], availableCharactersAR[12])
                                    .Replace(availableCharactersDA[13], availableCharactersAR[13])
                                    .Replace(availableCharactersDA[14], availableCharactersAR[14])
                                    .Replace(availableCharactersDA[15], availableCharactersAR[15])
                                    .Replace(availableCharactersDA[16], availableCharactersAR[16])
                                    .Replace(availableCharactersDA[17], availableCharactersAR[17])
                                    .Replace(availableCharactersDA[18], availableCharactersAR[18])
                                    .Replace(availableCharactersDA[19], availableCharactersAR[19])
                                    .Replace(availableCharactersDA[20], availableCharactersAR[20])
                                    .Replace(availableCharactersDA[21], availableCharactersAR[21])
                                    .Replace(availableCharactersDA[22], availableCharactersAR[22])
                                    .Replace(availableCharactersDA[23], availableCharactersAR[23])
                                    .Replace(availableCharactersDA[24], availableCharactersAR[24])
                                    .Replace(availableCharactersDA[25], availableCharactersAR[25])
                                    .Replace(availableCharactersDA[26], availableCharactersAR[26])
                                    .Replace(availableCharactersDA[27], availableCharactersAR[27])
                                    .Replace(availableCharactersDA[28], availableCharactersAR[28])
                                    .Replace(availableCharactersDA[29], availableCharactersAR[29])
                                    .Replace(availableCharactersDA[30], availableCharactersAR[30])
                                    .Replace(availableCharactersDA[31], availableCharactersAR[31])
                                    .Replace(availableCharactersDA[32], availableCharactersAR[32])
                                    .Replace(availableCharactersDA[33], availableCharactersAR[33])
                                    .Replace(availableCharactersDA[34], availableCharactersAR[34])
                                    .Replace(availableCharactersDA[35], availableCharactersAR[35])
                                    .Replace(availableCharactersDA[36], availableCharactersAR[36])
                                    .Replace(availableCharactersDA[37], availableCharactersAR[37])
                                    .Replace(availableCharactersDA[38], availableCharactersAR[38])
                                    .Replace(availableCharactersDA[39], availableCharactersAR[39])
                                    .Replace(availableCharactersDA[40], availableCharactersAR[40])
                                    .Replace(availableCharactersDA[41], availableCharactersAR[41])
                                    .Replace(availableCharactersDA[42], availableCharactersAR[42])
                                    .Replace(availableCharactersDA[43], availableCharactersAR[43])
                                    .Replace(availableCharactersDA[44], "");
		
    	arabicTextComponent.Text = jomlaBl3arbya;
    }

    public void copy()
    {
    	GUIUtility.systemCopyBuffer = jomlaBl3arbya;
    }
}
