using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class objectGenerator : MonoBehaviour
{
    List<GameObject> listofsquares;

    GameObject mySquareToGenerate;
    // Start is called before the first frame update
    void Start()
    {

        listofsquares = new List<GameObject>();
        //load the square prefab from a file
        mySquareToGenerate = Resources.Load<GameObject>("Prefabs/MySquare");


        
        //instantiate it in the center of the screen
        generateSquare(Vector3.zero,Quaternion.identity);

        generateRandomSquare();

        generateFrame();
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

    void generateSquare(Vector3 position,Quaternion rotation)
    {
        GameObject sq = Instantiate(mySquareToGenerate, position,rotation);
        //set a random colour
        sq.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        //add to list
        listofsquares.Add(sq);
        //give it a unique name
        sq.name = "Square-" + (listofsquares.Count);

    }


    //2. generate a frame of squares around the edge of the screen
    void generateFrame()
    {
        //top row
        for(float columncounter = -4.5f;columncounter<=4.5f;columncounter++)
        {
            generateSquare(new Vector3(columncounter,4.5f),Quaternion.identity);
        }
        //bottom row


        //first column


        //last column

    }


    //3. generate an X of boxes for the entire screen
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
        
    }
}
