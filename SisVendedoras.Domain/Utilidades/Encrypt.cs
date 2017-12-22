using CryptSharp;

namespace SisVendedoras.Dominio.Utilidades
{
    public class Encrypt
    {
        public string Codifica(string senha)
        {
            string senhaCodificada = Crypter.MD5.Crypt(senha);

            return senhaCodificada;
        }

        public bool Compara(string senha, string hash)
        {
            bool result = false;

            result = Crypter.CheckPassword(senha, hash);

            return result;
        }

        public string GerarNovaSenha()
        {
            string novaSenha = string.Empty;

            novaSenha = Crypter.MD5.GenerateSalt(CrypterOptions.None);

            return novaSenha;
        }
    }
}
