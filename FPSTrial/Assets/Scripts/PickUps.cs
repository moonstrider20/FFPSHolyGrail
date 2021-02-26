using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        PlayerInput controller = other.GetComponent<PlayerInput>();

        if (controller != null)
        {
            if (controller.health < controller.maxHealth)
            {
                controller.health += 10;
                Destroy(gameObject);
            }
        }
    }
}
