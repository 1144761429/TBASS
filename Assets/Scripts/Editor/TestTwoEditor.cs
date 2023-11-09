#if false
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;



[CustomEditor(typeof(TestTwo))]
public class TestTwoEditor : Editor
{
    [SerializeField] private VisualTreeAsset visualTree;
    
    
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement inspector = new VisualElement();
        
        inspector.Add(visualTree.CloneTree());
        return inspector;
    }
}

#endif