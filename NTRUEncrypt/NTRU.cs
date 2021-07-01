using System;

public class NTRU {

    int N,q,p;

    private fraction[] f;
    private int[] fp;
    private int[] fq;
    private int[] h;
    private fraction[] g;
    private int[] f_i;
    public NTRU(int N, int q, int p){
        this.N = N;
        this.q = q;
        this.p = p;
    }

    public void setPublicKey(fraction[] f, fraction[] g){
        this.f =f;
        f_i = new int[f.Length];
        for(int i = 0; i < f.Length; i++){
            f_i[i] = (int) f[i].num;
        }
        this.g  = g;
        initPublicKey();
    }


    public int[] getPublicKey(){
        return h;
    }
    private void initPublicKey() {
        fraction[] factorBase = new fraction[N+1];
        factorBase[0] = -1;
        factorBase[N] = 1;
        for(int i = 1; i < N ; i++){
            factorBase[i] = new fraction(0);
        }

        fraction[] inverseOfF = EEAlgorithm.EGCD(factorBase, f)[2];

        fp = PolynomOperator.toModulo(inverseOfF, p);
        fq = PolynomOperator.toModulo(inverseOfF, q);

        fraction[] fq_fract = new fraction[fq.Length];

        for(int i = 0; i < fq.Length; i++){
            fq_fract[i] = new fraction(fq[i]);
        }

        fraction[] mul = PolynomOperator.Multiply(fq_fract, g);
        fraction[] mulP = PolynomOperator.Multiply(mul, new fraction[]{p});
        mulP = PolynomOperator.getMultiplyNTRU(mulP, N);
        this.h = PolynomOperator.toModulo(mulP, q);
    }

    public int[] encrypt(int[] m, int[] r){
        int[] mul = PolynomOperator.Multiply(h,r);
        int[] mulT = PolynomOperator.getMultiplyNTRU(mul, N);
        mulT = PolynomOperator.toModulo(mulT, q);
        int[] res = PolynomOperator.Add(mulT, m);
        return res;
    }

    public int[] decrypt(int[] e){
        int[] a = PolynomOperator.Multiply(f_i, e);

        a = PolynomOperator.getMultiplyNTRU(a, N);
        a = PolynomOperator.toModulo(a, q, true);

        int[] b = PolynomOperator.toModulo(a, p, true);

        int[] c = PolynomOperator.Multiply(b, fp);
        c = PolynomOperator.getMultiplyNTRU(c, N);
        c = PolynomOperator.toModulo(c, p, true);
        return c;
    }

}
