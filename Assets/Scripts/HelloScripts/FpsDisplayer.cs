using UnityEngine;
using UnityEngine.UI;
namespace HelloScripts
{
    public class FpsDisplayer : MonoBehaviour
    {
        public int avgFrameRate;
        public Text display_Text;
        private float timer = 0;
        public void Update()
        {

            float current = 0;
            current = (int)(1f / Time.deltaTime);
            avgFrameRate = (int)current;

            if (timer < 1)
                timer += Time.deltaTime;
            else
            {
                timer = 0;
                display_Text.text = avgFrameRate.ToString() + " FPS";
            }

        }
    }
}