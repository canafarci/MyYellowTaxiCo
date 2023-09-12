using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace TaxiGame.UI
{
    public class Fader : MonoBehaviour
    {
        private Coroutine _fadeRoutine;
        private CanvasGroup _canvasGroup;

        [Inject]
        private void Init(CanvasGroup canvasGroup)
        {
            _canvasGroup = canvasGroup;
        }

        private void Start()
        {
            FadeLoad();
        }

        private void FadeLoad()
        {
            if (_fadeRoutine != null)
                StopCoroutine(_fadeRoutine);

            _fadeRoutine = StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            yield return Fade(0f, 0.1f); // Fading in: targetAlpha = 0, fadeSpeed = 0.1f
        }

        private IEnumerator FadeOut()
        {
            yield return Fade(1f, 0.1f); // Fading out: targetAlpha = 1, fadeSpeed = 0.1f
        }

        private IEnumerator Fade(float targetAlpha, float fadeSpeed)
        {
            yield return new WaitForSeconds(1f);

            while (!Mathf.Approximately(_canvasGroup.alpha, targetAlpha))
            {
                _canvasGroup.alpha = Mathf.MoveTowards(_canvasGroup.alpha, targetAlpha, fadeSpeed);
                yield return new WaitForSeconds(.05f);
            }
        }
    }
}
