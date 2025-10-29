class BlackJack
{
    public enum PlayState
    {
        Playing,
        Standing,
        Dead        
    }

    static void Main(string[] args)
    {
        Start();
    }

    static void Start()
    {
        //Inicializa el juego
        Console.WriteLine("¡Bienvenido al Black Jack! ¿Quieres jugar?");
        Console.WriteLine("1 -> SI");
        Console.WriteLine("2 -> NO");
        int play = Convert.ToInt32(Console.ReadLine());
        //Comprueba que sea correcto
        while (play < 1 | play > 2)
        {
            Console.WriteLine("Error, entrada no reconocida");
            Console.WriteLine("¡Bienvenido al Black Jack! ¿Quieres jugar?");
            Console.WriteLine("1 -> SI");
            Console.WriteLine("2 -> NO");
            play = Convert.ToInt32(Console.ReadLine());
        }
        if (play == 1)
            Game();
        else
            return;
    }

    static void Game()
    {
        //Variables externas al bucle
        int playerPoints = 0, aiPoints = 0;
        PlayState playerState = PlayState.Playing, aiState = PlayState.Playing;
        bool gameEnd = false;
        while (!gameEnd)
        {
            if (aiState == PlayState.Playing) //Primero, juega la IA
            {
                int dice = Dice();
                Console.WriteLine("La IA ha sacado un " + dice);
                aiPoints += dice;
                int aiDecision = new Random().Next(1, 3);
                Console.WriteLine("La IA tiene " + aiPoints + " puntos ");
                if (aiPoints > 21) //Se muere
                {
                    Console.WriteLine("La IA ha muerto con " + aiPoints + " puntos ");
                    aiState = PlayState.Dead;
                    aiPoints = -1;
                }
                else if (aiPoints >= 16 && aiPoints < 20 && aiDecision == 1) //Decide entre plantarse o arriesgarse
                {
                    Console.WriteLine("La IA se planta con " + aiPoints + " puntos ");
                    aiState = PlayState.Standing;
                }
            }
            if (playerState == PlayState.Playing) //Segundo, juega el jugador
            {
                PlayerPlay(playerPoints, out int newPlayerPoints);
                //Comprueba el estado del jugador
                if (newPlayerPoints == playerPoints) //Se planta
                {
                    Console.WriteLine("Te has plantado con " + playerPoints + " puntos");
                    playerState = PlayState.Standing;
                }
                else if (newPlayerPoints > 21) //Se muere
                {
                    Console.WriteLine("Te has muerto con " + newPlayerPoints + " puntos");
                    playerState = PlayState.Dead;
                    playerPoints = -1;
                }
                else //Juega normal
                {
                    playerPoints = newPlayerPoints;
                    Console.WriteLine("Tienes " + playerPoints + " puntos");
                }
            }
            if (playerState != PlayState.Playing && aiState != PlayState.Playing)
                gameEnd = true;
        }
        if (playerState == PlayState.Dead && aiState == PlayState.Dead) //Los dos se mueren
            Console.WriteLine("Habéis muerto los dos, así que no gana nadie");
        else
        {
            //Si alguien se muere, su puntuación es -1 para poder comparar los dos valores 
            if (playerPoints > aiPoints)
                Console.WriteLine("¡Enhorabuena! Has ganado");
            else if (playerPoints < aiPoints)
                Console.WriteLine("Has perdido contra la IA, ¡qué lástima!");
            else
                Console.WriteLine("Habéis empatado, así que no gana nadie");
        }
        //Comprueba si se juega una nueva partida o no
        NewGame();
    }

    static void PlayerPlay(int playerPoints, out int newPlayerPoints)
    {
        //Pide al jugador qué hacer
        Console.WriteLine("Tienes " + playerPoints + " puntos. ¿Qué quieres hacer?");
        Console.WriteLine("1 -> Jugar");
        Console.WriteLine("2 -> Plantarse");
        int play = Convert.ToInt32(Console.ReadLine());
        //Comprueba que sea correcto el número introducido
        while (play < 1 | play > 2)
        {
            Console.WriteLine("Error, entrada no reconocida");
            Console.WriteLine("Tienes " + playerPoints + " puntos. ¿Qué quieres hacer?");
            Console.WriteLine("1 -> Jugar");
            Console.WriteLine("2 -> Plantarse");
            play = Convert.ToInt32(Console.ReadLine());
        }
        if (play == 2) //Planta al jugador
            newPlayerPoints = playerPoints;
        else //Tira el dado
        {
            int dice = Dice();
            if (dice == 1) //En caso de comodín
            {
                Console.WriteLine("Has sacado un uno. ¿Uieres que sea 1 o 6?");
                Console.WriteLine("1 -> Mantener el 1");
                Console.WriteLine("2 -> Convertir el 1 en 6");
                int decision = Convert.ToInt32(Console.ReadLine());
                //Comprueba que sea correcto el número introducido
                while (decision < 1 | decision > 2)
                {
                    Console.WriteLine("Error, entrada no reconocida");
                    Console.WriteLine("Has sacado un uno. ¿Uieres que sea 1 o 6?");
                    Console.WriteLine("1 -> Mantener el 1");
                    Console.WriteLine("2 -> Convertir el 1 en 6");
                    decision = Convert.ToInt32(Console.ReadLine());
                }
                if (decision == 1)
                    dice = 1;
                else
                    dice = 6;
            }
            else //En caso de cualquier otro número
                Console.WriteLine("Has sacado un " + dice);
            //Asigna el nuevo valor
            newPlayerPoints = playerPoints + dice;
        }
    }

    static int Dice()
    {
        //Calcula la tirada de dado
        return new Random().Next(1, 7);
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