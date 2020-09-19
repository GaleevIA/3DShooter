using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    private Transform _player;

    void Start()
    {
        _player = GameObject.FindObjectOfType<SinglePlayer>().transform;
    }

    void LateUpdate()
    {
        Vector3 newPosition = _player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90f, _player.eulerAngles.y, 0f);
    }
}
