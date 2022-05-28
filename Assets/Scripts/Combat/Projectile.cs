using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;

using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {


        [SerializeField] float speed = 4f;
        [SerializeField] bool isHoming;
        [SerializeField] GameObject fireballImpact = null;
        float projectileDamage;
        Health target = null;
        GameObject instigator;
        private void Start()
        {
            transform.LookAt(GetAimPosition());

        }
        // Update is called once per frame
        void Update()
        {
            if (target == null) return;
            if (isHoming && !target.IsDead)
            {
                transform.LookAt(GetAimPosition());
            }
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        public void SetTarget(Health target, float damage, GameObject instigator)
        {
            this.target = target;
            projectileDamage = damage;
            this.instigator = instigator;
            Destroy(gameObject, 5f);
        }
        private Vector3 GetAimPosition()
        {
            CapsuleCollider capsuleCollider = target.GetComponent<CapsuleCollider>();
            if (capsuleCollider == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * (capsuleCollider.height / 1.5f);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (target.IsDead) return;
            if (other.CompareTag("Enemy") || other.CompareTag("Player"))
            {
                target.TakeDamage(instigator, projectileDamage);
                if (fireballImpact != null)
                {
                    GameObject impact = Instantiate(fireballImpact, GetAimPosition(), transform.rotation);
                    Destroy(impact, 0.6f);
                }

                Destroy(gameObject);
            }




        }
    }
}

