using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Option", menuName = "Option")]
[System.Serializable]
public class Option : ScriptableObject
{
    //Option name is what the option will display
    [SerializeField]
    private string _optionName;
    
    //Score given to be added by OptionManager for player save
    [SerializeField]
    private int _score;
    
    //The Next Dialogue after the option has been selected
    [SerializeField]
    private Dialogue _nextDialogue;

    [SerializeField]
    [TextArea]
    private string _critism;
    /*
     *** Read only
     * Left refrence private data (right variables)
     */
    public string optionName => _optionName;
    public int score => _score;
    public Dialogue nextDialogue => _nextDialogue;
    public string crtiism => _critism;
}
