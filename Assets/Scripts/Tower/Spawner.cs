using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Segment[] _segmentsTemplate;
    [SerializeField] private Block _blockTemplate;
    [SerializeField] private Finish _finishTemplate;
    [Space]
    [SerializeField] private Coin _coinTemplate;
    [SerializeField] private float _cointDropChance;
    [Space]
    [SerializeField] private int _towerSize;

    private void Start()
    {
        BuildTower();
    }

    private void BuildTower()
    {
        GameObject currentPoint = gameObject;
        float randomValue;

        for (int i = 0; i < _towerSize; i++)
        {
            currentPoint = BuildSegment(currentPoint, _segmentsTemplate[Random.Range(0, _segmentsTemplate.Length)].gameObject);
            currentPoint = BuildSegment(currentPoint, _blockTemplate.gameObject);

            randomValue = Random.Range(0, 100);

            if(randomValue < _cointDropChance)
            {
                BuildBonus(_coinTemplate.gameObject, currentPoint.transform);
            }
        }

        BuildSegment(currentPoint, _finishTemplate.gameObject);
    }

    private void BuildBonus(GameObject bonusTemplate,Transform currentSegment)
    {
        Vector3 bonusPosition = new Vector3(bonusTemplate.transform.position.x, currentSegment.position.y, bonusTemplate.transform.position.z);
        Instantiate(bonusTemplate, bonusPosition, Quaternion.identity, transform);
    }

    private GameObject BuildSegment(GameObject currentSegment, GameObject nextSegment)
    {
        return Instantiate(nextSegment, GetBuildPoint(currentSegment.transform, nextSegment.transform), Quaternion.identity, transform);
    }

    private Vector3 GetBuildPoint(Transform currentSegment, Transform nextSegment)
    {
        return new Vector3(transform.position.x, currentSegment.position.y + currentSegment.localScale.y / 2 + nextSegment.localScale.y / 2, transform.position.z);
    }
}

