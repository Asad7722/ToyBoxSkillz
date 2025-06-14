 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
    public class Tutorial : MonoBehaviour
    {
        public GameObject[] images;
            
            public int count = 0,Final_Count;
        float Anim_Time = 0.4f;
        public GameObject doneBtn;
        public GameObject nextBtn;
    
        private void OnEnable()
        {
            doneBtn.SetActive(false);
            Atstart();
            count = 0;
        Final_Count = images.Length - 1;
            images[count].SetActive(true);
            doneBtn.SetActive(false);
            nextBtn.SetActive(true);
            images[count].transform.DOLocalMoveX(0f, Anim_Time).SetEase(Ease.Linear);
            
        }
        private void OnDisable()
        {
            images[count].SetActive(false);
        }
        void Atstart()
        {
            for(int i=1;i< images.Length; i++)
            {
                images[i].gameObject.SetActive(false);
                images[i].transform.DOLocalMoveX(1100f,0f);
            }
        }
        public void onNext()
        {
            int a = count;
                count++;
                
                images[a].transform.DOLocalMoveX(-1100f, Anim_Time).SetEase(Ease.Linear).OnComplete(()=> {
                images[a].transform.DOLocalMoveX(0f, 0f);
                images[a].SetActive(false);
            });
                images[count].SetActive(true);
                images[count].transform.DOLocalMoveX(1100f, 0f);
                images[count].transform.DOLocalMoveX(0f, Anim_Time).SetEase(Ease.Linear);
         
            if (count == Final_Count)
            {
                doneBtn.SetActive(true);
                nextBtn.SetActive(false);
            }
            else
            {
            }
        }
        
    }
 