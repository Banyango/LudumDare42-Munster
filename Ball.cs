using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

	[SerializeField] private Rigidbody2D _rigidbody2D;
	[SerializeField] private AudioSource _audioSource;
	[SerializeField] private AudioSource _audioSource2;
	[SerializeField] private AudioClip _laughingBoy;
	[SerializeField] private AudioClip _laughingGirl;
	[SerializeField] private AudioClip _sadGirl;
	[SerializeField] private AudioClip _ballHitSound;
	[SerializeField] private Vector2 _force;


	public Camera CameraToCheckWorldPosWith;
	public bool IsAlone { get; set; }
	public bool IsSad { get; set; }

	public bool IsInSun
	{
		get { return _isInTheSun; }
	}

	private bool _isInTheSun;

	private void OnTriggerEnter2D(Collider2D other)
	{
		_isInTheSun = true;
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		_isInTheSun = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		_isInTheSun = false;
	}

	private void OnMouseUp()
	{
		if (_rigidbody2D.velocity.magnitude < 0.2f)
		{

			if (IsAlone && !_isInTheSun)
			{
				GirlHitsBall();
			} else if (IsAlone)
			{								
				_audioSource.clip = _sadGirl;

				_audioSource.Play();				
			}
		
			if (!IsAlone)
			{
				if (_isInTheSun)
				{
					BoyHitsBall();
				}
				else
				{
					GirlHitsBall();
				}
			}
		}		
	}

	private void BoyHitsBall()
	{
		HitBall(_laughingBoy);
	}

	private void GirlHitsBall()
	{
		if (IsSad)
		{
			HitBall(_sadGirl);			
		}
		else
		{
			HitBall(_laughingGirl);						
		} 
		
	}
	
	private void HitBall(AudioClip clip)
	{
		
		var ballWorldPosition = CameraToCheckWorldPosWith.WorldToScreenPoint(transform.position);
		
		_rigidbody2D.AddForce((ballWorldPosition - Input.mousePosition).normalized * Random.RandomRange(_force.x, _force.y), ForceMode2D.Impulse);

		_audioSource.clip = clip;

		_audioSource.Play();
	}
}