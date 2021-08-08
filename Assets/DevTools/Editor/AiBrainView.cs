using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
// Don't want the nesting in the editor that the namespace brings
public class AiBrainView : GraphView
{
    public new class UxmlFactory : UxmlFactory<AiBrainView, UxmlTraits>
    {
    }

    public AiBrainView()
    {
        Insert(0, new GridBackground());
        
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/DevTools/Editor/AiBrainEditor.uss");
        styleSheets.Add(styleSheet);
    }
}