using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ant : MonoBehaviour
{   
    public float speed;
    private float startX;
    private bool moveLeft;
    private bool moveRight;
    private bool moveUp;
    private bool indicatorBypass;
    private Rigidbody2D rigidBod;
    private Animator animator;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        rigidBod = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        startX = this.transform.position.x;

        indicatorBypass = Random.Range(0, 100) < 50;

        if (startX < 0)
        {
            moveRight = true;
        }
        else
        {
            moveLeft = true;
        }
    }

    // Update is called once per frame
    void Update()
    {   

        if (moveLeft)
        {   
            sprite.flipX = false;
            this.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else if (moveRight)
        {   
            sprite.flipX = true;
            this.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (moveUp)
        {   
            rigidBod.gravityScale = 0;
            this.transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else
        {
            rigidBod.gravityScale = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ant"))
        {
            if (moveLeft)
            {
                moveLeft = false;
                moveRight = true;
            }
            else
            {
                moveRight = false;
                moveLeft = true;
            }
        }

        if (other.CompareTag("RightPillar") || other.CompareTag("LeftPillar"))
        {   
            moveLeft = false;
            moveRight = false;
            moveUp = true;

        }

        if (other.CompareTag("PlatformIndicator") && !indicatorBypass)
        {   
            if (this.transform.position.x < 0)
            {
                moveUp = false;
                moveLeft = false;
                moveRight = true;
            }
            else
            {
                moveUp = false;
                moveRight = false;
                moveLeft = true;
            }
            
        }

        if (other.CompareTag("PlatformIndicator2"))
        {   
            if (this.transform.position.x < 0)
            {
                moveUp = false;
                moveLeft = false;
                moveRight = true;
            }
            else
            {
                moveUp = false;
                moveRight = false;
                moveLeft = true;
            }
            
        }

        if (other.CompareTag("JumpPoint"))
        {   
            animator.SetBool("isJump", true);
            
            if (Random.Range(0, 100) <= 50 && !indicatorBypass)
            {
                rigidBod.AddForce(new Vector2(30, 5f), ForceMode2D.Impulse);

            }
            else
            {   
                rigidBod.AddForce(new Vector2(55, 2.5f), ForceMode2D.Impulse);
                
            }
        }

        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().damage(1);
        }
        
    }

    void onEndJump()
    {
        animator.SetBool("isJump", false);
    }
}
