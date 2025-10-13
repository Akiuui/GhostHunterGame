using System.Collections;
using UnityEngine;

public class OnClickRunAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        animator.SetBool("dissapear", true);

        StartCoroutine(ResetState());
    }
    IEnumerator ResetState()
    {
        yield return new WaitForSeconds(3);
        animator.SetBool("dissapear", false);
    }
}
