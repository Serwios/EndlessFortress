using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerGenerator : MonoBehaviour {
    public GameObject thePlatform;
    public GameObject thePillar;
    public GameObject theTorch;

	public Transform generationPoint;
	public float distanceBetween;

	private float platformWidth;
    public float distanceBetweenMin;
    public float distanceBetweenMax;

	private float randomYKoef;
	private System.Random random = new System.Random();
    private int randomElementIndex;

    //We should add new platforms;
    private List<GameObject> gameObjects = new List<GameObject>();

    private float tempdistanceBetweenMax;
    private float tempdistanceBetweenMin;

    void Start() {
        platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;
        gameObjects.Add(thePlatform);
        gameObjects.Add(thePillar);
    }

    void Update() {
    	//When generator is above generatorpoint(Camera point) by x
    	if(transform.position.x < generationPoint.position.x) {
            tempdistanceBetweenMax = distanceBetweenMax;
            tempdistanceBetweenMin = distanceBetweenMin;

            randomYKoef = (float) Random.Range(-0.3f, 0.3f);
            randomElementIndex = Random.Range(0, gameObjects.Count);
            //Take random obj from object list
            GameObject selectedObject = gameObjects[randomElementIndex];

            //Change distance between components if x.size < 4
            if (selectedObject.GetComponent<BoxCollider2D>().size.x <= 4) {
                distanceBetweenMax = 1.0f;
                distanceBetweenMin = 1.0f;
            }

    		distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
            
            //Fix limit for y, because platform goes under screen 
            transform.position = new Vector3(
    		  (transform.position.x + platformWidth + distanceBetween), 
    		  transform.position.y + randomYKoef, 
    		  transform.position.z);
    
    		Instantiate (selectedObject, transform.position, transform.rotation);
    
            distanceBetweenMax = tempdistanceBetweenMax;
            distanceBetweenMin = tempdistanceBetweenMin;
            
    	}
    }
}
