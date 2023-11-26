using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// este código vai na luz da lanterna
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Light))]
public class Lanterna : MonoBehaviour {
 
    [Header("UI")]
    public Image imagemBateria;
    public Button botaoOpcional;

    [Header("Sons")]
    public AudioClip click;

    [Header("Duração")]
    public float tempoTotalBaterias = 250;

    [Header("Angulo")]
    public float anguloMinimo = 20.0f;
    public float anguloMaximo = 40.0f;

    [Header("Intensidade")]
    public float intensidadeMinima = 0.5f;
    public float intensidadeMaxima = 5.0f;

    [Header("Distancia")]
    public float distanciaMin = 3.0f;
    public float distanciaMax = 10.0f;

    [Header("Inicialização")]
    public bool comecarLigado = false;

    Light luzLanterna;
    AudioSource somLanterna;
    bool podeLigar;
    float tempoMinimo;
    float tempoMaxInicial;
    float inverseLerpFactor;
    [HideInInspector]
    public float tempoAtualLanterna;

    void Awake (){
        transform.root.tag = "Player";
        //
        luzLanterna = GetComponent<Light> ();
        luzLanterna.enabled = comecarLigado;
        //
        somLanterna = GetComponent<AudioSource> ();
        somLanterna.clip = click;
        somLanterna.loop = false;
        somLanterna.playOnAwake = false;
        //
        tempoAtualLanterna = tempoMaxInicial = tempoTotalBaterias;
        tempoMinimo = tempoTotalBaterias / 15;
        podeLigar = true;
        //
        if (imagemBateria) {
            imagemBateria.type = Image.Type.Filled;
            imagemBateria.fillMethod = Image.FillMethod.Horizontal;
            imagemBateria.fillOrigin = 0;
        }
        if (botaoOpcional) {
            botaoOpcional.onClick = new Button.ButtonClickedEvent();
            botaoOpcional.onClick.AddListener(() => BotaoUILanterna());
        }
    }

    void Update (){
        inverseLerpFactor = Mathf.InverseLerp (0, tempoMaxInicial, tempoAtualLanterna);
        ControleDaLanterna ();
        ControleDaUI ();
    }

    void ControleDaLanterna(){
        //Timer lanterna
        if (luzLanterna.enabled == true && podeLigar == true) {
            tempoAtualLanterna -= Time.deltaTime;
        }

        //Input lanterna
        if (Input.GetKeyDown (KeyCode.F)) {
            luzLanterna.enabled = !luzLanterna.enabled;
            somLanterna.PlayOneShot(somLanterna.clip);
        }

        //Checagens de tempo
        if (tempoAtualLanterna <= tempoMinimo) {
            luzLanterna.enabled = false;
            podeLigar = false;
        } else {
            podeLigar = true;
        }
        tempoAtualLanterna = Mathf.Clamp (tempoAtualLanterna, 0.0f, tempoMaxInicial);

        //Setar propriedades nas luzes
        luzLanterna.spotAngle = Mathf.Clamp((anguloMaximo * inverseLerpFactor), anguloMinimo, anguloMaximo);
        luzLanterna.range = Mathf.Clamp ((distanciaMax * inverseLerpFactor), distanciaMin, distanciaMax);
        luzLanterna.intensity = Mathf.Clamp ((intensidadeMaxima * inverseLerpFactor), intensidadeMinima, intensidadeMaxima);
    }

    void ControleDaUI(){
        if (imagemBateria) {
            imagemBateria.fillAmount = inverseLerpFactor;
        }
    }

    public void BotaoUILanterna(){
        luzLanterna.enabled = !luzLanterna.enabled;
        somLanterna.PlayOneShot(somLanterna.clip);
    }
}
