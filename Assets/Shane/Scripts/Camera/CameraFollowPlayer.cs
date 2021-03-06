using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private bool FollowPlayerAtStart;

    [Header("Player Info")]
    private Transform player;
    private Vector3 playerLastPosition;

    private void Start()
    {
        if (FollowPlayerAtStart)
            FindPlayer();
    }

    private void LateUpdate()
    {
        if (player == null)
            return;

        transform.position += player.position - playerLastPosition;
        playerLastPosition = player.position;
    }

    public void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
