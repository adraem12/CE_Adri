class AdivinarNumero
{
    static void Main(string[] args)
    {
        Start();
    }

    static void Start()
    {
        //Lee la cantidad de rondas
        int attempts;
        Console.WriteLine("¡Bienvenido a Adivina El Número! ¿Cuántos intentos quieres? (Escribe un número entero mayor que 0)");
        attempts = Convert.ToInt32(Console.ReadLine());
        //Comprueba que sea correcto
        if (attempts <= 0)
        {
            Console.WriteLine("Error");
            Start();
        }
        else
            Game(attempts);
    }

    static void Game(int attempts)
    {
        //Variables externas al bucle
        int currentAttempt = 1;
        List<int> guesses = [];
        bool gameEnd = false, victory = false;
        //Genera el número a adivinar
        int num = new Random().Next(1, (attempts * 2) + 1);
        //Esquema del juego
        while (!gameEnd)
        {
            Console.WriteLine("INTENTO " + currentAttempt + "/" + attempts);
            //Informa al jugador de sus anteriores intentos
            if (guesses.Count > 0)
            {
                Console.Write("Números que ya has provado: ");
                foreach (int numGuessed in guesses)
                    if (numGuessed != guesses.Last())
                        Console.Write(numGuessed + " - ");
                    else
                        Console.WriteLine(numGuessed);
            }
            //Lee el intento del jugador
            Console.WriteLine("Introduce tu intento entre 1 y " + attempts * 2 + ": ");
            int guess = Convert.ToInt32(Console.ReadLine());
            //Analiza si el número está en el rango o si ya se ha dicho
            if (guess > attempts * 2)
            {
                Console.WriteLine("Error");
                Console.WriteLine("Tu número es mayor al máximo permitido");
            }
            else if (guess <= 0)
            {
                Console.WriteLine("Error");
                Console.WriteLine("Tu número es menor al mínimo permitido");
            }
            else if (guesses.Contains(guess))
            {
                Console.WriteLine("Error");
                Console.WriteLine("Ya has dicho este número");
            }
            else //Añade el intento a la lista
            {
                guesses.Add(guess);
                if (guess == num) //Ganar
                {
                    gameEnd = true;
                    victory = true;
                }
                else if (currentAttempt == attempts) //Perder
                {
                    gameEnd = true;
                    victory = false;
                }
                else //Fallar un intento
                {
                    currentAttempt++;
                    Console.WriteLine("No has acertado");
                }
            }
        }
        //Muestra si el jugador gana o pierde
        if (victory)
            Console.WriteLine("¡Enhorabuena! Has acertado el número y has ganado");
        else
            Console.WriteLine("Has perdido contra la IA, ¡qué lástima!");
        //Comprueba si se juega una nueva partida o no
        NewGame();
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