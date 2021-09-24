using System;
using System.Threading.Tasks;
using Characters.Brains;
using Characters.Brains.BrainStates;
using Characters.Brains.Transitions;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
// Namespaces don't play nice with the UI editor
public sealed class NodeView : Node
{
    public BrainNode node;
    public Port input;
    public Port output;
    public Action<NodeView> onNodeSelected;

    public NodeView(BrainNode node) : base("Assets/DevTools/Editor/NodeView.uxml")
    {
        this.node = node;
        title = GetName(node);
        viewDataKey = node.guid;

        style.left = node.position.x;
        style.top = node.position.y;

        CreateInputPorts();
        CreateOutputPorts();
        SetupClasses();

        // Bind description tag
        var descriptionLabel = this.Q<Label>("description"); // name of UI element
        descriptionLabel.bindingPath = "description";  // name of property on BrainNode class
        if (node.description == string.Empty) node.description = "description";
        descriptionLabel.Bind(new SerializedObject(node));

        async void Callback(ChangeEvent<string> evt) => await UpdateName(evt.newValue);
        descriptionLabel.RegisterValueChangedCallback(Callback);

        var extrasContainer = this.Q<IMGUIContainer>("extras-container");
        node.AddExtras(extrasContainer);
    }

    private DateTime _lastTyped = DateTime.MinValue;
    private async Task UpdateName(string newName)
    {
        var myStartTime = DateTime.Now;
        _lastTyped = myStartTime;

        // We want to debounce the input, because SaveAssetIfDirty is intensive and can hang the UI if called to frequently
        await Task.Delay(500);

        if (_lastTyped != myStartTime) return;

        node.name = newName;
        AssetDatabase.SaveAssetIfDirty(node);
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        node.position = new Vector2(newPos.xMin, newPos.yMin);
    }

    public override void OnSelected()
    {
        base.OnSelected();
        onNodeSelected?.Invoke(this);
    }

    private void SetupClasses()
    {
        switch (node)
        {
            case RootNode _:
                AddToClassList("root");
                break;
            case BrainState _:
                AddToClassList("state");
                break;
            case BrainTransition _:
                AddToClassList("transition");
                break;
        }
    }

    private void CreateInputPorts()
    {
        if (!(node is RootNode))
        {
            // typeof(bool) is not used. It's a placeholder for nothing (unfortunately void/null is not allowed)
            input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));    
        }

        if (input != null)
        {
            input.portName = string.Empty;
            inputContainer.Add(input);
        }
    }

    private void CreateOutputPorts()
    {
        // typeof(bool) is not used. It's a placeholder for nothing (unfortunately void/null is not allowed)
        output = node switch
        {
            BrainState _ => InstantiatePort(
                Orientation.Horizontal,
                Direction.Output,
                Port.Capacity.Multi,
                typeof(bool)),
            BrainTransition _ => InstantiatePort(
                Orientation.Horizontal,
                Direction.Output,
                Port.Capacity.Single,
                typeof(bool)),
            RootNode _ => InstantiatePort(
                Orientation.Horizontal,
                Direction.Output,
                Port.Capacity.Single,
                typeof(bool)),
            _ => output
        };

        output.portName = string.Empty; // otherwise name is "Boolean", which takes up space and is misleading
        outputContainer.Add(output);
    }

    private static string GetName(BrainNode aNode)
    {
        return aNode switch
        {
            BrainState _ => "State",
            BrainTransition _ => "Transition",
            RootNode _ => "Root",
            _ => "No Name"
        };
    }
}