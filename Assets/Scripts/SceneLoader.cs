using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{

    public int SceneIndex;
    private GameObject SceneSpace;


    public void LoadScene(int sceneIndex)
    {
        if (SceneManager.GetAllScenes().Length > 1)
        {
            foreach (Scene s in SceneManager.GetAllScenes())
            {
                if (!s.name.Equals("SampleScene"))
                {
                    Settings.instance.previousScene = s.name;
                }
            }
        }
        else
        {
            Settings.instance.previousScene = "SampleScene";
        }

        if ((sceneIndex == 1 || sceneIndex == 3) && Settings.instance.previousScene.Equals("SampleScene"))
        {
            SceneSpace = GameObject.Find("SceneSpace");
            SceneSpace.SetActive(false);
            
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
            Settings.instance.currentScene = SceneManager.GetSceneByBuildIndex(sceneIndex).name;
        }
        else
        {
            foreach (Scene s in SceneManager.GetAllScenes())
            {
                if (!s.name.Equals("SampleScene"))
                {
                    SceneManager.UnloadSceneAsync(s);
                }
            }

            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
            Settings.instance.currentScene = SceneManager.GetSceneByBuildIndex(sceneIndex).name;
        }
    }


    public void OnMouseUp()
    {
        LoadScene(SceneIndex);        
    }


    public void Back()
    {
        foreach (Scene s in SceneManager.GetAllScenes())
            if(!s.name.Equals("SampleScene"))
        SceneManager.UnloadSceneAsync(s);
        GameObject sceneContainer = GameObject.Find("SceneContainer");
        SceneSpace = FindObject(sceneContainer, "SceneSpace");
        SceneSpace.SetActive(true);
    }


    public GameObject FindObject(GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

}
