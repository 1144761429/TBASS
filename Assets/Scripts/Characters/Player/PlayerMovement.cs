using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject graphicRep;
    public PlayerInputHandler InputHandler { get; private set; }
    private Rigidbody2D _rb;
    private PlayerSpeedHandler _speedHandler;
    [field: SerializeField] public BoxCollider2D MovementCollider { get; private set; }
    [field: SerializeField] public float CollisionOffset { get; private set; }
    [SerializeField] private ContactFilter2D _movementCollisionFilter;
    private List<RaycastHit2D> _raycastHits = new List<RaycastHit2D>();
    public Vector2 Forward { get; private set; }


    #region Monobehavior Methods    
    private void Awake()
    {
        InputHandler = GetComponent<PlayerInputHandler>();

        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        _speedHandler = GetComponent<PlayerSpeedHandler>();

        Forward = Vector2.right;
    }

    private void Update()
    {
        FlipPlayer();
        ChangePlayerForward();
        Move(InputHandler.MovementInput);
    }
    #endregion

    /// <summary>
    /// Check if the direction is movable.
    /// If it is movable, move towards it.
    /// </summary>
    /// <param name="direction">The direction of movement.</param>
    /// <returns>True if the direction is movable, and the player will perform move. 
    /// False if it is not movable.</returns>
    private void Move(Vector2 direction)
    {
        int count = MovementCollider.Cast(direction, _movementCollisionFilter, _raycastHits, CollisionOffset);
        if (count == 0)
        {
            _rb.velocity = direction * _speedHandler.Speed;
        }
        //Sliding on the edge on collision
        else
        {
            SlidingOnEdge(direction);
        }
    }

    private void SlidingOnEdge(Vector2 direction)
    {
        Vector2 collisionNormal = _raycastHits[0].normal;
        float angleFromNormalToInput = Vector2.SignedAngle(collisionNormal, new Vector2(-InputHandler.MovementInput.x, -InputHandler.MovementInput.y));
        if (angleFromNormalToInput < 0)
        {
            direction = RotateVector2(InputHandler.MovementInput, -90 - angleFromNormalToInput);
        }
        else
        {
            direction = RotateVector2(InputHandler.MovementInput, 90 - angleFromNormalToInput);
        }
        Debug.DrawRay(MovementCollider.bounds.center, MovementCollider.bounds.center + new Vector3(direction.x, direction.y) * 100, Color.blue, .1f);
        float newMoveSpeed = _speedHandler.Speed * Mathf.Cos((90 - Mathf.Abs(angleFromNormalToInput)) * Mathf.Deg2Rad);
        _rb.velocity = direction * newMoveSpeed;
    }

    private void FlipPlayer()
    {
        if (MouseUtil.GetVector2ToMouse(transform.position).x > 0)
        {
            graphicRep.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

        }
        else if (MouseUtil.GetVector2ToMouse(transform.position).x < 0)
        {
            graphicRep.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }

    private void ChangePlayerForward()
    {
        if (MouseUtil.GetVector2ToMouse(transform.position).x > 0)
        {
            Forward = Vector2.right;
        }
        else if (MouseUtil.GetVector2ToMouse(transform.position).x < 0)
        {
            Forward = Vector2.left;
        }
    }

    private Vector2 RotateVector2(Vector2 originalVector, float angleInDegrees)
    {
        float angleInRadians = Mathf.Deg2Rad * angleInDegrees;
        float cosine = Mathf.Cos(angleInRadians);
        float sine = Mathf.Sin(angleInRadians);

        Vector2 rotatedVector = new Vector2();
        rotatedVector.x = originalVector.x * cosine - originalVector.y * sine;
        rotatedVector.y = originalVector.x * sine + originalVector.y * cosine;

        return rotatedVector;
    }
}