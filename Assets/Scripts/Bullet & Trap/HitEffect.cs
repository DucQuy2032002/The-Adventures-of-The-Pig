using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    Animator AnimatorComponent;
    // Start is called before the first frame update
    private void Awake()
    {
        AnimatorComponent = GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine(PrepareNonLoopAnimation());
    }

    IEnumerator PrepareNonLoopAnimation()
    {
        var CurrentAnimationInfo = AnimatorComponent.GetCurrentAnimatorStateInfo(0);
        var AnimationDuration = CurrentAnimationInfo.length;
        yield return new WaitForSeconds(AnimationDuration);
        Destroy(this.gameObject);
    }

}
