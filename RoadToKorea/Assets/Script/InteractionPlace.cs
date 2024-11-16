using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    protected bool isInteractable = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && InteractionCondition() && isInteractable)
        {
            collision.GetComponent<Player>().OnInteracting += OnInteracting;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            try
            {
                collision.GetComponent<Player>().OnInteracting -= OnInteracting;
            }
            catch { }
        }
    }

    public abstract void OnInteracting();

    public abstract bool InteractionCondition();
}
