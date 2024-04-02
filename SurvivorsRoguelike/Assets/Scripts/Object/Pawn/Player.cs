using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : BasePawn
{
    [SerializeField] private Transform _indicator;
    public Transform Indicator { get { return _indicator; } }

    private float _gemCollectDistance = 1.0f;

    protected override bool Init()
    {
        if (base.Init() == false)
        { 
            return false;
        }

        ObjectType = Define.ObjectType.Player;
        
        return true;
    }

    protected override void OnEnableObject()
    {
        base.OnEnableObject();

        PawnSpriteRenderer.sortingOrder = (int)Define.SpriteSortingOrder.Player;

        PawnState = Define.PawnState.Idle;

        // TODO : юс╫ц
        Managers.Skill.AddSkill<BulletSkill>(transform.position);
        Managers.Skill.AddSkill<SwordSkill>(transform.position);
        Speed = 6.0f;
    }

    protected override void FadeAnimation()
    {
        switch (PawnState)
        {
            case Define.PawnState.Idle:
                PawnAnimator.CrossFade("Stand", 0.1f);
                break;
            case Define.PawnState.Moving:
                PawnAnimator.CrossFade("Run", 0.1f);
                break;
            case Define.PawnState.Dead:
                PawnAnimator.CrossFade("Dead", 0.1f);
                break;
            case Define.PawnState.Attacking:
                break;
        }
    }

    protected override void UpdateController()
    {
        base.UpdateController();

        InputKey();
        CollectGem();
    }

    protected override void FixedUpdateController()
    {
        base.FixedUpdateController();

        MovePlayer();
    }

    protected override void LateUpdateController()
    {
        base.LateUpdateController();

        if (PawnState != Define.PawnState.Dead && MoveDir.x != 0)
        {
            PawnSpriteRenderer.flipX = MoveDir.x < 0;
        }
    }

    private void MovePlayer()
    {
        if (MoveDir == Vector2.zero)
        {
            PawnState = Define.PawnState.Idle;
            return;
        }

        Vector3 movement = MoveDir * Speed * Time.fixedDeltaTime;
        transform.position += movement;

        if (MoveDir != Vector2.zero)
        {
            _indicator.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-movement.x, movement.y) * 180 / Mathf.PI);
        }

        PawnRigidbody2D.velocity = Vector2.zero;
        PawnState = Define.PawnState.Moving;
    }

    private void InputKey()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        MoveDir = new Vector2(horizontalInput, verticalInput).normalized;
    }

    private void CollectGem()
    {
        float sqrCollectDist = _gemCollectDistance * _gemCollectDistance;

        var findGems = Managers.Grid.GatherObjects(transform.position, _gemCollectDistance);

        foreach (var gameObject in findGems)
        {
            Gem gem = gameObject.GetComponent<Gem>();

            Vector3 dir = gem.transform.position - transform.position;
            if (dir.sqrMagnitude <= sqrCollectDist)
            {
                Managers.Object.Despawn(gem);
            }
        }
    }
}
