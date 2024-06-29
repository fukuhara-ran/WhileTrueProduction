using System;

public static class LayerMaskUtils
{
    /// <summary>
    /// Check if a layer is included in the layer mask.
    /// </summary>
    /// <param name="layerMask"> The layer mask to check. </param>
    /// <param name="layer"> The layer to check. </param>
    /// <returns> True if the layer is included in the layer mask. </returns>
    public static bool HasLayer(int layerMask, int layer)
    {
        return (layerMask & (1 << layer)) != 0;
    }

    /// <summary>
    /// Add a layer to the layer mask.
    /// </summary>
    /// <param name="layerMask"> The layer mask to modify. </param>
    /// <param name="layer"> The layer to add. </param>
    /// <returns> The modified layer mask. </returns>
    public static int AddLayer(int layerMask, int layer)
    {
        return layerMask | (1 << layer);
    }

    /// <summary>
    /// Remove a layer from the layer mask.
    /// </summary>
    /// <param name="layerMask"> The layer mask to modify. </param>
    /// <param name="layer"> The layer to remove. </param>
    /// <returns> The modified layer mask. </returns>
    public static int RemoveLayer(int layerMask, int layer)
    {
        return layerMask & ~(1 << layer);
    }
}
