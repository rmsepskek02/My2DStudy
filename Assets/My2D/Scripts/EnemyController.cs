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
        // 플레이어 감지
        public DetectionZone detectionZone;

        //이동 가능방향
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
        // 공격 타겟 설정
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
        // 이동 가능 상태/ 불가능 상태 -이동 제한
        public bool CanMove
        {
            get { return animator.GetBool(AnimationString.CanMove); }
        }

        // 감속 계수
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
            // 땅에서 이동시 벽을 만나면 방향 전환
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

        // 방향전환 반전
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
            // 적 감지 충돌체의 리스트 갯수가 0보다 크면 적이 감지된것
            HasTarget = detectionZone.detectedColliders.Count > 0;
        }
    }
}
