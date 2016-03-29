using System;
using UnityEngine;

public class Grayscale : ImageEffectBase
{
    public Texture textureRamp;

    [Range(-1.0f, 1.0f)]
    public float rampOffset;

    // Called by camera to apply image effect
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetTexture("_RampTex", textureRamp);
        material.SetFloat("_RampOffset", rampOffset);
        Graphics.Blit(source, destination, material);
    }
}
