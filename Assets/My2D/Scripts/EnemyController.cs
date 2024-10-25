using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace My2D
{
    public class EnemyController : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;
        private Animator animator;
        private TouchingDirections touchingDirections;
        private Damageable damageable;

        //�÷��̾� ����
        public DetectionZone detectionZone;
        //�������� ����
        public DetectionZone detectionCliff;

        //�̵��ӵ�
        [SerializeField] private float runSpeed = 4f;
        //�̵�����
        private Vector2 directionVector = Vector2.right;

        //�̵� ���� ����
        public enum WalkableDirection { Left, Right }

        //���� �̵� ����
        private WalkableDirection walkDirection = WalkableDirection.Right;
        public WalkableDirection WalkDiretion
        {
            get {  return walkDirection; }
            private set {
                //�̹��� �ø�
                transform.localScale *= new Vector2(-1, 1);

                //���� �̵��ϴ� ���Ⱚ
                if (value == WalkableDirection.Left)
                {
                    directionVector = Vector2.left;
                }
                else if (value == WalkableDirection.Right)
                {
                    directionVector = Vector2.right;
                }

                walkDirection = value;
            }
        }

        //���� Ÿ�� ����
        [SerializeField] private bool hasTarget = false;
        public bool HasTarget
        {
            get { return hasTarget; }
            private set {
                hasTarget = value;
                animator.SetBool(AnimationString.HasTarget, value);
            }
        }

        //�̵� ���� ����/�Ұ��� ���� - �̵� ����
        public bool CanMove
        {
            get { return animator.GetBool(AnimationString.CanMove); }
        }

        //���� ���
        [SerializeField] private float stopRate = 0.2f;
        #endregion

        private void Awake()
        {
            //����
            rb2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            touchingDirections = GetComponent<TouchingDirections>();

            damageable = GetComponent<Damageable>();
            damageable.hitAction += OnHit;

            detectionCliff.noColliderRamain += OnCliffDetection;
        }

        private void Update()
        {
            //�� ���� �浹ü�� ����Ʈ ������ 0���� ũ�� ���� ���� �Ȱ��̴�
            HasTarget = (detectionZone.detectedColliders.Count > 0);
        }

        private void FixedUpdate()
        {
            //������ �̵��� ���� ������ ���� ��ȯ
            if(touchingDirections.IsWall && touchingDirections.IsGround)
            {
                //������ȯ ����
                Flip();
            }

            if(!damageable.LockVelocity)
            {
                //�̵�
                if (CanMove)
                {
                    rb2D.velocity = new Vector2(directionVector.x * runSpeed, rb2D.velocity.y);
                }
                else
                {
                    //rb2D.velocity.x -> 0 : Lerp ����
                    rb2D.velocity = new Vector2(Mathf.Lerp(rb2D.velocity.x, 0f, stopRate), rb2D.velocity.y);
                }
            }            
        }

        //������ȯ ����
        private void Flip()
        {
            if (WalkDiretion == WalkableDirection.Left)
            {
                WalkDiretion = WalkableDirection.Right;
            }
            else if(WalkDiretion == WalkableDirection.Right)
            {
                WalkDiretion = WalkableDirection.Left;
            }
            else
            {
                Debug.Log("Error Flip Direction");
            }
        }

        public void OnHit(float damage, Vector2 knockback)
        {
            rb2D.velocity = new Vector2(knockback.x, rb2D.velocity.y + knockback.y);
        }

        public void OnCliffDetection()
        {
            if (touchingDirections.IsGround)
            {
                Flip();
            }
        }
    }
}
