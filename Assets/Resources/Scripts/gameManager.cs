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

    public List<GameRound> timings;

    public Player()
    {
        timings = new List<GameRound>();
    }

    public float calculateAverageReactionTime()
    {
        int totalRounds = timings.Count;
        float totalTime = 0;
        foreach(GameRound round in timings)
        {
            totalTime += round.playerReactionTime;
        }

        return totalTime / totalRounds;
    }

}

class GameRound
{
    public float playerReactionTime;

}


public class gameManager : MonoBehaviour
{
    Player currentPlayer;
    GameRound currentRound;
   
    GameObject squareToGenerate,enemyBox;

    GameObject startMenu,loadedStartMenu,countDownPrefab,loadedCountDown;

    float startTime,clickTime;

    void InitialiseStartMenu()
    {
        loadedStartMenu = Instantiate(startMenu,Vector3.zero,Quaternion.identity);
        GameObject.Find("startButton").GetComponent<Button>().onClick.AddListener(
            () =>
            {
                currentPlayer.playerName = GameObject.Find("nameInputField").GetComponent<InputField>().text;
                Debug.Log("START GAME" + currentPlayer.playerName);
                Destroy(loadedStartMenu);
                StartCoroutine(startCountDown());
                
                //carrierButton.enabled = false;
            }
        );

    }

    IEnumerator startCountDown()
    {
        loadedCountDown = Instantiate(countDownPrefab,Vector3.zero,Quaternion.identity);
        int counter = 3;
        while(counter>0)
        {  
            loadedCountDown.GetComponentInChildren<Text>().text = counter.ToString();
            counter -= 1;
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("coroutine 1 done");
        //call random box afterwards
        yield return spawnRandomBox();
    }


    //After the countdown finishes, I want to save the current time, wait a random amount of time
    //and spawn a box.
    IEnumerator spawnRandomBox()
    {
        Destroy(loadedCountDown);
        Debug.Log("coroutine 2 started");
        //wait for a random interval
        yield return new WaitForSeconds(Random.Range(0.2f, 2f));
        //the time when the box is spawned
        startTime = Time.time;
        //spawn the box in the shape of a diamond
        Quaternion diamondRotation = Quaternion.identity;
        diamondRotation.eulerAngles = new Vector3(0f, 0f, 45f);
        enemyBox = Instantiate(squareToGenerate, new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(-4.5f, 4.5f)), diamondRotation);
        enemyBox.transform.localScale = new Vector3(0.25f, 0.25f);
        //add a component to the enemy box
        enemyBox.AddComponent<BoxCollider2D>();
        Debug.Log("coroutine 2 done");
        yield return null;

    }



    //every time the player clicks, you are going to save the difference in time between the box being created and the player clicking
    //and you are going to increase the current round value.  Display the current round as text in the top left corner of the screen. (for next week)

    


    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = new Player();
        squareToGenerate = Resources.Load<GameObject>("Prefabs/MySquare");   
        startMenu = Resources.Load<GameObject>("Prefabs/StartMenu");   
        countDownPrefab = Resources.Load<GameObject>("Prefabs/CountDown");   

        InitialiseStartMenu();
    }

    //Task 1.  Modify the coroutine so a red box starts from the bottom left square and goes all the way
    //around the edge of the screen.

  

   
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
        sq.name = "Square-" + (currentPlayer.currentRound);
        return sq;

    }


   

    void clearScreen()
    {

    }

    //Display of all the timings of the player over 10 rounds with the average at the bottom


    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
       {
           Destroy(loadedStartMenu);
       }

       //checking whether I have clicked the left click mouse

       //create a UI showing a timer on the top right corner
       if (Input.GetMouseButtonDown(0))
       {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //imagine a straight line into the monitor
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPosition, Vector3.forward);
            //so if there is a gameobject here
            if (hit.collider !=null)
            {
                clickTime = Time.time;
                float reactionTime = clickTime - startTime;
                GameRound currentRound = new GameRound();
                currentRound.playerReactionTime = reactionTime;
                currentPlayer.timings.Add(currentRound);
                currentPlayer.currentRound++;
                Debug.Log(reactionTime);
                Debug.Log(currentPlayer.currentRound);
                Debug.Log(hit.collider.gameObject.name);
                Destroy(hit.collider.gameObject);
                StartCoroutine(startCountDown());
            }

       }
    }
}
