using UnityEngine;

public class OnClickRunAnimation : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        print("Mouse down");
        animator.SetBool("dissapear", true);
    }
}
