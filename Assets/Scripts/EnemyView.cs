using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{

	public Transform _enemy;
	public AnimationClip IdleSpecialAnim;
	public AnimationClip Attack03Anim;
	public AnimationClip DieAnim;
	public AnimationClip AppairAnim;
	public bool IsRun;
    private void Update()
    {
		if(IsRun)
		{ 
			_enemy.GetComponent<Animation>().Play(IdleSpecialAnim.name); 
		}
		else
		_enemy.GetComponent<Animation>().Play(Attack03Anim.name);
		
	}
    private void OnDestroy()
    {
        
    }
    private void Init()
	{
			
	}
	public virtual void Die(Enemy enemy)
    {
		_enemy.GetComponent<Animation>().Play(DieAnim.name);
	}
	public virtual void StartLife(Enemy enemy)
    {
		_enemy.GetComponent<Animation>().Play(AppairAnim.name);
	}
	public virtual void Run(Enemy enemy)
    {
		_enemy.GetComponent<Animation>().Play(Attack03Anim.name);
	}
	
}
		

