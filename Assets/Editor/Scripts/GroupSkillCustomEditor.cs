using UnityEditor;

[CustomEditor(typeof(GroupSkill), true)]
public class GroupSkillCustomEditor : Editor
{
    bool editDescription = true;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GroupSkill skill = (GroupSkill)target;

        skill.Name = EditorGUILayout.TextField("Name", skill.Name);
        
        editDescription = EditorGUILayout.Foldout(editDescription, "Description");
        if (editDescription)
        {
            EditorStyles.textField.wordWrap = true;
            skill.Description = EditorGUILayout.TextArea(skill.Description);
        }
        EditorGUILayout.Space();

        skill.RelatedSkill = (AIndividualSkill)EditorGUILayout.ObjectField(nameof(skill.RelatedSkill), skill.RelatedSkill, typeof(AIndividualSkill), true);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(skill.GroupMembers)), true);

        serializedObject.ApplyModifiedProperties();
    }
}