using Characters.Brains;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class AiBrainEditor : EditorWindow
{
    private AiBrainView _brainView;
    private InspectorView _inspectorView;
    
    [MenuItem("Brains/AiBrainEditor")]
    public static void OpenWindow()
    {
        var wnd = GetWindow<AiBrainEditor>();
        wnd.titleContent = new GUIContent("AiBrainEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        var root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/DevTools/Editor/AiBrainEditor.uxml");
        visualTree.CloneTree(root);

        // A stylesheet can be added to a VisualElement.
        // The style will be applied to the VisualElement and all of its children.
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/DevTools/Editor/AiBrainEditor.uss");
        root.styleSheets.Add(styleSheet);

        _brainView = root.Q<AiBrainView>();
        _inspectorView = root.Q<InspectorView>();
        _brainView.onNodeSelected = OnNodeSelectionChanged;

        OnSelectionChange();
    } 

    private void OnSelectionChange()
    {
        Debug.Log($"Selection changed! ${Selection.activeObject}");
        var brain = Selection.activeObject as Brain;
        if (brain)
        {
            _brainView.PopulateView(brain);
        }
    }

    private void OnNodeSelectionChanged(NodeView nodeView)
    {
        _inspectorView.UpdateSelection(nodeView);
    }
}