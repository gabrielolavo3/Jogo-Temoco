using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 targetPosition;
    private bool isMoving = false;

    //controlar se o jogador pode se mover
    public static bool jogadorPodeMover = true;

    void Update()
    {
        //o jogador não pode se mover, sai do Update
        if (!jogadorPodeMover)
            return;

        if (Input.GetMouseButtonDown(0)) // Detecta toque/click
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMoving = true;
        }

        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if ((Vector2)transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }
}
