using UnityEngine;
using UnityEngine.UIElements;

//public class Canon : DefaultTower
//{
//    [SerializeField] private GameObject bullet;
//    [SerializeField] private GameObject barrel;
//    private float cooldown = 1f;
//    private void Update()
//    {
//        TurnTower();
//        cooldown -= Time.deltaTime;
//        if (cooldown <= 0)
//        {
//            cooldown = 1 / TowerStats.fireRate;
//            Shoot();
//        }
//    }
//    public void Shoot()
//    {
//        if (enemyList.Count > 0)
//        {
//            if (enemyList[0].IsDead || enemyList[0] == null)
//            {
//                enemyList.RemoveAt(0);
//                return;
//            }
//            Vector3 towerForward = transform.forward;
//            Vector3 toEnemy = enemyList[0].gameObject.transform.position - transform.position;
//            float angle = Vector3.Angle(towerForward, toEnemy);
//            if (angle < Mathf.Abs(60))
//            {
//                GameObject bulletGO = Instantiate(bullet, transform.position, Quaternion.identity);
//                bulletGO.GetComponent<Bullet>().Init(enemyList[0], TowerStats.damage);
//            } 
//        }
//    }
//}