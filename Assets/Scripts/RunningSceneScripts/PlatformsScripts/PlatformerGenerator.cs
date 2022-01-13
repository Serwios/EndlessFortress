using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerGenerator : MonoBehaviour
{
    public List<GameObject> gameObjects = new List<GameObject>();
    public GameObject coinObject;
    public GameObject redCrystal;
    public GameObject goldenCrystal;
    public GameObject greenLifeCrystal;
    public GameObject hellCrystal;

    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;
    public float distanceBetweenMin;
    public float distanceBetweenMax;

    private float randomYKoef;
    private int randomElementIndex;
    private float absoluteYPosition;

    private float LOWER_Y_BORDER = -5.2F;
    private float HIGHER_Y_BORDER = 2.3F;
    private int oddsOfCreation;
    public static float globalKoef;

    void Start()
    {
        platformWidth = gameObjects[0].GetComponent<BoxCollider2D>().size.x;
        //this variable can controle game hardness
        globalKoef = 0.5f;
    }

    void Update()
    {
        generatePlatform();
    }

    private void generatePlatform()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            randomElementIndex = Random.Range(0, gameObjects.Count);
            GameObject randomlySelectedObject = gameObjects[randomElementIndex];
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            do
            {
                randomYKoef = (float)Random.Range(-0.5f, 0.5f);
                absoluteYPosition = transform.position.y + randomYKoef;
            } while (isPointNotInValidSquare(absoluteYPosition));

            transform.position = new Vector3(
              (transform.position.x + platformWidth + distanceBetween),
              absoluteYPosition,
              transform.position.z);

            Instantiate(randomlySelectedObject, transform.position, transform.rotation);

            createRandomObjectOnPlatform();
        }
    }

    //This odds system based on pie chart. 
    //I can`t create a normal readibility system, so decided to take the ratio of pieces to cake.
    private void createRandomObjectOnPlatform()
    {
        oddsOfCreation = Random.Range(0, 1000);

        //5%
        if (oddsOfCreation <= (50 * globalKoef))
        {
            Instantiate(goldenCrystal, transform.position, transform.rotation);
            return;
        }
        //8%
        else if (oddsOfCreation <= (130 * globalKoef) && PlayerPrefs.GetInt("coins") >= 50)
        {
            Instantiate(hellCrystal, transform.position, transform.rotation);
            return;
        }
        //15%
        else if (oddsOfCreation <= (280 * globalKoef))
        {
            Instantiate(greenLifeCrystal, transform.position, transform.rotation);
            return;
        }
        //15%
        else if (oddsOfCreation <= (430 * globalKoef))
        {
            Instantiate(redCrystal, transform.position, transform.rotation);
            return;
        }
        //35%
        else if (oddsOfCreation <= (780 * globalKoef))
        {
            Instantiate(coinObject, transform.position, transform.rotation);
            return;
        }
    }

    private bool isPointNotInValidSquare(float absoluteYPosition)
    {
        return absoluteYPosition < LOWER_Y_BORDER ||
                absoluteYPosition > HIGHER_Y_BORDER;
    }
}
