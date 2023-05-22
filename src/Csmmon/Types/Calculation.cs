using System.Diagnostics.CodeAnalysis;

namespace Csmmon
{
    public readonly struct Calculation
    {
        public string Equation { get; }

        public double Result { get; } = double.NaN;

        public Calculation(string equation, double result)
        {
            Equation = equation;

            Result = Convert.ToDouble(result);
        }

        public override string ToString()
            => $"{Result}";

        public override int GetHashCode()
            => Result.GetHashCode();

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is Calculation calc && calc.Result == Result)
                return true;
            return false;
        }

        public static bool operator ==(Calculation left, Calculation right)
            => left.Equals(right);

        public static bool operator !=(Calculation left, Calculation right)
            => !(left == right);
    }
}
