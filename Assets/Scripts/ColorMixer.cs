using UnityEditor;
using UnityEngine;

public class ColorMixer : ColorAbsorber
{
    public bool haveColor = false;

    GameObject otherObject;
    ColorAbsorber otherAbsorber;

    private void Update()
    {
        if (otherObject == null) return;

    }

    public override void Receive(Material material)
    {
        if(!haveColor)
        {
            base.Receive(material);
            haveColor = true;
        } else
        {
            Color mixedColor = MixColor(meshRenderer.material.color, material.color);
            meshRenderer.material.color = mixedColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        otherObject = other.gameObject;
        var player = other.gameObject.GetComponent<PlayerController>();
        otherAbsorber = player.ColorAbsorber;
        print(other.gameObject.name);
        if (otherAbsorber == null) return;
        Receive(otherAbsorber.GetMaterial());
    }

    private void OnTriggerExit(Collider other)
    {
        otherObject = null;
    }

    struct RYBObject 
    {
        public float r;
        public float y;
        public float b;
    }

    struct RGBObject
    {
        public float r;
        public float g;
        public float b;
    }

    RYBObject RGBToRYB(RGBObject rgb)
    {
        var r = rgb.r; var g = rgb.g; var b = rgb.b;
        var iw = Mathf.Min(r, g, b);
        var ib = Mathf.Min(1 - r, 1 - g, 1 - b);
        r -= iw; g -= iw; b -= iw;
        var minRG = Mathf.Min(r, g);
        var rnew = r - minRG; var ynew = (g + minRG) / 2; var bnew = (b + g - minRG) / 2;
        print($"[{rnew}, {ynew}, {bnew}] [{r}, {g}, {b}]");
        float n = Mathf.Max(rnew, ynew, bnew, 1) / Mathf.Max(r, g, b, 1);

        print("N " + n);
        var test = new RYBObject { r = (rnew / n) + ib, y = (ynew / n) + ib, b = (bnew / n) + ib };
        print($"TEST [{test.r},{test.y},{test.b}]");

        return new RYBObject { r = (rnew / n) + ib, y = (ynew / n) + ib, b = (bnew / n) + ib };
    }

    RGBObject RYBToRGB(RYBObject ryb)
    {
        var r = ryb.r; var y = ryb.y; var b = ryb.b;
        var iw = Mathf.Min(r, y, b);
        var ib = Mathf.Min(1 - r, 1 - y, 1 - b);
        r -= iw; y -= iw; b -= iw;
        var minYB = Mathf.Min(y, b);
        var rnew = r + y - minYB; var gnew = y + (2 * minYB); var bnew = 2 * (b - minYB);
        float n = Mathf.Max(rnew, gnew, bnew, 1) / Mathf.Max(r, y, b, 1);

        if (n == float.NaN) return new RGBObject { r = 1, g = 1, b = 1 };
        print($"[{rnew}, {gnew}, {bnew}] [{r}, {y}, {b}]");
        print("N2 " + n);

        var test = new RGBObject { r = (rnew / n) + ib, g = (gnew / n) + ib, b = (bnew / n) + ib };
        print($"TEST2 [{test.r},{test.g},{test.b}]");

        return new RGBObject { r = (rnew / n) + ib, g = (gnew / n) + ib, b = (bnew / n) + ib };
    }

    RYBObject MixColor(RYBObject color1, RYBObject color2)
    {
        var test = new RYBObject { r = Mathf.Min(1, color1.r + color2.r), y = Mathf.Min(1, color1.y + color2.y), b = Mathf.Min(1, color1.b + color2.b) };
        print($"MIXXXX [{test.r},{test.y},{test.b}]");
        return new RYBObject { r = Mathf.Min(1, color1.r + color2.r), y = Mathf.Min(1, color1.y + color2.y), b = Mathf.Min(1, color1.b + color2.b) };
        //return new RYBObject { r = color1.r + color2.r, y = color1.y + color2.y, b = color1.b + color2.b };
    }

    RGBObject MixColor(RGBObject color1, RGBObject color2)
    {
        var c1 = RGBToRYB(color1);
        var c2 = RGBToRYB(color2);
        var mix = MixColor(c1, c2);
        var midx2 = RYBToRGB(mix);
        return RYBToRGB(MixColor(RGBToRYB(color1), RGBToRYB(color2)));
    }

    Color MixColor(Color color1, Color color2)
    {
        RGBObject c1 = ColorToRGB(color1);
        RGBObject c2 = ColorToRGB(color2);
        RGBObject mixed = MixColor(c1, c2);
        print($"Mixed {mixed.r} {mixed.g} {mixed.b}");
        return new Color(mixed.r, mixed.g, mixed.b);
    }

    RGBObject ColorToRGB(Color color)
    {
        //int r = (int)(color.r * 255); int g = (int)(color.g * 255); int b = (int)(color.b * 255);
        return new RGBObject { r = color.r, g = color.g, b = color.b };
    }
}