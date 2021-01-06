using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//This class is for controlling what happens in the dialogue boxes themselves and helps controls which dialogue should be loaded next
//OptionManager will be controlling when then next Dialogue will start and keep track of the points (Note for Ricky) but may call it?
public class DialogueManager : MonoBehaviour
{
    //Get the nextButton, startButton, name text, and dialogue text in the panel. [This should always be the same and they should be set as prefabs]
    public Button nextSentence, startDialogue;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;


    //Gets the dialogue objects for the image to be toggled later in code
    public GameObject personImage;
    public GameObject phoneImage;

    //Gets the Conversation Manager game objects [Required in all scene that has any of the dialogue systems]
    //**NOTE*** Option Button will have OptionManager as an on click event which has the Dialogue Manager Object reference [Must Reference this Script's Object to Option Manager Game Object]
    private ConversationManager convoManager;
    [SerializeField]
    private ScoreManager scoreSystem;

    //Creates a queue for the sentences to be loaded.
    private Queue<string> sentences;

    //Gets the Index of the next Dialogue
    private int newDialogue;

    //Accesable Variable for the next Dialogue scriptable Object
    public Dialogue nextDialogue;

    // Start is called before the first frame update
    void Start()
    {
        /*
         * Finding Game Object reference based on tag or name [Required, may break on run-time]
         * */
        convoManager = GameObject.FindGameObjectWithTag("Conversation Manager").GetComponent<ConversationManager>();
        personImage = GameObject.Find("PersonDialogueBox");
        phoneImage = GameObject.Find("PhoneDialogueBox");

        //Hides all gameobject from player and stop interaction with game objects.
        personImage.SetActive(false); 
        phoneImage.SetActive(false);
        nameText.gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        nextSentence.gameObject.SetActive(false);

        //Creates a Queue called Sentences for strings to be added when grabbing the Dialogue.sentence array
        sentences = new Queue<string>();
    }

    /*Start Dialogue:
        Parameters: Dialogue Object
        Start dialogue - allows the NameText, DialogueText, NextSentenceButton all to be visible or accessible by other scripts.
        Then to makes the NextSentenceButton to be the > so the user knows to continue the dialogue and allow blinking animation.
        It checks if the dialogue is not 0/or is empty, to save the next dialogue.
        Then it checks to see if the dialogue in person or not. (Function call)
        Once it does that, it queues in all of the necessary dialogues in the queue and procceds to the next sentence.
        Loads dialogue into Enqueue in order and displays the first or next dialouge.
        **This is where the user sees the dialogue**
     */
    public void StartDialogue(Dialogue dialogue)
    {
        convoManager.currentDialogue = dialogue;
        //convoManager.currentDialogue.SetCompleted(false);
        nameText.gameObject.SetActive(true);
        dialogueText.gameObject.SetActive(true);
        nextSentence.gameObject.SetActive(true);
        //startDialogue.gameObject.SetActive(false); // start button is boss sprite so can't hide it
        startDialogue.enabled = false;              // disable start button instead to keep sprite visible
        nextSentence.GetComponentInChildren<TextMeshProUGUI>().text = ">";

        newDialogue = dialogue.nextDialogue.Length;

        if (newDialogue != 0)
        {
            nextDialogue = dialogue.nextDialogue[0];
        }
        
        CheckDialogueImage(dialogue.isInPerson);
        nameText.text = dialogue.Speaker;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();

        //Allows next button to blink again after a convo ends and a new one starts
        if (nextSentence.IsActive())
        {
            nextSentence.gameObject.GetComponentInChildren<ButtonBlinkingText>().StartBlinking();
        }
    }
    
    /*Next Dialogue:
        Calls the StartDialogue function and passes in the dialogue
     * */
    public void NextDialogue(Dialogue dialogue)
    {
        this.StartDialogue(dialogue);

    }

    /* Display Next Sentence
     *  Checks to see how many sentences are left.
     *  If there isn't a new Dialogue group then it ends the dialogue.
     *  If there is another Dialogue that needs to be display it will load that new dialogue to the dialogue box
     *  and continues the dialogue.
     */
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            //If there isn't new dialogue(next dialouge)
            if (newDialogue != 0)
            {
                EndDialogue();

                // sets the current dialogue to completed
                convoManager.currentDialogue.SetCompleted(true);
                NextDialogue(nextDialogue);
            }
            else //nothing else end dialogue
            {
                EndDialogue();
                convoManager.currentDialogue.SetCompleted(true);
            }
            return;

        }
        string sentence = sentences.Dequeue();


        dialogueText.text = sentence;
    }

    /*Check Dialogue Image
     *  Parameter: boolean isInPerson
     *  Checks to see if the dialogue is meant to be in person.
     *  If it is it loads setactive the PersonImage Dialogue image.
     *  If not, its the PersonImagine Dialogue image.
     */
    void CheckDialogueImage(bool isInPerson)
    {
        if(isInPerson)
        {
            personImage.SetActive(true);
            phoneImage.SetActive(false);
        }
        else
        {
            phoneImage.SetActive(true);
            personImage.SetActive(false);
        }
    }

    /*End Dialogue
     *  End Dialogue hides all of the dialogue system images and text boxes.
     */
    void EndDialogue()
    {
        nameText.gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(false);
        nextSentence.gameObject.SetActive(false);
        //startDialogue.gameObject.SetActive(false); // start button is boss sprite so can't hide it
        personImage.SetActive(false);
        phoneImage.SetActive(false);
        convoManager.currentDialogue.SetCompleted(true);

        //Calls Conversation Manager to end the Conversation
        convoManager.EndConversation();
    }

    /*Reset All Dialogue
     *  Resets each dialogue in the dialogue folder to incomplete status.
     */
    public void ResetAllDialogues()
    {
        Dialogue[] dialogues;

        // casts each dialogue to an object and loads it into an object array
        object[] dialObj = Resources.LoadAll("Conversation/Dialogues", typeof (Dialogue));
        
        for (int i = 0; i < dialObj.Length; i++)
        {
            dialogues = new Dialogue[dialObj.Length];
            for (int x = 0; x < dialObj.Length; x++)
            {
                // casts the dialogue objects back to dialogues and adds it to dialogue array
                dialogues[x] = (Dialogue)dialObj[x];

                // sets the dialogues to incomplete status
                dialogues[x].SetCompleted(false);
            }
        }
    }
}
