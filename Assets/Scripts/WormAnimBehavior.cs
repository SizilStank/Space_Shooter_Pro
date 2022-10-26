using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WormAnimBehavior : MonoBehaviour
{

    [SerializeField] private Animator _animator;


    [SerializeField] private bool _canAnimRun;

    //when do we use getcomponet?

    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent<Animator>(out _animator))
        {
            Debug.LogError("_animator is Null");
            _animator.enabled = false;
        }

        _canAnimRun = false;
        _animator.SetBool("CanAnimRun", false);
           
    }

    private void OnMouseOver()
    {
        _canAnimRun = true;

        if(_canAnimRun == true)
        {
            _animator.SetBool("CanAnimRun", true);
        }
    }

    private void OnMouseExit()
    {
        _canAnimRun = false;

        if (_canAnimRun == false)
        {
            _animator.SetBool("CanAnimRun", false);
        }
    }
}
