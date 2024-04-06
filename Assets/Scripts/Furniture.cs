using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Furniture : MonoBehaviour
{   
    public int health;
    public Rigidbody2D rigidBod;
    public GameObject playerObj;
    private Animator animator;
    private Player player;
    public Rigidbody2D baseRigidBod;
    private HingeJoint2D hinge;
    private bool interactTrigger;
    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        player = playerObj.GetComponent<Player>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (interactTrigger && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("interact");
            if (hinge.connectedBody != rigidBod)
            {
                hinge.connectedBody = rigidBod;
                player.lockJump(false);
                
            }
            
        }
        if (hinge.connectedBody == rigidBod && Input.GetKeyDown(KeyCode.DownArrow))
        {
            hinge.connectedBody = baseRigidBod;
            player.lockJump(true);

        }

        if (this.health <= health / 2 && !animator.GetBool("isVeryDamaged"))
        {
            animator.SetBool("isDamaged", true);
        }

        if (this.health <= health / 4)
        {
            animator.SetBool("isDamaged", false);
            animator.SetBool("isVeryDamaged", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.CompareTag("Player"))
        {   
            interactTrigger = true;
            
        }
       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactTrigger = false;
        }

    }

    public void damage(int damage)
    {
        this.health -= damage;

        if (this.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
