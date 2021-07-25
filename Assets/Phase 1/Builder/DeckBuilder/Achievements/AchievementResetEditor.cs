using UnityEditor;
using UnityEngine;

namespace Phase_1.Builder.DeckBuilder.Achievements
{
    [CustomEditor(typeof(AchievementReset), true)]
    public class AchievementResetEditor : Editor
    {
        private AchievementReset _achievementReset;
        
        private void OnEnable()
        {
            Debug.Log("Enabled");
            _achievementReset = (AchievementReset)target;
            Debug.Log(_achievementReset);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
            if (GUILayout.Button("Reset"))
            {
                _achievementReset.Reset();
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}
