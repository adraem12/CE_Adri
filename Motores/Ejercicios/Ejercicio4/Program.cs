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
    List<int> notas = [];
    int notasMayores = 0, notasMenores = 0;
    while (notas.Count < 10)
    {
        Console.Write("Introduce una nota");
        notas.Add(Convert.ToInt32(Console.ReadLine()));
    }
    foreach (int nota in notas)
        if (nota >= 7)
            notasMayores++;
        else
            notasMenores++;
    Console.WriteLine("Hay " + notasMayores + " notas mayores o iguales a 7 y " + notasMenores +" notas menores a 7");
}

static void Ejercicio2()
{ 
    List<float> alturas = [];
    while (alturas.Count < 10)
    {
        Console.Write("Introduce una altura");
        alturas.Add(float.Parse(Console.ReadLine()));
    }
    float alturaMedia = 0;
    foreach (float altura in alturas)
        alturaMedia += altura;
    Console.WriteLine("La altura media es de " + (alturaMedia / 10));
}

static void Ejercicio3()
{
    List<float> sueldos = [];
    int sueldosMayores = 0, sueldosMenores = 0;
    int trabajadores;
    Console.Write("Introduce la cantidad de trabajadores: ");
    trabajadores = Convert.ToInt32(Console.ReadLine());
    while (sueldos.Count < trabajadores)
    {
        Console.Write("Introduce un sueldo");
        sueldos.Add(float.Parse(Console.ReadLine()));
    }
    float sueldoTotal = 0;
    foreach (float sueldo in sueldos)
    {
        if (sueldo <= 300)
            sueldosMenores++;
        else
            sueldosMayores++;
        sueldoTotal += sueldo;
    }
    Console.WriteLine("Hay " + sueldosMayores + " trabajadores que cobran más de 300 y " + sueldosMenores + " trabajadores que cobran menos de 300");
    Console.WriteLine("El gasto de la empresa en sueldos es de " + sueldoTotal);
}

static void Ejercicio4()
{
    List<int> lista = [];
    int sumatorio = 0;
    for (int i = 1; i <= 25; i++)
        lista.Add(i * 11);
    while (sumatorio + 8 <= 500)
    {
        sumatorio += 8;
        lista.Add(sumatorio);
    }
    lista.Sort();
    foreach (int num in lista)
        Console.WriteLine(num);
}

static void Ejercicio5()
{
    List<int> ListaA = [];
    List<int> ListaB = [];
    int totalA = 0, totalB = 0;
    while (ListaA.Count < 15)
    {
        Console.Write("Añade un número a la lista A: ");
        int x = Convert.ToInt32(Console.ReadLine());
        ListaA.Add(x);
        totalA += x;
    }
    while (ListaB.Count < 15)
    {
        Console.Write("Añade un número a la lista B: ");
        int x = Convert.ToInt32(Console.ReadLine());
        ListaB.Add(x);
        totalB += x;
    }
    if (totalA > totalB)
        Console.WriteLine("Lista A mayor");
    else if (totalB > totalA)
        Console.WriteLine("Lista B mayor");
    else
        Console.WriteLine("Listas iguales");
}

static void Ejercicio6()
{
    List<int> numList = [];
    int par = 0, impar = 0;
    Console.Write("Introduce cuántos numeros quieres: ");
    int cantidad = Convert.ToInt32(Console.ReadLine());
    while (numList.Count < cantidad)
    {
        Console.Write("Introduce un número: ");
        int x = Convert.ToInt32(Console.ReadLine());
        numList.Add(x);
        if (x % 2 == 0) par++;
        else impar++;
    }
    Console.WriteLine("En tu lista hay " + par + " números pares y " + impar + " números impares");
}