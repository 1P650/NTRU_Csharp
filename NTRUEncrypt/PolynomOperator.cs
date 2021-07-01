using System;
using System.Collections.Generic;


public class PolynomOperator {

    public static fraction[] Add(fraction[] p1_Orig, fraction[] p2_Orig) {

        fraction[] p1 = new fraction[p1_Orig.Length];
        fraction[] p2 = new fraction[p2_Orig.Length];

        Array.Copy(p1_Orig, p1, p1.Length);
        Array.Copy(p2_Orig, p2, p2.Length);

        if (p1.Length < p2.Length) {
            fraction[] p_tmp = p1;
            p1 = p2;
            p2 = p_tmp;
        }
        for (int i = 0; i < p2.Length; i++) {
            p1[i] += p2[i];
        }
        return polynomDeleteZeros(p1);
    }

    public static int[] Add(int[] p1_Orig, int[] p2_Orig) {

        int[] p1 = new int[p1_Orig.Length];
        int[] p2 = new int[p2_Orig.Length];

        Array.Copy(p1_Orig, p1, p1.Length);
        Array.Copy(p2_Orig, p2, p2.Length);

        if (p1.Length < p2.Length) {
            int[] p_tmp = p1;
            p1 = p2;
            p2 = p_tmp;
        }
        for (int i = 0; i < p2.Length; i++) {
            p1[i] += p2[i];
        }
        return p1;
    }

    public static fraction[] Sub(fraction[] p1_Orig, fraction[] p2_Orig) {
        fraction[] p1 = new fraction[p1_Orig.Length];
        fraction[] p2 = new fraction[p2_Orig.Length];

        Array.Copy(p1_Orig, p1, p1.Length);
        Array.Copy(p2_Orig, p2, p2.Length);
        p2 = Multiply(p2, new fraction[]{-1});

        if (p1.Length < p2.Length) {
            fraction[] p_tmp = p1;
            p1 = p2;
            p2 = p_tmp;
        }
        for (int i = 0; i < p2.Length; i++) {
            p1[i] += p2[i];
        }
        return polynomDeleteZeros(p1);
    }


    public static fraction[] Multiply(fraction[] p1_Orig, fraction[] p2_Orig) {
        fraction[] p1 = new fraction[p1_Orig.Length];
        fraction[] p2 = new fraction[p2_Orig.Length];

        Array.Copy(p1_Orig, p1, p1.Length);
        Array.Copy(p2_Orig, p2, p2.Length);

        if (Degree(p1) < Degree(p2)) {
            fraction[] p_tmp = p1;
            p1 = p2;
            p2 = p_tmp;
        }

        fraction[] p3 = new fraction[p1.Length + p2.Length];
        for (int i = 0; i < p3.Length; i++) {
            p3[i] = new fraction(0);
        }
        for (int i = 0; i < p1.Length; i++) {
            for (int j = 0; j < p2.Length; j++) {
                p3[i + j] = p3[i + j] + (p1[i] * p2[j]);
            }
        }

        return polynomDeleteZeros(p3);
    }

    public static int[] Multiply(int[] p1_Orig, int[] p2_Orig){
        int[] p1 = new int[p1_Orig.Length];
        int[] p2 = new int[p2_Orig.Length];

        Array.Copy(p1_Orig, p1, p1.Length);
        Array.Copy(p2_Orig, p2, p2.Length);

        if (Degree(p1) < Degree(p2)) {
            int[] p_tmp = p1;
            p1 = p2;
            p2 = p_tmp;
        }

        int[] p3 = new int[p1.Length + p2.Length];
        for (int i = 0; i < p3.Length; i++) {
            p3[i] = 0;
        }
        for (int i = 0; i < p1.Length; i++) {
            for (int j = 0; j < p2.Length; j++) {
                p3[i + j] = p3[i + j] + (p1[i] * p2[j]);
            }
        }

        return p3;
    }

    public static fraction[] getMultiplyNTRU(fraction[] p_orig, int N){
        fraction[] p = new fraction[p_orig.Length];
        for(int i = 0; i < p.Length; i++){
            p[i] = new fraction(0);
        }
        Array.Copy(p_orig, p, p.Length);
        if(p.Length < N) return p_orig;
        for(int i = N; i < p.Length; i++){
            p[i%N]= p [i%N] + p[i];
        }

        fraction[] p_return = new fraction[N];
        for(int i = 0; i < p_return.Length; i++){
            p_return[i] = new fraction(0);
        }
        Array.Copy(p, p_return, p_return.Length);
        return p_return;
    }

    public static int[] getMultiplyNTRU(int[] p_orig, int N){
        int[] p = new int[p_orig.Length];
        for(int i = 0; i < p.Length; i++){
            p[i] = 0;
        }
        Array.Copy(p_orig, p, p.Length);
        if(p.Length < N) return p_orig;
        for(int i = N; i < p.Length; i++){
            p[i%N]= p [i%N] + p[i];
        }

        int[] p_return = new int[N];
        for(int i = 0; i < p_return.Length; i++){
            p_return[i] = 0;
        }
        Array.Copy(p, p_return, p_return.Length);
        return p_return;
    }


    public static int Degree(fraction[] p) {
        if (p.Length == 1) return 0;
        int dgP = p.Length - 1;
        while (p[dgP] == new fraction(0, 1) && dgP > 0) dgP--;
        return dgP;
    }

    public static int Degree(int[] p) {
        if (p.Length == 1) return 0;
        int dgP = p.Length - 1;
        while (p[dgP] == 0 && dgP > 0) dgP--;
        return dgP;
    }

    public static string polynomToString(fraction[] p) {
        string output = "";
        for (int i = 0; i < p.Length; i++) {
            if (p[i].num == 0) continue;
            string quotient = ((p[i] == new fraction(1, 1) ) && i != 0? "": "" + p[i].ToString());
            string variable = (i == 0 ? "":"x");
            string power = (i == 0 || i == 1 ? "":"^" + i);
            string plus = (i == p.Length - 1 ? "":"+");
            output += quotient + variable + power + " " + plus + " ";
        }

        return output;
    }

    public static string polynomToString(int []p) {
        string output = "";
        for (int i = 0; i < p.Length; i++) {
            if (p[i] == 0) continue;
            string quotient = ((p[i] ==1)  && i != 0? "": "" + p[i]);
            string variable = (i == 0 ? "":"x");
            string power = (i == 0 || i == 1 ? "":"^" + i);
            string plus = (i == p.Length -1 ? "":"+");
            output += quotient + variable + power + " " + plus + " ";
        }

        return output;
    }

    public static string polynomToString(fraction[] p, string separator) {
        return String.Join(separator, p);
    }

    public static fraction[] polynomDeleteZeros(fraction[] p) {
        int i = p.Length-1;
        while (p[i].num == 0 && i > 0) i--;
            if (i == 0) {
                fraction[] rtq = new fraction[1];
                rtq[0] = p[i];
                return rtq;
            }
        fraction[] rtf = new fraction[i + 1];
        Array.Copy(p, rtf, rtf.Length);
        return rtf;


    }

    public static int[] toModulo(fraction[] p, int modulo){
        int [] n = new int[p.Length];
        for(int i = 0; i < p.Length; i++){
            n[i] = (int) (p[i].num % modulo) * modInverse(p[i].den, modulo);
            if(n[i]<0){
                while (n[i]<0) n[i]+=modulo;
            }
            n[i]%=modulo;
        }
        return n;
    }

    public static int[] toModulo(int[] p, int modulo){
        int [] n = new int[p.Length];
        for(int i  = 0; i < n.Length; i++){
            n[i] = 0;
        }
        for(int i = 0; i < p.Length; i++){
            n[i] =  (p[i] % modulo);
            if(n[i]<0){
                while (n[i]<0) n[i]+=modulo;
            }
            n[i]%=modulo;
        }
        return n;
    }

    public static int[] toModulo(int[] p, int modulo, bool half_modulo){
        int [] n = new int[p.Length];
        for(int i  = 0; i < n.Length; i++){
            n[i] = 0;
        }
        for(int i = 0; i < p.Length; i++){
            n[i] =  (p[i] % (modulo));
        }

        for(int i = 0; i < n.Length; i++){
            if(n[i] >= modulo/2) n[i]-=modulo;
            if(n[i] == -modulo+1) n[i]+=modulo;
        }
        return n;
    }

    static int modInverse(long a1, long m1)
    {
        long a = a1;
        long m = m1;

        for (int x = 1; x < m; x++)
            if (((a%m) * (x%m)) % m == 1)
                return x;
        return 1;
    }
}
