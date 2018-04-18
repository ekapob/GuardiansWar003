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
		if(PlayerStats.Money >= 100)
			buildManager.SelectTurretToBuild (stdTurret,"Turret");
		else
			PauseAndExitButton.Instance.RunNoGold ();
	}

	public void SelectMisTurret()
	{
		if(PlayerStats.Money >= 500)
			buildManager.SelectTurretToBuild (misTurret,"MissileLauncher");
		else
			PauseAndExitButton.Instance.RunNoGold ();
	}
	public void SelectLasTurret()
	{
		if (PlayerStats.Money >= 225)
			buildManager.SelectTurretToBuild (lasTurret, "LaserBeamer");
		else
			PauseAndExitButton.Instance.RunNoGold ();
	}
}
