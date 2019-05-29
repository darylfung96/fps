using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EntryDoorSpawner : NetworkBehaviour {

    public GameObject entryDoorPrefab;

    public override void OnStartServer()
    {
        Vector3 spawnPosition = new Vector3(-23.32f, 2.76f, -26.39f);
        Quaternion spawnRotation = Quaternion.Euler(180f, 0, 0);
        GameObject entryDoor = Instantiate(entryDoorPrefab, spawnPosition, spawnRotation);
    }
}
