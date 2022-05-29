using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{

	protected Enemy _enemy;
	public AnimationClip Attack03Anim;
	public AnimationClip DieAnim;
	public AnimationClip AppairAnim;

	private void Init()
	{
			
	}
	public virtual void Die()
    {
		_enemy.GetComponent<Animation>().Play(DieAnim.name);
	}
	
}
		

