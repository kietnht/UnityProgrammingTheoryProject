using System.Collections.Generic;
using UnityEngine;

public class ColorAbsorber : MonoBehaviour
{
    protected Renderer meshRenderer;

    // ENCAPSULATION
    public Color GetColor()
    {
        return meshRenderer.material.color;
    }

    // ENCAPSULATION
    public Material GetMaterial()
    {
        return meshRenderer.material;
    }

    private void Awake()
    {
        meshRenderer = GetComponent<Renderer>();
    }

    public virtual void Receive(Material material)
    {
        meshRenderer.material = material;
    }
}