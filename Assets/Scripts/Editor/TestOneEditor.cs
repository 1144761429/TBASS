#if false
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

[CustomEditor(typeof(Test))]
public class TestOneEditor : Editor
{
    [SerializeField] private VisualTreeAsset visualTree;
    private ToolbarButton addButton;

    
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement inspector = new VisualElement();
        
        inspector.Add(visualTree.CloneTree());
        addButton.RegisterCallback(SaySTH);
        inspector.Add(addButton);
        return inspector;
    }
    
    private static void SaySTH(EventCallback<Button.ButtonClickedEvent> e)
    {
        Debug.Log("Hi there");
    }
}

#endif
