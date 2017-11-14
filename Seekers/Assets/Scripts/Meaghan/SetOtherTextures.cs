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
        texture = GetComponent<MeshRenderer>();

       texture.material.SetTexture("_MainTex", carTexture.material.GetTexture("_MainTex"));
	}
	
}
