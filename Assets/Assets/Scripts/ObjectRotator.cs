using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    [Header("Настройки")]
    [Tooltip("Скорость вращения (градусов в секунду)")]
    public float rotationSpeed = 90f;

    // Флаги состояния: крутимся мы сейчас или нет
    private bool _isRotatingLeft = false;
    private bool _isRotatingRight = false;

    private void Update()
    {
        // Если зажат флаг влево
        if (_isRotatingLeft)
        {
            // Вращаем против часовой стрелки
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
        // Если зажат флаг вправо
        else if (_isRotatingRight)
        {
            // Вращаем по часовой стрелке
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
    }

    public void StartRotatingLeft()
    {
        _isRotatingLeft = true;
    }

    public void StartRotatingRight()
    {
        _isRotatingRight = true;
    }

    public void StopRotating()
    {
        _isRotatingLeft = false;
        _isRotatingRight = false;
    }
}