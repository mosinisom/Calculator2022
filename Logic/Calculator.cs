namespace Calculator2022.Logic;

public class Calculator
{
    //константы
    const int INPUT1 = 1;
    const int OPERATION = 2;
    const int INPUT2 = 3;
    const int RESULT = 4;
    const int ERROR = 5;

    public const string Clear = "C";
    public const string BackSpace = "B";
    //...

    //данные (приватные)
    string _screen; //экран калькулятора
    string _memory;
    string _op;
    int _state;

    //публичные методы
    //получить "содержимое" экрана
    public string Screen
    {
        get { return _screen; }
    }

    //конструктор
    public Calculator()
    {
        _screen = "0";
        _memory = "";
        _op = "";
        _state = INPUT1;
    }

    //"нажатие" на кнопку
    public void Press (string key)
    {
        if (key == Clear)
        {
            _screen = "0";
            _state = INPUT1;
        }
        else if (key == BackSpace)
        {
            if (_screen.Length > 1)
                _screen = _screen.Substring(0, _screen.Length - 1);
            else
                _screen = "0";
        }
        else if (key == "=")
        {
            if (_state == INPUT2)
            {
                _screen = Calculate(_memory, _op, _screen);
                _state = RESULT;
            }
        }
        else if (key == "+/-")
        {
            if (_state == OPERATION)
            {
               // ничего не делаем
            }
            else if (_state == INPUT2 || _state == RESULT || _state == INPUT1)
            {
                if (_screen != "0")
                {
                    if (_screen[0] == '-')
                        _screen = _screen[1..];
                    else
                        _screen = "-" + _screen;
                }
            }
        }
        else if (key == ",")
        {
            if (_state == RESULT)
            {
                _screen = "0,";
                _state = INPUT1;
            }
            else if (_state == OPERATION)
            {
                _screen = "0,";
                _state = INPUT2;
            }
            else if (_state == INPUT1 || _state == INPUT2)
            {
                if (!_screen.Contains(','))
                    _screen += ",";
            }
        }
        else if (key == "+")
        {
            if (_state == INPUT1)
            {
                _memory = _screen;
                _op = "+";
                _state = OPERATION;
            }
            else if (_state == INPUT2)
            {
                _screen = Calculate(_memory, _op, _screen);
                _memory = _screen;
                _op = "+";
                _state = OPERATION;
            }
            else if (_state == OPERATION)
            {
                _op = "+";
            }
            else if (_state == RESULT)
            {
                _memory = _screen;
                _op = "+";
                _state = OPERATION;
            }
        }
        else if (key == "-")
        {
            if (_state == INPUT1)
            {
                _memory = _screen;
                _op = "-";
                _state = OPERATION;
            }
            else if (_state == INPUT2)
            {
                _screen = Calculate(_memory, _op, _screen);
                _memory = _screen;
                _op = "-";
                _state = OPERATION;
            }
            else if (_state == OPERATION)
            {
                _op = "-";
            }
            else if (_state == RESULT)
            {
                _memory = _screen;
                _op = "-";
                _state = OPERATION;
            }
        }
        else if (key == "×")
        {
            if(_state == INPUT1)
            {
                _memory = _screen;
                _op = "×";
                _state = OPERATION;
            }
            else if (_state == INPUT2)
            {
                _screen = Calculate(_memory, _op, _screen);
                _memory = _screen;
                _op = "×";
                _state = OPERATION;
            }
            else if (_state == OPERATION)
            {
                _op = "×";
            }
            else if (_state == RESULT)
            {
                _memory = _screen;
                _op = "×";
                _state = OPERATION;
            }
        }
        else if (key == "÷")
        {
            if (_state == INPUT1)
            {
                _memory = _screen;
                _op = "÷";
                _state = OPERATION;
            }
            else if (_state == INPUT2)
            {
                _screen = Calculate(_memory, _op, _screen);
                _memory = _screen;
                _op = "÷";
                _state = OPERATION;
            }
            else if (_state == OPERATION)
            {
                _op = "÷";
            }
            else if (_state == RESULT)
            {
                _memory = _screen;
                _op = "÷";
                _state = OPERATION;
            }
        }
        else
        {
            if (_state == OPERATION)
            {
                _screen = key;   
                _state = INPUT2;
            }
            else if (_state == RESULT)
            {
                _screen = key;
                _state = INPUT1;
            }
            else if (_screen == "0")
                _screen = key;
            else
                _screen += key;
        }
    
    string Calculate(string mem, string op, string scr)
    {
        double m = double.Parse(mem);
        double s = double.Parse(scr);
        double r = 0;
        switch(op)
        {
            case "+":
                r = m + s;
                break;
            case "-":
                r = m - s;
                break;
            case "×":
                r = m * s;
                break;
            case "÷":
                if (s == 0)
                {
                    _state = ERROR;
                    return "Error";
                }
                r = m / s;
                break;
        }
            return r.ToString("0.##########").TrimEnd('0').TrimEnd(',');
    }
    }
}

