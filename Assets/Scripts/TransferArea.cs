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
        var player = other.gameObject.GetComponent<PlayerController>();
        if (player == null) return;

        player.ColorAbsorber.Receive(material);
    }
}
