using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("target"))
        {
            print("hit"+collision.gameObject.name+"!");
            CreateBulletImpactEffect(collision);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("wall"))
        {
            print("hitwall!");
            CreateBulletImpactEffect(collision);    
            Destroy(gameObject);
        }
    }
    void CreateBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact =objectWeHit.contacts[0];

        GameObject hole = Instantiate( globalReferences.instance.bulletImpactEffectPrefab,
            contact.point,
            Quaternion.LookRotation(contact.normal) );

        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
}
