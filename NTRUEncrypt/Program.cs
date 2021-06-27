using static System.Console;

using System;
using System.Collections;
using static PolynomOperator;


public class Program {
    //NTRU 503

    const int N = 11;

    const int q = 32;
    const int p = 3;

    /*
    todo 1) Операции с многочленами В ПОЛЕ Q
    todo 2) GCD и обратный полином
    todo 3) Реализация алгоритма, собственно

     */


    const int df = 4;
    const int dg = 3;


    public static void Main(String[] args) {
        WriteLine("NTRU ENCRYPT C# VERSION 0.0 \n" +
        "");
        //2+x+x^2+2x^4+x^6+x^9+2x^10
        //x^11 + 2
        fraction[] factorBase = new fraction[]{-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1};
        fraction [] polynom = new fraction[]{-1, 1, 1, 0, -1, 0, 1, 0, 0, 1, -1};
        fraction[] u = new fraction[0], v = new fraction[0], g = new fraction[0];




        fraction[][] result = EEAlgorithm.EGCD(factorBase, polynom);
        g = result[0];
        u = result[1];
        v = result[2];

        int[] v3 = NTRU.toModulo(v,3);
        int[] v32 = NTRU.toModulo(v,32);

        WriteLine(polynomToString(v3));
        WriteLine(polynomToString(v32));


    }


}
