using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;


[CustomPropertyDrawer(typeof(AttackBehavior))]
public class AttackBehaviorPropertyDrawer : PropertyDrawer
{
    // public override VisualElement CreatePropertyGUI(SerializedProperty property)
    // {
    //     VisualTreeAsset rootAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("f8f28cfb858bfbd4ea87437beb444a74");
    //     TemplateContainer root = rootAsset.CloneTree();
    //     root.Add(new Label("Can u see diz"));
    //     root.BindProperty(property);
    //
    //     return root;
    // }

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        return base.CreatePropertyGUI(property);
    }
}
