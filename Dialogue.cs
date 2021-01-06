using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
[System.Serializable]
public class Dialogue : ScriptableObject
{
    //Character Class - Scriptable Object Require for Dialogue Scriptable Object
    [SerializeField]
    private Character _character;
    
    //Dialogue for Character
    [SerializeField]
    [TextArea(3, 5)]
    private string[] _sentence;
    
    //If Dialogue is followed up with options, Place Option Scriptable Objects here, 4 at most
    [SerializeField]
    private List<Option> _listOptions;
    
    //Boolean for if Dialogue Box is in person or on phone, blue vs green image. Find sprite
    [SerializeField]
    private bool _isInPerson;
    
    //If Dialogue is followed up with different dialogue and does not have options.
    [SerializeField]
    private Dialogue[] _nextDialogue;
    
    //When Dialogue is finished and conversation is over than bool = true if not bool = false
    public bool isComplete = false;                 // added this to help trigger button activations

    /*
     ***Read only when doing dialogue.[list below]
     Left side names is the reference to private variable (right side)
         */
    public Character characterInfo => _character;
    public string Speaker => _character.name;
    public string[] sentences => _sentence;
    public List<Option> listOptions => _listOptions;
    public bool isInPerson => _isInPerson;
    public Dialogue[] nextDialogue => _nextDialogue;

    //Check if Dialogue is enabled
    public void OnEnable()
    {
        isComplete = false;
    }
    //If the dialogue is completed
    public void SetCompleted(bool value)
    {
        isComplete = value;
    }

    /*Dialogue Status
     *  Returns whether or not the dialogue is complete.
     */
    public bool DialogueStatus()
    {
        //Debug.Log(isComplete + " in status");
        return isComplete;
    }
}
