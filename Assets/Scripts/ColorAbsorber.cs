using System.Collections.Generic;
using UnityEngine;

public class ColorAbsorber : MonoBehaviour
{
    protected Renderer meshRenderer;

    public Color GetColor()
    {
        return meshRenderer.material.color;
    }

    public Material GetMaterial()
    {
        return meshRenderer.material;
    }

    private void Start()
    {
        meshRenderer = GetComponentInChildren<Renderer>();
    }

    public virtual void Receive(Material material)
    {
        meshRenderer.material = material;
    }
}