using UnityEngine;

public class SceneData : MonoBehaviour
{
    public static SceneData Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
