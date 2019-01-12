using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerAttackposition;
    public AnimationCurve speedanimationcurve;
    public float speed;
    [Range(1, 10)]
    public int jumpspeed;
    private float localspeed = 0;
    private float localjumpspeed = 0;
    private float jumptime = 0.6f;
    private int jumpcount = 2;
    private bool jumped
    {
        get { return GetComponent<Animator>().GetBool("isjumped"); }

    }
    private bool isattacked
    {
        get
        {
            return
this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
        }

    }
    public bool triggerattacked;

    private bool isright = true;
    

    // Use this for initialization
    void Start()
    {
        triggerattacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        float botdistance = this.GetComponent<CapsuleCollider2D>().bounds.extents.y - this.GetComponent<CapsuleCollider2D>().offset.y;

        if (!jumped)
        {
            jumpcount = 2;
        }
        if(triggerattacked && isattacked)
        {
            
                triggerattacked = false;
            
        }
        if (!jumped && !triggerattacked && Input.GetKeyDown(KeyCode.J))
        {

            GetComponent<Animator>().SetInteger("state", 4);
            triggerattacked = true;
            localspeed = 0;
        }
        if (!triggerattacked)
        {


            if (Input.GetKey(KeyCode.D))
            {
                //transform.GetComponent<SpriteRenderer>().flipX = false;
                if (!isright)
                    flip();
              
                localspeed = speed;
                if (!jumped || (GetComponent<Animator>().GetInteger("state") != 3 && GetComponent<Animator>().GetInteger("state") != 2))
                    GetComponent<Animator>().SetInteger("state", 1);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                // transform.GetComponent<SpriteRenderer>().flipX = true;
                if(isright)
                    flip();
                   
             
                localspeed = -speed;
                if (!jumped || (GetComponent<Animator>().GetInteger("state") != 3 && GetComponent<Animator>().GetInteger("state") != 2))
                    GetComponent<Animator>().SetInteger("state", 1);
            }
            else
            {
                transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                localspeed = 0;
                if (!jumped)
                {

                    GetComponent<Animator>().SetInteger("state", 0);
                }

            }
            if (Input.GetKeyDown(KeyCode.Space) && jumpcount != 0)
            {
                localjumpspeed = jumpspeed;
                jumptime = 0;
                jumpcount--;
                GetComponent<Animator>().SetInteger("state", 2);
                GetComponent<Animator>().SetBool("isjumped", true);
            }
        }

    }

    private void FixedUpdate()
    {
        if (jumped)
        {
            if (jumptime < 0.5f)
            {
                jumptime += Time.fixedDeltaTime;
            }
            else
            {
                GetComponent<Animator>().SetInteger("state", 3);

            }
            localjumpspeed *= speedanimationcurve.Evaluate(jumptime);
        }
        else
            localjumpspeed = 0;
        transform.GetComponent<Rigidbody2D>().velocity = new Vector2(localspeed, localjumpspeed);

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {


        float botdistance = this.GetComponent<CapsuleCollider2D>().bounds.extents.y - this.GetComponent<CapsuleCollider2D>().offset.y + 0.01f;

        if (Physics2D.Raycast(transform.position, -Vector2.up, botdistance, 1 << 9))
        {
            // Debug.Log(true);
            GetComponent<Animator>().SetBool("isjumped", false);
        }

    }
    void flip()
    {
        isright = !isright;
        transform.Rotate(Vector3.up * 180);
    }
}
