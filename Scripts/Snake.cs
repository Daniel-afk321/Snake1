using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    //Variável de Lista
    private List<Transform> _segments = new List<Transform>();
    //Prefab de direção
    public Transform segmentPrefab;
    //Vetor de direção que está feito pra começar da Direita
    public Vector2 direction = Vector2.right;
    //Tamanho que estará iniciando o player
    public int initialSize = 4;
    

    //Faz parte do método que compõem a classe MonoBehaviour
    private void Start()
    {
        //Está chamando o método ResetState
        ResetState();
    }

    //Faz parte do método que compõem a classe MonoBehaviour
    private void Update()
    {
        //Está fazendo a configuração do controle para cima e para baixo com a tecla W e com a Tecla S
        if (this.direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                this.direction = Vector2.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                this.direction = Vector2.down;
            }
        }
        //Está fazendo a configuração do controle para Direita e para esquerda com a tecla D e com a Tecla A
        else if (this.direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                this.direction = Vector2.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                this.direction = Vector2.left;
            }
        }
    }

    private void FixedUpdate()
    {
        
        // Constrói o corpo da cobra e a cabeça dela
        for (int i = _segments.Count - 1; i > 0; i--) {
            _segments[i].position = _segments[i - 1].position;
        }

        // Está posicionando os itens
        float x = Mathf.Round(this.transform.position.x) + this.direction.x;
        float y = Mathf.Round(this.transform.position.y) + this.direction.y;

        this.transform.position = new Vector2(x, y);
    }

    //Método de crescimento do corpo da cobra
    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }
    
    //Esta resetando o jogo 
    public void ResetState()
    {
        //Direção inicial  do player para direita e a limitação
        this.direction = Vector2.right;
        this.transform.position = Vector3.zero;

        // Está Destruindo o Array (corpo da cobra)
        for (int i = 1; i < _segments.Count; i++) {
            Destroy(_segments[i].gameObject);
        }

        // Faz volta a rotação do jogo normal quando a pessoa morre
        _segments.Clear();
        _segments.Add(this.transform);

        // Quando ele come um item a mais está chamando o método Grow
        for (int i = 0; i < this.initialSize - 1; i++) {
            Grow();
        }
    }

    //Faz parte do método que compõem a classe MonoBehaviour
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Toda vez que pega a fruta com a tag Food chama o método Grow para adicionar um quadrado
        if (other.tag == "Food") {
            Grow();
        }
        //Toda vez que o player bater em um  objeto com a tag Obstacle ele está chamando o método ResetState
        else if (other.tag == "Obstacle") {
            ResetState();
        }
    }

}
