using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicProvider : Graphic
{
    [SerializeField] private List<Graphic> graphics = new List<Graphic>();

    public List<Graphic> Graphics => graphics;

    public override void SetAllDirty() => graphics?.ForEach(g => g.SetAllDirty());
    public override void SetLayoutDirty() => graphics?.ForEach(g => g.SetLayoutDirty());
    public override void SetVerticesDirty() => graphics?.ForEach(g => g.SetVerticesDirty());
    public override void SetMaterialDirty() => graphics?.ForEach(g => g.SetMaterialDirty());

    public override void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha) =>
        graphics?.ForEach(g => g.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha));

    public override void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha,
        bool useRGB) =>
        graphics?.ForEach(g => g.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha, useRGB));

    public override void CrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale) =>
        graphics?.ForEach(g => g.CrossFadeAlpha(alpha, duration, ignoreTimeScale));

    public override void SetNativeSize() => graphics?.ForEach(g => g.SetNativeSize());
    public override bool Raycast(Vector2 sp, Camera eventCamera) => false;
    public override void OnCullingChanged() => graphics?.ForEach(g => g.OnCullingChanged());
    public override void Rebuild(CanvasUpdate update) => graphics?.ForEach(g => g.Rebuild(update));
    public override void LayoutComplete() => graphics?.ForEach(g => g.LayoutComplete());
    public override void GraphicUpdateComplete() => graphics?.ForEach(g => g.GraphicUpdateComplete());
#if UNITY_EDITOR
    public override void OnRebuildRequested() => graphics?.ForEach(g => g.OnRebuildRequested());
#endif
}