using UnityEngine;
using TMPro;

public class StatManager : MonoBehaviour
{
    public static StatManager Instance { get; private set; }

    [Header("Population")]
    [SerializeField] private int space = 0;
    [SerializeField] private int population = 0;

    [Header("Jobs")]
    [SerializeField] private int totalJobs = 0;
    [SerializeField] private int jobsTaken = 0;

    [Header("Buildings")]
    [SerializeField] private int residentialBuildings = 0;
    [SerializeField] private int commercialZones = 0;
    [SerializeField] private int industrialZones = 0;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI populationText;

    private const int peoplePerResidentialBuilding = 5;
    private const int jobsPerCommercialZone = 2;
    private const int jobsPerIndustrialZone = 10;
    private const float employmentRate = 0.8f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdatePopulationAndJobs()
    {
        space = residentialBuildings * peoplePerResidentialBuilding;
        totalJobs = (commercialZones * jobsPerCommercialZone) + (industrialZones * jobsPerIndustrialZone);
        population = Mathf.Min(space, totalJobs);
        jobsTaken = Mathf.Min(totalJobs, Mathf.FloorToInt(population * employmentRate));
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (populationText != null)
        {
            populationText.text = $"Population: {population}";
        }
    }

    public void AddHouseCapacity(int capacity)
    {
        residentialBuildings += capacity;
        UpdatePopulationAndJobs();
    }

    public void AddResidentialBuilding()
    {
        residentialBuildings++;
        UpdatePopulationAndJobs();
    }

    public void RemoveResidentialBuilding()
    {
        residentialBuildings--;
        UpdatePopulationAndJobs();
    }

    public void AddCommercialZone()
    {
        commercialZones++;
        UpdatePopulationAndJobs();
    }

    public void RemoveCommercialZone()
    {
        commercialZones--;
        UpdatePopulationAndJobs();
    }

    public void AddIndustrialZone()
    {
        industrialZones++;
        UpdatePopulationAndJobs();
    }

    public void RemoveIndustrialZone()
    {
        industrialZones--;
        UpdatePopulationAndJobs();
    }
}
