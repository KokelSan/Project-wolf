using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AIndividualSkill), true)]
public class IndividualSkillCustomEditor : Editor
{
    bool editDescription = true;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        AIndividualSkill skill = (AIndividualSkill)target;

        skill.Name = EditorGUILayout.TextField("Name", skill.Name);
        
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
        skill.TargetAllCharacters = GUILayout.Toggle(skill.TargetAllCharacters, "Target All Characters", "Button");
        skill.CanSelfTarget = GUILayout.Toggle(skill.CanSelfTarget, "Can Self Target", "Button");
        GUILayout.EndHorizontal();

        if (skill.TargetAllCharacters)
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