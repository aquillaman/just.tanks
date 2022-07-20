using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Editor
{
    public class CustomMenu
    {
        [MenuItem("Game/Start")]
        public static void StartGame()
        {
            if (!EditorApplication.isPlaying)
            {
                EditorSceneManager.sceneOpened -= EditorSceneManagerOnsceneOpened;
                EditorSceneManager.sceneOpened += EditorSceneManagerOnsceneOpened;
                EditorSceneManager.OpenScene("Assets/Scenes/main.unity", OpenSceneMode.Single);
            }
        }

        private static void EditorSceneManagerOnsceneOpened(Scene scene, OpenSceneMode mode)
        {
            EditorApplication.isPlaying = true;
        }

        private static void EditorSceneManagerOnsceneLoaded(Scene scene, LoadSceneMode mode)
        {
            EditorApplication.isPlaying = true;   
        }
    }
}