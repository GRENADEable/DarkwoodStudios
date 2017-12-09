using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeKey : MonoBehaviour {

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            GameVariables.Axe += 1;
            Destroy(gameObject);
        }
    }
}
