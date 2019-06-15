using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AutoScroll : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 0.4f;
    private float maxScroll;
    private ScrollRect scrollRect;
    private RectTransform contenRectTransform;
    private Vector2 defaultPosition;
    private bool canMove;
    private void Start()
    {
        this.scrollRect = GetComponent<ScrollRect>();
        this.contenRectTransform = this.scrollRect.content;
        this.maxScroll = this.contenRectTransform.rect.yMax;
        canMove = true;
        //this.StartCoroutine(this.ActivateAutoMove());
    }
    private void Update()
    {
        bool hasScrolled = this.contenRectTransform.position.y > this.maxScroll;
        if (canMove & !hasScrolled)
            this.Move();
        else
            SceneManager.LoadScene("SampleScene");
    }
    private void Move()
    {
        Vector2 contentPosition = this.contenRectTransform.position;
        Vector2 newPosition = new Vector2(contentPosition.x, contentPosition.y + this.scrollSpeed);
        this.contenRectTransform.position = newPosition;
    }
}
