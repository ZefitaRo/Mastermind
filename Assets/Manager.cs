using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace Zef
{
    public class Manager : MonoBehaviour
    {
        [SerializeField] Mastermind mastermind;
        
        [SerializeField] GameObject[] gameSlot = new GameObject[10];
        [SerializeField] GameObject[] gameVerif = new GameObject[4];
       
        private int currentSlot = 0, currentCol = 1;
        
        private Sprite emptySprite;
        
        [SerializeField] private string[] code = new string[4];
        void Start()
        {
            mastermind.GetNewSecretCode();
            emptySprite = gameSlot[currentSlot].transform.Find("c1").GetComponent<Image>().sprite;

        }

        public void ColorSelect(Sprite sp)
        {
            if (!mastermind.HiddenSlot.activeInHierarchy) return;

            gameSlot[currentSlot].transform.Find("c" + currentCol).GetComponent<Image>().sprite = sp;
            code.SetValue(sp.name, currentCol -1);

            currentCol++;
            if (currentCol == 5) currentCol = 1;
        }

        public void Cancel()
        {
            for (int i = 1; i < 5; i++)
            {
                gameSlot[currentSlot].transform.Find("c" + i).GetComponent<Image>().sprite = emptySprite;
            }

            currentCol = 1;
        }

        public void Replay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ExitGame() 
        {
            Application.Quit();
        }

        public void Check()
        {
            if (!mastermind.HiddenSlot.activeInHierarchy) return;
            
            int x = 0;

            foreach (Transform child in gameSlot[currentSlot].transform)
            {
                if (child.tag == "Verif")
                {
                    gameVerif[x] = child.gameObject;
                    x++;
                }
            }

            for (int i = 1; i < 5; i++)
            {
                if (gameSlot[currentSlot].transform.Find("c" + i).GetComponent<Image>().sprite == emptySprite) return;
                
            }
            
            int nbGoodPosition = mastermind.GetGoodPosition(code);
            for (int i = 0; i < nbGoodPosition; i++)
            {
                gameVerif[i].GetComponent<Image>().sprite = mastermind.Black;
            }

            int nbWrongPosition = mastermind.GetWrongPosition();
            for (int i = nbGoodPosition; i < nbWrongPosition + nbGoodPosition; i++)
            {
                gameVerif[i].GetComponent<Image>().sprite = mastermind.White;
            }

            if (nbGoodPosition == 4)
            {
                Debug.Log("You Win");
                Win();
                return;
            }

            if (currentSlot == 9)
            {
                Loose();
                Debug.Log("You Loose");
                return;
            }

            currentSlot++;

            Color originColor = gameSlot[currentSlot].GetComponent<Image>().color;
            Color selectionColor = originColor;
            selectionColor.a = 0.25f;

            gameSlot[currentSlot].GetComponent<Image>().color = selectionColor;
            gameSlot[currentSlot - 1].GetComponent<Image>().color = originColor;
        }

        void Win()
        {
            mastermind.HiddenSlot.SetActive(false);
        }

        void Loose()
        {
            mastermind.HiddenSlot.SetActive(false);
        }
        
        
        
    }
}
