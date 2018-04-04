using UnityEngine;

public class Shop : MonoBehaviour {

	public TurretBlueprint stdTurret;
	public TurretBlueprint misTurret;
	public TurretBlueprint lasTurret;

	Manager buildManager;

	void Start ()
	{
		buildManager = Manager.instance;
	}
		
	public void SelectStdTurret()
	{
		buildManager.SelectTurretToBuild (stdTurret,"Turret");
	}

	public void SelectMisTurret()
	{
		buildManager.SelectTurretToBuild (misTurret,"MissileLauncher");
	}
	public void SelectLasTurret()
	{
		buildManager.SelectTurretToBuild (lasTurret,"LaserBeamer");
	}
}
