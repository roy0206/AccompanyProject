using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    protected bool isInteractable = true;
    protected Sprite buttonIcon;
    [SerializeField] string iconLocation;

    private void Awake()
    {
        buttonIcon = Resources.Load<Sprite>(iconLocation);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && InteractionCondition() && isInteractable)
        {
            var player = collision.GetComponent<Player>();
            player.OnInteracting += OnInteracting;
            player.ChangeInteractionButtonTexture(buttonIcon);
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
