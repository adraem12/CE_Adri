int ejercicio;
Console.Write("Elige un ejercicio entre 1 y 4");
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
}

static void Ejercicio1()
{
    float sueldo;
    Console.Write("Escribe tu sueldo: ");
    sueldo = float.Parse(Console.ReadLine());
    if (sueldo >= 1200)
        Console.WriteLine("Tienes que pagar impuestos");
}

static void Ejercicio2()
{
    int x, y;
    Console.Write("Introduce primer número: ");
    x = Convert.ToInt32(Console.ReadLine());
    Console.Write("Introduce segundo número: ");
    y = Convert.ToInt32(Console.ReadLine());
    if (x > y)
        Console.WriteLine("La suma de los números es " + (x + y));
    else if (x < y)
    {
        Console.WriteLine("El producto de los números es " + (x * y));
        Console.WriteLine("La división de los números es " + (x / y));
    }
    else
        Console.WriteLine("Son iguales");
}

static void Ejercicio3()
{
    int notaA, notaB, notaC, notaFinal;
    Console.Write("Introduce primera nota: ");
    notaA = Convert.ToInt32(Console.ReadLine());
    Console.Write("Introduce segunda nota: ");
    notaB = Convert.ToInt32(Console.ReadLine());
    Console.Write("Introduce tercera nota: ");
    notaC = Convert.ToInt32(Console.ReadLine());
    notaFinal = (notaA + notaB + notaC) / 3;
    if (notaFinal >= 5)
        Console.WriteLine("Promocionado");
    else
        Console.WriteLine("No promocionado");
}
static void Ejercicio4()
{
    int num;
    Console.Write("Introduce número positivo de uno o dos dígitos: ");
    num = Convert.ToInt32(Console.ReadLine());
    if (num.ToString().ToArray().Length > 2)
    {
        Console.WriteLine("El número tiene más de dos dígitos");
        Ejercicio4();
    }
    else if (num.ToString().ToArray().Length == 2)
        Console.WriteLine("El número tiene dos dígitos");
    else if (num.ToString().ToArray().Length == 1)
        Console.WriteLine("El número tiene un dígito");
}