using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class EnemyController : MonoBehaviour
    {
        private Animator animator;
        private Rigidbody2D rb;
        [SerializeField] private float runSpeed = 4f;
        private Vector2 directionVector = Vector2.right;
        private TouchingDirections touchingDirections;
        // �÷��̾� ����
        public DetectionZone detectionZone;

        //�̵� ���ɹ���
        public enum WalkableDirection
        {
            Left,
            Right,
        }
        private WalkableDirection walkDirection = WalkableDirection.Right;
        public WalkableDirection WalkDirection
        {
            get { return walkDirection; }
            set
            {
                transform.localScale *= new Vector2(-1,1);
                if(value == WalkableDirection.Left)
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
        // ���� Ÿ�� ����
        [SerializeField] private bool hasTarget = false;
        public bool HasTarget
        {
            get { return hasTarget; }
            private set
            {
                hasTarget = value;
                animator.SetBool(AnimationString.HasTarget, value);
            }
        }
        // �̵� ���� ����/ �Ұ��� ���� -�̵� ����
        public bool CanMove
        {
            get { return animator.GetBool(AnimationString.CanMove); }
        }

        // ���� ���
        private float stopRate = 0.2f;
        // Start is called before the first frame update
        void Awake()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            touchingDirections = GetComponent<TouchingDirections>();
        }

        private void FixedUpdate()
        {
            // ������ �̵��� ���� ������ ���� ��ȯ
            if (touchingDirections.IsWall && touchingDirections.IsGround)
            {
                Flip();
            }
            if(CanMove)
            {
                rb.velocity = new Vector2(directionVector.x * runSpeed, rb.velocity.y);
            }
            else
            {
                // rb.velocity.x -> 0 : Lerp
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, stopRate), rb.velocity.y);
                //rb.velocity = new Vector2(0f, rb.velocity.y);
            }
        }

        // ������ȯ ����
        void Flip()
        {
            if(WalkDirection == WalkableDirection.Left)
            {
                WalkDirection = WalkableDirection.Right;
                
            }
            else if (WalkDirection == WalkableDirection.Right)
            {
                WalkDirection = WalkableDirection.Left;
            }
            else
            {
                Debug.Log("ERROR FLIP DIRECTION");
            }
        }
        // Update is called once per frame
        void Update()
        {
            // �� ���� �浹ü�� ����Ʈ ������ 0���� ũ�� ���� �����Ȱ�
            HasTarget = detectionZone.detectedColliders.Count > 0;
        }
    }
}
