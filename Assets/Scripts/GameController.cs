using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public static GameController instance;

	[Range(-1f, 2f)] public float temperature;
	private float perceivedTemperature;
	public float temperatureLossSpeed = -0.1f; 

    private void Start()
    {
		instance = this;
    }

	private void Update()
    {
        
    }
}
