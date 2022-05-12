int FNV(char *str, long m, int p, int h0)
{
    if (m == 0)
        return h0;

    return (FNV(str, m - 1, p, h0) * p) ^ str[m];
}