using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterInteraction : MonoBehaviour
{
	[Range(0.1f, 2f)]public float castRadius = 0.5f;
	[Range(0.1f, 5f)]public float castDistance = 1f;
	public LayerMask interactionLayers;
	public Transform objectHoldLocator;
	public TextMeshProUGUI objectNameDisplay;

	public InteractableObject objectInHand;

	private void Start()
    {
		objectNameDisplay.text = "";
		objectInHand = null;
    }

	private void Update()
	{
		if(Input.GetMouseButtonUp(0))
			{ Drop();				return; }
		else if(Input.GetMouseButton(0))
			{ GrabHoldUpdate();		return; }

		EmptyHandUpdate();
	}

	private void EmptyHandUpdate()
	{
		if (Physics.SphereCast(transform.position, castRadius, transform.forward, out RaycastHit hit, castDistance, interactionLayers))
		{
			InteractableObject iObj = hit.transform.GetComponent<InteractableObject>();
			objectNameDisplay.text = iObj.displayName;
			if (Input.GetMouseButtonDown(0))
			{
				Grab(iObj);
			}
		}
		else
		{
			objectNameDisplay.text = "";
		}
	}
	private void GrabHoldUpdate()
	{
		if (objectInHand)
		{
			objectNameDisplay.text = objectInHand.displayName;
			objectInHand.rb.MovePosition(objectHoldLocator.position);
		}
	}

	private void Drop()
	{
		Debug.Log("Drop!");
		if(objectInHand)
		{
			objectInHand.rb.useGravity = true;
			objectInHand = null;
		}

	}

	private void Grab(InteractableObject _object)
	{
		Debug.Log("Grab!");
		objectInHand = _object;
		objectInHand.rb.isKinematic = false;
		objectInHand.rb.useGravity = false;
	}
}
