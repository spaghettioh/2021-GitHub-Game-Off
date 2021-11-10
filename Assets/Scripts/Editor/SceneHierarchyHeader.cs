using UnityEngine;
using UnityEditor;
// https://answers.unity.com/questions/1530509/what-is-the-best-way-to-draw-icons-in-unitys-hiera.html

[InitializeOnLoad]
public static class SceneHierarchyHeader
{
    static SceneHierarchyHeader()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
    }

    static void HierarchyWindowItemOnGUI(int instanceID, Rect hierarchyItemContainer)
    {
        var hierachyItem = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (hierachyItem != null && hierachyItem.name.StartsWith("//", System.StringComparison.Ordinal))
        {
            // Use standard RGB values to change the background color
            Vector3 containerRGB = new Vector3(32, 33, 36) / 255;

            EditorGUI.DrawRect(hierarchyItemContainer, new Color(containerRGB.x, containerRGB.y, containerRGB.z));
            EditorGUI.DropShadowLabel(hierarchyItemContainer, hierachyItem.name.Replace("/", "").ToUpperInvariant());
        }
    }
}
