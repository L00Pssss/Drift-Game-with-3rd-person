using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class ColorGeneration : MonoBehaviour
{
    [SerializeField] private Image _imagePrefab;

    [SerializeField] private Transform _parent; 

    private void Awake()
    {
        GenerateImages();
    }

    private HashSet<Color> usedColors = new HashSet<Color>();

    private void GenerateImages()
    {

        for (int i = 0; i < 8; i++)
        {
            Image newImage = Instantiate(_imagePrefab, _parent);

            Color randomColor;
            do
            {
                randomColor = new Color(Random.value, Random.value, Random.value);
            } while (usedColors.Contains(randomColor));

            newImage.color = randomColor;
            usedColors.Add(randomColor);
        }

    }
}
