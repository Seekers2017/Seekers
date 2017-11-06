using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Reset : MonoBehaviour
{

    public string SceneName = "CrusHour";
	void Update()
    {
		if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneName);
        }
	}
}
