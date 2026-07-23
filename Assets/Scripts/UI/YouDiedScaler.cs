using UnityEngine;
using DG.Tweening;
using TMPro;

public class YouDiedScaler : MonoBehaviour
{
    
    public void ToScale(TextMeshProUGUI text)
    {
        text.transform.DOScale(new Vector3(8, 8, 8), 3f).SetUpdate(true);
    }
}
