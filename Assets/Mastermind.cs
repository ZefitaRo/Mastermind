using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Zef
{
    public class Mastermind : MonoBehaviour
    {
        [Tooltip("Sprite des petites boules")]
        public Sprite Yellow, Blue, Red, Green, White, Black;
        [Header("Code secret")]
        //[HideInInspector]
        public string[] secretCode = new string[4];
        public string[] secretCodeTemp = new string[4];
        

        [HideInInspector]
        public int i = 1;
        private Dictionary<string, Sprite> dicoSprite = new Dictionary<string, Sprite>();
        private string[] codePlayer = new string[4];
        public GameObject HiddenSlot;
        
        // Start is called before the first frame update
        void Awake()
        {
            dicoSprite.Add("yellow", Yellow);
            dicoSprite.Add("blue", Blue);
            dicoSprite.Add("red", Red);
            dicoSprite.Add("green", Green);
            dicoSprite.Add("white", White);
            dicoSprite.Add("black", Black);
        }

        public Array GetNewSecretCode()
        {
            for (int i = 0; i < 4; i++)
            {
                int rnd = UnityEngine.Random.Range(0, dicoSprite.Count);
                secretCode.SetValue(dicoSprite.ElementAt(rnd).Key, i);
            }

            transform.Find("c1").GetComponent<Image>().sprite = dicoSprite[secretCode.GetValue(0).ToString()];
            transform.Find("c2").GetComponent<Image>().sprite = dicoSprite[secretCode.GetValue(1).ToString()];
            transform.Find("c3").GetComponent<Image>().sprite = dicoSprite[secretCode.GetValue(2).ToString()];
            transform.Find("c4").GetComponent<Image>().sprite = dicoSprite[secretCode.GetValue(3).ToString()];
            
            return secretCode;
        }

        public int GetGoodPosition(string[] code)
        {
            Array.Copy(secretCode, secretCodeTemp, secretCode.Length);

            int good = 0;

            for (int i = 0; i < secretCodeTemp.Length; i++)
            {
                if (code[i] == secretCodeTemp[i])
                {
                    good++;
                    code[i] = "good";
                    secretCodeTemp[i] = "good";
                }
            }
            Array.Copy(code, codePlayer, code.Length);
            return good;
        }

        public int GetWrongPosition()
        {
            int wrong = 0;
            for (int i = 0; i < codePlayer.Length; i++)
            {
                for (int j = 0; j < secretCodeTemp.Length; j++)
                {
                    if (codePlayer[i] == secretCodeTemp[j] && (codePlayer[i] != "good") && secretCodeTemp[j] != "good")
                    {
                        secretCodeTemp[j] = "wrong";
                        wrong++;
                        break;
                    }
                }
            }

            return wrong;
        }

    }
}
