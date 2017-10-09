using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(SpawnManagerScript))]
//[CanEditMultipleObjects]

public class SpawnerCustomInspector : Editor
{
    //SpawnerScript spawnerScript;
    //SpawnManagerScript spawnManagerScript;
    //GameObject spawnManager;

    public override void OnInspectorGUI()
    {
        //spawnerScript = GameObject.Find("SpawnerScript").GetComponent<SpawnerScript>();
        //spawnManagerScript = GameObject.Find("SpawnManagerScript").GetComponent<SpawnManagerScript>();
        //spawnManager = GameObject.Find("SpawnManager").GetComponent<GameObject>();

        //displays original GUI
        //base.OnInspectorGUI();

        //get access to the attached script (SpawnManager itself)
        //SpawnManagerScript myTarget = (SpawnManagerScript)target;

        //EditorGUILayout.MinMaxSlider(myTarget.hthKitSpawnLmt, myTarget.transform.childCount);

        //EditorGUILayout.MinMaxSlider(0, 10);


        //this.Repaint();

        //EditorGUILayout.IntField(myTarget.transform.childCount);


    }
}
