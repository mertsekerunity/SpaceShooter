using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;
    [SerializeField] float baseScrollSpeed = 0.1f;
    PlayerController playerController;
    

    Vector2 offset;
    Material material;

    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        offset = moveSpeed * Time.deltaTime * playerController.rawInput;
        offset[1] += baseScrollSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
