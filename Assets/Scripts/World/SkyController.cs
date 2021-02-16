using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : MonoBehaviour
{
    public float parallax;
    MeshRenderer mr;
    Material mat;

    GameObject Player;

    private void Start()
    {
        Player = Game.getPlayer().gameObject;

        mr = GetComponent<MeshRenderer>();
        mat = mr.material;
    }

    private void Update()
    {
        Vector3 pos = new Vector3(Player.transform.position.x, Player.transform.position.y, 1);

        gameObject.transform.position = pos;

        Vector2 offset = mat.mainTextureOffset;

        offset.x = transform.position.x / transform.localScale.x / parallax;

        offset.y = transform.position.y / transform.localScale.y / parallax;

        mat.mainTextureOffset = offset;
    }



}
