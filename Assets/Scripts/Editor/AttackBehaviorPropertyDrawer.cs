using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

#if false

[CustomPropertyDrawer(typeof(AttackBehavior))]
public class AttackBehaviorPropertyDrawer : PropertyDrawer
{
    private VisualElement root;
    private ToolbarButton testButton;
    
    
    
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        root = new VisualElement();
        testButton = new ToolbarButton(SayHello);
        VisualTreeAsset rootAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Editor/AttackBehaviorPropertyDrawer.uxml");
        root.Add(rootAsset.CloneTree());
        root.Add(testButton);

        

        return root;
    }   

    private void OnChangeAttackBehaviorType(SerializedPropertyChangeEvent e)
    {
        Debug.Log(root.childCount);
        if (e.changedProperty.enumValueIndex == (int)EAttackBehaviorType.Range)
        {
            Debug.Log(root.childCount);
        }
    }

    private void SayHello()
    {
        Debug.Log("U PRESSED A BUTTON");
    }
}

#endif