using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    //These varaibles are used to represent the cards on screen to the player
    public const int gridRows = 2;
    public const int gridCols = 5;
    public const float offsetX = 35f;
    public const float offsetY = 35f;

    [SerializeField] private MainCard originalCard;
    [SerializeField] private Sprite[] images;
    private void Start()
    {
        //This is the position of the first card and all the others are then offset from this
        Vector3 startPos = originalCard.transform.position;
        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4};
        numbers = ShuffleArray(numbers);
        //If we are using the main card then we keep it otherwise it instantiates a new card
        for (int i = 0; i < gridCols; i++)
        {
            for(int j = 0; j < gridRows; j++ )
            {
                MainCard card;
                if(i== 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MainCard;
                }
                //This sets the Cards position and assigns it an Image and ID
                int index = j * gridCols + i;
                int id = numbers[index];
                card.ChangeCardImage(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }
    //This function shuffles the array and returns them in a new order so that the cards are random everytime
    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for(int i = 0; i <newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }
    //This next section of the code will check for matching or none matching pairs and assign score accordingly
    private MainCard firstRevealed;
    private MainCard secondRevealed;
    private int score = 0;
    [SerializeField] private TextMesh scoreLabel;

    public bool canReveal
    {
        get { return secondRevealed == null; }
    }
    //This checks for Card matches and verifys if a match has been made
    public void CardRevealed(MainCard card)
    {
        if (firstRevealed == null)
        {
            firstRevealed = card;
        }
        else
        {
            secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }
    private IEnumerator CheckMatch()
    {
        if(firstRevealed.id == secondRevealed.id)
        {
            score++;
            scoreLabel.text = "Score: " + score;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            firstRevealed.Flipback();
            secondRevealed.Flipback();
        }
        firstRevealed = null;
        secondRevealed = null;
    }
}
