using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class ColorExplosion : ColorAbsorber
{
    public GameObject explodeVFX;

    Queue<Material> colorMaterialQueue = new Queue<Material>();

    // POLYMORPHISM
    public override void Receive(Material material)
    {
        base.Receive(material);
        colorMaterialQueue.Enqueue(material);
        StartCoroutine(ExplodeColor());
    }

    IEnumerator ExplodeColor()
    {
        yield return new WaitForSeconds(3);
        
        var vfx = Instantiate(explodeVFX, transform);
        var colorMaterial = colorMaterialQueue.Dequeue();
        foreach (var child in vfx.GetComponentsInChildren<ParticleSystem>())
        {
            ParticleSystem.MainModule main = child.main;
            main.startColor = colorMaterial.color;
        }
        meshRenderer.material = colorMaterial;
    }
}
