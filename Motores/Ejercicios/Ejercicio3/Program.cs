int ejercicio;
Console.Write("Elige un ejercicio entre 1 y 8");
ejercicio = Convert.ToInt32(Console.ReadLine());
switch (ejercicio)
{
    case 1:
        Ejercicio1();
        break;
    case 2:
        Ejercicio2();
        break;
    case 3:
        Ejercicio3();
        break;
    case 4:
        Ejercicio4();
        break;
    case 5:
        Ejercicio5();
        break;
    case 6:
        Ejercicio6();
        break;
    case 7:
        Ejercicio7();
        break;
    case 8:
        Ejercicio8();
        break;
}

static void Ejercicio1()
{
    for (int i = 0; i < 5; i++)
        Console.WriteLine("Hola. ¿Cómo estás?");
}

static void Ejercicio2()
{
    int num;
    Console.Write("Introduce un número: ");
    num = Convert.ToInt32(Console.ReadLine());
    if (num <= 0)
    {
        Console.WriteLine("Error");
        Ejercicio2();
    }
    for (int i = 0; i < num; i++)
        Console.WriteLine("Hola. ¿Cómo estás?");
}

static void Ejercicio3()
{ 
        for (int i = 5; i >= 0; i--)
        Console.WriteLine(i);
}

static void Ejercicio4()
{ 
    int num;
    Console.Write("Introduce un número: ");
    num = Convert.ToInt32(Console.ReadLine());
    if (num <= 0)
    {
        Console.WriteLine("Error");
        Ejercicio4();
    }
    for (int i = num; i >= 0; i--)
        Console.WriteLine(i);
}

static void Ejercicio5()
{
    int x, y;
    Console.Write("Introduce un número: ");
    x = Convert.ToInt32(Console.ReadLine());
    Console.Write("Introduce un número mayor que el primero: ");
    y = Convert.ToInt32(Console.ReadLine());
    if (y <= x)
    {
        Console.WriteLine("Error");
        Ejercicio5();
    }
    for (int i = x; i <= y; i++)
        Console.WriteLine(i);
}

static void Ejercicio6()
{ 
    int x, y;
    Console.Write("Introduce un número: ");
    x = Convert.ToInt32(Console.ReadLine());
    Console.Write("Introduce otro número diferente: ");
    y = Convert.ToInt32(Console.ReadLine());
    if (y == x)
    {
        Console.WriteLine("Error");
        Ejercicio6();
    }
    int mayor = Math.Max(x, y);
    int menor = Math.Min(x, y);
    for (int i = menor; i <= mayor; i++)
        Console.WriteLine(i);
}

static void Ejercicio7()
{
    for (int i = 0; i <= 10; i++)
        Console.WriteLine("5 * " + i + " = " + (i * 5));
}

static void Ejercicio8()
{
    int num;
    Console.Write("Introduce un número: ");
    num = Convert.ToInt32(Console.ReadLine());
    for (int i = 0; i <= 10; i++)
        Console.WriteLine(num + " * " + i + " = " + (i * num));
}