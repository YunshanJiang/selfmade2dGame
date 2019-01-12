using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerMovement Playermovement;
    public LayerMask enemeymask;
    private bool triggerattacked
    {
        get
        {
            if (Playermovement != null)
                return Playermovement.triggerattacked;
            else
                return false;
        }
    }
    private float attacntrange = 0.15f;
    private Transform attackposition
    {
        get
        {
            if (Playermovement != null)
                return Playermovement.playerAttackposition;
            else
                return null;
        }
       
    }
    private Collider2D[] contactobjE = new Collider2D[] { };
    // Use this for initialization

    void Start()
    {

        Playermovement = this.GetComponent<PlayerMovement>();

    }

    //Update is called once per frame

    void Update()
    {
        
    if (triggerattacked)
        {
           contactobjE = Physics2D.OverlapCircleAll(attackposition.position, attacntrange, enemeymask);
            for(int i = 0; i < contactobjE.Length; i++)
            {
                if (!contactobjE[i].GetComponent<Enemy>().beingattaked)
                {
                    contactobjE[i].GetComponent<Enemy>().beingattaked = true;
                    Debug.Log(contactobjE[i].transform.name);

                }
            }
        }
       else if(contactobjE.Length != 0)
        {
            for (int i = 0; i < contactobjE.Length; i++)
            {
                contactobjE[i].GetComponent<Enemy>().beingattaked = false;
            }
            contactobjE = new Collider2D[] { };
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
       
        Gizmos.DrawWireSphere(attackposition.position,attacntrange);
    }
}
