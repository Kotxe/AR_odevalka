using UnityEngine;
using System.Collections.Generic;

public class WardrobeManager : MonoBehaviour
{
    [Header("Настройки")]
    [Tooltip("Список всех доступных вещей в игре")]
    public List<ClothingItem> database; // Сюда мы перетащим все наши ScriptableObjects

    [Tooltip("Точки, где будут появляться предметы (твои 4 колбы)")]
    public Transform[] spawnPoints;

    // Скрытый список, чтобы помнить, что сейчас висит в воздухе, и удалять это
    private List<GameObject> currentPreviews = new List<GameObject>();

    // Метод, который мы повесим на UI кнопки (Верх, Низ и т.д.)
    // Чтобы кнопки могли вызывать этот метод, нам нужно сделать "обертку", 
    // так как Unity UI кнопки не умеют напрямую передавать Enum.

    // Временные методы для кнопок (позже можно сделать красивее, но сейчас так проще)
    public void ShowTops() => SpawnByCategory(ClothingCategory.Top);
    public void ShowBottoms() => SpawnByCategory(ClothingCategory.Bottom);
    public void ShowFullBody() => SpawnByCategory(ClothingCategory.FullBody);
    public void ShowAccessories() => SpawnByCategory(ClothingCategory.Accessories);

    // Основная логика
    private void SpawnByCategory(ClothingCategory categoryToSpawn)
    {
        // 1. Удаляем всё, что было заспавнено до этого
        ClearCurrentPreviews();

        // 2. Ищем подходящие вещи в базе данных
        int spawnIndex = 0; // Индекс текущей колбы (0, 1, 2, 3)

        foreach (var item in database)
        {
            // Если категория совпадает
            if (item.category == categoryToSpawn)
            {
                // Проверяем, есть ли свободные места (не больше 4 вещей)
                if (spawnIndex >= spawnPoints.Length)
                {
                    Debug.Log("Внимание: вещей больше, чем слотов! Лишние не показаны.");
                    break;
                }

                // 3. Создаем превью (Instantiate)
                // Берем префаб из карточки товара, ставим в точку spawnPoints[i]
                GameObject newPreview = Instantiate(item.previewPrefab, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);

                // Важно: Сохраняем ссылку на созданный объект, чтобы потом удалить
                currentPreviews.Add(newPreview);

                spawnIndex++; // Переходим к следующей колбе
            }
        }
    }

    // Метод очистки колб
    public void ClearCurrentPreviews()
    {
        foreach (var obj in currentPreviews)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        currentPreviews.Clear();
    }
}