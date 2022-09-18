using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAura : ColorAbsorber
{
    [SerializeField] GameObject auraVFX;
    [SerializeField] float auraTimeout = 5;

    GameObject currentAura;
    const string nameModifier = "Aura";

    public override void Receive(Material material)
    {
        base.Receive(material);
        if (currentAura != null) Destroy(currentAura);

        currentAura = Instantiate(auraVFX, transform);

        string auraFileName = material.name + nameModifier;
        var auraMaterial = Resources.Load<Material>("Materials/Colors/" + auraFileName);
        currentAura.GetComponentInChildren<Renderer>().material = auraMaterial;

        Destroy(currentAura, auraTimeout);
    }
}
