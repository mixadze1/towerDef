using TMPro;
using UnityEditor;
using UnityEngine;
public abstract class Tower : GameTileContent
{
    [SerializeField, Range(1.5f, 10.5f)] public float _targetingRange = 1.5f;

    public abstract TowerType Type { get; }
    protected bool IsAcquiareTarget(out TargetPoint target)
    {

        if (TargetPoint.FillBuffer(transform.localPosition, _targetingRange))
        {
            target = TargetPoint.GetBuffered(0);
            return true;
        }
        target = null;
        return false;
    }

    protected bool IsTargetTracked(ref TargetPoint target)
    {
        if (target == null)
        {
            return false;
        }
        Vector3 myPos = transform.localPosition;
        Vector3 targetPosition = target.Position;
        if(Vector3.Distance(myPos,targetPosition) < _targetingRange + target.ColliderSize * target.Enemy.Scale)
        {
            target = null;
            return false;
        }
        return true;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 position = transform.localPosition;
        position.y += 0.01f;
        Gizmos.DrawWireSphere(position,_targetingRange);
    }
}

