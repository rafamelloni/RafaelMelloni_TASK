using UnityEngine;

public static class UIPositioning
{
    private static readonly Vector3[] Corners = new Vector3[4];

    public static void PlaceLeftOf(RectTransform panel, RectTransform slot, float spacing)
    {
        slot.GetWorldCorners(Corners);

        Vector3 position = Corners[1];
        position.x -= spacing * panel.lossyScale.x;
        panel.position = position;

        ClampToScreen(panel);
    }

    private static void ClampToScreen(RectTransform panel)
    {
        panel.GetWorldCorners(Corners);

        Vector3 offset = Vector3.zero;

        if (Corners[0].x < 0f)
            offset.x = -Corners[0].x;

        if (Corners[0].y < 0f)
            offset.y = -Corners[0].y;
        else if (Corners[1].y > Screen.height)
            offset.y = Screen.height - Corners[1].y;

        panel.position += offset;
    }
}