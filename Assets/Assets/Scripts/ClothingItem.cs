using UnityEngine;

[CreateAssetMenu(fileName = "New Clothing Item", menuName = "Clothing System/Clothing Item")]
public class ClothingItem : ScriptableObject
{
    [Header("Basic Info")]
    public string itemName;             // Название
    public ClothingCategory category;   // Категория (выбираем из списка)

    [Header("Prefabs")]
    public GameObject realPrefab;       // Большая модель (надевается на манекен)
    public GameObject previewPrefab;    // Маленькая модель (лежит в колбе)

    [Header("Adjustment")]
    // Эти настройки нужны, чтобы подогнать одежду на манекене, если пивот модели кривой
    public Vector3 positionOffset;
    public Vector3 rotationOffset;
}