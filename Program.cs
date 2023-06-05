using System;

class Program
{
    static Program mainApp = new Program();
    enum OP
    {
        UNKNOWN,
        MINUS,
        PLUS,
        MULTIPLY,
        DEVIDE,
        MOD
    }

    static void Main()
    {
        Console.WriteLine("----- Rules -----");
        Console.WriteLine("Enter Left num, operation, right num.");
        Console.WriteLine("Calculator ignore any non-number symbols");
        Console.WriteLine("Calculator can calculate only one operation");
        Console.WriteLine("Write \"exit\" to exit");
        Console.WriteLine("-----------------");

        while (true)
        {
            Console.WriteLine("Write an expression");
            string str = Console.ReadLine();
            if (str == "exit")
                break;

            Console.WriteLine(mainApp.Calculate(str));
        }
    }
    double Calculate(string in_str)
    {
        string left = "";
        string right = "";
        double left_num = 0;
        double right_num = 0;
        OP op = OP.UNKNOWN;

        for (int i = 0; i < in_str.Length; i++)
        {
            switch (in_str[i])
            {
                case '-':
                    if (op == OP.UNKNOWN)
                    {
                        if (left.Length == 0)
                            left += in_str[i];
                        else
                            op = OP.MINUS;
                    }
                    else
                    if (right.Length == 0)
                        right += in_str[i];
                    break;
                case '+':
                    op = OP.PLUS;
                    break;
                case '*':
                    op = OP.MULTIPLY;
                    break;
                case '/':
                    op = OP.DEVIDE;
                    break;
                case '%':
                    op = OP.MOD;
                    break;
                default:
                    {
                        if ((in_str[i] > '9' || in_str[i] < '0') &&
                            in_str[i] != '.' && in_str[i] != ',')
                            continue;

                        if (op == OP.UNKNOWN)
                            left += in_str[i] == '.' ? ',' : in_str[i];
                        else
                            right += in_str[i] == '.' ? ',' : in_str[i];
                        break;
                    }
            }
        }

        if (op == OP.UNKNOWN)
        {
            Console.WriteLine("Invalid operation");
            return 0f;
        }

        if (left.Length == 0 || !Double.TryParse(left, out left_num))
        {
            Console.WriteLine("Invalid left argument");
            return 0f;
        }

        if (right.Length == 0 || !Double.TryParse(right, out right_num))
        {
            Console.WriteLine("Invalid right argument");
            return 0f;
        }
        if (!CheckRules(left_num, right_num, op))
            return 0f;

        switch (op)
        {
            case OP.MINUS:
                Console.Write("[Calculate] " + left_num + " - " + right_num + " = ");
                return left_num - right_num;
            case OP.PLUS:
                Console.Write("[Calculate] " + left_num + " + " + right_num + " = ");
                return left_num + right_num;
            case OP.MULTIPLY:
                Console.Write("[Calculate] " + left_num + " * " + right_num + " = ");
                return left_num * right_num;
            case OP.DEVIDE:
                Console.Write("[Calculate] " + left_num + " / " + right_num + " = ");
                return left_num / right_num;
            case OP.MOD:
                Console.Write("[Calculate] " + left_num + " % " + right_num + " = ");
                return left_num % right_num;
        }

        return 0f;
    }

    bool CheckRules(double in_left, double in_right, OP in_op)
    {
        switch (in_op)
        {
            case OP.DEVIDE:
            case OP.MOD:
                {
                    if (in_right == 0)
                    {
                        Console.WriteLine("Devide by zero");
                        return false;
                    }

                    return true;
                }
            case OP.MINUS:
            case OP.PLUS:
            case OP.MULTIPLY:
                return true;
        }
        return false;
    }
}
