namespace UrunSitesi.WebAPIUsing.Tools
{
    public class FileHelper
    {
        public static string FileLoader(IFormFile formFile, string klasorYolu = "/wwwroot/Images/")
        {
            string dosyaAdi = "";

            if (formFile != null)
            {
                dosyaAdi = formFile.FileName;
                string dizin = Directory.GetCurrentDirectory() + klasorYolu + dosyaAdi;
                using var stream = new FileStream(dizin, FileMode.Create);
                formFile.CopyTo(stream);
            }

            return dosyaAdi;
        }
        public static bool FileRemover(string fileName, string klasorYolu = "/wwwroot/Images/")
        {
            string dizin = Directory.GetCurrentDirectory() + klasorYolu + fileName;

            if (File.Exists(dizin))
            {
                File.Delete(dizin); // dizindeki dosyayı sil
                return true; // silme gerçekleşirse true dön
            }
            return false; // eğer silme gerçekleşmezse false dön
        }
    }
}
