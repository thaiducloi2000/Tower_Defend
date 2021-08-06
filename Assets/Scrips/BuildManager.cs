using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("THere are more  than 1 instance");
            return;
        }
        instance = this;
    }

    public GameObject BuildEffect;

    private TurretBlueprint turretToBuild;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money <turretToBuild.cost)
        {
            Debug.Log("Not enough money to build that");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;
        GameObject turret=(GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(),Quaternion.identity);
        node.turret=turret;


        GameObject effect=(GameObject) Instantiate(BuildEffect,node.GetBuildPosition(),Quaternion.identity);
        Destroy(effect, 5f);
        Debug.Log("Turret Build !! Money Left: " + PlayerStats.Money);
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}
