using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : MonoBehaviour
{
	[SerializeField]
    private GameObject afterImagePrefab;
    private Queue<GameObject> availableObects = new Queue<GameObject>();

    public static PlayerAfterImagePool Instance {get; private set;}

    public void Awake(){
    	Instance = this;
    	GrowPool();
    }

    private void GrowPool(){
    	for (int i = 0; i < 10; i++) {
    		var instanceToAdd = Instantiate(afterImagePrefab);
    		instanceToAdd.transform.SetParent(transform);
    		AddToPool(instanceToAdd);
    	}
    }

    public void AddToPool(GameObject instance) {
    	instance.SetActive(false);
    	availableObects.Enqueue(instance);
    }

    public GameObject GetFromPool(){
    	if(availableObects.Count == 0) {
    		GrowPool();
    	}

    	var instance = availableObects.Dequeue();
    	instance.SetActive(true);

    	return instance;
    }
}
