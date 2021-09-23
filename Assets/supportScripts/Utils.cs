using UnityEngine;

public static class Utils
{
    public static bool IsInLayerMask(GameObject obj, LayerMask layer) => ((layer.value & (1 << obj.layer)) > 0);
}