using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOtherTextures : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer carTexture;

    private MeshRenderer texture;

	// Use this for initialization
	void Start ()
    {
        //Obtain the current texture
        texture = GetComponent<MeshRenderer>();

        //Make the texture equal to it's parent
        texture.material.SetTexture("_MainTex", carTexture.material.GetTexture("_MainTex"));
	}
}
