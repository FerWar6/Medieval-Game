using UnityEngine;

[CreateAssetMenu(menuName = "EnemyData", fileName = "Data")]

public class EnemyDataScriptableObject : ScriptableObject
{
    [Range(50, 500)]
    public int health = 100;

    public float pathingCooldown = 1f;
    public float attackCooldown = 0.5f;

    public float sightRange = 7.5f;
    public float attackRange = 5;
    public float alertRange = 10;

    public GameObject projectile;
}
