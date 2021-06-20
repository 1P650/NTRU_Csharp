using System;
using System.Collections;
using System.Runtime.Remoting.Lifetime;
using static Operator.PolynomOperator;

public class Operator {

private int MODULO = 1;

    public Operator(int modulo){
        setModulo(modulo);
    }

    public void setModulo(int modulo){
        this.MODULO = modulo;
    }

    public  class PolynomOperator{
        public static int[] polynomMultiply(int[] p1_O, int[] p2_O, int modulo){
            int[] p1 = new int[p1_O.Length];
            int[] p2 = new int[p2_O.Length];

            Array.Copy(p1_O, p1, p1.Length);
            Array.Copy(p2_O, p2, p2.Length);

            int [] p3 = new int[p1.Length + p2.Length];
            for(int i = 0; i < p1.Length; i++){
                for(int j = 0; j < p2.Length; j++){
                    p3[j+i] += p2[j]*p1[i];
                }
            }

            for(int i = 0; i < p3.Length; i++){
                p3[i] = p3[i] %modulo;
                if(p3[i]<0)p3[i]+=modulo;
            }
            return polynomDeleteZeros(p3);
        }
        public static int[] polynomAdd(int[] p1_O, int [] p2_O, int modulo){
            int[] p1 = new int[p1_O.Length];
            int[] p2 = new int[p2_O.Length];

            Array.Copy(p1_O, p1, p1.Length);
            Array.Copy(p2_O, p2, p2.Length);

            if(p1.Length < p2.Length){
                for(int i = 0; i < p1.Length; i++){
                    p2[i]+=p1[i];
                }

                for(int i  =0; i < p2.Length; i++){
                    p2[i]%=modulo;
                    if(p2[i]<0)p2[i]+=modulo;
                }
                return polynomDeleteZeros(p2);
            }
            else{
                for(int i = 0; i < p2.Length; i++){
                    p1[i]+=p2[i];
                }

                for(int i  =0; i < p1.Length; i++){
                    p1[i]%=modulo;
                    if(p1[i]<0) p1[i]+=modulo;
                }
                return polynomDeleteZeros(p1);
            }

        }
        public static int[] polynomDeleteZeros(int[] p){
            int i;
            for(i = p.Length -1; i >=0; i--){
                if(p[i]!=0) break;
            }
            if(i == 0){
                int[] rtq = new int[1];
                rtq[0] = p[0]; return p;
            }
            int[] rtf = new int[i+1];
            Array.Copy(p, rtf, rtf.Length);
            return rtf;
        }
        public static int deg(int[] p1){
            int dgM = p1.Length-1;
            while (dgM>0 && p1[dgM] == 0) dgM--;
            return dgM;
        }


    }


    public  int[] EuclidDivMod(int[] p1, int[] p2){
        int[] sub = polynomMultiply(EuclidDivQnt(p1,p2), p2, MODULO);
        return polynomAdd(p1, (sub), MODULO);
    }

    public  int[] EuclidDivQnt(int[] p1, int[] p2){
        //Console.WriteLine("P1 = " + String.Join(" ", p1) + " DEG = " + deg(p1));
        //Console.WriteLine("P1 = " + Program.PolynomToString(p1) + " DEG = " + deg(p1));

        //Console.WriteLine("P2 = " + String.Join(" ", p2) + " DEG = " + deg(p2));
        //Console.WriteLine("P2 = " +Program.PolynomToString(p2) + " DEG = " + deg(p2));



        int power = deg(p1) - deg(p2);

        //Console.WriteLine("POWER = " + power);
        int quotient = -(p1[deg(p1)] * ModAriphmetics.INV(p2[deg(p2)],MODULO))%MODULO;
        //Console.WriteLine("QUOTIENT = " + quotient*(-1));
        int[] mul = new int[Math.Max(p1.Length, p2.Length)];
        mul[power] = quotient;
        //Console.WriteLine("\n");
        return mul;
    }

    public  int[] GCD(int[] p1, int [] p2, ArrayList divs){

        if(deg(p2)==0){
         return polynomDeleteZeros(p2);
        }

        int[] m = EuclidDivMod(p1, p2);


        int[] q = polynomMultiply(EuclidDivQnt(p1,p2), new int[]{-1}, MODULO);
        divs.Add(q);

        return GCD(p2, m, divs);
    }


    public  class ModAriphmetics{
        public static int INV(int a, int m) {
            for (int x = 1; x < m; x++)
                if (((a%m) * (x%m)) % m == 1)
                    return x;
            return 1;
        }

    }




    public  int[] polynomGetInverse(int[] p, int[] x){
        ArrayList divs = new ArrayList();
        GCD(x,p,divs);
        int[] inverse = getNextInvPolynom(new int[]{1}, (int[]) divs[divs.Count -1], divs.Count- 1, divs, 0);
       return inverse;
    }

    public  int[] getNextInvPolynom(int[] p1, int[] p2, int INDEX, ArrayList divs, int round){
        //Console.WriteLine("GetNextIntPolynom ROUND = " + round);
        //Console.WriteLine("P1 = " + Program.PolynomToString(p1));
        //Console.WriteLine("P2 = " + Program.PolynomToString(p2));
        //Console.WriteLine("MULTIPLIER = " + Program.PolynomToString((int[])divs[INDEX]) );
        //Console.WriteLine("\n");

        if(round == divs.Count-1) {
            if(divs.Count % 2 == 0){
                return p1;
            }
            return p2;
        }

        if(round % 2 == 0) {
            int[] next = polynomAdd(p1, polynomMultiply(p2, (int[]) divs[INDEX-1], MODULO), MODULO);
            return getNextInvPolynom(
                    next,
                    p2,
                    INDEX -1,
                    divs,
                    round+1

            );
        }
        else{
            int[] next = polynomAdd(p2, polynomMultiply(p1, (int[]) divs[INDEX-1], MODULO), MODULO);
            return getNextInvPolynom(
                    p1,
                    next,
                    INDEX -1,
                    divs,
                    round+1

            );
        }
    }


}
