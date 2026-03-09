using UnityEngine;
using System.Collections.Generic;

public class ManequinManager : MonoBehaviour
{
    [Header("Точки привязки")]
    // Для простоты, если пивоты моделей настроены хорошо (в 0,0,0), то можно перетащить сюда сам Манекен.
    public Transform clothingParent;

    // --- Хранилище того, что сейчас надето ---
    private GameObject _currentTop;
    private GameObject _currentBottom;
    private GameObject _currentFullBody;

    // Аксессуаров может быть много, поэтому используем список
    private List<GameObject> _currentAccessories = new List<GameObject>();

    // --- ГЛАВНЫЙ МЕТОД: Надеть вещь ---
    public void EquipItem(ClothingItem newItem)
    {
        // Смотрим категорию новой вещи
        switch (newItem.category)
        {
            case ClothingCategory.Top:
                // Если надеваем Верх:
                RemoveObject(_currentTop);      // Снимаем старый верх
                RemoveObject(_currentFullBody); // Снимаем костюм (правило 1)
                _currentTop = SpawnClothing(newItem); // Надеваем новый
                break;

            case ClothingCategory.Bottom:
                // Если надеваем Низ:
                RemoveObject(_currentBottom);   // Снимаем старый низ
                RemoveObject(_currentFullBody); // Снимаем костюм (правило 1)
                _currentBottom = SpawnClothing(newItem); // Надеваем новый
                break;

            case ClothingCategory.FullBody:
                // Если надеваем Костюм:
                RemoveObject(_currentFullBody); // Снимаем старый костюм
                RemoveObject(_currentTop);      // Снимаем верх (правило 2)
                RemoveObject(_currentBottom);   // Снимаем низ (правило 2)
                _currentFullBody = SpawnClothing(newItem); // Надеваем новый
                break;

            case ClothingCategory.Accessories:
                // Если надеваем Аксессуар:
                // Просто добавляем новый, ничего не удаляя (правило 3)
                GameObject newAccessory = SpawnClothing(newItem);
                _currentAccessories.Add(newAccessory);
                break;
        }

        // Для текущей отладки
        AudioManager.Instance.PlayEquip();
        Debug.Log($"Надето: {newItem.itemName}");
    }

    // --- Метод создания модели на манекене ---
    private GameObject SpawnClothing(ClothingItem item)
    {
        // Создаем копию префаба (RealPrefab)
        GameObject clothingObj = Instantiate(item.realPrefab, clothingParent);

        // Применяем настройки смещения из ScriptableObject (если надо подправить позицию)
        clothingObj.transform.localPosition = item.positionOffset;
        clothingObj.transform.localEulerAngles = item.rotationOffset;

        return clothingObj;
    }

    // --- Метод безопасного удаления ---
    private void RemoveObject(GameObject obj)
    {
        if (obj != null)
        {
            Destroy(obj);
        }
    }

    // --- КНОПКА СБРОСА ВСЕГО ---
    public void ResetAllClothing()
    {
        RemoveObject(_currentTop);
        RemoveObject(_currentBottom);
        RemoveObject(_currentFullBody);

        // Удаляем все аксессуары из списка
        foreach (var acc in _currentAccessories)
        {
            RemoveObject(acc);
        }
        _currentAccessories.Clear(); // Очищаем список

        AudioManager.Instance.PlayUnequip();
        Debug.Log("Манекен полностью раздет.");
    }

}