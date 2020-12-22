using UnityEngine;

public class MenuOpener : MonoBehaviour
{

    public GameObject Panel;


    public void OpenPanel()
    {
        if (Panel != null)
        {
            Animator animator = Panel.GetComponent<Animator>();

            if (animator != null)
            {
                bool isOpen = animator.GetBool("open");
                animator.SetBool("open", !isOpen);
            }
        }
    }

}
