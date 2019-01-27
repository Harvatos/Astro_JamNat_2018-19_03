using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
	public bool isBurning { get; private set; }

	public string displayName = "Object";
	public Rigidbody rb { get; private set; }
	public MeshRenderer[] meshRenderer;
	public Material burningMaterial;

	public string audioEvent;
	public string subtitle;

	private float burnSpeed = 0.5f;
	private float burnProgress = 0f;

    private void Start()
    {
		rb = GetComponent<Rigidbody>();
		gameObject.layer = 9;
        meshRenderer = GetComponentsInChildren<MeshRenderer>();
    }

	private void Update()
	{
		if (isBurning)
		{
			burnProgress += Time.deltaTime * burnSpeed;
			// meshRenderer.material.SetFloat("_DissolveValue", burnProgress);
            foreach (MeshRenderer mr in meshRenderer)
            {
                // mr.material.SetFloat("_DissolveValue", burnProgress);
                foreach(Material m in mr.materials)
                {
                    m.SetFloat("_DissolveValue", burnProgress);
                }
            }
        }
	}

	public void OnTriggerStay(Collider other)
	{
		if (!isBurning && other.CompareTag("FirePit") && GameController.instance.playerInteraction.objectInHand != this)
		{
			isBurning = true;
			AkSoundEngine.PostEvent(audioEvent, GameController.instance.playerObject);
			GameController.instance.BoostHeat();
            //  meshRenderer.material = burningMaterial;
            foreach (MeshRenderer mr in meshRenderer)
            {
				//foreach (Material modMat in mr.materials)
				//{
				//	modMat = burningMaterial;
				//}
                mr.material = burningMaterial;
            }
            Destroy(gameObject, 1f / burnSpeed);
		}
	}
}
