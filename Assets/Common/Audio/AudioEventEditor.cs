using UnityEditor;
using UnityEngine;

namespace Common.Audio
{
    [CustomEditor(typeof(AudioEvent), true)]
    public class AudioEventEditor : Editor
    {
        [SerializeField] private AudioSource previewer;

        public void OnEnable()
        {
            previewer = EditorUtility
                .CreateGameObjectWithHideFlags("Audio Preview", HideFlags.HideAndDontSave, typeof(AudioSource))
                .GetComponent<AudioSource>();
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
            if (GUILayout.Button("Preview"))
            {
                ((AudioEvent) target).Play(previewer);
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}