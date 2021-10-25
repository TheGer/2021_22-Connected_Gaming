using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Connected Gaming Game 1

//10 round game for each player.
//At the beginning of the game, we are going to ask for the player name
//The player is going to be testing his actual reaction time
//For each round, we will have a count down of 3 seconds.
//After those 3 seconds, the game will wait for a random number of seconds before 
//spawning a square in a random location.
//The player will need to click on the randomly located square
//The time taken for the player to see the square and click it will be stored
//That time will be stored, and the round ends.  A next round button is displayed
//The player again will be presented with a three second countdown
//and his reaction time will be tested again.
//After ten rounds, we will calculate the average reaction time, and display it to the player.


class Player
{
    public string playerName;
    public float averageReactionTime;
    public int currentRound;

    public List<float> reactionTimes;

}

class GameRound
{
    public int gameRoundNumber;

    public float playerReactionTime;

}


public class gameManager : MonoBehaviour
{
    Player currentPlayer;
    GameRound currentRound;
   
    GameObject squareToGenerate;

    GameObject startMenu,loadedStartMenu;


    void InitialiseStartMenu()
    {
        loadedStartMenu = Instantiate(startMenu,Vector3.zero,Quaternion.identity);
        GameObject.Find("startButton").GetComponent<Button>().onClick.AddListener(
            () =>
            {
                currentPlayer.playerName = GameObject.Find("nameInputField").GetComponent<Text>().text;
                Debug.Log("START GAME" + currentPlayer.playerName);
                Destroy(loadedStartMenu);
                //carrierButton.enabled = false;
            }
        );

    }

    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = new Player();
        currentRound = new GameRound();
        currentRound.gameRoundNumber = 1;   
        squareToGenerate = Resources.Load<GameObject>("Prefabs/MySquare");   
        startMenu = Resources.Load<GameObject>("Prefabs/StartMenu");   

        InitialiseStartMenu();
    }

    //Task 1.  Modify the coroutine so a red box starts from the bottom left square and goes all the way
    //around the edge of the screen.

    //a typical coroutine
    IEnumerator countDown()
    {
        //infinite loop because this is a coroutine
       yield return null;
    }

   
    //1. generate another square at a new random position and colour
    void generateRandomSquare()
    {
        //random coordinates
        float randomX = Random.Range(-4.5f,4.5f);
        float randomY = Random.Range(-4.5f,4.5f);

        //random angle between -90 to +90
        float randomAngle = Random.Range(-90f,90f);

        Quaternion randomAngleQ = Quaternion.Euler(0f,0f,randomAngle);

        //create the random square
        generateSquare(new Vector3(randomX,randomY),randomAngleQ);

    }

    GameObject generateSquare(Vector3 position,Quaternion rotation)
    {
        GameObject sq = Instantiate(squareToGenerate, position,rotation);
        //set a random colour
        sq.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        //add to list
        //give it a unique name
        sq.name = "Square-" + (currentRound.gameRoundNumber);
        return sq;

    }


   

    void clearScreen()
    {

    }




    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
       {
           Destroy(loadedStartMenu);
       }
    }
}
