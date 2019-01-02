using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    public GameObject player;
    [Range(0, 0.5f)]
    public float XDeadzone;
    [Range(0, 0.5f)]
    public float YDeadzone;

    public float smoothTime = 0.15f;

    public float YMaxValue = 0;

    public float YMinValue = 0;


    public float XMaxValue = 0;

    public float XMinValue = 0;

    private float XDistanceWithCamera
    {
        get
        {
            return Mathf.Abs(player.transform.position.x - transform.position.x);
        }
        
    }
    private float YDistanceWithCamera
    {
        get
        {
            return Mathf.Abs(player.transform.position.y - transform.position.y);
        }

    }

    Vector3 velocity = Vector3.zero;

    private bool moveCam = false;
    // Use this for initialization
    void Start () {
		
	}
    private void FixedUpdate()
    {
        Vector3 targetposition = player.transform.position;
        targetposition.y = Mathf.Clamp(targetposition.y, YMinValue, YMaxValue);
        targetposition.x = Mathf.Clamp(targetposition.x, XMinValue, XMaxValue);
        targetposition.z = transform.position.z;
        if(moveCam)
        {
           
                transform.position = Vector3.SmoothDamp(transform.position, targetposition, ref velocity, smoothTime);
          
            
            if(Mathf.Abs(transform.position.x - targetposition.x) < 0.01f &&
                Mathf.Abs(transform.position.y - targetposition.y) < 0.01f)
            {
                moveCam = false;
               
            }
        }
    }
    // Update is called once per frame
    void Update () {
        if( moveCam == false && ((XDistanceWithCamera > XDeadzone) || (YDistanceWithCamera < YDeadzone)))
        {
            
            moveCam = true;
        }
	}
}
