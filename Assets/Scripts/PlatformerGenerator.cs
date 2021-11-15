using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerGenerator : MonoBehaviour {
    public GameObject thePlatform;
	public Transform generationPoint;
	public float distanceBetween;

	private float platformWidth;

	private float randomYKoef;
	private System.Random random = new System.Random(); 


    void Start() {
        platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x;
    }

    void Update() {
    	randomYKoef = (float) random.NextDouble();

    	//When generator is above generatorpoint(Camera point) by x
    	if(transform.position.x < generationPoint.position.x) {
    		transform.position = new Vector3(
    			(transform.position.x + platformWidth + distanceBetween), 
    			transform.position.y * randomYKoef, 
    			transform.position.z);

    		Instantiate (thePlatform, transform.position, transform.rotation);
    	}
         
    }
}
