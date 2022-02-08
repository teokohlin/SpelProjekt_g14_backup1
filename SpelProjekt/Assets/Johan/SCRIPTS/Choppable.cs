using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choppable : MonoBehaviour
{
    public int health = 1;
    public ParticleSystem Particle_effect;

    //[HideInInspector]
    private bool dead = false; //kan inte anv�nda static bool, d� blir den true f�r ALLA //private eller protected spelar nog ingen roll

    public int dropAmount = 1;
    public GameObject dropPrefab;

    public virtual void LoseHealth(int damage)
    {

        Particle_effect.Play();

        health -= damage;
        if (health <= 0)
        {
            dead = true;

        }

        if (dead == true)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        for (int i = 0; i < dropAmount; i++)
        {
            Vector3 ranPos = new Vector3(
                transform.position.x + Random.Range(-.5f, .5f), 
                transform.position.y, 
                transform.position.z + Random.Range(-.5f, .5f));
            //Quaternion ranRot = new Quaternion(
            //    dropPrefab.transform.rotation.x,
            //    dropPrefab.transform.rotation.y + Random.Range(0,360),
            //    dropPrefab.transform.rotation.z,
            //    dropPrefab.transform.rotation.w
            //    );


            GameObject drop = Instantiate(dropPrefab, ranPos, dropPrefab.transform.rotation);
            drop.transform.Rotate(0, Random.Range(0, 360), 0 );
        }
        Destroy(gameObject);
    }
}
