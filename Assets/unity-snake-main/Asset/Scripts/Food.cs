using UnityEngine;

public class Food : MonoBehaviour
{
    public Collider2D gridArea;

    private void Start()
    {
        //Está chamando o método RandomizePosition
        RandomizePosition();
    }

    public void RandomizePosition()
    {
        //Ele procura o tamanho de are do projeto
        Bounds bounds = this.gridArea.bounds;

        //Está fazendo com que a posição da fruta Spawn em pontos diferentes (randômico)
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        // Ele arredonda um valor para inteiro
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        this.transform.position = new Vector2(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Está chamando o método RandomizePosition
        RandomizePosition();
    }

}
