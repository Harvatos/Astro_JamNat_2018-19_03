using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterInteraction : MonoBehaviour
{	
	[Range(0.1f, 2f)]public float castRadius = 0.5f;
	[Range(0.1f, 5f)]public float castDistance = 1f;
	public LayerMask interactionLayers;
	public TextMeshProUGUI objectNameDisplay;

	public InteractableObject objectInHand;

	public Transform holdPosition;

	private float holdLerp = 0f;

	private void Start()
    {
		objectNameDisplay.text = "";
		objectInHand = null;
    }

	private void Update()
	{
		
		if(Input.GetMouseButtonUp(0))
			{ Drop();				return; }
		else if(Input.GetMouseButtonDown(0))
			{ EmptyHandUpdate();	return; }
		else if(Input.GetMouseButton(0))
			{ GrabHoldUpdate();		return; }

		EmptyHandUpdate();
	}

	private void LateUpdate()
	{
		if (objectInHand)
		{
			objectInHand.transform.position = Vector3.Lerp(objectInHand.transform.position, holdPosition.position, holdLerp);
			objectInHand.transform.rotation = Quaternion.Lerp(objectInHand.transform.rotation, holdPosition.rotation, holdLerp);
		}
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
			holdLerp = Mathf.Clamp01(holdLerp + Time.deltaTime * 2f);
		}
	}

	private void Drop()
	{
		Debug.Log("Drop!");
		if(objectInHand)
		{
			objectInHand.rb.useGravity = true;
			objectInHand.rb.isKinematic = false;
			objectInHand = null;
		}
	}

	private void Grab(InteractableObject _object)
	{
		Debug.Log("Grab!");
		holdLerp = 0f;
		objectInHand = _object;
		objectInHand.rb.isKinematic = true;
		objectInHand.rb.useGravity = false;
		holdPosition.rotation = objectInHand.transform.rotation;
	}
}
