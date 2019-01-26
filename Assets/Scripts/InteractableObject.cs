using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
	public bool isBurning { get; private set; }

	public string displayName = "Object";
	public Rigidbody rb { get; private set; }
	public MeshRenderer mR { get; private set; }
	private Material mat;

	public string audioEvent;

	public float burnSpeed = 0.5f;
	private float burnProgress = 0f;

    private void Start()
    {
		rb = GetComponent<Rigidbody>();
		mR = GetComponent<MeshRenderer>();
		mat = mR.material;
		gameObject.layer = 9;
    }

	private void Update()
	{
		if (isBurning)
		{
			burnProgress += Time.deltaTime * burnSpeed;
			//mat.SetVector("_DissolveValue", new Vector4(burnProgress,0f,0f,0f));
			mat.SetFloat("_DissolveValue", burnProgress);
		}
	}

	public void OnTriggerStay(Collider other)
	{
		if (!isBurning && other.CompareTag("FirePit") && GameController.instance.playerInteraction.objectInHand != this)
		{
			isBurning = true;
			GameController.instance.BoostHeat();
			Destroy(gameObject, 1f / burnSpeed);
		}
	}
}
