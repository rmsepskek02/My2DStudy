using UnityEngine;

namespace My2D
{
    //�÷��̾� �̵� ���� ����ȿ�� �Ÿ� ���ϱ�
    public class ParallaxEffect : MonoBehaviour
    {
        #region Variables
        public Camera camera;           //ī�޶�
        public Transform followTarget;  //�÷��̾�

        //���� ��ġ
        private Vector2 startingPostion;    //���� ��ġ (���, ī�޶�)
        private float startingZ;            //�����Ҷ� ����� z�� ��ġ��

        //������������ ������ ī�޶� �ִ� ��ġ������ �Ÿ�
        private Vector2 CamMoveSinceStart => startingPostion - (Vector2)camera.transform.position;

        //���� �÷��̾���� z�� �Ÿ�
        private float zDistanceFromTarget => transform.position.z - followTarget.position.z;
        //
        private float ClippingPlane => camera.transform.position.z + (zDistanceFromTarget > 0 ? camera.farClipPlane : camera.nearClipPlane);
        //���� �Ÿ� factor
        private float ParallaxFactor => Mathf.Abs(zDistanceFromTarget) / ClippingPlane;
        #endregion

        private void Start()
        {
            //�ʱ�ȭ
            startingPostion = transform.position;
            startingZ = transform.position.z;
        }


        private void Update()
        {
            Vector2 newPositon = startingPostion + CamMoveSinceStart * ParallaxFactor;
            transform.position = new Vector3(newPositon.x, newPositon.y, startingZ);
        }

    }
}