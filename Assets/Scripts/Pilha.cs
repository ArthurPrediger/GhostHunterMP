using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pilha : MonoBehaviour {

    public Button botaoOpcional;
    public KeyCode teclaPegarBateria = KeyCode.E;
    [Range(10, 500)]
    public float tempoQueVaiRepor = 50;
    [Range(0.1f, 3.0f)]
    public float distanciaMinima = 2;
    //
    bool dentroDoColisor;
    Lanterna lanternaTemp;
    Transform target;
    float distancia;

    void Start() {
        dentroDoColisor = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (botaoOpcional) {
            botaoOpcional.onClick = new Button.ButtonClickedEvent();
            botaoOpcional.onClick.AddListener(() => BotaoUIPegarBateria());
            botaoOpcional.gameObject.SetActive(false);
        }

    }

    void Update() {
        distancia = Vector3.Distance(transform.position, target.position);
        if (distancia < distanciaMinima) {
            dentroDoColisor = true;
        } else {
            dentroDoColisor = false;
            lanternaTemp = null;
        }
        //
        if (dentroDoColisor && Input.GetKeyDown(teclaPegarBateria)) {
            lanternaTemp = target.gameObject.GetComponentInChildren<Lanterna>();
            if (lanternaTemp) {
                float soma = lanternaTemp.tempoAtualLanterna + (tempoQueVaiRepor * 0.5f);
                if (soma < lanternaTemp.tempoTotalBaterias) {
                    lanternaTemp.tempoAtualLanterna += tempoQueVaiRepor;
                    Destroy(this.gameObject);
                }
            }
        }
        //
        if (botaoOpcional) {
            if (dentroDoColisor) {
                botaoOpcional.gameObject.SetActive(true);
            } else {
                botaoOpcional.gameObject.SetActive(false);
            }
        }
    }

    public void BotaoUIPegarBateria() {
        if (dentroDoColisor) {
            lanternaTemp = target.gameObject.GetComponentInChildren<Lanterna>();
            if (lanternaTemp) {
                float soma = lanternaTemp.tempoAtualLanterna + (tempoQueVaiRepor * 0.5f);
                if (soma < lanternaTemp.tempoTotalBaterias) {
                    lanternaTemp.tempoAtualLanterna += tempoQueVaiRepor;
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
