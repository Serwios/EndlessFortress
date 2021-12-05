using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerGenerator : MonoBehaviour {
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

    private float LOWER_Y_BORDER_FOR_PLATFORM_GENERATION = -5.2F;

    private int randomPossilityToCreateCoin;
	private float HIGHER_Y_BORDER_FOR_COIN_GENERATION = 4.0F;
	private float LOWER_Y_BORDER_FOR_COIN_GENERATION = -4.0F;

    void Start() {
        platformWidth = gameObjects[0].GetComponent<BoxCollider2D>().size.x;
    }

    void Update() {
    	generatePlatform();
    }

    private void generatePlatform() {
        if(transform.position.x < generationPoint.position.x) {
            randomYKoef = (float) Random.Range(-0.3f, 0.3f);
            randomElementIndex = Random.Range(0, gameObjects.Count);

            GameObject randomlySelectedObject = gameObjects[randomElementIndex];
         
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
            absoluteYPosition = transform.position.y + randomYKoef;

            do{
                absoluteYPosition = transform.position.y + randomYKoef;
            }while(
            	absoluteYPosition < LOWER_Y_BORDER_FOR_PLATFORM_GENERATION);

            transform.position = new Vector3(
              (transform.position.x + platformWidth + distanceBetween), 
              absoluteYPosition, 
              transform.position.z);

            Instantiate(randomlySelectedObject, transform.position, transform.rotation);
            Instantiate(coinObject, transform.position, transform.rotation);
        }
    } 
}
