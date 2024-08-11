using UnityEditor;

[CustomEditor(typeof(Character), true)]
public class CharacterCustomEditor : Editor
{
    bool editDescription = true;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Character character = (Character)target;

        character.Name = EditorGUILayout.TextField("Name", character.Name);
        
        editDescription = EditorGUILayout.Foldout(editDescription, "Description");
        if (editDescription)
        {
            EditorStyles.textField.wordWrap = true;
            character.Description = EditorGUILayout.TextArea(character.Description);
        }
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(character.IndividualSkills)), true);
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(character.GroupSkills)), true);

        serializedObject.ApplyModifiedProperties();
    }
}