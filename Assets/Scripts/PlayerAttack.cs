using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hero
{
    public class PlayerAttack : MonoBehaviour
    {

        public GameObject bulletPrefab;


        void Update()
        {

        }

        private void Attack()
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position + transform.forward * 2, Quaternion.identity);
        }
    }
}
