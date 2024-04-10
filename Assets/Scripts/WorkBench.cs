using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkBench : MonoBehaviour
{   
    public GameObject[] shelf;
    public GameObject playerObj;
    private int i;
    private Player player;
    private bool interactTrigger;
    // Start is called before the first frame update
    void Start()
    {
        player = playerObj.GetComponent<Player>();
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (interactTrigger && Input.GetKeyDown(KeyCode.UpArrow) && player.getWood() >= 10)
        {
            player.updateWoodCount(-10);
            shelf[i].transform.position = this.transform.position;
            i++;
            
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
}
