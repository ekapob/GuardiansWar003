using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGamePause : MonoBehaviour {

	void Start () 
	{
		CanvasGameplayControl.Instance.winStat = true;
		//Battle Log Collect
		// 1 - knight
		// 2 - mon
		if (MotherScript.Instance.currentGameSide == 1) {
			foreach (TestNode node in TestNode1.Instance.node) {
				if (node.GetTurretName() != null) {
					string name = node.GetTurretName ();
					Debug.Log (node.GetNodeNo() + " " + name);
					if (name == PlayerStats.Instance.tw1lv1.name+"(Clone)") {
						MotherScript.Instance.playerLog [0]++;
					} else if (name == PlayerStats.Instance.tw1lv2.name+"(Clone)") {
						MotherScript.Instance.playerLog [1]++;
					}/* else if (name == PlayerStats.Instance.tw1lv3.name+"(Clone)") {
						MotherScript.Instance.playerLog [2]++;
					}*/ else if (name == PlayerStats.Instance.tw2lv1.name+"(Clone)") {
						MotherScript.Instance.playerLog [3]++;
					} else if (name == PlayerStats.Instance.tw2lv2.name+"(Clone)") {
						MotherScript.Instance.playerLog [4]++;
					}/* else if (name == PlayerStats.Instance.tw2lv3.name+"(Clone)") {
						MotherScript.Instance.playerLog [5]++;
					}*/ else if (name == PlayerStats.Instance.tw3lv1.name+"(Clone)") {
						MotherScript.Instance.playerLog [6]++;
					} else if (name == PlayerStats.Instance.tw3lv2.name+"(Clone)") {
						MotherScript.Instance.playerLog [7]++;
					}/* else if (name == PlayerStats.Instance.tw3lv3.name+"(Clone)") {
						MotherScript.Instance.playerLog [8]++;
					}*/
				}
				MotherScript.Instance.playerLog [9] = UnitShop.Instance.lvlUnit [0];
				MotherScript.Instance.playerLog [10] = UnitShop.Instance.lvlUnit [1];
				MotherScript.Instance.playerLog [11] = UnitShop.Instance.lvlUnit [2];
				MotherScript.Instance.playerLog [12] = UnitShop.Instance.lvlUnit [3];
			}
		} else if (MotherScript.Instance.currentGameSide == 2) {
			foreach (TestNode node in TestNode2.Instance.node) {
				if (node.GetTurretName() != null) {
					string name = node.GetTurretName ();
					if (name == PlayerStats.Instance.tw1lv1.name+"(Clone)") {
						MotherScript.Instance.playerLog [0]++;
					} else if (name == PlayerStats.Instance.tw1lv2.name+"(Clone)") {
						MotherScript.Instance.playerLog [1]++;
					}/* else if (name == PlayerStats.Instance.tw1lv3.name+"(Clone)") {
						MotherScript.Instance.playerLog [2]++;
					}*/ else if (name == PlayerStats.Instance.tw2lv1.name+"(Clone)") {
						MotherScript.Instance.playerLog [3]++;
					} else if (name == PlayerStats.Instance.tw2lv2.name+"(Clone)") {
						MotherScript.Instance.playerLog [4]++;
					}/* else if (name == PlayerStats.Instance.tw2lv3.name+"(Clone)") {
						MotherScript.Instance.playerLog [5]++;
					}*/ else if (name == PlayerStats.Instance.tw3lv1.name+"(Clone)") {
						MotherScript.Instance.playerLog [6]++;
					} else if (name == PlayerStats.Instance.tw3lv2.name+"(Clone)") {
						MotherScript.Instance.playerLog [7]++;
					}/* else if (name == PlayerStats.Instance.tw3lv3.name+"(Clone)") {
						MotherScript.Instance.playerLog [8]++;
					}*/
				}
				MotherScript.Instance.playerLog [9] = UnitShop.Instance.lvlUnit [0];
				MotherScript.Instance.playerLog [10] = UnitShop.Instance.lvlUnit [1];
				MotherScript.Instance.playerLog [11] = UnitShop.Instance.lvlUnit [2];
				MotherScript.Instance.playerLog [12] = UnitShop.Instance.lvlUnit [3];
			}

		}/* else if (MotherScript.Instance.currentGameSide == 3) {
			foreach (TestNode node in TestNode3.Instance.node) {
				if (node.GetTurretName() != null) {
					string name = node.GetTurretName () + "(Clone)";
					if (name == PlayerStats.Instance.tw1lv1.name+"(Clone)") {
						MotherScript.Instance.playerLog [0]++;
					} else if (name == PlayerStats.Instance.tw1lv2.name+"(Clone)") {
						MotherScript.Instance.playerLog [1]++;
					} else if (name == PlayerStats.Instance.tw1lv3.name+"(Clone)") {
						MotherScript.Instance.playerLog [2]++;
					} else if (name == PlayerStats.Instance.tw2lv1.name+"(Clone)") {
						MotherScript.Instance.playerLog [3]++;
					} else if (name == PlayerStats.Instance.tw2lv2.name+"(Clone)") {
						MotherScript.Instance.playerLog [4]++;
					} else if (name == PlayerStats.Instance.tw2lv3.name+"(Clone)") {
						MotherScript.Instance.playerLog [5]++;
					} else if (name == PlayerStats.Instance.tw3lv1.name+"(Clone)") {
						MotherScript.Instance.playerLog [6]++;
					} else if (name == PlayerStats.Instance.tw3lv2.name+"(Clone)") {
						MotherScript.Instance.playerLog [7]++;
					} else if (name == PlayerStats.Instance.tw3lv3.name+"(Clone)") {
						MotherScript.Instance.playerLog [8]++;
					}
				}
			}

		} else if (MotherScript.Instance.currentGameSide == 4) {
			foreach (TestNode node in TestNode3.Instance.node) {
				if (node.GetTurretName() != null) {
					string name = node.GetTurretName () + "(Clone)";
					if (name == PlayerStats.Instance.tw1lv1.name+"(Clone)") {
						MotherScript.Instance.playerLog [0]++;
					} else if (name == PlayerStats.Instance.tw1lv2.name+"(Clone)") {
						MotherScript.Instance.playerLog [1]++;
					} else if (name == PlayerStats.Instance.tw1lv3.name+"(Clone)") {
						MotherScript.Instance.playerLog [2]++;
					} else if (name == PlayerStats.Instance.tw2lv1.name+"(Clone)") {
						MotherScript.Instance.playerLog [3]++;
					} else if (name == PlayerStats.Instance.tw2lv2.name+"(Clone)") {
						MotherScript.Instance.playerLog [4]++;
					} else if (name == PlayerStats.Instance.tw2lv3.name+"(Clone)") {
						MotherScript.Instance.playerLog [5]++;
					} else if (name == PlayerStats.Instance.tw3lv1.name+"(Clone)") {
						MotherScript.Instance.playerLog [6]++;
					} else if (name == PlayerStats.Instance.tw3lv2.name+"(Clone)") {
						MotherScript.Instance.playerLog [7]++;
					} else if (name == PlayerStats.Instance.tw3lv3.name+"(Clone)") {
						MotherScript.Instance.playerLog [8]++;
					}
				}
			}

		}*/
		Time.timeScale = 0f;
	}
}
