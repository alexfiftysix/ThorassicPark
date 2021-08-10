using UnityEditor;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
// Don't want the nesting in the editor that the namespace brings
// TODO: Can we manually set the path, so we can get our nice namespace back?
public class InspectorView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<InspectorView, UxmlTraits>
    {
    }

    private Editor _editor;

    public InspectorView()
    {
    }

    public void UpdateSelection(NodeView nodeView)
    {
        Clear();
        UnityEngine.Object.DestroyImmediate(_editor);

        _editor = Editor.CreateEditor(nodeView.state);
        var container = new IMGUIContainer(() => _editor.OnInspectorGUI() );
        Add(container);
    }
}