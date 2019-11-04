using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaTrigger : MonoBehaviour
{
    [SerializeField] private GameObject SpawnManager;
    [SerializeField] private GameObject gameCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ArenaTriggerRoutine();
            Destroy(other);
        }
    }

    IEnumerator ArenaTriggerRoutine()
    {
        yield return new WaitForSeconds(4);
        SpawnManager.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(true);
        
    }
}
