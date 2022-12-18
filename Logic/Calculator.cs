using System.Globalization;

namespace Calculator2022.Logic;

public class Calculator
{
    //данные (приватные)
    string _screen; //экран калькулятора
    string _memory;
    string _op;
    string temp;
    CalcState _state;

    //публичные методы
    //получить "содержимое" экрана
    public string Screen
    {
        get => _screen;
    }

    //конструктор
    public Calculator()
    {
        _screen = "0";
        _memory = "";
        _op = "";
        _state = CalcState.Input1;
    }

    public void Press(string key)
    {
        try
        {
            switch (_state)
            {
                case CalcState.Input1: _state = ProcessInput1(key); break;
                case CalcState.Input2: _state = ProcessInput2(key); break;
                case CalcState.Operation: _state = ProcessOperation(key); break;
                case CalcState.Result: _state = ProcessResult(key); break;
                case CalcState.Error: _state = ProcessError(key); break;
            }
        }
        catch
        {
            _screen = "Error";
            _state = CalcState.Error;
        }
    }

    private CalcState ProcessInput1(string key)
    {
        switch(GetKeyKind(key))
        {
            case CalcKey.Digit:
                _screen = AddDigit(_screen, key);
                return CalcState.Input1;
            case CalcKey.Dot:
                _screen = AddDot(_screen);
                return CalcState.Input1;
            case CalcKey.ChangeSign:
                _screen = ChangeSign(_screen);
                return CalcState.Input1;
            case CalcKey.Operation:
                _memory = _screen;
                _op = key;
                return CalcState.Operation;
            case CalcKey.Result:
                return CalcState.Input1;
            case CalcKey.Clear:
                Clear();
                return CalcState.Input1;
            case CalcKey.Back:
                _screen = Back(_screen);
                return CalcState.Input1;
        }
        return CalcState.Error;
    }

    private CalcState ProcessInput2(string key)
    {
        switch (GetKeyKind(key))
        {
            case CalcKey.Digit:
                _screen = AddDigit(_screen, key);
                return CalcState.Input2;
            case CalcKey.Dot:
                _screen = AddDot(_screen);
                return CalcState.Input2;
            case CalcKey.ChangeSign:
                _screen = ChangeSign(_screen);
                return CalcState.Input2;
            case CalcKey.Operation:
                _screen = Calculate(_memory, _screen, _op);
                _memory = _screen;
                _op = key;
                return CalcState.Operation;
            case CalcKey.Result:
                temp = _memory;
                _memory = _screen;
                _screen = temp;
                if (_op == "-" || _op == "/")
                {
                    _screen = Calculate(_screen,_memory, _op);
                } else
                {
                    _screen = Calculate(_memory, _screen, _op);
                }
                return CalcState.Result;
            case CalcKey.Clear:
                Clear();
                return CalcState.Input1;
            case CalcKey.Back:
                _screen = Back(_screen);
                return CalcState.Input2;
        }
        return CalcState.Error;

    }

    private CalcState ProcessOperation(string key)
    {
        switch (GetKeyKind(key))
        {
            case CalcKey.Digit:
                _screen = key;
                return CalcState.Input2;
            case CalcKey.Dot:
                _screen = "0.";
                return CalcState.Input2;
            case CalcKey.ChangeSign:
                return CalcState.Operation;
            case CalcKey.Operation:
                _op = key;
                return CalcState.Operation;
            case CalcKey.Result:
                _screen = Calculate(_memory, _screen, _op);
                return CalcState.Result;
            case CalcKey.Clear:
                Clear();
                return CalcState.Input1;
            case CalcKey.Back:
                return CalcState.Operation;
        }
        return CalcState.Error;

    }


    private CalcState ProcessResult(string key)
    {
        switch (GetKeyKind(key))
        {
            case CalcKey.Digit:
                _screen = key;
                return CalcState.Input1;
                
            case CalcKey.Dot:
                _screen = "0.";
                return CalcState.Input1;
                
            case CalcKey.ChangeSign:
                _screen = ChangeSign(_screen);
                return CalcState.Result;
                
            case CalcKey.Operation:
                _memory = _screen;
                _op = key;
                return CalcState.Operation;
                
            case CalcKey.Result:
                if (_op == "-" || _op == "/")
                {
                    _screen = Calculate(_screen, _memory, _op);
                }
                else
                {
                    _screen = Calculate(_memory, _screen, _op);
                }
                return CalcState.Result;
                
            case CalcKey.Clear:
                Clear();
                return CalcState.Input1;
                
            case CalcKey.Back:
                return CalcState.Result;
        }
        return CalcState.Error;

    }

    string Calculate(string arg1, string arg2, string op)
    {
        double x = double.Parse(arg1, CultureInfo.InvariantCulture);
        double y = double.Parse(arg2, CultureInfo.InvariantCulture);
        double res = 0;

        switch (op)
        {
            case "+": res = x + y; break;
            case "-": res = x - y; break;
            case "*": res = x * y; break;
            case "/":
                if (y == 0)
                    throw new Exception();
                res = x / y; 
                break;
            default:
                throw new Exception();
        }

        return res.ToString("0.##########", CultureInfo.InvariantCulture);
    }

    private CalcState ProcessError(string key)
    {
        switch (GetKeyKind(key))
        {
            case CalcKey.Digit:
            case CalcKey.Dot:
            case CalcKey.ChangeSign:
            case CalcKey.Operation:
            case CalcKey.Result:
            case CalcKey.Back:
                return CalcState.Error;
            case CalcKey.Clear:
                Clear();
                return CalcState.Input1;
        }
        return CalcState.Error;
    }

    string AddDigit(string num, string key)
    {
        if (num == "0")
            if (key == "0")
                return "0";
            else
                return key;
        return num + key;
    }

    string AddDot(string num)
    {
        if (!num.Contains("."))
            return num + ".";
        return num;
    }

    string ChangeSign (string num)
    {
        if (num == "0")
            return "0";
        if (num.StartsWith("-"))
            return num.Substring(1);
        return "-" + num;
    }

    string Back(string num)
    {
        string res = num.Substring(0, num.Length - 1);
        if (num.Length == 1)
            return "0";
        else if (num.Length == 2 && num.StartsWith("-"))
            return "0";
        else if (res.EndsWith("."))
            return res.Substring(0, res.Length - 1);
        else
            return res;
    }

    private void Clear()
    {
        _screen = "0";
        _memory = "";
        _op = "";
    }

    CalcKey GetKeyKind(string key)
    {
        switch(key)
        {
            case "0":
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":
                return CalcKey.Digit;
            case ".":
                return CalcKey.Dot;
            case "-/+":
                return CalcKey.ChangeSign;
            case "-":
            case "+":
            case "*":
            case "/": 
                return CalcKey.Operation;
            case "=":
                return CalcKey.Result;
            case "C":
                return CalcKey.Clear;
            case "B":
                return CalcKey.Back;
            default:
                return CalcKey.Undefined;
        }
    }
}

enum CalcState
{
    Input1,
    Operation,
    Input2,
    Result,
    Error
}

enum CalcKey
{
    Undefined,
    Digit,
    Dot,
    ChangeSign,
    Operation,
    Result,
    Clear,
    Back,
}

