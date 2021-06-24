using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	public List<string> guessedPeople;
	[HideInInspector]
    public List<string> people;
    // [HideInInspector]
    public List<string> questions;

    string currentQ, currentAnswer1, currentAnswer2, answer;
    public int currentQuestionInt = 0;

    [Header("GameObjects")]
    public GameObject questionGO;
	public GameObject answerGO;
	public GameObject buttonsParentGO;
	public GameObject replayButtonGO;

	int currentQuestionFontSize; 

    bool isMale;
    bool isFemale;
    bool isAlive;
    bool isReal;
    bool speaksEnglish;
    bool isYoutuber;
    bool isAnimator;
    bool gaming;
    bool minecraftVideos;
    bool hideFace;
    bool speaksSwedish;
    bool speaksIrish;
    bool isSquid;
    bool haveAGoose;
    bool lastNameDanger;
    bool wetHisBed;
    bool halfMexican;
    bool isSinger;
    bool isTheGreatestCOAT;
    bool isFamous4Mods;
    bool ownsAClothingCompany;
    bool isAGameDev;
    bool isRelatedToScience;
    bool madeSomeWeirdVods;

    // bool button1Clicked, button2Clicked;

    void Start()
    {
    	// UnityEditor.PlayerSettings.SplashScreen.backgroundColor = new Color(0, 0, 0, 1);

    	FrstShit();
    }

    void Update()
    {
    	Questions();
    	Answers();
    	Guess();
    }

    public void button1()
    {
    	// button1Clicked = true;
    	GetAnswerFrmButton(0);
    }

    public void button2()
    {
        // button2Clicked = true;
        GetAnswerFrmButton(1);
    }
    public void Replay()
    {
    	SceneManager.LoadScene("game");
    }

    void FrstShit()
    {
    	// Screen.SetResolution(900, 600, false);

    	questions.Add("is this person a male or a female ?");
    	questions.Add("is this person alive ?");
    	questions.Add("is this person real ?");
    	questions.Add("does this person speaks english ?");
    	questions.Add("is this person a youtuber ?");
    	questions.Add("is this person an animator ?");
    	questions.Add("did this person made some gaming videos ?");
    	questions.Add("did this person made some minecraft videos ?");
    	questions.Add("does this person hide pliplop face ?");
    	questions.Add("does this person speaks swedish ?");
    	questions.Add("does this person speaks irish ?");
    	questions.Add("squid up ?");
    	questions.Add("does this person have a goose ?");
    	questions.Add("is pliplop last name means danger ?");
    	questions.Add("did this person wet his bed until he was 8 years old ?");
    	questions.Add("is this person half mexican ?");
    	questions.Add("is this person a singer ?");
    	questions.Add("is this person the greatest commentator of all time ?");
    	questions.Add("is this person famous for mods ?");
    	questions.Add("does this person owns a clothing company ?");
    	questions.Add("is this person a gamedev ?");
    	questions.Add("is this person related to science ?");
    	questions.Add("did this person made some weird videos ?");

    	people.Add("Jaiden Animations");
    	people.Add("PewDiePie");
    	people.Add("jacksepticeye");
    	people.Add("TheOdd1sOut");
    	people.Add("CallMeCarson");
    	people.Add("Corpse Husband");
    	people.Add("Domics");
    	people.Add("Andrei Terbea");
    	people.Add("Daidus");
    	people.Add("Emirichu");
    	people.Add("LixianTV");
    	people.Add("SomeThingElseYT");
    	people.Add("Ksi");
    	people.Add("penguinz0");
    	people.Add("MxR");
    	people.Add("Markiplier");
    	people.Add("Dani");
    	people.Add("Roomie");
    	people.Add("Tom Scott");
    	people.Add("Vsauce");

    	guessedPeople = people;
    	currentQuestionFontSize = 35;
    	currentAnswer1 = "male";
        currentAnswer2 = "female";
        questionGO.SetActive(true);
        answerGO.SetActive(true);
        buttonsParentGO.SetActive(true);
        replayButtonGO.SetActive(false);
    }

    void Questions()
    {
    	if(guessedPeople.Count > 1)
    	{
	    	foreach(string question in questions)
	    	{
	    		currentQ = questions[currentQuestionInt];
	    	}

	    	if(isMale)
	    	{
		    	foreach(string question in questions)
		    	{
		    		string temp;
		    		temp = questions[currentQuestionInt].Replace("this person", "he");
		    		currentQ = temp.Replace("pliplop", "his");
		    	}
	    	}

		    if(isFemale)
		    {
		    	foreach(string question in questions)
		    	{
		    		string temp;
		    		temp = questions[currentQuestionInt].Replace("this person", "she");
		    		currentQ = temp.Replace("pliplop", "her");
		    	}
		    }
		}

	    buttonsParentGO.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentAnswer1;
		buttonsParentGO.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentAnswer2;

		questionGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = currentQuestionFontSize;
    	questionGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentQ;
    }

    void Answers()
    {
    	answerGO.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = answer;
    	
    	if(guessedPeople.Count == 0)
    	{
    		answer = "not found";
    		currentQ = "character not found or not added YET click the button or press r to restart";
    		currentQuestionFontSize = 25;

			if(Input.GetKeyDown(KeyCode.R))
		    	SceneManager.LoadScene("game");
			
    		buttonsParentGO.SetActive(false);
    		replayButtonGO.SetActive(true);
    	}

    	if(guessedPeople.Count == 1)
    	{
    		answer = guessedPeople[0];
    		currentQ = "gooot'em";

    		if(Input.GetKeyDown(KeyCode.R))
		    	SceneManager.LoadScene("game");

    		buttonsParentGO.SetActive(false);
    		replayButtonGO.SetActive(true);
    	}
    	
    	if(guessedPeople.Count > 1)
			answer = "trying to guess";
    }

    void GetAnswerFrmButton(int trueOrFalse)
    {
    	if(trueOrFalse == 0)
    	{
    		if(currentQuestionInt == 0) // change that with currentQ == is this person alive (i dunno)
	        {
	        	currentAnswer1 = "yes";
	        	currentAnswer2 = "no";
	        	isMale = true;
	        	currentQuestionFontSize = 40;
	        }
	        if(currentQuestionInt == 1)
	        {
	        	isAlive = true;
	        	currentQuestionFontSize = 40;
	        }
	        if(currentQuestionInt == 2)
	        {
	        	isReal = true;
	        	currentQuestionFontSize = 35;
	        }
	        if(currentQuestionInt == 3)
	        {
	        	speaksEnglish = true;
	        }
	        if(currentQuestionInt == 4)
	        {
	        	isYoutuber = true;
	        }
	        if(currentQuestionInt == 5)
	        {
	        	isAnimator = true;
	        	currentQuestionFontSize = 30;
	        }
	        if(currentQuestionInt == 6)
	        {
	        	gaming = true;
	        	currentQuestionFontSize = 30;
	        }
	        if(currentQuestionInt == 7)
	        {
	        	minecraftVideos = true;
	        	currentQuestionFontSize = 35;
	        }
	        if(currentQuestionInt == 8)
	        {
	        	hideFace = true;
	        }
	        if(currentQuestionInt == 9)
	        {
	        	speaksSwedish = true;
	        }
	        if(currentQuestionInt == 10)
	        {
	        	speaksIrish = true;
	        }
	        if(currentQuestionInt == 11)
	        {
	        	isSquid = true;
	        }
	        if(currentQuestionInt == 12)
	        {
	        	haveAGoose = true;
	        }
	        if(currentQuestionInt == 13)
	        {
	        	lastNameDanger = true;
	        }
	        if(currentQuestionInt == 14)
	        {
	        	wetHisBed = true;
	        }
	        if(currentQuestionInt == 15)
	        {
	        	halfMexican = true;
	        }
	        if(currentQuestionInt == 16)
	        {
	        	isSinger = true;
	        }
	        if(currentQuestionInt == 17)
	        {
	        	isTheGreatestCOAT = true;
	        }
	        if(currentQuestionInt == 18)
	        {
	        	isFamous4Mods = true;
	        }
	        if(currentQuestionInt == 19)
	        {
	        	ownsAClothingCompany = true;
	        }
	        if(currentQuestionInt == 20)
	        {
	        	isAGameDev = true;
	        }
	        if(currentQuestionInt == 21)
	        {
	        	isRelatedToScience = true;
	        }
	        if(currentQuestionInt == 22)
	        {
	        	madeSomeWeirdVods = true;
	        }
	        if(currentQuestionInt == 22)
	        {
	        	currentQuestionInt--;
	        }
	        currentQuestionInt++;
	        // button1Clicked = false;
    	}
    	if(trueOrFalse == 1)
    	{
    		if(currentQuestionInt == 0)
	        {
	        	currentAnswer1 = "yes";
	        	currentAnswer2 = "no";
	        	isFemale = true;
	        	currentQuestionFontSize = 40;
	        }
	        if(currentQuestionInt == 1)
	        {
	        	isAlive = false;
	        	currentQuestionFontSize = 40;
	        }
	        if(currentQuestionInt == 2)
	        {
	        	isReal = false;
	        	currentQuestionFontSize = 35;
	        }
	        if(currentQuestionInt == 3)
	        {
	        	speaksEnglish = false;
	        }
	        if(currentQuestionInt == 4)
	        {
	        	isYoutuber = false;
	        }
	        if(currentQuestionInt == 5)
	        {
	        	isAnimator = false;
	        	currentQuestionFontSize = 30;
	        }
	        if(currentQuestionInt == 6)
	        {
	        	gaming = false;
	        	currentQuestionFontSize = 30;
	        }
	        if(currentQuestionInt == 7)
	        {
	        	minecraftVideos = false;
	        	currentQuestionFontSize = 35;
	        }
	        if(currentQuestionInt == 8)
	        {
	        	hideFace = false;
	        }
	        if(currentQuestionInt == 9)
	        {
	        	speaksSwedish = false;
	        }
	        if(currentQuestionInt == 10)
	        {
	        	speaksIrish = false;
	        }
	        if(currentQuestionInt == 11)
	        {
	        	isSquid = false;
	        }
	        if(currentQuestionInt == 12)
	        {
	        	haveAGoose = false;
	        }
	        if(currentQuestionInt == 13)
	        {
	        	lastNameDanger = false;
	        }
	        if(currentQuestionInt == 14)
	        {
	        	wetHisBed = false;
	        }
	        if(currentQuestionInt == 15)
	        {
	        	halfMexican = false;
	        }
	        if(currentQuestionInt == 16)
	        {
	        	isSinger = false;
	        }
	        if(currentQuestionInt == 17)
	        {
	        	isTheGreatestCOAT = false;
	        }
	        if(currentQuestionInt == 18)
	        {
	        	isFamous4Mods = false;
	        }
	        if(currentQuestionInt == 19)
	        {
	        	ownsAClothingCompany = false;
	        }
	        if(currentQuestionInt == 20)
	        {
	        	isAGameDev = false;
	        }
	        if(currentQuestionInt == 21)
	        {
	        	isRelatedToScience = false;
	        }
	        if(currentQuestionInt == 22)
	        {
	        	madeSomeWeirdVods = false;
	        }
	        if(currentQuestionInt == 22)
	        {
	        	currentQuestionInt--;
	        }
	        currentQuestionInt++;
	        // button2Clicked = false;
    	}
    }

    void Guess()
    {
    	// guessedPeople.Contains
	    if(!gaming && currentQuestionInt == 7)
    	{
	    	currentQuestionInt ++;
	    }

    	if(!isAnimator && currentQuestionInt == 11)
    	{
	    	currentQuestionInt += 5;
	    }
	    if(isAnimator && currentQuestionInt == 8)
    	{
	    	currentQuestionInt += 3;
	    }

    	if(isMale)
    	{
    		guessedPeople.Remove("Jaiden Animations");
	    	guessedPeople.Remove("Emirichu");
    	}
    	if(isFemale)
    	{
	    	guessedPeople.Clear();
	    	guessedPeople.Add("Jaiden Animations");
	    	guessedPeople.Add("Emirichu");
    	}
    	if(isAlive && currentQuestionInt == 2)
    	{
    	}
    	if(!isAlive && currentQuestionInt == 2)
    	{
    		guessedPeople.Clear();
    	}
    	if(!isReal && currentQuestionInt == 3)
    	{
    	}
    	if(!isReal && currentQuestionInt == 3)
    	{
    		guessedPeople.Clear();
    	}
    	if(speaksEnglish && currentQuestionInt == 4)
    	{
    	}
    	if(!speaksEnglish && currentQuestionInt == 4)
    	{
    		guessedPeople.Clear();
    	}
    	if(isYoutuber && currentQuestionInt == 5)
    	{
    	}
    	if(!isYoutuber && currentQuestionInt == 5)
    	{
    		guessedPeople.Clear();
    	}
    	if(isAnimator && isMale && currentQuestionInt == 6)
    	{
    		guessedPeople.Clear();
	    	guessedPeople.Add("TheOdd1sOut");
	    	guessedPeople.Add("Domics");
	    	guessedPeople.Add("Andrei Terbea");
	    	guessedPeople.Add("Daidus");
	    	guessedPeople.Add("LixianTV");
	    	guessedPeople.Add("SomeThingElseYT");
    	}
    	if(isAnimator && isFemale && currentQuestionInt == 6)
    	{
    		guessedPeople.Clear();
    		guessedPeople.Add("Jaiden Animations");
	    	guessedPeople.Add("Emirichu");
    	}
    	if(!isAnimator && currentQuestionInt == 6)
    	{
	    	guessedPeople.Remove("Jaiden Animations");
	    	guessedPeople.Remove("TheOdd1sOut");
	    	guessedPeople.Remove("Domics");
	    	guessedPeople.Remove("Andrei Terbea");
	    	guessedPeople.Remove("Daidus");
	    	guessedPeople.Remove("Emirichu");
	    	guessedPeople.Remove("LixianTV");
	    	guessedPeople.Remove("SomeThingElseYT");
    	}
    	if(gaming && currentQuestionInt == 7)
    	{
	    	guessedPeople.Remove("TheOdd1sOut");
	    	guessedPeople.Remove("Domics");
	    	guessedPeople.Remove("Andrei Terbea");
	    	guessedPeople.Remove("Daidus");
	    	guessedPeople.Remove("Emirichu");
	    	guessedPeople.Remove("SomeThingElseYT");
    	}
    	if(!gaming && currentQuestionInt == 7)
    	{
	    	guessedPeople.Remove("Jaiden Animations");
	    	guessedPeople.Remove("PewDiePie");
	    	guessedPeople.Remove("jacksepticeye");
	    	guessedPeople.Remove("CallMeCarson");
	    	guessedPeople.Remove("Corpse Husband");
	    	guessedPeople.Remove("LixianTV");
    	}
    	if(minecraftVideos && currentQuestionInt == 8)
    	{
	    	guessedPeople.Remove("TheOdd1sOut");
	    	guessedPeople.Remove("Corpse Husband");
	    	guessedPeople.Remove("Domics");
	    	guessedPeople.Remove("Andrei Terbea");
	    	guessedPeople.Remove("Daidus");
	    	guessedPeople.Remove("Emirichu");
	    	guessedPeople.Remove("SomeThingElseYT");
    	}
    	if(!minecraftVideos && currentQuestionInt == 8)
    	{
	    	guessedPeople.Remove("Jaiden Animations");
	    	guessedPeople.Remove("PewDiePie");
	    	guessedPeople.Remove("jacksepticeye");
	    	guessedPeople.Remove("CallMeCarson");
	    	guessedPeople.Remove("LixianTV");
    	}
    	if(hideFace && currentQuestionInt == 9)
    	{
	    	guessedPeople.Clear();
	    	guessedPeople.Add("Corpse Husband");
    	}
    	if(!hideFace && currentQuestionInt == 9)
    	{
	    	guessedPeople.Remove("Corpse Husband");
    	}
    	if(speaksSwedish && currentQuestionInt == 10)
    	{
    		guessedPeople.Clear();

    		if(minecraftVideos && gaming)
    		{
		    	guessedPeople.Add("PewDiePie");
    		}

	    	guessedPeople.Add("Roomie");
    	}
    	if(!speaksSwedish && currentQuestionInt == 10)
    	{
	    	guessedPeople.Remove("PewDiePie");
	    	guessedPeople.Remove("Roomie");
    	}
    	if(speaksIrish && currentQuestionInt == 11)
    	{
    		guessedPeople.Clear();
	    	guessedPeople.Add("jacksepticeye");
    	}
    	if(!speaksIrish && currentQuestionInt == 11)
    	{
	    	guessedPeople.Remove("jacksepticeye");
    	}
    	if(isSquid && currentQuestionInt == 12)
    	{
    		guessedPeople.Clear();
	    	guessedPeople.Add("Daidus");
    	}
    	if(!isSquid && currentQuestionInt == 12)
    	{
	    	guessedPeople.Remove("Daidus");
    	}
    	if(haveAGoose && currentQuestionInt == 13)
    	{
    		guessedPeople.Clear();
	    	guessedPeople.Add("Andrei Terbea");
    	}
    	if(!haveAGoose && currentQuestionInt == 13)
    	{
	    	guessedPeople.Remove("Andrei Terbea");
    	}
    	if(lastNameDanger && currentQuestionInt == 14)
    	{
    		guessedPeople.Clear();
	    	guessedPeople.Add("Domics");
    	}
    	if(!lastNameDanger && currentQuestionInt == 14)
    	{
	    	guessedPeople.Remove("Domics");
    	}
    	if(wetHisBed && currentQuestionInt == 15)
    	{
    		guessedPeople.Clear();
	    	guessedPeople.Add("TheOdd1sOut");
    	}
    	if(!wetHisBed && currentQuestionInt == 15)
    	{
	    	guessedPeople.Remove("TheOdd1sOut");
    	}
    	if(halfMexican && currentQuestionInt == 16)
    	{
	    	guessedPeople.Clear();
	    	guessedPeople.Add("SomeThingElseYT");
    	}
    	if(!halfMexican && currentQuestionInt == 16)
    	{
	    	guessedPeople.Remove("SomeThingElseYT");
    	}
    	if(isSinger && currentQuestionInt == 17)
    	{
    		guessedPeople.Clear();

    		if(hideFace)
    		{
		    	guessedPeople.Add("Corpse Husband");
    		}
    		if(halfMexican)
    		{
		    	guessedPeople.Add("SomeThingElseYT");
    		}

	    	guessedPeople.Add("Ksi");

	    	if(speaksSwedish)
	    	{
	    		guessedPeople.Add("Roomie");
	    	}
    	}
    	if(!isSinger && currentQuestionInt == 17)
    	{
	    	guessedPeople.Remove("Corpse Husband");
	    	guessedPeople.Remove("SomeThingElseYT");
	    	guessedPeople.Remove("Ksi");
	    	guessedPeople.Remove("Roomie");
    	}
    	if(isTheGreatestCOAT && currentQuestionInt == 18)
    	{
    		guessedPeople.Clear();
	    	guessedPeople.Add("penguinz0");
    	}
    	if(!isTheGreatestCOAT && currentQuestionInt == 18)
    	{
	    	guessedPeople.Remove("penguinz0");
    	}
    	if(isFamous4Mods && currentQuestionInt == 19)
    	{
    		guessedPeople.Clear();
	    	guessedPeople.Add("MxR");
    	}
    	if(!isFamous4Mods && currentQuestionInt == 19)
    	{
	    	guessedPeople.Remove("MxR");
    	}
    	if(ownsAClothingCompany && currentQuestionInt == 20)
    	{
    		guessedPeople.Clear();

    		if(speaksSwedish)
    		{
		    	guessedPeople.Add("PewDiePie");
    		}
    		if(speaksIrish)
    		{
		    	guessedPeople.Add("jacksepticeye");
    		}
	    	guessedPeople.Add("Markiplier");
    	}
    	if(!ownsAClothingCompany && currentQuestionInt == 20)
    	{
	    	guessedPeople.Remove("jacksepticeye");
	    	guessedPeople.Remove("PewDiePie");
	    	guessedPeople.Remove("Markiplier");
    	}
    	if(isAGameDev && currentQuestionInt == 21)
    	{
    		guessedPeople.Clear();

    		if(isAnimator)
    		{
	    		guessedPeople.Add("LixianTV");
    		}

	    	guessedPeople.Add("Dani");
    	}
    	if(!isAGameDev && currentQuestionInt == 21)
    	{
	    	guessedPeople.Remove("LixianTV");
	    	guessedPeople.Remove("Dani");
    	}
    	if(isRelatedToScience && currentQuestionInt == 22)
    	{
	    	guessedPeople.Clear();
	    	guessedPeople.Add("Tom Scott");
	    	guessedPeople.Add("Vsauce");
    	}
    	if(!isRelatedToScience && currentQuestionInt == 22)
    	{
	    	guessedPeople.Remove("Tom Scott");
	    	guessedPeople.Remove("Vsauce");
    	}
    	if(madeSomeWeirdVods && currentQuestionInt == 22)
    	{
	    	guessedPeople.Clear();
	    	guessedPeople.Add("Vsauce");
    	}
    	if(!isRelatedToScience && !madeSomeWeirdVods && currentQuestionInt == 22)
    	{
	    	guessedPeople.Remove("Vsauce");
    	}
    }
}