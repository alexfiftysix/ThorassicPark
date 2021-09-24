using Characters.Brains;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// For unity reasons
// ReSharper disable once CheckNamespace
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
        // TODO: In v2021, can add && AssetDatabase.CanOpenAssetInEditor(brain.GetInstanceId())
        // Then add code up AiBrainView.PopulateView() to create a root node if one doesn't exist already
        // Doesn't exist in this version, so we can't add root node on open without errors
        if (Selection.activeObject is Brain brain)
        {
            _brainView.PopulateView(brain);
        }
    }

    private void OnNodeSelectionChanged(NodeView nodeView)
    {
        _inspectorView.UpdateSelection(nodeView);
    }
}