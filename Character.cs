using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Character", menuName = "Character")]
[System.Serializable]
public class Character : ScriptableObject
{
    //Character data used for Password Check and character set up.

    //Sprite image of character
    [SerializeField]
    private Sprite _characterImage;
    
    //Name of the character, Accessed when Dialogue is being read in
    [SerializeField]
    private string _characterName;
    
    //Characters Birthday
    [SerializeField]
    private int[] _mmddyyyy = new int[3];

    //Characters id number for quick checks
    [SerializeField]
    private int _id;

    //Characeters Home Address
    [SerializeField]
    private string _address;
    
    /*
     * Read only, Left is reference for the right variable
     * */
    public Sprite image => _characterImage;
    public string Name => _characterName;
    public int[] birthday => _mmddyyyy;
    public int idNumber => _id;
    public string address => _address;
}
