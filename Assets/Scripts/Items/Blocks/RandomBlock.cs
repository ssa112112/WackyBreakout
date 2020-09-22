using UnityEngine;

/// <summary>
/// Generate random block instead itself
/// </summary>
public class RandomBlock : MonoBehaviour
{
    [SerializeField]
    GameObject standartBlockPrefab;
    [SerializeField]
    GameObject bonusBlockPrefab;
    [SerializeField]
    GameObject freezerBlockPrefab;
    [SerializeField]
    GameObject speedupBlockPrefab;
    void Start()
    {
        GenerateRandomBlock();
        DestroyImmediate(gameObject);
    }


    /// <summary>
    /// Generate a random block
    /// </summary>
    void GenerateRandomBlock()
    {
        GameObject randomPrefab;

        //Set sum of all probabilities
        var sumOfProbabilities = ConfigurationUtils.BonusBlockProbability + ConfigurationUtils.FreezertBlockProbability
            + ConfigurationUtils.SpeedupBlockProbability + ConfigurationUtils.StandartBlockProbability;

        //Generate random number between 0 and sum of all probabilities
        var randomNumber = Random.Range(0, sumOfProbabilities);

        //Define randomPrefab
        //It's the shortest algorithm
        if (randomNumber < ConfigurationUtils.BonusBlockProbability)
            randomPrefab = bonusBlockPrefab;
        else if (randomNumber < ConfigurationUtils.FreezertBlockProbability + ConfigurationUtils.BonusBlockProbability)
            randomPrefab = freezerBlockPrefab;
        else if (randomNumber < ConfigurationUtils.FreezertBlockProbability + ConfigurationUtils.BonusBlockProbability
            + ConfigurationUtils.SpeedupBlockProbability)
            randomPrefab = speedupBlockPrefab;
        else
            randomPrefab = standartBlockPrefab;

        //Create new object from randomPrefab
        Instantiate(randomPrefab, transform.position, Quaternion.identity);
    }
}
