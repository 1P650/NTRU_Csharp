using System;

public struct fraction {

    public int num;
    public int den;


    public fraction(int numerator, int denominator)
    {
        if (denominator == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.", nameof(denominator));
        }
        int gcd = GCDI(Math.Abs(numerator), Math.Abs(denominator));

        if(denominator < 0) {
            numerator*=-1;
            denominator*=-1;
        }
        num = numerator/gcd;
        den = denominator/gcd;

    }

    public fraction(int numerator){
        num = numerator;
        den = 1;
    }




    public static fraction operator +(fraction a) {
      return a;
    }
    public static fraction operator -(fraction a) {
        return new fraction(-a.num, a.den);
    }

    public static fraction operator +(fraction a, fraction b)
    => new fraction(a.num * b.den + b.num * a.den, a.den * b.den);

    public static fraction operator -(fraction a, fraction b)
    => a + (-b);

    public static fraction operator *(fraction a, fraction b)
    => new fraction(a.num * b.num, a.den * b.den);


    public static bool operator ==(fraction a, fraction b){
        if(a.num == b.num && a.den == b.den) return true;
        return false;
    }

    public static bool operator !=(fraction a, fraction b){
        if(a.num == b.num && a.den == b.den) return false;
        return true;
    }

    public static implicit operator int(fraction d) => d.num/d.den;
    public static implicit operator fraction(int b) => new fraction(b,1);


    public static fraction operator --(fraction a)=> a - (new fraction(1,1));
    public static fraction operator ++(fraction a)=> a + (new fraction(1,1));

    public static fraction operator /(fraction a, fraction b)
    {
        if (b.num == 0)
        {
            throw new DivideByZeroException();
        }
        return new fraction(a.num * b.den, a.den * b.num);
    }

    public override string ToString() {
        if(den == 1 ) return num + "";
        return num + "/" + den;
    }

    private static int GCDI(int a, int b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }

}
