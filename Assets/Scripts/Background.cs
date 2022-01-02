using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.5f;

    Material material;
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += new Vector2(0, moveSpeed) * Time.deltaTime;
    }
}
