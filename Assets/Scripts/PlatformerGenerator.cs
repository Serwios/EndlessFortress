using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerGenerator : MonoBehaviour {
    public List<GameObject> gameObjects = new List<GameObject>();

	public Transform generationPoint;
	public float distanceBetween;

	private float platformWidth;
    public float distanceBetweenMin;
    public float distanceBetweenMax;

	private float randomYKoef;
	private System.Random random = new System.Random();
    private int randomElementIndex;

    private float tempdistanceBetweenMax;
    private float tempdistanceBetweenMin;

    void Start() {
        platformWidth = gameObjects[0].GetComponent<BoxCollider2D>().size.x;
    }

    void Update() {
    	if(transform.position.x < generationPoint.position.x) {
            randomYKoef = (float) Random.Range(-0.3f, 0.3f);
            randomElementIndex = Random.Range(0, gameObjects.Count);
            GameObject selectedObject = gameObjects[randomElementIndex];
         
    		distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
            transform.position = new Vector3(
    		  (transform.position.x + platformWidth + distanceBetween), 
    		  transform.position.y + randomYKoef, 
    		  transform.position.z);

            Debug.Log("x:"+transform.position.x + ", y:"+transform.position.y + ", plw:"+platformWidth + ", db:"+distanceBetween + ", z:"+transform.position.z + ", rk: "+randomYKoef);
            Debug.Log("distance: " + (int) platformWidth + distanceBetween);

    
    		Instantiate (selectedObject, transform.position, transform.rotation);
    	}
    }
}
