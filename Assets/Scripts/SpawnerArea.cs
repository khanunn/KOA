using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnerArea : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Vector3 areaCenter;
    public Vector3 areaSize;

    private void OnValidate()
    {
        areaCenter = this.transform.position;
    }

    private void OnEnable()
    {
        EventManager.instance.killEvents.onMonsterKilled += SpawnRandomObject;
    }
    private void OnDisable()
    {
        EventManager.instance.killEvents.onMonsterKilled -= SpawnRandomObject;
    }

    private void Start()
    {
        //SpawnRandomObject();
    }

    private async void SpawnRandomObject(string monsterID)
    {
        float randomX = Random.Range(areaCenter.x - areaSize.x / 2, areaCenter.x + areaSize.x / 2);
        float randomY = Random.Range(areaCenter.y - areaSize.y / 2, areaCenter.y + areaSize.y / 2);
        float randomZ = Random.Range(areaCenter.z - areaSize.z / 2, areaCenter.z + areaSize.z / 2);

        Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);
        await Task.Delay(2000);
        Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(areaCenter, areaSize);
    }
}
