using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCarMaterial : MonoBehaviour {

    //List of the cars and their renderers
    [SerializeField]
    private List<Texture> availableTextures;
    [SerializeField]
    private List<MeshRenderer> carMeshRenderers;



    void Awake()
    {
        //For each car
        for (int i = 0; i < carMeshRenderers.Count; i++)
        {
            if (availableTextures.Count == 0)
                break;

            //Choose a random int for texture assignment
            int randIndex = Random.Range(0, availableTextures.Count);


            //Set the texture
            carMeshRenderers[i].material.SetTexture("_MainTex", availableTextures[randIndex]);

            //Remove it so we don't double up
            availableTextures.RemoveAt(randIndex);
        }
    }
}
