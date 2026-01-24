using UnityEngine;

public class PlayerView
{
    private Animator animator;

    private const string IsWalk = nameof(IsWalk);
    private const string IsSwampWalk = nameof(IsSwampWalk);

    public PlayerView(Animator animator)
    {
        this.animator = animator;
    }

    public void ViewWalk()
    {
        animator.SetBool(IsWalk, true);
        animator.SetBool(IsSwampWalk, false);
    }

    public void ViewSwampWalk()
    {
        animator.SetBool(IsWalk, false);
        animator.SetBool(IsSwampWalk, true);
    }

    public void ViewIdle()
    {
        animator.SetBool(IsWalk, false);
        animator.SetBool(IsSwampWalk, false);
    }
}