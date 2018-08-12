using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class Game : MonoBehaviour
{
    [SerializeField] private PlayableDirector _houseIntro;
    [SerializeField] private PlayableDirector _boyScene;
    [SerializeField] private PlayableDirector _endScene;
    [SerializeField] private PlayableDirector _theEnd;
    [SerializeField] private Camera _menuCamera;
    [SerializeField] private Camera _ballScene;
    [SerializeField] private Camera _ballSceneSad;
    [SerializeField] private Ball _ball;
    [SerializeField] private Animator _ballAnimator;

    public void Start()
    {
        _menuCamera.enabled = true;

        StartCoroutine(GameBlah());
        
    }

    public IEnumerator GameBlah()
    {
        while (!Input.anyKey)
        {
            yield return null;
        }
		
        _menuCamera.enabled = false;
		
        yield return new WaitForSeconds(1f);
		
        // play intro
        _houseIntro.Play();

        while (_houseIntro.state == PlayState.Playing)
        {
            yield return null;
        }
		
        _houseIntro.Stop();
		
        yield return new WaitForSeconds(1f);
		
        // ball game one
        _ballAnimator.Play("bedroom_1");
        _ballScene.gameObject.SetActive(true);

        _ball.IsAlone = true;
        _ball.CameraToCheckWorldPosWith = _ballScene;

        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(10);
            _ballAnimator.SetTrigger("Next");			
        }
        
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(10);
            _ballAnimator.SetTrigger("Back");			
        }


        _ballScene.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);
        		
        // boy enters house scene.
        
        _boyScene.Play();
				
        while (_boyScene.state == PlayState.Playing)
        {
            yield return null;
        }
		
        _boyScene.Stop();
        
        // ball game sad
		
        yield return new WaitForSeconds(1f);
		
        _ballAnimator.Play("bedroom_1");
        _ballSceneSad.gameObject.SetActive(true);

        _ball.IsAlone = true;
        _ball.IsSad = true;
        _ball.CameraToCheckWorldPosWith = _ballSceneSad;

        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(10);
            _ballAnimator.SetTrigger("Next");			
        }

        _ballSceneSad.gameObject.SetActive(false);
		
        _endScene.Play();
		
        while (_endScene.state == PlayState.Playing)
        {
            yield return null;
        }
		
        _endScene.Stop();

        _ball.IsAlone = false;
        _ball.IsSad = false;
        _ball.CameraToCheckWorldPosWith = _ballScene;

        _ballAnimator.Play("bedroom_4");
        _ballScene.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(40f);
                
        _theEnd.Play();
        
    } 
}