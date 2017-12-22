using System.Drawing;
using System.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Data;
using System.Text;

namespace SisVendedoras.Dominio.Utilidades
{
    public class Utilidades
    {
        private const string REGEXEMAIL = @"/^[_a-z0-9]+(\.[_a-z0-9]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$/";

        public static string RegexEmail
        {
            get
            {
                return REGEXEMAIL;
            }
        }

        #region Propriedades

        public static string AccessKeyAmazon
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["AccessKeyAmazon"]) ? ConfigurationManager.AppSettings["AccessKeyAmazon"] : string.Empty; }
        }

        public static string SecretAccessKeyAmazon
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["SecretAccessKeyAmazon"]) ? ConfigurationManager.AppSettings["SecretAccessKeyAmazon"] : string.Empty; }
        }

        public static string BucketFotoPerfilS3
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["BucketFotoPerfilS3"]) ? ConfigurationManager.AppSettings["BucketFotoPerfilS3"] : string.Empty; }
        }

        public static string ServiceUrlS3
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["ServiceUrlS3"]) ? ConfigurationManager.AppSettings["ServiceUrlS3"] : string.Empty; }
        }

        public static string UrlS3FotoPerfilOriginal
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["UrlS3FotoPerfilOriginal"]) ? ConfigurationManager.AppSettings["UrlS3FotoPerfilOriginal"] : string.Empty; }
        }

        public static string UrlS3FotoPerfil
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["UrlS3FotoPerfil"]) ? ConfigurationManager.AppSettings["UrlS3FotoPerfil"] : string.Empty; }
        }

        public static string UrlS3GaleriaOriginal
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["UrlS3GaleriaOriginal"]) ? ConfigurationManager.AppSettings["UrlS3GaleriaOriginal"] : string.Empty; }
        }

        public static string UrlS3Galeria
        {
            get { return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["UrlS3Galeria"]) ? ConfigurationManager.AppSettings["UrlS3Galeria"] : string.Empty; }
        }

        public static int LimitUploadFotos
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["LimitUploadFotos"]);
            }
        }

        public static string PathFotoOriginal
        {
            get
            {
                return ConfigurationManager.AppSettings["PathFotoOriginal"];
            }
        }

        #endregion

        #region Metodos

        // <summary>
        /// Remove caracteres não numéricos
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveNaoNumericos(string text)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
            string ret = reg.Replace(text, string.Empty);
            return ret;
        }
        /// <summary>
        /// Valida se um cpf é válido
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static bool ValidaCPF(string cpf)
        {
            //Remove formatação do número, ex: "123.456.789-01" vira: "12345678901"
            cpf = RemoveNaoNumericos(cpf);
            if (cpf.Length > 11)
                return false;
            while (cpf.Length != 11)
                cpf = '0' + cpf;
            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;
            if (igual || cpf == "12345678909")
                return false;
            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpf[i].ToString());
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];
            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];
            resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
            if (numeros[10] != 11 - resultado)
                return false;
            return true;
        }

        public static bool ValidaCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else

                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public static bool ValidarCPFCNPJ(long idTipoEmpresa, string cpfCnpj)
        {
            bool result = false;

            if (idTipoEmpresa.Equals(1))
            {
                result = ValidaCPF(cpfCnpj);
            }
            else
            {
                result = ValidaCnpj(cpfCnpj);
            }

            return result;
        }

        public static string RedimensionarImagem(byte[] originalFile, int newWidth, int maxHeight, bool onlyResizeIfWider, string path = "", string nome = "")
        {
            MemoryStream memstr = new MemoryStream(originalFile);
            Image fullsizeImage = Image.FromStream(memstr);

            string imageTobase64 = string.Empty;

            // Prevent using images internal thumbnail
            fullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            fullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);

            if (onlyResizeIfWider)
            {
                if (fullsizeImage.Width <= newWidth)
                {
                    newWidth = fullsizeImage.Width;
                }
            }

            int newHeight = fullsizeImage.Height * newWidth / fullsizeImage.Width;

            if(newWidth == 0)
            {
                if(maxHeight == 0)
                    maxHeight = fullsizeImage.Height;

                var ratio = maxHeight * 100 / fullsizeImage.Height;

                newWidth = fullsizeImage.Width * ratio / 100;
                newHeight = maxHeight;
            }

            if (newHeight > maxHeight)
            {
                // Resize with height instead
                newWidth = fullsizeImage.Width * maxHeight / fullsizeImage.Height;
                newHeight = maxHeight;
            }

            Image newImage = fullsizeImage.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);

            using (MemoryStream ms = new MemoryStream())
            {
                if(!string.IsNullOrWhiteSpace(path))
                    newImage.Save(path + "\\" + nome +".Jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                else
                {
                    /* Convert this image back to a base64 string */
                    newImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    imageTobase64 = Convert.ToBase64String(ms.ToArray());
                }
            }

            // Clear handle to original file so that we can overwrite it if necessary
            fullsizeImage.Dispose();

            return imageTobase64;
        }

        #endregion

        #region Localization Geolocalization

        /// <summary>
        /// This function queries the google api and returns only lat and lng
        /// </summary>
        /// <param name="street"></param>
        /// <param name="number"></param>
        /// <param name="neighborhood"></param>
        /// <param name="cityName"></param>
        /// <param name="StateName"></param>
        /// <returns>OBS: Return this longitude, latitude</returns>
        public static string GetLocalization(string street, string number, string neighborhood, string cityName, string StateName)
        {
            string localization = string.Empty;

            try
            {
                string address = string.Format("{0} - {1}, {2}, {3}, {4}", street, number, neighborhood, cityName, StateName);
                string url = "http://maps.google.com/maps/api/geocode/xml?address=" + address + "&sensor=false";

                WebRequest request = WebRequest.Create(url);

                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        DataSet dsResult = new DataSet();
                        dsResult.ReadXml(reader);

                        if (dsResult != null && dsResult.Tables["result"] != null && dsResult.Tables["result"].Rows != null)
                        {
                            foreach (DataRow row in dsResult.Tables["result"].Rows)
                            {
                                if (dsResult.Tables["geometry"] != null)
                                {
                                    string geometry_id = dsResult.Tables["geometry"].Select("result_id = " + row["result_id"].ToString())[0]["geometry_id"].ToString();

                                    DataRow location = dsResult.Tables["location"].Select("geometry_id = " + geometry_id)[0];

                                    if (location != null)
                                    {
                                        localization = string.Format("{0}, {1}", location["lng"], location["lat"]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return localization;
        }

        public static string GetLatitude(string localization)
        {
            string latitude = string.Empty;
            if (!string.IsNullOrEmpty(localization))
            {
                string[] peace = localization.Split(',');
                latitude = peace[1];
            }
            return latitude;
        }
        public static string GetLongitude(string localization)
        {
            string longitude = string.Empty;
            if (!string.IsNullOrEmpty(localization))
            {
                string[] peace = localization.Split(',');
                longitude = peace[0];
            }
            return longitude;
        }

        /// <summary>
        /// Method to treat the location formats 2
        /// POINT(-47.024263 -22.864052599999997) or -48.5610137, -22.3045660
        /// </summary>
        /// <param name="localization"></param>
        /// <returns></returns>
        public static string GetLatitudeFromReturnSQLSERVER(string localization)
        {
            string latitude = string.Empty;
            if (!string.IsNullOrEmpty(localization))
            {
                string[] peace = localization.Split(' ');
                if (peace.Count().Equals(3))
                {
                    latitude = peace[2].Replace(")", "");
                }
                else
                {
                    latitude = GetLatitude(localization);
                }
            }
            return latitude;
        }

        /// <summary>
        /// Method to treat the location formats 2
        /// POINT(-47.024263 -22.864052599999997) or -48.5610137, -22.3045660
        /// </summary>
        /// <param name="localization"></param>
        /// <returns></returns>
        public static string GetLongitudeFromReturnSQLSERVER(string localization)
        {
            string longitude = string.Empty;
            if (!string.IsNullOrEmpty(localization))
            {
                string[] peace = localization.Split(' ');
                if (peace.Count().Equals(3))
                {
                    longitude = peace[1].Replace("(", "");
                }
                else
                {
                    longitude = GetLongitude(localization);
                }
            }
            return longitude;
        }

        #endregion

        #region Enum

        public enum EnumTipoEmpresa
        {
            F = 1,
            J = 2
        }

        #endregion
    }
}
