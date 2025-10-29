int ejercicio;
Console.Write("Elige un ejercicio entre 1 y 2");
ejercicio = Convert.ToInt32(Console.ReadLine());
switch (ejercicio)
{
    case 1:
        Ejercicio1();
        break;
    case 2:
        Ejercicio2();
        break;
}

static void Ejercicio1()
{
    for (int x = 0; x < 5; x++)
    {
        string text = "";
        for (int y = 0; y < x; y++)
            text += "*";
        Console.WriteLine(text);
    }
}

static void Ejercicio2()
{
    for (int x = 1; x <= 10; x++)
    {
        string text = "";
        for (int y = 1; y <= 10; y++)
            text += " " + x * y;
        Console.WriteLine(text);
    }
}