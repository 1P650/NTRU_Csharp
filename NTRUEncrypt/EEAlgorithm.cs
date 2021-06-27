using static PolynomOperator;

using System;
using static System.Console;
using System.Collections;
using System.Collections.Generic;

public class EEAlgorithm {



 /*   public static fraction[] QUO(fraction[] p1, fraction[] p2){
        Console.WriteLine("P1 = " + polynomToString(p1));
        Console.WriteLine("P2 = " + polynomToString(p2));
        int p1D = Degree(p1), p2D = Degree(p2);
        int power = p1D - p2D;




        Console.WriteLine("POWER = " + power);
        fraction quotient = -(p1[p1D] / p2[p2D]);
        Console.WriteLine("QUOTIENT = " +quotient*(-1));
        fraction[] mul = new fraction[Math.Max(p1.Length, p2.Length)];
        for(int i = 0; i < mul.Length; i++){
            mul[i] = new fraction(0);
        }
        mul[power] = quotient;
        Console.WriteLine("\n");
        return mul;
    }*/


    public static fraction[] QUO(fraction[] p1, fraction[] p2){
        if(ZERO(p2)) throw new DivideByZeroException();
       // Console.WriteLine("P1 = " + polynomToString(p1));
        //Console.WriteLine("P2 = " + polynomToString(p2));
        fraction[] q = new fraction[]{0};
        fraction[] r = new fraction[p1.Length];
        Array.Copy(p1, r, r.Length);
        //Console.WriteLine("R = " + polynomToString(r));
        int d = Degree(p2);
        //Console.WriteLine("DEGREE P2 = " + d);
        fraction c = p2[d];
        //Console.WriteLine("LC P2 = " + c.ToString());

        while(Degree(r) >= d){
            fraction curr_Q = r[Degree(r)]/c;
            if(curr_Q.num == 0) break;

            //Console.WriteLine("CURR_Q = " + curr_Q.ToString());

            int curr_D = Degree(r) - d;
            //Console.WriteLine("CURR_D = " + curr_D);

            fraction[] S = new fraction[curr_D+1];

            for(int i = 0; i < S.Length; i++){
                S[i] = new fraction(0);
            }
            S[curr_D] = curr_Q;
            //Console.WriteLine("S = " + polynomToString(S));
            q = Add(q, S);
            fraction[] e = Multiply(p2, S);
            fraction[] m = Multiply(e, new fraction[]{-1});
            r = Add(r, m);

            //Console.WriteLine("Q = " + polynomToString(q));
            //Console.WriteLine("R = " + polynomToString(r));
            //Console.WriteLine("R D = " + Degree(r));
            //Write("\n");
        }
        q = Multiply(q, new fraction[]{-1});
         //Console.WriteLine("Q = " + polynomToString(q));
        //WriteLine("\n");
        return q;
    }

    public static fraction[][] EGCD(fraction[] a, fraction[] b){
        fraction[] r0 = new fraction[a.Length];
        fraction[] r1 = new fraction[b.Length];
        Array.Copy(a, r0, r0.Length);
        Array.Copy(b, r1, r1.Length);

        List<fraction[]> rI = new List<fraction[]>();
        rI.Add(r0);
        rI.Add(r1);


        fraction[] s0 = new fraction[]{1};
        fraction[] s1 = new fraction[]{0};

        List<fraction[]> sI = new List<fraction[]>();
        sI.Add(s0);
        sI.Add(s1);

        fraction[] t0 = new fraction[]{0};
        fraction[] t1 = new fraction[]{1};

        List<fraction[]> tI = new List<fraction[]>();
        tI.Add(t0);
        tI.Add(t1);

        int i  = 0;
        for(i = 1; ZERO(rI[i]) == false; i++){
            fraction[] q = QUO(rI[i-1], rI[i]);

            fraction[] rtmp = Add(rI[i-1], Multiply(rI[i], q));
            rI.Add(rtmp);

            fraction[] stmp = Add(sI[i-1], Multiply(sI[i], q));
            sI.Add(stmp);

            fraction[] ttmp = Add(tI[i-1], Multiply(tI[i], q));
            tI.Add(ttmp);
        }



        fraction[] mul = new fraction[]{1};
        if(rI[i-1][0].num < 0) {
            mul[0] = -1;
        }
        if(rI[i-1][0] != 1 && rI[i-1][0].num!=0){
            mul[0] = new fraction(rI[i-1][0].den, rI[i-1][0].num);
        }

        fraction[] g = Multiply(rI[i-1], mul);
        fraction[] u = Multiply(sI[i-1], mul);
        fraction[] v = Multiply(tI[i-1], mul);
        fraction[][] rtq = new fraction[3][];
        rtq[0] = new fraction[g.Length];
        rtq[1] = new fraction[u.Length];
        rtq[2] = new fraction[v.Length];
        Array.Copy(g, rtq[0], g.Length);
        Array.Copy(u, rtq[1], u.Length);
        Array.Copy(v, rtq[2], v.Length);
        return rtq;

    }

    public static bool ZERO(fraction[] pz){
        if(pz.Length == 1 && pz[0].num == 0) return true;
        return false;
    }

}
