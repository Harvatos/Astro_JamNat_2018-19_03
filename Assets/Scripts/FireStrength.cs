using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStrength : MonoBehaviour
{

    public float fireStrength;

    public Light fireLight;
    public Transform flames;

    // Update is called once per frame
    void Update()
    {
        changeScale();
        changeLight();
    }

    void changeScale()
    {
        flames.localScale = new Vector3(fireStrength, fireStrength, fireStrength);
    }

    void changeLight()
    {
        fireLight.intensity = Mathf.Lerp(10f, 500f, fireStrength * 0.5f);
    }
}
