int ejercicio;
Console.Write("Elige un ejercicio entre 1 y 9");
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
    case 9:
        Ejercicio9();
        break;
}

static void Ejercicio1()
{
    int dia, mes, anyo;
    Console.Write("Introduce el día: ");
    dia = Convert.ToInt32(Console.ReadLine());
    if (dia > 31 || dia <= 0)
    {
        Console.WriteLine("Erróneo");
        Ejercicio1();
    }
    Console.Write("Introduce el mes: ");
    mes = Convert.ToInt32(Console.ReadLine());
    if (mes > 12 || mes <= 0)
    {
        Console.WriteLine("Erróneo");
        Ejercicio1();
    }
    Console.Write("Introduce el año: ");
    anyo = Convert.ToInt32(Console.ReadLine());
    if (mes <= 3 && mes > 0)
        Console.WriteLine("ole");
}

static void Ejercicio2()
{
    int x, y, z;
    Console.Write("Introduce primer número: ");
    x = Convert.ToInt32(Console.ReadLine());
    Console.Write("Introduce segundo número: ");
    y = Convert.ToInt32(Console.ReadLine());
    Console.Write("Introduce tercer número: ");
    z = Convert.ToInt32(Console.ReadLine());
    if (x == y && y == z)
    {
        Console.WriteLine("Los tres números son iguales");
        Ejercicio2();
    }
    else
        Console.WriteLine("El número más grande es " + Math.Max(Math.Max(x, y), z));
}

static void Ejercicio3()
{
    int x, y, z;
    Console.Write("Introduce primer número: ");
    x = Convert.ToInt32(Console.ReadLine());
    Console.Write("Introduce segundo número: ");
    y = Convert.ToInt32(Console.ReadLine());
    Console.Write("Introduce tercer número: ");
    z = Convert.ToInt32(Console.ReadLine());
    if (x != y || y != z || x != z)
    {
        Console.WriteLine("Los tres números no son iguales");
        Ejercicio3();
    }
    else
        Console.WriteLine("El resultado es " + (x + y) * z);
}

static void Ejercicio4()
{
    int x, y, z;
    Console.Write("Introduce primer número: ");
    x = Convert.ToInt32(Console.ReadLine());
    Console.Write("Introduce segundo número: ");
    y = Convert.ToInt32(Console.ReadLine());
    Console.Write("Introduce tercer número: ");
    z = Convert.ToInt32(Console.ReadLine());
    if (x < 10 | y < 10 | z < 10)
        Console.WriteLine("Alguno de los números es menor a diez");
}

static void Ejercicio5()
{
    int x, y;
    Console.Write("Introduce la coordenada X: ");
    x = Convert.ToInt32(Console.ReadLine());
    Console.Write("Introduce la coordenada Y: ");
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
            Console.WriteLine("El punto se encuentra en el primer cuadrante");
        else if (x < 0 && y < 0)
            Console.WriteLine("El punto se encuentra en el segundo cuadrante");
        else if (x < 0 && y > 0)
            Console.WriteLine("El punto se encuentra en el tercer cuadrante");
        else if (x > 0 && y < 0)
            Console.WriteLine("El punto se encuentra en el cuarto cuadrante");
    }
}

static void Ejercicio6()
{
    float sueldo; 
    int antiguedad;
    Console.Write("Introduce el sueldo: ");
    sueldo = float.Parse(Console.ReadLine());
    Console.Write("Introduce la antigüedad: ");
    antiguedad = Convert.ToInt32(Console.ReadLine());
    if (sueldo >= 500)
        Console.WriteLine("El sueldo se mantiene igual");
    else
    {
        float nuevoSueldo;
        if (antiguedad >= 10)
            nuevoSueldo = (sueldo / 100) * 120;
        else
            nuevoSueldo = (sueldo / 100) * 105;
        Console.WriteLine("El nuevo sueldo es de " + nuevoSueldo);
    }
}

static void Ejercicio7()
{
    float x, y, z, rangoVariacion;
    Console.Write("Introduce el primer número: ");
    x = float.Parse(Console.ReadLine());
    Console.Write("Introduce el segundo número: ");
    y = float.Parse(Console.ReadLine());
    Console.Write("Introduce el tercero número: ");
    z = float.Parse(Console.ReadLine());
    float numMayor = MathF.Max(x, MathF.Max(y, z));
    float numMenor = MathF.Min(x, MathF.Min(y, z));
    Console.WriteLine("El número mayor es " + numMayor);
    Console.WriteLine("El número menor es " + numMenor);
    Console.WriteLine("El rango de variación es " + (numMayor - numMenor));
}

static void Ejercicio8()
{
    int x;
    Console.Write("Introduce un número entero: ");
    x = Convert.ToInt32(Console.ReadLine());
    if (x == 0)
        Console.WriteLine("El número es nulo");
    else if (x > 0)
        Console.WriteLine("El número es positivo");
    else
        Console.WriteLine("El número es negativo");
}

static void Ejercicio9()
{
    int objTotales, objCompletados;
    Console.Write("Introduce la cantidad total de objetivos: ");
    objTotales = Convert.ToInt32(Console.ReadLine());
    Console.Write("Introduce los objetivos completados por el jugador: ");
    objCompletados = Convert.ToInt32(Console.ReadLine());
    if (objCompletados > objTotales)
    {
        Console.WriteLine("Error");
        Ejercicio9();
    }
    else
    {
        float porciento = 100 * objCompletados / objTotales;
        if (porciento == 100)
            Console.WriteLine("¡Nivel perfecto! * * * * ");
        else if (porciento >= 90)
            Console.WriteLine("¡Nivel superado! * * * ");
        else if (porciento < 90 && porciento >= 75)
            Console.WriteLine("¡Nivel superado! * * ");
        else if (porciento < 75 && porciento >= 50)
            Console.WriteLine("¡Nivel superado! * ");
        else
            Console.WriteLine("Nivel no superado :(");
        Ejercicio9();
    }
}