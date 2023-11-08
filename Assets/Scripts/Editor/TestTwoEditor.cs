using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

#if true


[CustomEditor(typeof(TestTwo))]
public class TestTwoEditor : Editor
{
    [SerializeField] private VisualTreeAsset visualTree;
    
    
    public override VisualElement CreateInspectorGUI()
    {
        VisualElement inspector = new VisualElement();
        
        inspector.Add(visualTree.CloneTree());
        var temp = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("f8f28cfb858bfbd4ea87437beb444a74");
        inspector.Add(temp.CloneTree());
        //inspector.Add(temp.CloneTree());
        //inspector.Add(temp);
        
        return inspector;
    }
}
#endif