/***********************************
**
**
** Adrià Sánchez Sanz
**
**
***********************************/

class Examen1
{
    static void Main(string[] args)
    {
        //Inicializa el menú de la contraseña
        Console.WriteLine("Bienvenido. Necesitaré que introduzcas la contraseña:");
        int password = Convert.ToInt32(Console.ReadLine()); //Comprueba que sea correcto

        if (password != 12345678)
        {
            Console.WriteLine("Acceso denegado, pepito. Inténtalo más tarde");
            Console.WriteLine("Gracias por usar nuestro programa. Nos vemos pronto");
            return;
        }
        else
        {
            Console.WriteLine("¡Acceso permitido!");
            Start();
        }
    }

    static void Start()
    {
        //Inicializa el menú para elegir el programa
        Console.WriteLine("¿Qué quieres hacer?");
        Console.WriteLine("1 - Averiguar mitad");
        Console.WriteLine("2 - Calcular puntuación final de nivel");
        Console.WriteLine("3 - Mostrar los divisores de un número");
        Console.WriteLine("4 - Adivinar un número secreto");
        Console.WriteLine("99 - Salir");
        int option = Convert.ToInt32(Console.ReadLine());

        while (option < 1 | option > 4 && option != 99) //Comprueba que sea correcto
        {
            Console.WriteLine("Error, entrada no reconocida");
            Console.WriteLine("¿Qué quieres hacer?");
            Console.WriteLine("1 - Averiguar mitad");
            Console.WriteLine("2 - Calcular puntuación final de nivel");
            Console.WriteLine("3 - Mostrar los divisores de un número");
            Console.WriteLine("4 - Adivinar un número secreto");
            Console.WriteLine("99 - Salir");
            option = Convert.ToInt32(Console.ReadLine());
        }

        switch (option) //Elige el programa correspondiente
        {
            case 1:
                Programa1();
                break;
            case 2:
                Programa2();
                break;
            case 3:
                Programa3();
                break;
            case 4:
                Programa4();
                break;
            case 99:
                Console.WriteLine("Gracias por usar nuestro programa. Nos vemos pronto.");
                break;
        }
    }

    static void Programa1()
    {
        //Introduce los números
        Console.WriteLine("Dime el primer número:");
        int firstNum = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Dime el segundo número:");
        int secondNum = Convert.ToInt32(Console.ReadLine());

        if (firstNum == 0 | secondNum == 0) //Comprueba si alguno de los números es 0
            Console.WriteLine("Error, no se puede dividir entre 0");
        else
        {  
            if (firstNum * 2 == secondNum | secondNum * 2 == firstNum) //Comprueba si algún número es el doble del otro
                Console.WriteLine("Hay un número que es la mitad del otro");
            else
                Console.WriteLine("No hay ningún número que sea la mitad del otro");
        }

        Console.WriteLine(""); //Espacio para la consola
        Start(); //Vuelve al menú
    }

    static void Programa2()
    {
        //Variables locales
        int enemyStars = 0;
        int civsStars = 0;
        int totalStars;
        int timeStars;
        float percent;

        //Introduce los datos relacionados con los enemigos
        Console.WriteLine("Dime los enemigos derrotados:");
        int deadEnemies = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Dime los enemigos totales:");
        int totalEnemies = Convert.ToInt32(Console.ReadLine());

        if (deadEnemies > totalEnemies) //Comprueba si es correcto
        {
            Console.WriteLine("Error, no puede haber más enemigos derrotados de los que hay en total");
            Programa2();
        }
        percent = 100 * deadEnemies / totalEnemies; //Calcula las estrellas de los enemigos
        if (percent >= 80)
            enemyStars += 5;
        else if (percent < 80 && percent >= 60)
            enemyStars += 4;
        else if (percent < 60 && percent >= 40)
            enemyStars += 3;
        else if (percent < 20 && percent >= 40)
            enemyStars += 2;
        else
            enemyStars += 1;

        //Introduce los datos relacionados con los civiles
        Console.WriteLine("Dime los ciudadanos rescatados:");
        int rescuedCivs = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Dime los ciudadanos totales:");
        int totalCivs = Convert.ToInt32(Console.ReadLine());

        if (rescuedCivs > totalCivs) //Comprueba si es correcto
        {
            Console.WriteLine("Error, no puede haber más civiles rescatados de los que hay en total");
            Programa2();
        }
        percent = 100 * rescuedCivs / totalCivs; //Calcula las estrellas de los civiles
        if (percent >= 80)
            civsStars += 5;
        else if (percent < 80 && percent >= 60)
            civsStars += 4;
        else if (percent < 60 && percent >= 40)
            civsStars += 3;
        else if (percent < 20 && percent >= 40)
            civsStars += 2;
        else
            civsStars += 1;
        totalStars = (civsStars + enemyStars) / 2; //Calcula las estrellas totales

        //Introduce el tiempo invertido
        Console.WriteLine("Dime el tiempo empleado (en segundos):");
        int time = Convert.ToInt32(Console.ReadLine());
        if (time <= 0) //Comprueba si es correcto
        {
            Console.WriteLine("Error, el tiempo no puede ser negativo ni 0");
            Programa2();
        }
        timeStars = Convert.ToInt32(MathF.Ceiling(time / 600)); //Calcula las estrellas a restar

        Console.WriteLine("PUNTUACIÓN FINAL"); //Escribe los resultados
        Console.Write("Enemigos:");
        Console.Write(enemyStars);
        for (int i = 0; i < enemyStars; i++) //Dibuja las estrellas conseguidas
            Console.Write("*");
        Console.WriteLine(" Estrellas");

        Console.Write("Ciudadanos:");
        Console.Write(civsStars);
        for (int i = 0; i < civsStars; i++) //Dibuja las estrellas conseguidas
            Console.Write("*");
        Console.WriteLine(" Estrellas");

        Console.Write("Puntuación promedio:");
        Console.Write(totalStars);
        for (int i = 0; i < totalStars; i++) //Dibuja las estrellas conseguidas
            Console.Write("*");
        Console.WriteLine(" Estrellas");

        Console.Write("Puntuación final:");
        int defStars = totalStars - timeStars;
        if (defStars < 1) defStars = 1; //Las estrellas no pueden ser menores a uno
        Console.Write(defStars);
        for (int i = 0; i < defStars; i++) //Dibuja las estrellas conseguidas
            Console.Write("*");
        Console.WriteLine(" Estrellas");

        Console.WriteLine(""); //Espacio para la consola
        Start(); //Vuelve al menú
    }

    static void Programa3()
    {
        //Introduce el número a analizar
        Console.WriteLine("Dime un número:");
        int num = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Los divisores de " + num + " son:");
        if (num > 0) //Comprueba si el número es positivo, negativo o cero
        {
            for (int i = 1; i <= num; i++)
                if (num % i == 0) //Calcula sus divisores (el resto de su división debe ser 0)
                    Console.WriteLine(i);
        }
        else if (num < 0)
        {
            for (int i = -1; i >= num; i--)
                if (num % i == 0)
                    Console.WriteLine(Math.Abs(i));
        }
        else
            Console.WriteLine("Error, no se puede dividir entre 0");

        Console.WriteLine(""); //Espacio para la consola
        Start(); //Vuelve al menú
    }

    static void Programa4()
    {
        //Variables locales
        int guesses = 1;
        int realNum = new Random().Next(1, 101);

        Console.WriteLine("He pensado un número entre 1 y 100");
        Console.WriteLine("Intenta adivinarlo:");
        int numGuessed = Convert.ToInt32(Console.ReadLine()); //Primer intento

        while (numGuessed != realNum)
        {
            guesses++; //Añade un intento
            if (numGuessed > realNum) //Si el número es mayor
                Console.WriteLine("Tu número es mayor que el mío.");
            else //Si el número es menor
                Console.WriteLine("Tu número es menor que el mío.");
            Console.WriteLine("Intenta adivinarlo:");
            numGuessed = Convert.ToInt32(Console.ReadLine());
        }
        Console.WriteLine("¡Enhorabuena! ¡El número que había pensado era el " + realNum + "!");
        Console.WriteLine("Lo has acertado en " + guesses + " intentos.");

        Console.WriteLine(""); //Espacio para la consola
        Start(); //Vuelve al menú
    }
}