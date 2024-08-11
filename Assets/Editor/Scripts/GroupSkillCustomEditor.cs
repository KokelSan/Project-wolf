using UnityEditor;
using UnityEngine;

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

        skill.RelatedSkill = (AIndividualSkill)EditorGUILayout.ObjectField("Related Skill", skill.RelatedSkill, typeof(AIndividualSkill), true);
        EditorGUILayout.Space();

        skill.AllCharactersAreMembers = GUILayout.Toggle(skill.AllCharactersAreMembers, "All Characters are members", "Button");

        if (skill.AllCharactersAreMembers)
        {
            skill.GroupMembers?.Clear();
        }
        else
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(skill.GroupMembers)), true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}