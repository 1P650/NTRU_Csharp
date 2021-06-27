public class NTRU {
    public static int[] toModulo(fraction[] p, int modulo){
        int [] n = new int[p.Length];
        for(int i = 0; i < p.Length; i++){
            n[i] = (p[i].num % modulo) * modInverse(p[i].den, modulo);
            if(n[i]<0){
                while (n[i]<0) n[i]+=modulo;
            }
            n[i]%=modulo;
        }
        return n;
    }

    static int modInverse(int a, int m)
    {

        for (int x = 1; x < m; x++)
            if (((a%m) * (x%m)) % m == 1)
                return x;
        return 1;
    }

}
