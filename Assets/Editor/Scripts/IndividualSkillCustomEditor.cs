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
        skill.CanPerformOnAllCharacters = GUILayout.Toggle(skill.CanPerformOnAllCharacters, "Can Perform On All Characters", "Button");
        skill.CanPerformOnSelf = GUILayout.Toggle(skill.CanPerformOnSelf, "Can Perform On Self", "Button");
        GUILayout.EndHorizontal();


        if (skill.CanPerformOnAllCharacters)
        {
            skill.PotentialTargetTypes?.Clear();
        }
        else
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(skill.PotentialTargetTypes)), true);
        }        

        serializedObject.ApplyModifiedProperties();
    }
}