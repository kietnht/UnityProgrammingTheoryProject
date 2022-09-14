using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class TransferArea : MonoBehaviour
{
    public Material material;

    Renderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.sharedMaterial = material;
    }

    private void Update()
    {
        if(meshRenderer.sharedMaterial != material)
        {
            meshRenderer.sharedMaterial = material;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var receiver = other.gameObject.GetComponent<ColorAbsorber>();
        if (receiver == null) return;

        receiver.Receive(material);
    }
}
