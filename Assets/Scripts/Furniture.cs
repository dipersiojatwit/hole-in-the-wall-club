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
    private int startHealth;
    // Start is called before the first frame update
    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        player = playerObj.GetComponent<Player>();
        animator = GetComponent<Animator>();
        startHealth = health;

    }

    // Update is called once per frame
    void Update()
    {
        if (interactTrigger && Input.GetKeyDown(KeyCode.UpArrow))
        {
            player.isGrabbing(true);
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
            player.isGrabbing(false);

        }

        if (health <= startHealth / 2)
        {
            animator.SetBool("isDamaged", true);
        }

        if (health <= startHealth / 4)
        {
            animator.SetBool("isDamaged", false);
            animator.SetBool("isVeryDamaged", true);
        }

        if (this.health <= 1)
        {
            Destroy(this.gameObject);
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

    public bool damage(int damage)
    {
        this.health -= damage;
        return health < 5;

    }

    public int getHealth()
    {
        return health;
    }

}
