using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARUIManager : MonoBehaviour
{
    [Header("AR Настройки")]
    public ARTrackedImageManager imageManager; // Ссылка на искатель картинок
    public GameObject mainUIPanel;             // Главная панель интерфейса (которую прячем)

    [Header("Подменю (Панели с цифрами)")]
    public GameObject[] subMenus; // 0-Верх, 1-Низ, 2-Костюм, 3-Аксессуары

    [Header("Базы одежды (Scriptable Objects)")]
    public ClothingItem[] tops;
    public ClothingItem[] bottoms;
    public ClothingItem[] fullBodies;
    public ClothingItem[] accessories;

    private ManequinManager _currentManequin; // Ссылка на заспавненный манекен

    private void OnEnable()
    {
        // Подписываемся на событие "Камера нашла или потеряла картинку"
        imageManager.trackedImagesChanged += OnImageChanged;
        mainUIPanel.SetActive(false); // Прячем UI на старте
    }

    private void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnImageChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        bool isTracking = false;

        // Проверяем все найденные картинки
        foreach (var trackedImage in imageManager.trackables)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                isTracking = true; // Картинка в кадре!

                // Ищем манекен внутри найденной картинки
                if (_currentManequin == null)
                {
                    _currentManequin = trackedImage.GetComponentInChildren<ManequinManager>();
                }
                break;
            }
        }

        // Показываем или прячем интерфейс
        mainUIPanel.SetActive(isTracking);
    }

    // Метод для кнопок категорий (передаем 0, 1, 2 или 3)
    public void ToggleSubMenu(int menuIndex)
    {
        for (int i = 0; i < subMenus.Length; i++)
        {
            // Если индекс совпал и меню было закрыто - открываем. Иначе - закрываем.
            bool shouldBeOpen = (i == menuIndex) && !subMenus[i].activeSelf;
            subMenus[i].SetActive(shouldBeOpen);
        }
    }

    // В Unity кнопке передаем цифру (индекс вещи в массиве: 0, 1, 2 или 3)

    public void EquipTop(int itemIndex)
    {
        if (_currentManequin != null && itemIndex < tops.Length)
            _currentManequin.EquipItem(tops[itemIndex]);
    }

    public void EquipBottom(int itemIndex)
    {
        if (_currentManequin != null && itemIndex < bottoms.Length)
            _currentManequin.EquipItem(bottoms[itemIndex]);
    }

    public void EquipFullBody(int itemIndex)
    {
        if (_currentManequin != null && itemIndex < fullBodies.Length)
            _currentManequin.EquipItem(fullBodies[itemIndex]);
    }

    public void EquipAccessory(int itemIndex)
    {
        if (_currentManequin != null && itemIndex < accessories.Length)
            _currentManequin.EquipItem(accessories[itemIndex]);
    }

    public void ResetAll()
    {
        if (_currentManequin != null) _currentManequin.ResetAllClothing();
    }
}