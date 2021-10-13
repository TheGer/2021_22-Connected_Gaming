using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectGenerator : MonoBehaviour
{

    GameObject mySquareToGenerate;
    // Start is called before the first frame update
    void Start()
    {
        //load the square prefab from a file
        mySquareToGenerate = Resources.Load<GameObject>("Prefabs/MySquare");

        //instantiate it in the center of the screen
        GameObject sq = Instantiate(mySquareToGenerate, Vector3.zero,Quaternion.identity);

        //set it to a random colour
        sq.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
