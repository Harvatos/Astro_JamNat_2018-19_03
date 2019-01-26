using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
	public string displayName = "Object";
	public Rigidbody rb { get; private set; }	

    private void Start()
    {
		rb = GetComponent<Rigidbody>();
		gameObject.layer = 9;
    }
}
