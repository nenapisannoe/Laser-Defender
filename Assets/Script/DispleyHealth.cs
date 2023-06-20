using TMPro;
using UnityEngine;

public class DispleyHealth : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    Player player;
    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        player = FindObjectOfType<Player>();
    }
    
    // Update is called once per frame
    void Update()
    {
        healthText.text = player.GetHealth().ToString();
    }
}
