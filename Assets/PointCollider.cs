using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCollider : MonoBehaviour
{
    public bool isTouched = false;

    private void Awake()
    {
        Debug.Log("Created point " + this.name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isTouched = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isTouched = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isTouched = false;
    }
}
