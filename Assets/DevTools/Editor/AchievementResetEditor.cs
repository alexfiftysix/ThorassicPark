using Phase_1.Builder.DeckBuilder.Achievements;
using UnityEditor;
using UnityEngine;

namespace DevTools.Editor
{
    [CustomEditor(typeof(AchievementReset), true)]
    public class AchievementResetEditor : UnityEditor.Editor
    {
        private AchievementReset _achievementReset;
        
        private void OnEnable()
        {
            _achievementReset = (AchievementReset)target;
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
