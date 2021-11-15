using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerGenerator : MonoBehaviour {
    public GameObject thePlatform;
    public GameObject thePillar;

	public Transform generationPoint;
	public float distanceBetween;

	private float platformWidth;
    public float distanceBetweenMin;
    public float distanceBetweenMax;

	private float randomYKoef;
	private System.Random random = new System.Random();

    //We should add new platforms;
    private List<GameObject> gameObjects = new List<GameObject>();

    void Start() {
        platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;
        gameObjects.Add(thePlatform);
        gameObjects.Add(thePillar);
    }

    void Update() {
    	//When generator is above generatorpoint(Camera point) by x
    	if(transform.position.x < generationPoint.position.x) {
            randomYKoef = (float) Random.Range(-0.3f, 0.3f);
    		distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            transform.position = new Vector3(
    			(transform.position.x + platformWidth + distanceBetween), 
    			transform.position.y + randomYKoef, 
    			transform.position.z);

    		Instantiate (thePlatform, transform.position, transform.rotation);
    	}
         
    }
}
