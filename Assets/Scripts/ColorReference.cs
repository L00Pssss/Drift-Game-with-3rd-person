using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorReference : MonoBehaviour
{
    [SerializeField] Material _carMaterial;

    public void SetMaterial(Color color)
    {
        _carMaterial.color = color;
    }
}
