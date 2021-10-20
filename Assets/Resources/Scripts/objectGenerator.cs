using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class objectGenerator : MonoBehaviour
{
    List<GameObject> listofsquares;

    GameObject mySquareToGenerate;

    GameObject frameObject;

    GameObject tempObject;

    bool animate;

    // Start is called before the first frame update
    void Start()
    {
        animate = false;
        listofsquares = new List<GameObject>();
        //load the square prefab from a file
        mySquareToGenerate = Resources.Load<GameObject>("Prefabs/MySquare");

        frameObject = new GameObject();
        frameObject.name = "My Frame";
        
        //instantiate it in the center of the screen
      

        generateFrame(frameObject.transform);

        StartCoroutine(animateFrame());
    }

    //Task 1.  Modify the coroutine so a red box starts from the bottom left square and goes all the way
    //around the edge of the screen.

    //a typical coroutine
    IEnumerator animateFrame()
    {
        //infinite loop because this is a coroutine
        while(true)
        {
            //if animate is true, animate
            if (animate)
            {
                foreach(GameObject square in listofsquares)
                {
                    square.GetComponent<SpriteRenderer>().color = Random.ColorHSV();    
                }
                //0.2 second gap between color changes
                yield return new WaitForSeconds(0.2f);
            }
            //if not, do nothing
            yield return null;
        }
    }



    //method used start controlling the square
    void controlSquare()
    {

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
        GameObject sq = Instantiate(mySquareToGenerate, position,rotation);
        //set a random colour
        sq.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        //add to list
        listofsquares.Add(sq);
        //give it a unique name
        sq.name = "Square-" + (listofsquares.Count);
        return sq;


    }


    //2. generate a frame of squares around the edge of the screen
    void generateFrame(Transform parent)
    {
        GameObject tempSquare;

        //top and bottom row
        for(float columncounter = -4.5f;columncounter<=4.5f;columncounter++)
        {
           tempSquare=generateSquare(new Vector3(columncounter,4.5f),Quaternion.identity);
           tempSquare.transform.parent = parent;

           tempSquare= generateSquare(new Vector3(columncounter,-4.5f),Quaternion.identity);
           tempSquare.transform.parent = parent;
        }
        
        //first and last column NO OVERLAP
        for(float rowcounter = -3.5f;rowcounter<=3.5f;rowcounter++)
        {
            tempSquare=generateSquare(new Vector3(-4.5f,rowcounter),Quaternion.identity);
            tempSquare.transform.parent = parent;
            tempSquare=generateSquare(new Vector3(4.5f,rowcounter),Quaternion.identity);
            tempSquare.transform.parent = parent;
        }        
    }


    //3. generate an X of boxes for the entire screen, so two diagonals crossing in the middle
    void generateCross()
    {

    }


    //4. generate a pyramid of boxes
    void generatePyramid()
    {

    }

    //clear the entire screen area and generate one random square in the middle
    void clearScreen()
    {

    }




    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //flip the value of animate
            animate = !animate;
            Debug.Log(animate);
        }
    }
}
