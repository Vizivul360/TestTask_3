using UnityEngine.UI;
using UnityEngine;
using Base;
using Game;
using Ecs;

namespace UI
{
    public class FormBehaviour : MonoBehaviour, IFormBind, Listener<Coin>
    {
        public GameObject overlay, timerObj, gameOver;

        public Text coins, timer, finish;

        public Button pause, resume;

        public string timeFormat;
        
        public ObjPool hpBars, hitTexts;

        private IGame game;

        private void Awake()
        {
            pause.onClick.AddListener(setPause);
            resume.onClick.AddListener(setResume);
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause) setPause();
        }

        private void setPause()
        {
            overlay.SetActive(true);
            game.Pause();
        }

        private void setResume()
        {
            overlay.SetActive(false);
            game.Resume();
        }

        public void SetGame(IGame obj)
        {
            game = obj;
        }

        public void On(Coin obj)
        {
            coins.text = obj.value.ToString();
        }

        public object GetHpBar()
        {
            return hpBars.Get();
        }

        public object GetHitText()
        {
            return hitTexts.Get();
        }

        public void Loose()
        {
            GameOver(false);
        }

        public void Win()
        {
            GameOver(true);
        }

        private void GameOver(bool isWin)
        {
            gameOver.SetActive(true);

            finish.text = isWin ? "You Win" : "You Loose";
            finish.color = isWin ? Color.green : Color.red;
        }

        public void SetTimer(float seconds)
        {
            timer.text = seconds.ToString(timeFormat);
            timerObj.SetActive(seconds > 0);
        }
    }

    public interface IFormBind
    {
        void SetGame(IGame obj);

        void SetTimer(float seconds);

        void Loose();

        void Win();

        object GetHpBar();

        object GetHitText();
    }
}
