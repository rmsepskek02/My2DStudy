using UnityEngine;

namespace My2D
{
    //�߻�ü(ȭ��) �߻�
    public class ProjectileLauncher : MonoBehaviour
    {
        #region Variables
        public GameObject projectilePrefab;
        public Transform firePoint;
        #endregion

        //�߻�ü(ȭ��) �߻�
        public void FireProjectile()
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, projectilePrefab.transform.rotation);
            Destroy(projectile, 5f);

            //ȭ���� ���� ����
            Vector3 originScale = projectile.transform.localScale;
            projectile.transform.localScale = new Vector3(
                originScale.x * transform.localScale.x > 0 ? 1 : -1,
                originScale.y,
                originScale.z);

        }
    }
}
