using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SceneController : MonoBehaviour
{
	public void LoadGame(string input)
	{
		SceneManager.LoadScene("Game");
	}
}