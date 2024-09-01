using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ASkill), true)]
public class ASkillCustomEditor : Editor
{
    bool editDescription = true;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        ASkill skill = (ASkill)target;

        skill.ActionVerb = EditorGUILayout.TextField("Action verb", skill.ActionVerb);

        editDescription = EditorGUILayout.Foldout(editDescription, "Description");
        if (editDescription)
        {
            EditorStyles.textField.wordWrap = true;
            skill.Description = EditorGUILayout.TextArea(skill.Description);
        }
        EditorGUILayout.Space();

        skill.TargetNb = EditorGUILayout.IntField("Target Number", skill.TargetNb);

        skill.Frequency = (ASkillFrequency)EditorGUILayout.ObjectField(nameof(skill.Frequency), skill.Frequency, typeof(ASkillFrequency), true);
        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        skill.CanTargetAllCharacters = GUILayout.Toggle(skill.CanTargetAllCharacters, "Target All Characters", "Button");
        skill.CanSelfTarget = GUILayout.Toggle(skill.CanSelfTarget, "Can Self Target", "Button");
        GUILayout.EndHorizontal();

        if (skill.CanTargetAllCharacters)
        {
            skill.TargetedCharacters?.Clear();
        }
        else
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(skill.TargetedCharacters)), true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}