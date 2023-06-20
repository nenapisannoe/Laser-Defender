using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
   [SerializeField] public int currentScore = 0;
   public void Awake()
   {
      SetUpSingleton();
   }
    
   private void SetUpSingleton()
   {
      if (FindObjectsOfType(GetType()).Length > 1)
      {
         Destroy(gameObject);
      }
      else
      {
         DontDestroyOnLoad(gameObject);
      }
    
   }
   public void AddToScore(int score)
   {
      currentScore += score;
   }

   public int GetScore()
   {
      return currentScore;
   }

   public void RestartGame()
   {
      Destroy(gameObject);
   }
   
}
