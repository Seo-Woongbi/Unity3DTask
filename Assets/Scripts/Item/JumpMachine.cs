using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMachine : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField] float jumpPower;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up *jumpPower, ForceMode.Impulse);
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
       if(collision.gameObject.TryGetComponent(out Player player))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up *jumpPower, ForceMode.Impulse);
        }
    }*/
}
