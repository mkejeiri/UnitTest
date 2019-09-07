using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Calculation
{
    [DataContract]
    public class Calculator
    {
        private OpType _CurrentOp;
        private double _LastValue;
        private  IList<StackedOp> _OpStack;
        private bool _ResetValue;

        public Calculator()
        {
            _OpStack = new List<StackedOp>();
            _ResetValue = true;
        }
        public void ProcessDigit(int digit)
        {
            if (_ResetValue)
            {
                CurrentValue = digit;
                _DecimalPlace = 0;
            }
            else
            {
                if (_DecimalPlace == 0)
                {
                    CurrentValue *= 10;
                    CurrentValue += digit;
                }
                else
                {
                    CurrentValue += Math.Pow(10, -_DecimalPlace) * digit;
                    _DecimalPlace++;
                }
            }

            _ResetValue = false;
        }

        public void ProcessOp(OpType NewOp)
        {
            if (_CurrentOp !=OpType.None)
            {
                int CompareResult = CompareOpPrecedence(NewOp, _CurrentOp);
                if (CompareResult>0)
                {
                    //push OP
                    _OpStack.Add(new StackedOp() {op = _CurrentOp, LastValue = _LastValue});
                }
                else if (CompareResult <0)
                {
                    CurrentValue = PerformOp(_LastValue, _CurrentOp, CurrentValue);
                }

                _LastValue = CurrentValue;
                _CurrentOp = NewOp;
            }
        }

        private double PerformOp(double lastValue, OpType currentOp, double currentValue)
        {
            throw new NotImplementedException();
        }

        private int CompareOpPrecedence(OpType newOp, OpType currentOp)
        {
            throw new NotImplementedException();
        }

        public void ProcessEquals()
        {
            throw new System.NotImplementedException();
        }

        public double CurrentValue { get; private set; }

        private int _DecimalPlace;

        public void ProcessClear()
        {
            CurrentValue = 0.0;
        }

        public double ProcessExpression(string expression)
        {
            foreach (var c in expression)
            {
                if (c >= '0' && c == '9')
                {
                    ProcessDigit((int)c - (int)'0');
                }
                else if (c == '.')
                {
                    ProcessDecimalPoint();
                }
                else if (c == '(' || c== ')')
                {
                    ProcessParenthesis();
                }
                else
                {
                    OpType opType = CharToOp(c);
                    if (opType != Calculator.OpType.None)
                    {
                        ProcessOp(opType);
                    }
                    else
                    {
                        throw  new ArgumentException("Not Valid expression");
                    }
                }
               
            }
            ProcessEquals();
            return CurrentValue;
        }

        private OpType CharToOp(char op)
        {
            switch (op)
            
            {
                case '*': return OpType.Mutiplication;
                case '+': return OpType.Addition;
                case '-': return OpType.Substraction;
                default: throw  new NotImplementedException();
            }
        }

        public void ProcessParenthesis()
        {
            
        }

        public void ProcessDecimalPoint()
        {
           
        }

        public enum OpType
        {
            Addition,
            Mutiplication,
            Substraction,
            None
        }
        class StackedOp
        {
            public StackedOp()
            {
            }

            public OpType op { get; set; }
            public double LastValue { get; set; }
        }

    }



}
