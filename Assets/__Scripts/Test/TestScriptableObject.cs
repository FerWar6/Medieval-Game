using UnityEngine;

[CreateAssetMenu( menuName = "TestingScriptable", fileName = "Data")]
public class TestScriptableObject : ScriptableObject
{
    public string prefabName;

    public int numberOfPrefabsToCreate;
    public Vector3[] spawnPoints;
}