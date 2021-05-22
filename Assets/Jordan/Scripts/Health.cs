using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Item
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<SimplePlayerMove>().Health += value;
            Destroy(gameObject);
            Debug.Log("colliding");
        }
    }
}
