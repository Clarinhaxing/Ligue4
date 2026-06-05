using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject red, blue;

    bool isPlayer, hasGameFinished;

    [SerializeField]
    TMP_Text turnMessage;

    const string RED_MESSAGE = "Vez do Vermelho";
    const string BLUE_MESSAGE = "Vez do Azul";

    Color RED_COLOR = new Color32(231, 29, 54, 255);
    Color BLUE_COLOR = new Color32(0, 120, 255, 255);

    Board myBoard;

    private void Awake()
    {
        isPlayer = true;
        hasGameFinished = false;

        turnMessage.text = RED_MESSAGE;
        turnMessage.color = RED_COLOR;

        myBoard = new Board();
    }

    public void GameStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hasGameFinished)
                return;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (!hit.collider)
                return;

            if (hit.collider.CompareTag("Press"))
            {
                Column column = hit.collider.GetComponent<Column>();

                if (column == null)
                    return;

                // Coluna cheia
                if (column.targetLocation.y > 1.5f)
                    return;

                Vector3 spawnPos = column.spawnLocation;
                Vector3 targetPos = column.targetLocation;

                // Cria a peça
                GameObject circle = Instantiate(isPlayer ? red : blue);

                circle.transform.position = spawnPos;

                Mover mover = circle.GetComponent<Mover>();

                if (mover != null)
                {
                    mover.targetPosition = targetPos;
                }

                // Próxima posição disponível
                column.targetLocation = new Vector3(
                    targetPos.x,
                    targetPos.y + 0.7f,
                    targetPos.z
                );

                // Atualiza tabuleiro
                myBoard.UpdateBoard(column.columnIndex - 1, isPlayer);

                // Verifica vitória
                if (myBoard.Result(isPlayer))
                {
                    turnMessage.text = (isPlayer ? "Vermelho" : "Azul") + " venceu!";
                    hasGameFinished = true;
                    return;
                }

                // Troca mensagem
                turnMessage.text = !isPlayer ? RED_MESSAGE : BLUE_MESSAGE;
                turnMessage.color = !isPlayer ? RED_COLOR : BLUE_COLOR;

                // Troca jogador
                isPlayer = !isPlayer;
            }
        }
    }
}