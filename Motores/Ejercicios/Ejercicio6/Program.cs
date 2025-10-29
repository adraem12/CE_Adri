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
    int[] ints = new int[10];
    for (int x = 0; x < 10; x++)
    {
        Console.Write("Introduce un número: ");
        ints[x] = Convert.ToInt32(Console.ReadLine());
    }
    for (int y = 4; y < 10; y++)
        Console.WriteLine(ints[y]);
}

static void Ejercicio2()
{
    List<int[]> tris = [];
    int equ = 0, iso = 0, esc = 0;
    Console.Write("Introduce cuántos triángulos analizar: ");
    int cantidadTris = Convert.ToInt32(Console.ReadLine());
    for (int i = 1; i <= cantidadTris; i++)
    {
        int[] newTri = new int[3];
        Console.Write("Introduce la cara A del triángulo " + i + ": ");
        newTri[0] = Convert.ToInt32(Console.ReadLine());
        Console.Write("Introduce la cara B del triángulo " + i + ": ");
        newTri[1] = Convert.ToInt32(Console.ReadLine());
        Console.Write("Introduce la cara C del triángulo " + i + ": ");
        newTri[2] = Convert.ToInt32(Console.ReadLine());
        tris.Add(newTri);
        if (newTri[0] == newTri[1] && newTri[1] == newTri[2])
        {
            equ++;
            Console.WriteLine("Has introducido un triángulo equilátero");
        }
        else if (newTri[0] != newTri[1] && newTri[0] != newTri[2] && newTri[1] != newTri[2])
        {
            esc++;
            Console.WriteLine("Has introducido un triángulo escaleno");
        }
        else
        {
            iso++;
            Console.WriteLine("Has introducido un triángulo isósceles");
        }
    }
    Console.WriteLine("De los triángulos dados, hay " + equ + " equiláteros, " + iso + " isósceles y " + esc + " escalenos");
    if (equ < esc && equ < iso)
        Console.WriteLine("El tipo con menos cantidad es equilátero");
    else if (iso < esc && iso < equ)
        Console.WriteLine("El tipo con menos cantidad es isósceles");
    else if (esc < iso && esc < equ)
        Console.WriteLine("El tipo con menos cantidad es escaleno");
}

static void Ejercicio3()
{
    int puntos;
    int coord1 = 0, coord2 = 0, coord3 = 0, coord4 = 0;
    Console.Write("Introduce cuántos puntos quieres introducir: ");
    puntos = Convert.ToInt32(Console.ReadLine());
    for (int i = 1; i <= puntos; i++)
    {
        int x, y;
        Console.Write("Introduce la coordenada X del punto " + i + ": ");
        x = Convert.ToInt32(Console.ReadLine());
        Console.Write("Introduce la coordenada Y del punto " + i + ": ");
        y = Convert.ToInt32(Console.ReadLine());
        if (x == 0 | y == 0)
        {
            if (x == y)
                Console.WriteLine("El punto se encuentra en el origen");
            else
                Console.WriteLine("El punto no se encuentra en ningún cuadrante");
            Ejercicio5();
        }
        else
        {
            if (x > 0 && y > 0)
                coord1++;
            else if (x < 0 && y < 0)
                coord2++;
            else if (x < 0 && y > 0)
                coord3++;
            else if (x > 0 && y < 0)
                coord4++;
        }
    }
    Console.WriteLine("De los puntos dados, hay " + coord1 + " en el cuadrante 1, " + coord2 + " en el cuadrante 2, " + coord3 + " en el cuadrante 3 y " + coord4 + " en el cuadrante 4");
}

static void Ejercicio4()
{
    int pos = 0, neg = 0, mults = 0, par = 0;
    for (int i = 1; i <= 6; i++)
    {
        int num;
        Console.Write("Introduce el valor " + i + ": ");
        num = Convert.ToInt32(Console.ReadLine());
        if (num > 0)
            pos++;
        else if (num < 0)
            neg++;
        if (num % 15 == 0)
            mults++;
        if (num % 2 == 0)
            par++;
    }
    Console.WriteLine("De los valores dados, " + pos + " son positivos, " + neg + " son negativos, " + mults + " son múltiples de 15 y " + par + " son pares");
}

static void Ejercicio5()
{
    int num;
    Console.Write("Introduce un valor: ");
    num = Convert.ToInt32(Console.ReadLine());
    if (IsPrime(num))
        Console.WriteLine("El número es primo");
    else
        Console.WriteLine("El número no es primo");
}

static bool IsPrime(int number)
{
    if (number == 1) return false;
    if (number == 2) return true;
    double limit = Math.Ceiling(Math.Sqrt(number));
    for (int i = 2; i <= limit; ++i)
        if (number % i == 0)
            return false;
    return true;
}

static void Ejercicio6()
{
    string text = "";
    float result;
    int num, pow;
    Console.Write("Introduce un número: ");
    num = Convert.ToInt32(Console.ReadLine());
    Console.Write("Introduce su potencia: ");
    pow = Convert.ToInt32(Console.ReadLine());
    result = MathF.Pow(num, pow);
    text += "(";
    for (int i = 1; i <= pow; i++)
        if (i != pow)
            text += num + "*";
        else
            text += num;
    text += ")";
    Console.WriteLine(num + "^" + pow + " = " + result + " -> " + text);
}