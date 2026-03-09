using UnityEngine;
using UnityEngine.EventSystems; // Нужно для проверки, не жмем ли мы на UI

public class ARTouchController : MonoBehaviour
{
    [Header("Настройки жестов")]
    public float rotationSpeed = 0.2f;
    public float scaleSpeed = 0.005f;
    public float minScale = 0.05f;
    public float maxScale = 0.5f;

    void Update()
    {
        // Если нет касаний - ничего не делаем
        if (Input.touchCount == 0) return;

        // ВАЖНО: Блокируем жесты, если палец нажал на кнопку UI
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;

        // ВРАЩЕНИЕ (Свайп)
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                // Крутим вокруг оси Y (зеленая стрелка) в зависимости от движения пальца по оси X
                transform.Rotate(0, -touch.deltaPosition.x * rotationSpeed, 0);
            }
        }
        // МАСШТАБИРОВАНИЕ (Щипок)
        else if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // Находим позиции пальцев в предыдущем кадре
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;
            Vector2 touch2PrevPos = touch2.position - touch2.deltaPosition;

            // Считаем дистанцию между пальцами тогда и сейчас
            float prevMagnitude = (touch1PrevPos - touch2PrevPos).magnitude;
            float currentMagnitude = (touch1.position - touch2.position).magnitude;
            float difference = currentMagnitude - prevMagnitude;

            // Меняем размер
            Vector3 newScale = transform.localScale + Vector3.one * (difference * scaleSpeed);

            // Ограничиваем размер, чтобы манекен не стал молекулой или гигантом
            newScale.x = Mathf.Clamp(newScale.x, minScale, maxScale);
            newScale.y = Mathf.Clamp(newScale.y, minScale, maxScale);
            newScale.z = Mathf.Clamp(newScale.z, minScale, maxScale);

            transform.localScale = newScale;
        }
    }
}