using UnityEngine;

public class RechargerNiveau : MonoBehaviour
{
    public void Recharger()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }
}

