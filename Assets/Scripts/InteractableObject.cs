using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
	public bool isBurning { get; private set; }

	public string displayName = "Object";
	public Rigidbody rb { get; private set; }

	public string audioEvent;

	public float burnSpeed = 0.5f;
	private float burnProgress = 0f;

    private void Start()
    {
		rb = GetComponent<Rigidbody>();
		gameObject.layer = 9;
    }

	private void Update()
	{
		if (isBurning)
		{
			burnProgress += Time.deltaTime * burnSpeed;
			if (burnProgress > 1f)
			{
				Destroy(gameObject);
			}
		}
	}

	public void OnTriggerStay(Collider other)
	{
		if (!isBurning && other.CompareTag("FirePit") && GameController.instance.playerInteraction.objectInHand != this)
		{
			isBurning = true;
			GameController.instance.BoostHeat();
		}
	}
}
