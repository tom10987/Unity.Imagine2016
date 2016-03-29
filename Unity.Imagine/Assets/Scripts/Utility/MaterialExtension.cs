using UnityEngine;
using System;
using System.Collections.Generic;

public enum BlendMode
{
    OPAQUE,
    CUTOUT,
    FADE,
    TRANSPARENT
}

public static class MaterialExtension
{
    static Dictionary<BlendMode, Action<Material>> _actions = null;

    static MaterialExtension()
    {
        CreateInstance();
    }

    public static void SetupMaterialWithBlendMode(this Material material, BlendMode mode)
    {
        _actions[mode](material);
    }

    static void CreateInstance()
    {
        if (_actions != null) return;
        _actions = new Dictionary<BlendMode, Action<Material>>();

        _actions.Add
            (
                BlendMode.OPAQUE,
                (Material material) =>
                {
                    material.SetFloat("_Mode", 0);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = -1;
                }
            );

        _actions.Add
            (
                BlendMode.CUTOUT,
                (Material material) =>
                {

                    material.SetFloat("_Mode", 1);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.EnableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 2450;

                }
            );

        _actions.Add
            (
                BlendMode.FADE,
                (Material material) =>
                {

                    material.SetFloat("_Mode", 2);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 3000;
                }
            );

        _actions.Add
            (
                BlendMode.TRANSPARENT,
                (Material material) =>
                {

                    material.SetFloat("_Mode", 3);
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 3000;
                }
            );
    }
}
