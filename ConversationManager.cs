using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    public Dialogue currentDialogue;
    public Dialogue[] listDialogue;
    public List<Option> currentOptions;

    private DialogueManager dialManager;
    private OptionManager optManager;

    //Find reference for Dialogue Manager and Option Manager through Tag reference and component grab
    public void Start()
    {
        dialManager = GameObject.FindGameObjectWithTag("Dialogue Manager").GetComponent<DialogueManager>();
        optManager = GameObject.FindGameObjectWithTag("Option Manager").GetComponent<OptionManager>();
    }

    //Starts a conversation and goes to Dialogue Manager
    public void startConversation()
    {
        currentDialogue = listDialogue[0];
        dialManager.StartDialogue(currentDialogue);
        //EndCoversation();
    }

    //Ends Conversation and checks to see if there is any options, if not nothing happens
    public void EndConversation()
    {
        if (currentDialogue.listOptions.Count != 0)
        {
           optManager.DisplayOptions(currentDialogue.listOptions);
        }
    }

}
