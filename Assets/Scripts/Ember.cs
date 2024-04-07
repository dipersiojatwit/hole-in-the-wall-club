using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ember : MonoBehaviour
{   
    public float damage;
    public float speed;
    public float lifeTime;
    private float startX;
    private bool isDamaging;
    private bool canMove;
    private bool moveLeft;
    private bool moveRight;
    private Furniture furniture;
    // Start is called before the first frame update
    void Start()
    {   
        startX = this.transform.position.x;
        canMove = true;

        if (this.transform.position.x < 0)
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
        if (moveLeft && canMove)
        {
            this.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else if (moveRight && canMove)
        {
            this.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        if (isDamaging)
        {
            furniture.damage(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.CompareTag("Furniture"))
        {
            furniture = other.gameObject.GetComponent<Furniture>();
            isDamaging = true;
            canMove = false;
        }
        else
        {
            isDamaging = false;
        }

        if (other.CompareTag("LeftPillar"))
        {   
            moveLeft = false;
            moveRight = true;
        }

        if (other.CompareTag("RightPillar"))
        {   
            moveLeft = true;
            moveRight = false;
        }
    }
}
