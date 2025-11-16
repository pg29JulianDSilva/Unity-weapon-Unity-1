using UnityEngine;
using System;

public class TargetCube : MonoBehaviour
{
    public string bulletTag = "Bullet";

    public bool destroyBulletOnHit = true;

    public ParticleSystem destroyEffect;

    //This are the triggers for the collisions on physics

    //This one is litteraly when it bounce
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == bulletTag)
        {
            //you can also write the if with collision.collider.gameObject.CompareTag("Bullet)

            HandleHit(collision.gameObject);
            //GameObject.Destroy(gameObject, 1);
        }
    }

    //This one is when (ontrigger) is on (This one also gives less information
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == bulletTag)
        {
            //you can also write the if with collision.collider.gameObject.CompareTag("Bullet)

            HandleHit(other.gameObject);

            //GameObject.Destroy(gameObject, 1);
        }
    }

    //You're supposed to use 1 of both, but in this case we write both to learn

    private void HandleHit(GameObject bullet)
    {
        if (destroyEffect)
        {
            ParticleSystem fx = Instantiate(destroyEffect, transform.position, Quaternion.identity);
            fx.Play();
            Destroy(fx.gameObject, 0.5f);
        }

        if (destroyBulletOnHit)
        {
            Destroy(bullet, 1f);
        }

        GameObject.Destroy(gameObject);
    }

}
