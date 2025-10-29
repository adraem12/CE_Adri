int ejercicio;
Console.Write("Elige un ejercicio entre 1 y 6: ");
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
}

static void Ejercicio1()
{
    for (int i = 1; i <= 10; i++)
        Console.WriteLine(i);
}

static void Ejercicio2()
{
    int result = 0;
    for (int i = 1; i <= 10; i++)
        result += i;
    Console.WriteLine("La suma de los primeros 10 números naturales es " + result);
}

static void Ejercicio3()
{
    Console.Write("Introduce cuántos números naturales sumar: ");
    int cantidad = Convert.ToInt32(Console.ReadLine());
    int result = 0;
    for (int i = 0; i < cantidad; i++)
        result += i + 1;
    Console.WriteLine("La suma de los primeros " + cantidad + " números naturales es " + result);
}

static void Ejercicio4()
{
    int sum = 0;
    float prom = 0;
    for (int i = 1; i <= 10; i++)
    {
        Console.Write("Introduce el valor " + i +": ");
        sum += Convert.ToInt32(Console.ReadLine());
    }
    prom = sum / 10;
    Console.WriteLine("La suma de los números introducidos es " + sum + " y el promedio es " + prom);
}

static void Ejercicio5()
{
    Console.Write("Introduce un número mayor que 1: ");
    int valor = Convert.ToInt32(Console.ReadLine());
    int sum = 0;
    for (int i = 2; i <= valor; i += 2)
        sum += i;
    Console.WriteLine("La suma de los números pares entre 1 y " + valor + " es " + sum);
}

static void Ejercicio6()
{
    string text = "";
    Console.Write("Introduce un número: ");
    int valor = Convert.ToInt32(Console.ReadLine());
    int factorTotal = 1;
    for (int i = 1; i <= valor; i++)
    {
        if (i != valor)
            text += i + "*";
        else
            text += i;
        factorTotal *= i;
    }
    Console.WriteLine(valor + "! = " + text + " = " + factorTotal);
}