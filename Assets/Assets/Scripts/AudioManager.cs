using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // --- СИНГЛТОН (Чтобы скрипт был доступен отовсюду) ---
    public static AudioManager Instance;

    [Header("Настройки источника")]
    public AudioSource audioSource; // Сюда перетащим компонент Audio Source

    [Header("Аудио Клипы")]
    public AudioClip clickSound;    // Клик по кнопкам
    public AudioClip equipSound;    // Звук ткани/надевания
    public AudioClip unequipSound;  // Звук снятия/сброса

    private void Awake()
    {
        // Если менеджера еще нет, назначаем себя главным.
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // --- МЕТОДЫ ДЛЯ ВЫЗОВА ---

    public void PlayClick()
    {
        PlaySound(clickSound);
    }

    public void PlayEquip()
    {
        PlaySound(equipSound);
    }

    public void PlayUnequip() // Для кнопки сброса
    {
        PlaySound(unequipSound);
    }

        // Вспомогательный метод
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            // PlayOneShot позволяет накладывать звуки друг на друга
            audioSource.PlayOneShot(clip);
        }
    }
}