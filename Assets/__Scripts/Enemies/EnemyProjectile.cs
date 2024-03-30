using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public LayerMask groundLayerMask;
    public LayerMask playerLayerMask;

    private void OnCollisionEnter(Collision collision)
    {
        if ((groundLayerMask & (1 << collision.gameObject.layer)) != 0)
        {
            PlayParticleAndDestroy();
        }
        if ((playerLayerMask & (1 << collision.gameObject.layer)) != 0)
        {
            PlayerData.instance.ModifyPlayerHealth(10, false);
            Camera.main.GetComponent<CamAnimation>().PlayBloodEffect();
            PlayParticleAndDestroy();
        }
    }

    private void PlayParticleAndDestroy()
    {
        // Play a particle effect
        Destroy(gameObject);
    }
}

