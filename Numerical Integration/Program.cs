using System;
using System.Text;

class NumericalIntegration
{
    static void Main(string[] args)
    {
        Func<double, double> function = x => Math.Sin(x); 

        double a = 0; 
        double b = Math.PI; 
        double epsilon = 1e-6;


        int n = 8;

        ComputeIntegral("Левых прямоугольников", LeftRectangle, function, a, b, n);
        ComputeIntegral("Правых прямоугольников", RightRectangle, function, a, b, n);
        ComputeIntegral("Центральных прямоугольников", CentralRectangle, function, a, b, n);
        ComputeIntegral("Трапеций", Trapezoidal, function, a, b, n);
        ComputeIntegral("Симпсона", Simpson, function, a, b, n);
        ComputeIntegral("Симпсона 3/8", Simpson38, function, a, b, n);
        ComputeIntegral("Гаусса 3-го порядка", Gaussian3, function, a, b, n);
        //ComputeIntegral("Гаусса 4-го порядка", Gaussian4, function, a, b, n);
    }

    static void ComputeIntegral(string methodName, Func<Func<double, double>, double, double, int, double> method,
                             Func<double, double> function, double a, double b, int n)
    {
        double result = 0;
        double intervalWidth = (b - a) / n;

        for (int i = 0; i < n; i++)
        {
            double subA = a + i * intervalWidth;
            double subB = subA + intervalWidth;

            result += method(function, subA, subB, 1);
        }

        Console.WriteLine($"Метод: {methodName}, Интеграл: {result}, Шаг: {intervalWidth}, Разбиений: {n}");
    }


    // Методы интегрирования

    static double LeftRectangle(Func<double, double> f, double a, double b, int n)
    {
        double h = (b - a) / n;
        double sum = 0;
        for (int i = 0; i < n; i++)
        {
            sum += f(a + i * h);
        }
        return sum * h;
    }

    static double RightRectangle(Func<double, double> f, double a, double b, int n)
    {
        double h = (b - a) / n;
        double sum = 0;
        for (int i = 1; i <= n; i++)
        {
            sum += f(a + i * h);
        }
        return sum * h;
    }





    /*static double CentralRectangle(Func<double, double> f, double a, double b, int n)
    {
        double h = (b - a) / n;
        double sum = 0;
        for (int i = 0; i < n; i++)
        {
            sum += f(a + i * h + h / 2);
        }
        return sum * h;
    }

    static double Trapezoidal(Func<double, double> f, double a, double b, int n)
    {
        double h = (b - a) / n;
        double sum = 0.5 * (f(a) + f(b));
        for (int i = 1; i < n; i++)
        {
            sum += f(a + i * h);
        }
        return sum * h;
    }

    static double Simpson(Func<double, double> f, double a, double b, int n)
    {
        if (n % 2 != 0) n++; 
        double h = (b - a) / n;
        double sum = f(a) + f(b);
        for (int i = 1; i < n; i++)
        {
            sum += (i % 2 == 0 ? 2 * f(a + i * h) : 4 * f(a + i * h));
        }
        return sum * h / 3;
    }

    static double Simpson38(Func<double, double> f, double a, double b, int n)
    {
        if (n % 3 != 0) n += (3 - (n % 3)); 
        double h = (b - a) / n;
        double sum = f(a) + f(b);
        for (int i = 1; i < n; i++)
        {
            if (i % 3 == 0)
                sum += 2 * f(a + i * h);
            else
                sum += 3 * f(a + i * h);
        }
        return sum * (3.0 * h / 8.0);
    }

    static double Gaussian3(Func<double, double> f, double a, double b, int n)
    {
        double[] weights = { 5.0 / 9.0, 8.0 / 9.0, 5.0 / 9.0 };
        double[] nodes = { -Math.Sqrt(3.0 / 5.0), 0.0, Math.Sqrt(3.0 / 5.0) };

        double h = (b - a) / 2;
        double mid = (b + a) / 2;
        double sum = 0;

        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i] * f(mid + h * nodes[i]);
        }

        return sum * h;
    }
*/
    /*static double Gaussian4(Func<double, double> f, double a, double b, int n)
    {
        // Применение Гаусса 4-го порядка
        double[] weights = { 0.347854845137453857, 0.652145154862546142 };
        double[] nodes = { 0.861136311594052575, 0.339981043584856264 };

        double h = (b - a) / 2;
        double mid = (b + a) / 2;
        double sum = 0;

        for (int i = 0; i < weights.Length; i++)
        {
            sum += weights[i] * f(mid + h * nodes[i]);
        }

        return sum * h;
    }*/
}
