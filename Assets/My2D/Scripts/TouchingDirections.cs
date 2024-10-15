using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    // 바닥체크, 벽 체크, 천정체크
    public class TouchingDirections : MonoBehaviour
    {
        private CapsuleCollider2D touchingCollider;
        [SerializeField] private ContactFilter2D contackFilter;
        [SerializeField] private float groundDistance = 0.05f;
        [SerializeField] private float ceilingDistance = 0.05f;
        [SerializeField] private float wallDistance = 0.05f;
        private RaycastHit2D[] groundHits = new RaycastHit2D[5];
        private RaycastHit2D[] wallHits = new RaycastHit2D[5];
        private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
        private Animator animator;

        [SerializeField] private bool isGround;
        public bool IsGround
        {
            get { return isGround; }
            set
            {
                isGround = value;
                animator.SetBool(AnimationString.IsGround, isGround);
            }
        }
        [SerializeField] private bool isWall;
        public bool IsWall
        {
            get { return isWall; }
            set
            {
                isWall = value;
                //animator.SetBool(AnimationString.IsWall, isWall);
            }
        }
        [SerializeField] private bool isCeiling;
        public bool IsCeiling
        {
            get { return isCeiling; }
            set
            {
                isCeiling = value;
                //animator.SetBool(AnimationString.IsWall, isWall);
            }
        }
        private Vector2 WalkDirection => (transform.localScale.x > 0) ? Vector2.right : Vector2.left;
        private void Awake()
        {
            touchingCollider = GetComponent<CapsuleCollider2D>();
            animator = GetComponent<Animator>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        private void FixedUpdate()
        {
            IsGround = (touchingCollider.Cast(Vector2.down, contackFilter, groundHits, groundDistance) > 0);
            isCeiling = (touchingCollider.Cast(Vector2.up, contackFilter, ceilingHits, ceilingDistance) > 0);
            IsWall = (touchingCollider.Cast(WalkDirection, contackFilter, wallHits, wallDistance) > 0 );
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
