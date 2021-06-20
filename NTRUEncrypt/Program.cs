using static System.Console;

using System;
using System.Collections;

public class Program {
    //NTRU 503

    const int N = 11;

    const int q = 32;
    const int p = 3;

    /*
    todo 1) Операции с многочленами ПО МОДУЛЮ
    todo 2) Нахождение обратного полинома, алгоритм Евклида ПО МОДУЛЮ, ВСЕ ОПЕРАЦИИ ПО МОДУЛЮ N
    todo 3) Реализация алгоритма, собственно

     */


    const int df = 4;
    const int dg = 3;


    public static void Main(String[] args) {
        WriteLine("NTRU ENCRYPT C# VERSION 0.0 \n" +
        "");
        //2+x+x^2+2x^4+x^6+x^9+2x^10
        //x^11 + 2
         int[] factorBase3 = new int[]{2,0,0,0,0,0,0,0,0,0,0,1};
         int [] polynom3 = new int[]{2,1,1,0,2,0,1,0,0,1,2};

        int[] factorBase32 = new int[]{31,0,0,0,0,0,0,0,0,0,0,1};
        int[] polynom32 = new int[]{31,1,1,0,31,0,1,0,0,1,31};




        Operator NTRUOperator = new Operator(32);
        int[] c = NTRUOperator.polynomGetInverse(polynom32, factorBase32);
        WriteLine(PolynomToString(c));



    }

    public static string PolynomToString(int[] p){
        string output = "";
        for(int i = 0; i < p.Length; i++){
           if(p[i]==0) continue;
            string quotient = (p[i] == 1 && i!=0? "": ""+ p[i]);
            string variable = (i == 0 ? "":"x");
            string power = (i == 0 || i == 1 ? "":"^"+i);
            string plus = (i == Operator.PolynomOperator.deg(p) ? "":"+");
            output += quotient+variable+power+" " + plus + " ";
       }

        return output;
    }
}
