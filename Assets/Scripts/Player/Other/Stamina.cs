using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{


    public float currentStamina { get; private set; }
    public bool canSpandStamina { get; private set; }

    [Header("Images")]
    public Image usedStamina;
    public Image secondUsedStamina;
    
    [Header("Info")]
    public float maxStamina;
    public float timeToHideStamina = 1f;
    public bool change;

    private bool show;
    private float timer;
    private Animator anim;




    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentStamina = maxStamina;
    }

    private void Update()
    {
        anim.SetBool("Show", show); //Play Animation

        if(show)
        {
            timer += Time.deltaTime;
            if(timer >= timeToHideStamina)
            {
                timer = 0;
                HideStamina();
            }
        }

        //Cooldown stamina
        if(!canSpandStamina)
        {
            if (currentStamina >= maxStamina)
            {
                canSpandStamina = true;
            }
        }
    }


    public void ShowStamina() => show = true;

    public void HideStamina() => show = false;

    public void IncreaseStamina()
    {
        if(currentStamina < maxStamina)
        {
            currentStamina = Mathf.Lerp(currentStamina, maxStamina + 1, 0.6f * Time.deltaTime);
            usedStamina.fillAmount = currentStamina / maxStamina;
            ShowStamina();
        }


    }
    public void DecriseStamina()
    {
        ShowStamina();
        currentStamina = Mathf.Lerp(currentStamina, -1, 0.5f * Time.deltaTime);

        usedStamina.fillAmount = currentStamina / maxStamina;
        secondUsedStamina.fillAmount = usedStamina.fillAmount + 0.1f;

        if(currentStamina <= 1f)
        {
            canSpandStamina = false;
        }
    }

}
