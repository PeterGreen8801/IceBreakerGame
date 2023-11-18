using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestCheck : MonoBehaviour
{
    Player player;

    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    int chestPointAmount = 25;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TreasureChest treasureChest))
        {
            //Add points
            player.AddPoints(chestPointAmount);
            player.PlayTreasureChestSound();
            Destroy(other.gameObject);
        }
    }
}
