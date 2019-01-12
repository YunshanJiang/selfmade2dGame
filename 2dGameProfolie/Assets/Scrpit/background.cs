using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour {

    private float length
    {
        get;
        set;
    }

    public GameObject player;
    public GameObject Camera;
    private Vector3 movevelocity = Vector3.zero;
	// Use this for initialization
	void Start () {
        //Debug.Log(GetComponent<SpriteRenderer>().bounds.max.x - GetComponent<SpriteRenderer>().bounds.min.x);
        length = GetComponent<SpriteRenderer>().bounds.max.x - GetComponent<SpriteRenderer>().bounds.min.x;
    }


    private void FixedUpdate()
    {
        
            //float targetx = (player.transform.position.x / (13.12f + (length / 2))) * 13.12f;
            //Debug.Log(targetx);
            Vector3 newposition = new Vector3(Camera.transform.position.x, transform.position.y,transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, newposition,ref movevelocity,0);
        

    }


    // Update is called once per frame
    void Update () {
		
	}

   
}
