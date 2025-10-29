class PiedraPapelTijera
{
    static void Main(string[] args)
    {
        Start();
    }

    static void Start()
    {
        //Lee la cantidad de rondas
        int rounds;
        Console.WriteLine("¡Bienvenido a Piedra, Papel o Tijera! ¿Cuántas rondas quieres jugar? (Introduce un número impar y mayor que uno)");
        rounds = Convert.ToInt32(Console.ReadLine());
        //Comprueba que sea correcto
        if (rounds % 2 == 0 || rounds <= 1)
        {
            Console.WriteLine("Error");
            Start();
        }
        else
            Game(rounds);
    }

    static void Game(int rounds)
    {
        //Variables externas al bucle
        int currentRound = 1, winPlayer = 0, winAI = 0, lastAIHand = 1;
        bool gameEnd = false;
        //Esquema del juego
        while (!gameEnd)
        {
            Console.WriteLine("RONDA " + currentRound);
            PlayerPlay(out int playerHand);
            AIPlay(lastAIHand, out int aiHand);
            lastAIHand = aiHand;
            if (playerHand == aiHand) //Caso de empate, no suma victoria ni ronda a nadie
                Console.WriteLine("¡Empate!");
            else
            {
                currentRound++; //Avanza una ronda y suma la victoria a quien corresponda
                if (PlayerIsWinner(playerHand, aiHand))
                {
                    winPlayer++;
                    Console.WriteLine("¡Has ganado la ronda!");
                }
                else
                {
                    winAI++;
                    Console.WriteLine("Has perdido la ronda :(");
                }
            }
            //Comprueba si ya se han completado las rondas
            if (winPlayer > rounds / 2 || winAI > rounds / 2)
                gameEnd = true;
            else
                Console.WriteLine("Vais " + winPlayer + " a " + winAI);
        }
        if (winPlayer > winAI)
            Console.WriteLine("¡Enhorabuena! Has ganado a la IA " + winPlayer + " a " + winAI);
        else
            Console.WriteLine("Has perdido contra la IA, ¡qué lástima!" + winPlayer + " a " + winAI);
        //Comprueba si se juega una nueva partida o no
        NewGame();
    }

    static void PlayerPlay(out int finalHand)
    {
        //Lee la jugada introducida
        Console.WriteLine("¿Cuál es tu jugada?");
        Console.WriteLine("1 -> Piedra");
        Console.WriteLine("2 -> Papel");
        Console.WriteLine("3 -> Tijera");
        int hand = Convert.ToInt32(Console.ReadLine());
        //Comprueba que sea correcta
        while (hand < 1 | hand > 3)
        {
            Console.WriteLine("Error, jugada no reconocida");
            Console.WriteLine("¿Cuál es tu jugada?");
            Console.WriteLine("1 -> Piedra");
            Console.WriteLine("2 -> Papel");
            Console.WriteLine("3 -> Tijera");
            hand = Convert.ToInt32(Console.ReadLine());
        }
        finalHand = hand;
    }

    static void AIPlay(int lastHand, out int finalHand)
    {
        //Genera una jugada aleatoria y comprueba que sea correcta
        Random rnd = new();
        int hand = rnd.Next(1, 4);
        //Comprueba que no se juegue dos veces la misma jugada
        while (hand == lastHand)
            hand = rnd.Next(1, 4);
        finalHand = hand;
        //Escribe el texto
        string text = "";
        switch (hand)
        {
            case 1:
                text = "Piedra";
                break;
            case 2:
                text = "Papel";
                break;
            case 3:
                text = "Tijera";
                break;
        }
        Console.WriteLine("La jugada de la IA ha sido " + text);
    }

    static bool PlayerIsWinner(int playerHand, int aiHand)
    {
        //Hace las comprobaciones utilizando la mano del jugador de base
        switch (playerHand)
        {
            case 1: //Piedra
                if (aiHand == 3) //Tijera
                    return true;
                else
                    return false;
            case 2: //Papel
                if (aiHand == 1) //Piedra
                    return true;
                else
                    return false;
            case 3: //Tijera
                if (aiHand == 2) //Papel
                    return true;
                else
                    return false;
            default:
                return false;
        }
    }

    static void NewGame()
    {
        //Pregunta al jugador
        Console.WriteLine("¿Quieres volver a jugar?");
        Console.WriteLine("1 -> SI");
        Console.WriteLine("2 -> NO");
        int checkPlayAgain = Convert.ToInt32(Console.ReadLine());
        //Comprueba que sea correcto el número introducido
        while (checkPlayAgain < 1 | checkPlayAgain > 2)
        {
            Console.WriteLine("Error, entrada no reconocida");
            Console.WriteLine("¿Quieres volver a jugar?");
            Console.WriteLine("1 -> SI");
            Console.WriteLine("2 -> NO");
            checkPlayAgain = Convert.ToInt32(Console.ReadLine());
        }
        if (checkPlayAgain == 1)
            Start();
        else
            return;
    }
}