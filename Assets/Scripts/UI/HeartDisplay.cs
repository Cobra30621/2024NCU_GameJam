using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class HeartDisplay : MonoBehaviour
{
    [Required]
    [SerializeField]
    private Sprite haveHeart, noneHeart;

    [Required]
    public List<Image> heartImages;
    
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.OnHealthChanged.AddListener(UpdateHeartDisplay);
    }

    private void UpdateHeartDisplay(int currentHeart, int maxHeart)
    {
        foreach (var image in heartImages)
        {
            image.sprite = currentHeart > 0 ? haveHeart : noneHeart;
            currentHeart--;
        }
    }
}
