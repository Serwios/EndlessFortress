using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerGenerator : MonoBehaviour
{
    public List<GameObject> gameObjects = new List<GameObject>();
    public GameObject coinObject;

    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;
    public float distanceBetweenMin;
    public float distanceBetweenMax;

    private float randomYKoef;
    private System.Random random = new System.Random();
    private int randomElementIndex;
    private float absoluteYPosition;

    private float tempdistanceBetweenMax;
    private float tempdistanceBetweenMin;

    private float LOWER_Y_BORDER = -5.2F;
    private float HIGHER_Y_BORDER = 2.3F;

    private int randomPossilityToCreateCoin;
    private int oddsOfCreation = 3;

    void Start()
    {
        platformWidth = gameObjects[0].GetComponent<BoxCollider2D>().size.x;
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

            //potentionally infinite loop
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

            if (randomElementIndex == oddsOfCreation)
            {
                Instantiate(coinObject, transform.position, transform.rotation);
            }
        }
    }

    private bool isPointNotInValidSquare(float absoluteYPosition)
    {
        return absoluteYPosition < LOWER_Y_BORDER ||
                absoluteYPosition > HIGHER_Y_BORDER;
    }
}
