using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//This class controls all of the options and handles all of the option buttons
//This should not try to control anything in the dialogue manager but may call it?
public class OptionManager : MonoBehaviour
{
    //Gets the Objects to able to communicate with the proper scripts
    private DialogueManager dialManager;
    public ScoreManager scoreSystem;
    
    //Gets the list options that the scripte is attached to
    public Option[] currentOptionList;
    
    //Sets up an array of buttons for randomization
    public Button[] buttonArray = new Button[4];

    //Holds the index of what option for the specific button
    private int[] randomizedKey = new int[4];

    public Ticket currentTicket;
    
    // Start is called before the first frame update
    void Start()
    {
        dialManager = GameObject.FindGameObjectWithTag("Dialogue Manager").GetComponent<DialogueManager>();
        deactivateButton();
    }

    // Called if the currentDialogue has options after all of the sentences have been displayed
    public void DisplayOptions(List<Option> options)
    {
        //Creates a temp list that copies the list of the currentDialogue.options List
        List<Option> tempList = new List<Option>(options);

        //Local index for randmoization
        int index;
        for (int i = 0; i < options.Count; i++)
        {
            //Index = value return from randomized from Temp list  ***NOTE*** Temp list gets smaller ever loop
            index = randomizer(tempList.Count);
            
            //Text of the option at value i is set to the name of the option at tempList index ***NOTE*** NOT THE SAME AS currentOptionList
            buttonArray[i].GetComponentInChildren<TextMeshProUGUI>().text = tempList[index].optionName;
            buttonArray[i].gameObject.SetActive(true);

            //Removes the object in the temp list until 0
            tempList.Remove(tempList[index]);
        }
        
        //Sets the list to an array for currentOptionList
        currentOptionList = options.ToArray();
    }
    //Randomized fucntion, takes in min and max and returns an int.
    private int randomizer(int size)
    {
       return Random.Range(0, size);
    }
    //When button has onclick event, option button returns value assigned from 0-3.
    //optionIndex = button value assigned on the button game Object
    public void ChosenOption(int optionIndex)
    {
        //Local int index
        int index = 0;
        for (int i = 0; i < currentOptionList.Length; i++) { 
            //Check to see which option was chosen because of the randomization of the options, matches against the name
            if (buttonArray[optionIndex].GetComponentInChildren<TextMeshProUGUI>().text == currentOptionList[i].optionName)
            {
                index = i;
                currentTicket.SaveChoice(currentOptionList[i].optionName, currentOptionList[i].crtiism);
                break;
            } 
        }
        //Once the check finds which option it was, set the values based on the placement and calculate the score of the level.
        Dialogue nextDialogue = currentOptionList[index].nextDialogue;
        
        //Score of the option scriptable Object is sent to the Score Manager script.
        scoreSystem.setScore(currentOptionList[index].score);

        //Gets the nextdialogue scriptable objects and send its to the dialogue Manager for next dialogue display
        dialManager.NextDialogue(nextDialogue);
        deactivateButton();

    }
    //Deactivates all options button
    private void deactivateButton()
    {
        for (int i = 0; i < buttonArray.Length; i++)
        {
            buttonArray[i].gameObject.SetActive(false);
        }
    }
}
