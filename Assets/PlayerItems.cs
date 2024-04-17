using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public List<GameObject> playerItemsList = new List<GameObject>();

    void Start()
    {
        PlayerData.instance.OnInventoryUpdate.AddListener(UpdateItems);

    }

    private void UpdateItems()
    {
        if(PlayerData.instance.latestObjAdded.name == "Keyitem_PrisonSword")
        {
            playerItemsList[0].gameObject.SetActive(true);
        }
    }
    private void OnDestroy()
    {
        PlayerData.instance.OnInventoryUpdate.RemoveListener(UpdateItems);
    }
}
