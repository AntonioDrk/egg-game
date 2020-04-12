using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InteractEvent:UnityEvent <bool>{}

public class Interactable : MonoBehaviour
{
    /// <summary>
    /// Will be responsible of invoking functions from other scripts on interaction.
    /// The functions called need to have one argument of type bool
    /// When it's an Entry interaction the function will be called with TRUE
    /// When it's an Exit interaction the function will be called with FALSE
    /// </summary>
    [SerializeField] protected InteractEvent _interactCallback;
    [SerializeField] protected Animator _anim;

    void Start()
    {
        if (_interactCallback == null)
        {
            Debug.LogError("Interaction callback is empty! Are you sure you want to use this script?");
        }

        _anim = GetComponent<Animator>();
        
        if (_anim == null)
        {
            //Debug.LogError("There's no animator on this object.");
        }
    }

    /// <summary>
    /// Function to set the animation of the object going, it's used for Entry and Exit Animation through the param
    /// </summary>
    /// <param name="isExitAnim">If the animation is an Entry Animation or not, defaults to true</param>
    protected void RunAnimation(bool isEntryAnim = true)
    {
        _anim.SetBool("interact", isEntryAnim);
    }
    
}
