using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class TrailEffect : MonoBehaviour
    {
        #region Variables
        //�÷��̾� �ܻ� ȿ�� : 2�ʵ��� �ܻ�ȿ�� �߻�
        private bool isTrailActive = false;
        [SerializeField] private float trailActiveTime = 2f;        //�ܻ� ȿ�� ��ȿ �ð�
        [SerializeField] private float trailRefreshRate = 0.1f;     //�ܻ���� �߻� ����
        [SerializeField] private float trailDestroyDelay = 1f;      //1�Ŀ� ų -> ���̵� �ƿ�

        private SpriteRenderer playerRenderer;

        public Material ghostMaterial;      //�ܻ� ���͸���
        [SerializeField] private string shaderValueRef = "_Alpha";
        [SerializeField] private float shaderValueRate = 0.1f;      //���İ� ���� ����
        [SerializeField] private float shaderValueRefreshRate = 0.05f; //���İ� ���ҵǴ� �ð� ����
        #endregion

        private void Awake()
        {
            playerRenderer = GetComponent<SpriteRenderer>();
        }

        //2�ʵ��� �ܻ�ȿ�� �߻�
        public void StartActiveTrail()
        {
            if (isTrailActive) return;

            isTrailActive = true;
            StartCoroutine(ActiveTrail(trailActiveTime));
        }

        //activTime���� �ܻ�ȿ�� �߻�
        IEnumerator ActiveTrail(float activTime)
        {
            while (activTime > 0)
            {
                activTime -= trailRefreshRate;

                //�ܻ� ����� - ���� ��ġ��
                GameObject ghostObejct = new GameObject();  //���̶�Űâ�� �� ������Ʈ �����
                //Ʈ������ ����
                ghostObejct.transform.SetPositionAndRotation(transform.position, transform.rotation); 
                ghostObejct.transform.localScale = transform.localScale;
                //SpriteRenderer ����
                SpriteRenderer renderer = ghostObejct.AddComponent<SpriteRenderer>();
                renderer.sprite = playerRenderer.sprite;
                renderer.sortingLayerName = playerRenderer.sortingLayerName;
                renderer.sortingOrder = playerRenderer.sortingOrder - 1;
                renderer.material = ghostMaterial;

                //���͸���Ӽ�(���İ�) ����
                StartCoroutine(AnimateMaterialFloat(renderer.material, shaderValueRef, 0f, shaderValueRate, shaderValueRefreshRate));

                Destroy(ghostObejct, trailDestroyDelay);

                yield return new WaitForSeconds(trailRefreshRate);
            }
            isTrailActive = false;
        }

        //���͸���Ӽ�(���İ�) ����
        IEnumerator AnimateMaterialFloat(Material mat, string valueRef, float goal, float rate, float refreshRate)
        {
            float valueToAminate = mat.GetFloat(valueRef);

            while (valueToAminate > goal)
            {
                valueToAminate -= rate;
                mat.SetFloat(valueRef, valueToAminate);
                yield return new WaitForSeconds(refreshRate);
            }
        }

    }
}