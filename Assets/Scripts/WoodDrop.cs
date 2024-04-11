using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodDrop : MonoBehaviour
{
    public ParticleSystem pickUp;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {   
            Vector3 pos = this.transform.position;
            Instantiate(pickUp, pos, Quaternion.identity);
            other.gameObject.GetComponent<Player>().updateWoodCount(1);
            Destroy(this.gameObject);
        }
    }
}


