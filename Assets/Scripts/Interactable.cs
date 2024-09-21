﻿using UnityEngine;

public class Interactable : MonoBehaviour
{
    public virtual void OnInteract()
    {
        Debug.Log("Interacted with " + gameObject.name);
        // Add your interaction logic here
    }
}
