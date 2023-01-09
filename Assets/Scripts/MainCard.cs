using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour
{
    //Creates an Editable variable in the inspector
    [SerializeField] private SceneController controller;
    [SerializeField] private GameObject Card_Back;
    //Checks for player clicking the card
    public void OnMouseDown()
    {
        if(Card_Back.activeSelf && controller.canReveal)
        {
            Card_Back.SetActive(false);
            controller.CardRevealed(this);
        }
    }
    private int _id;
    public int id
    {
        get { return _id; }
    }

    public void ChangeCardImage(int id, Sprite image)
    {
        _id = id;
        //Gets the sprite renderer component and changes the property of the sprite.
        GetComponent<SpriteRenderer>().sprite = image; 
    }
    //If the player does not correctly match cards then it will flip them back over.
    public void Flipback()
    {
        Card_Back.SetActive(true);
    }
}
