using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace WinCSCryptor
{
    public partial class Encryptor : Form
    {
        // Объявляем CspParameters, которая содержит параметры, передаваемые  поставщику служб шифрования
        CspParameters cspPar = new CspParameters();
        // Объявляем RSACryptoServiceProvider, который выполняет шифрование и расшифровку
        RSACryptoServiceProvider rsa;

        public Encryptor()
        {
            // Стандартный метод для поддержки конструктора
            InitializeComponent();
        }

        // Метод вывода информации о файле в InfoBox
        private void UpdateInfo()
        {
            String Filename = BoxFilename.Text;
            if (!Filename.Equals(""))
            {
                FileInfo oFileInfo = new FileInfo(Filename);

                if (oFileInfo.Exists)
                {
                    String Info = "";
                    Info += "Name: " + oFileInfo.Name + "\n";
                    Info += "Extension: " + oFileInfo.Extension + "\n";

                    long Size = oFileInfo.Length;
                    if(Size < 1024)
                    {
                        Info += "Size: " + Size + " Bytes\n";
                    }
                    else if(Size >= 1024 && Size < 1048576)
                    {
                        Info += "Size: " + (Size / 1024 + 1) + " KB\n";
                    }
                    else if (Size >= 1048576 && Size < 1073741824)
                    {
                        Info += "Size: " + (Size / 1048576 + 1) + " MB\n";
                    }
                    else if (Size >= 1073741824 && Size < 1099511627776)
                    {
                        Info += "Size: " + (Size / 1073741824 + 1) + " GB\n";
                    }
                    else
                    {
                        Info += "Size: " + (Size / 1099511627776 + 1) + " TB\n";
                    }

                    Info += "Creation Time: " + oFileInfo.CreationTime + "\n";

                    BoxInfo.Text = Info;
                }

                else
                {
                    BoxInfo.Text = "";
                }
            }
            else
            {
                BoxInfo.Text = "";
            }
        }

        // Метод шифрования файла
        private void EncryptFile(string inFile)
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            bool ercheck = false;
            // Создаем объект RejindaelManaged для доступа к управляемой версии Rejindael алгоритма
            RijndaelManaged rejAlg = new RijndaelManaged();
            // Задаем размер ключа и размер блока (Возможные варианты: 256, 192 или 128) в битах
            rejAlg.KeySize = 256;
            rejAlg.BlockSize = 256;
            // Задаем режим блочного шифра - Сцепление блоков шифротекста
            rejAlg.Mode = CipherMode.CBC;

            // Создаем объект шифратор со сгенерированным ключом и вектором инициализации
            // При пустых параметрах они генерируются автоматически
            ICryptoTransform transform = rejAlg.CreateEncryptor();

            // Зашифровываем ключ rejindael с помощью алгоритма RSA с заданными параметрами (в нашем случае только один параметр)
            // Это имя контейнера в RSA - он выступает как пароль
            // rsa = new RSACryptoServiceProvider(cspPar);
            byte[] keyEncrypted = rsa.Encrypt(rejAlg.Key, false);

            // Создаём массивы для длины ключа и длины вектора инициализации
            byte[] KeyLen = new byte[4];
            byte[] IVLen = new byte[4];

            // Переводим длины в байты и записываем в массивы
            int lKey = keyEncrypted.Length;
            KeyLen = BitConverter.GetBytes(lKey);
            int lIV = rejAlg.IV.Length;
            IVLen = BitConverter.GetBytes(lIV);
            
            // Добавляем к файлу раширение ".enc"
            string outFile = inFile + ".enc";

            try
            {
                using (FileStream outFs = new FileStream(outFile, FileMode.Create))
                {
                    // Записываем в поток выходного файла:
                    // - длину ключа (первые 4 байта)
                    // - длину ВИ (вторые 4 байта)
                    // - Сам ключ, зашифрованный через RSA
                    // - Сам ВИ (шифровать не обязательно)
                    // - Зашифрованную часть файла

                    outFs.Write(KeyLen, 0, 4);
                    outFs.Write(IVLen, 0, 4);
                    outFs.Write(keyEncrypted, 0, lKey);
                    outFs.Write(rejAlg.IV, 0, lIV);

                    // Зашифровываем сам контент файла через CryptoStream c помощью алгоритма Rejindael
                    using (CryptoStream outStreamEncrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                    {

                        // Счетчик для № блока. Зашифровывая поблочно мы сохраняем память и
                        // имеем возможность зашифровывать большие файлы
                        int count = 0;

                        // Высчитываем размер блока (он может быть любым)
                        int blockSize = rejAlg.BlockSize / 8;
                        byte[] data = new byte[blockSize];

                        using (FileStream inFs = new FileStream(inFile, FileMode.Open))
                        {
                            do
                            {
                                count = inFs.Read(data, 0, blockSize);
                                outStreamEncrypted.Write(data, 0, count);
                            }
                            while (count > 0);
                            inFs.Close();
                        }
                        outStreamEncrypted.FlushFinalBlock();
                        outStreamEncrypted.Close();
                    }
                    outFs.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                ercheck = true;
            }

            stopwatch.Stop();
            if (!ercheck)
            {
                
                MessageBox.Show("File encrypted!\n"+stopwatch.Elapsed);
            }
        }

        // Метод расшифрования файла
        private void DecryptFile(string inFile)
        {
            bool ercheck = false;
            // Создаем объект RejindaelManaged для доступа к управляемой версии Rejindael алгоритма
            RijndaelManaged rejAlg = new RijndaelManaged();
            rejAlg.KeySize = 256;
            rejAlg.BlockSize = 256;
            // Задаем режим блочного шифра - Сцепление блоков шифротекста
            rejAlg.Mode = CipherMode.CBC;

            // Создадим массивы для ключа и вектора инициализации
            byte[] KeyLen = new byte[4];
            byte[] IVLen = new byte[4];

            // Получим имя файла без расширения .enc
            string outFile = inFile.Substring(0, inFile.LastIndexOf("."));

            // Используем FileStream для загрузки зашифрованного файла и сохранения расшифрованного файла
            try
            {
                //Открываем заданный файл
                using (FileStream inFs = new FileStream(inFile, FileMode.Open))
                {
                    inFs.Seek(0, SeekOrigin.Begin);
                    //Прочитаем первые 4 байта - длина ключа и запишем их в буфер
                    inFs.Read(KeyLen, 0, 3);
                    inFs.Seek(4, SeekOrigin.Begin);
                    //Прочитаем вторые 4 байта - длина вектора инициализации и запишем их в буфер
                    inFs.Read(IVLen, 0, 3);

                    // Переведем длины из байтов в целочисленные значения
                    int KeyLength = BitConverter.ToInt32(KeyLen, 0);
                    int IVLength = BitConverter.ToInt32(IVLen, 0);

                    // Определяем начало зашифрованного текста и его длину
                    int startC = KeyLength + IVLength + 8;
                    int lenC = (int)inFs.Length - startC;

                    // Создаем массивы для зашифрованного ключа и вектора инициализации
                    byte[] KeyEncrypted = new byte[KeyLength];
                    byte[] IV = new byte[IVLength];

                    // Достаем ключ и вектор инициализации (с учетом их длин)
                    // И записываем их в буфер
                    inFs.Seek(8, SeekOrigin.Begin);
                    inFs.Read(KeyEncrypted, 0, KeyLength);
                    inFs.Seek(8 + KeyLength, SeekOrigin.Begin);
                    inFs.Read(IV, 0, IVLength);
                    
                    //Создаем массив для расшифрованного ключа
                    byte[] KeyDecrypted = null;

                    if(KeyEncrypted != null)
                        KeyDecrypted = rsa.Decrypt(KeyEncrypted, false);
                    else
                    {
                        MessageBox.Show("No key present!");
                    }

                    // Создаем объект-дешифратор с указанным ключом и вектором инициализации 
                    ICryptoTransform transform = rejAlg.CreateDecryptor(KeyDecrypted, IV);

                    // Расшифровываем данные из входного потока и заносим в выходной
                    using (FileStream outFs = new FileStream(outFile, FileMode.Create))
                    {

                        int count = 0;

                        // Задаем размер блока в байтах (в нашем случае - это 256 / 8)
                        int blockSize = rejAlg.BlockSize / 8;
                        byte[] data = new byte[blockSize];
                        // Подобное разбитие дешифрации на блоки позволяет сохранять память и
                        // дает возможность шифровать файлы больших размеров

                        // Устанавливаем указатель потока считывания на начало зашифрованного текста
                        inFs.Seek(startC, SeekOrigin.Begin);

                        using (CryptoStream outStreamDecrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                        {
                            do
                            {
                                count = inFs.Read(data, 0, blockSize);
                                outStreamDecrypted.Write(data, 0, count);
                            }
                            while (count > 0);

                            // Очистка буфера и закрытие потока
                            outStreamDecrypted.FlushFinalBlock();
                            outStreamDecrypted.Close();
                        }
                        outFs.Close();
                    }
                    inFs.Close();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("удалено") || ex.Message.Contains("deleted"))
                {
                    MessageBox.Show("Incorrect key!");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
                ercheck = true;
            }
            if(!ercheck)
            {
                MessageBox.Show("File decrypted!");
            }
        }

        // Event Listener для нажатия кнопки открытия диалогового окна для поиска файла
        private void ButtonBrowse_Click(object sender, EventArgs e)
        {
            DialogResult dialog = OpenFileDialog.ShowDialog();

            if(dialog == DialogResult.OK)
            {
                BoxFilename.Text = OpenFileDialog.FileName;
            }
        }

        // Event Listener для обновления информации о файле
        private void BoxFilename_TextChanged(object sender, EventArgs e)
        {
            UpdateInfo();
        }

        // Event Listener для нажатия кнопки шифрования
        private void ButtonEncrypt_Click(object sender, EventArgs e)
        {
            String Filename = BoxFilename.Text;
            FileInfo oFileInfo = new FileInfo(Filename);

            // Проверка на существование файла и на созданного объекта RSA
            if (!oFileInfo.Exists)
            {
                MessageBox.Show("File not found;");
            }
            else if(rsa == null)
            {
                MessageBox.Show("Key not set!");
            }
            else
            {
                EncryptFile(oFileInfo.FullName);
            }
        }

        // Event Listener для нажатия кнопки создания объекта RSA с параметрами
        private void ButtonCreateKey_Click(object sender, EventArgs e)
        {
            // Проверяем TextBox на наличие достоверного пароля
            String password = @BoxPassword.Text;
            if (password.Length < 4)
            {
                MessageBox.Show("Password must be at least 4 symbols long!");
            }
            else
            {
                // Сохраняем ключ в контейнере, создаем новый encryptor с данным ключом
                cspPar.KeyContainerName = password;
                rsa = new RSACryptoServiceProvider(cspPar);
                // Сохраняем ключ в CSP
                rsa.PersistKeyInCsp = true;
                // Выводим информацию о ключе
                if (!rsa.PublicOnly)
                    labelKey.Text = "Key: " + cspPar.KeyContainerName + " - Full Key Pair";
            }
        }

        // Event Listener для нажатия кнопки расшифрования
        private void ButtonDecrypt_Click(object sender, EventArgs e)
        {
            String Filename = BoxFilename.Text;
            FileInfo oFileInfo = new FileInfo(Filename);

            // Проверка на существование файла и на созданного объекта RSA
            if (!oFileInfo.Exists)
            {
                MessageBox.Show("File not found;");
            }
            else if (rsa == null)
            {
                MessageBox.Show("Key not set!");
            }
            // Проверка на наличие приватного ключа в RSA
            else if (rsa.PublicOnly)
            {
                MessageBox.Show("Can't decrypt with public only key!");
            }
            else if(oFileInfo.Extension != ".enc")
            {
                MessageBox.Show("Must choose .enc file for decryption");
            }
            else
            {
                DecryptFile(oFileInfo.FullName);
            }
        }

        // Event Listener для нажатия кнопки сохранения RSA с заданными параметрами
        private void ButtonExport_Click(object sender, EventArgs e)
        {
            if (rsa != null)
            {
                DialogResult dialog = FolderBrowser.ShowDialog();

                if (dialog == DialogResult.OK)
                {
                    String Filename = BoxFilename.Text;
                    FileInfo oFileInfo = null;
                    if (!Filename.Equals(""))
                        oFileInfo = new FileInfo(Filename);
                    bool ercheck = false;
                    try
                    {
                        if (oFileInfo != null && oFileInfo.Exists)
                        {
                            StreamWriter sw = new StreamWriter(FolderBrowser.SelectedPath + "\\" + oFileInfo.Name.Substring(0, oFileInfo.Name.LastIndexOf(".")) + "Key.enk", false);
                            // Проверка на добавление приватных параметров в ключ
                            if (CheckBoxPrivate.Checked && !rsa.PublicOnly)
                                sw.Write(rsa.ToXmlString(true));
                            else
                                sw.Write(rsa.ToXmlString(false));
                            sw.Close();
                        }
                        else
                        {
                            StreamWriter sw = new StreamWriter(FolderBrowser.SelectedPath + "\\Key.enk", false);
                            // Проверка на добавление приватных параметров в ключ
                            if (CheckBoxPrivate.Checked && !rsa.PublicOnly)
                                sw.Write(rsa.ToXmlString(true));
                            else
                                sw.Write(rsa.ToXmlString(false));
                            sw.Close();
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        ercheck = true;
                    }
                    if(!ercheck)
                    {
                        MessageBox.Show("Key exported!");
                    }
                }
            }
            else
            {
                MessageBox.Show("No key!");
            }
        }

        // Event Listener для нажатия кнопки загрузки RSA с заданными параметрами
        private void ButtonImport_Click(object sender, EventArgs e)
        {
            DialogResult dialog = OpenKeyFile.ShowDialog();

            if (dialog == DialogResult.OK)
            {
                String Filename = OpenKeyFile.FileName;
                FileInfo oFileInfo = new FileInfo(Filename);

                try
                {
                    if (oFileInfo.Extension == ".enk")
                    {
                        StreamReader sr = new StreamReader(oFileInfo.FullName);
                        if (rsa == null)
                        {
                            cspPar.KeyContainerName = "Temp";
                            rsa = new RSACryptoServiceProvider(cspPar);
                        }
                        // Чтение ключа из файла
                        string keytxt = sr.ReadToEnd();
                        // Инициализирует объект RSA с ключом в формате Xml
                        rsa.FromXmlString(keytxt);
                        rsa.PersistKeyInCsp = true;
                        if (rsa.PublicOnly == true)
                            labelKey.Text = "Key: Public Key From " + oFileInfo.Name;
                        else
                            labelKey.Text = "Key: Full Key Pair From " + oFileInfo.Name;
                        sr.Close();
                    }
                    else
                    {
                        MessageBox.Show("You must choose a .enk file!");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}