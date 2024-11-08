using System;
using System.Text;

class NumericalIntegration
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.GetEncoding(1251);
        // Задаем функцию интегрирования
        Func<double, double> function = x => Math.Sin(x); // Пример функции f(x) = sin(x)

        // Устанавливаем пределы интегрирования
        double a = 0; // Нижний предел
        double b = Math.PI; // Верхний предел

        // Задаем желаемую точность
        double epsilon = 1e-6;

        // Вычисляем интеграл с использованием различных методов
        ComputeIntegral("Левых прямоугольников", LeftRectangle, function, a, b, epsilon);
        ComputeIntegral("Правых прямоугольников", RightRectangle, function, a, b, epsilon);
        ComputeIntegral("Центральных прямоугольников", CentralRectangle, function, a, b, epsilon);
        ComputeIntegral("Трапеций", Trapezoidal, function, a, b, epsilon);
        ComputeIntegral("Симпсона", Simpson, function, a, b, epsilon);
        ComputeIntegral("Симпсона 3/8", Simpson38, function, a, b, epsilon);
        ComputeIntegral("Гаусса 3-го порядка", Gaussian3, function, a, b, epsilon);
        ComputeIntegral("Гаусса 4-го порядка", Gaussian4, function, a, b, epsilon);
    }

    static void ComputeIntegral(string methodName, Func<Func<double, double>, double, double, int, double> method,
                                 Func<double, double> function, double a, double b, double epsilon)
    {
        double result;
        int n = 1;

        // Итерируем пока не достигнем необходимой точности
        do
        {
            result = method(function, a, b, n);
            n *= 2; // Увеличиваем количество разбиений
        } while (Math.Abs(method(function, a, b, n) - result) >= epsilon);

        Console.WriteLine($"Метод: {methodName}, Интеграл: {result}, Шаг: {(b - a) / n}, Разбиений: {n / 2}");
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

    static double CentralRectangle(Func<double, double> f, double a, double b, int n)
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
        if (n % 2 != 0) n++; // Симпсон требует четное количество разбиений
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
        if (n % 3 != 0) n += (3 - (n % 3)); // Симпсон 3/8 требует кратное 3 количество разбиений
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
        // Применение Гаусса 3-го порядка
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

    static double Gaussian4(Func<double, double> f, double a, double b, int n)
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
    }
}
