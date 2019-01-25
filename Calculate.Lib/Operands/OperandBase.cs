﻿namespace Calculate.Lib.Operands
{
    public abstract class OperandBase
    {
        public OperandBase(OperandType type)
        {
            Type = type;
        }

        public OperandBase()
        {
        }

        public OperandType Type { get; set; }
    }
}