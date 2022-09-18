using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlink : ColorAbsorber
{
    [SerializeField] int blinkCount = 5;
    [SerializeField] float blinkTime = 0.2f;

    Material baseMaterial;
    Material receiveMaterial;

    private void Start()
    {
        baseMaterial = meshRenderer.material;
    }

    public override void Receive(Material material)
    {
        base.Receive(material);
        receiveMaterial = material;
        StartCoroutine(Blink());
    }

    private void RevertMaterial()
    {
        meshRenderer.material = baseMaterial;
    }

    private void ApplyMaterial()
    {
        meshRenderer.material = receiveMaterial;
    }

    IEnumerator Blink()
    {
        var count = blinkCount;
        var isUsingBase = false;
        while(count > 0)
        {
            yield return new WaitForSeconds(blinkTime);
            if(isUsingBase)
            {
                ApplyMaterial();
            } else
            {
                RevertMaterial();
            }
            isUsingBase = !isUsingBase;
            count--;
        }
        ApplyMaterial();
    }
}
